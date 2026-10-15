using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace EmployeeSalaryManagementSystem
{
    public partial class DataEntryForm : Form
    {
        private string tableName;
        private string[] columnNames;
        private string[] columnCaptions;
        private DataRowView? dataRow;
        private string connectionString = "Server=localhost;Database=employ_salary;Uid=root;Pwd=YOUR_PASSWORD;";

        private Button btnOK;
        private Button btnCancel;
        private TextBox[]? textBoxes;

        public DataEntryForm(string tableName, string[] columnNames, string[] columnCaptions, DataRowView? dataRow)
        {
            this.tableName = tableName;
            this.columnNames = columnNames;
            this.columnCaptions = columnCaptions;
            this.dataRow = dataRow;

            InitializeComponent();
            this.Load += DataEntryForm_Load;
        }

        private void InitializeComponent()
        {
            btnOK = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // btnOK
            // 
            btnOK.Location = new Point(195, 464);
            btnOK.Margin = new Padding(6);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(138, 42);
            btnOK.TabIndex = 0;
            btnOK.Text = "确定";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += BtnOK_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(379, 464);
            btnCancel.Margin = new Padding(6);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(138, 42);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "取消";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += BtnCancel_Click;
            // 
            // DataEntryForm
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(733, 554);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Margin = new Padding(6);
            Name = "DataEntryForm";
            Text = "DataEntryForm";
            ResumeLayout(false);
        }

        private void DataEntryForm_Load(object sender, EventArgs e)
        {
            if (columnNames == null || columnCaptions == null || columnNames.Length != columnCaptions.Length)
            {
                MessageBox.Show("列名或列标题配置有误，请检查！");
                return;
            }

            Controls.Clear();

            textBoxes = new TextBox[columnNames.Length];
            int yPosition = 20;
            int labelWidth = 100;

            foreach (string caption in columnCaptions)
            {
                Size textSize = TextRenderer.MeasureText(caption + ":", Font);
                labelWidth = Math.Max(labelWidth, textSize.Width);
            }

            for (int i = 0; i < columnNames.Length; i++)
            {
                Label label = new Label
                {
                    Text = columnCaptions[i] + ":",
                    Location = new Point(30, yPosition),
                    Size = new Size(labelWidth, 20),
                    AutoSize = false
                };
                Controls.Add(label);

                TextBox tb = new TextBox
                {
                    Location = new Point(30 + labelWidth, yPosition),
                    Size = new Size(180, 20)
                };
                if (dataRow != null && !dataRow.Row.IsNull(columnNames[i]))
                {
                    tb.Text = dataRow[columnNames[i]].ToString();
                }
                Controls.Add(tb);
                textBoxes[i] = tb;

                yPosition += 30;
            }

            int buttonY = yPosition + 20;
            btnOK.Location = new Point(100, buttonY);
            btnCancel.Location = new Point(200, buttonY);

            Controls.Add(btnOK);
            Controls.Add(btnCancel);

            int formWidth = 30 + labelWidth + 180 + 30;
            ClientSize = new Size(formWidth, buttonY + 40);

            Text = dataRow == null ? "新增数据" : "修改数据";
        }

        private void BtnOK_Click(object? sender, EventArgs? e)
        {
            if (textBoxes == null)
            {
                MessageBox.Show("表单控件未正确初始化！");
                return;
            }

            try
            {
                if (dataRow == null)
                {
                    string insertQuery = $"INSERT INTO `{tableName}` (";
                    string valuesQuery = "VALUES (";
                    for (int i = 0; i < columnNames.Length; i++)
                    {
                        insertQuery += $"`{columnNames[i]}`";
                        valuesQuery += $"@{columnNames[i]}";
                        if (i < columnNames.Length - 1)
                        {
                            insertQuery += ", ";
                            valuesQuery += ", ";
                        }
                    }
                    insertQuery += ") " + valuesQuery + ")";

                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        MySqlCommand command = new MySqlCommand(insertQuery, connection);
                        for (int i = 0; i < columnNames.Length; i++)
                        {
                            command.Parameters.AddWithValue($"@{columnNames[i]}", textBoxes[i].Text);
                        }
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("新增成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("新增失败！");
                        }
                    }
                }
                else
                {
                    DataRow row = dataRow.Row;
                    row.BeginEdit();
                    bool hasChanges = false;
                    for (int i = 0; i < columnNames.Length; i++)
                    {
                        string newValue = textBoxes[i].Text;
                        string currentValue = row[columnNames[i]]?.ToString() ?? "";
                        if (newValue != currentValue)
                        {
                            row[columnNames[i]] = newValue;
                            hasChanges = true;
                        }
                    }
                    row.EndEdit();
                    if (!hasChanges && row.RowState != DataRowState.Modified)
                    {
                        MessageBox.Show("未检测到数据修改！");
                        return;
                    }
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"操作失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object? sender, EventArgs? e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
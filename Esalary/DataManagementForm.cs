using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace EmployeeSalaryManagementSystem
{
    public partial class DataManagementForm : Form
    {
        private string tableName;
        private string formTitle;
        private string[] columnNames;
        private string[] columnCaptions;
        private string filter;

        private Panel panelTable = new Panel();
        public DataGridView dataGridView1 = new DataGridView();
        private Panel panelButtons = new Panel();
        private Button btnAdd = new Button();
        private Button btnDelete = new Button();
        private Button btnModify = new Button();
        private Button btnExit = new Button();

        private DataTable dataTable = new DataTable();
        private MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
        private MySqlCommandBuilder commandBuilder;


        public DataManagementForm(
            string tableName,
            string formTitle,
            string[] columnNames,
            string[] columnCaptions,
            string filter = ""
        )
        {
            this.tableName = tableName;
            this.formTitle = formTitle;
            this.columnNames = columnNames;
            this.columnCaptions = columnCaptions;
            this.filter = filter;

            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            // 表格容器
            panelTable.Dock = DockStyle.Fill;
            panelTable.Height = 300;

            // DataGridView
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = false;
            panelTable.Controls.Add(dataGridView1);

            // 按钮容器
            panelButtons.Dock = DockStyle.Bottom;
            panelButtons.Height = 60;
            panelButtons.BackColor = Color.LightGray;

            // 功能按钮
            btnAdd.Text = "新增";
            btnAdd.Size = new Size(75, 30);
            btnAdd.Location = new Point(20, 15);
            btnAdd.Click += BtnAdd_Click;

            btnDelete.Text = "删除";
            btnDelete.Size = new Size(75, 30);
            btnDelete.Location = new Point(120, 15);
            btnDelete.Click += BtnDelete_Click;

            btnModify.Text = "修改";
            btnModify.Size = new Size(75, 30);
            btnModify.Location = new Point(220, 15);
            btnModify.Click += BtnModify_Click;

            btnExit.Text = "退出";
            btnExit.Size = new Size(75, 30);
            btnExit.Location = new Point(420, 15);
            btnExit.Click += BtnExit_Click;

            panelButtons.Controls.Add(btnAdd);
            panelButtons.Controls.Add(btnDelete);
            panelButtons.Controls.Add(btnModify);
            panelButtons.Controls.Add(btnExit);

            ClientSize = new Size(800, 380);
            Controls.Add(panelTable);
            Controls.Add(panelButtons);
            Text = formTitle;

            if (tableName.Equals("msalary", StringComparison.OrdinalIgnoreCase) ||  tableName.Equals("bonus", StringComparison.OrdinalIgnoreCase))
            {
                SetReadOnlyMode(); // 月薪表和年终奖表：只读模式
            }
            else
            {
                SetEditableMode(); // 其他表：可编辑模式
            }
        }

        // 加载数据
        private void LoadData()
        {
            try
            {
                string connectionString = "Server=localhost;Database=employ_salary;Uid=root;Pwd=YOUR_PASSWORD;";
                string query = $"SELECT * FROM `{tableName}`";
                if (!string.IsNullOrEmpty(filter))
                {
                    query += $" WHERE {filter}";
                }

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    dataAdapter.SelectCommand = cmd;
                    commandBuilder = new MySqlCommandBuilder(dataAdapter);

                    dataTable.Clear();
                    dataAdapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;
                    for (int i = 0; i < columnCaptions.Length; i++)
                    {
                        if (dataGridView1.Columns.Count > i)
                        {
                            dataGridView1.Columns[i].HeaderText = columnCaptions[i];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"数据加载失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 设置只读模式
        private void SetReadOnlyMode()
        {
            dataGridView1.ReadOnly = true;
            // 隐藏增删改按钮
            btnAdd.Visible = false;
            btnDelete.Visible = false;
            btnModify.Visible = false;
        }

        // 设置可编辑模式
        private void SetEditableMode()
        {
            dataGridView1.ReadOnly = true;
            // 显示增删改按钮
            btnAdd.Visible = true;
            btnDelete.Visible = true;
            btnModify.Visible = true;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var entryForm = new DataEntryForm(tableName, columnNames, columnCaptions, null);
            if (entryForm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择要删除的行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            DataRow dataRow = dataTable.Rows[selectedRow.Index];

            try
            {
                dataRow.Delete();
                int affectedRows = dataAdapter.Update(dataTable);

                if (affectedRows > 0)
                {
                    MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                else
                {
                    MessageBox.Show("删除失败，请重试！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dataTable.RejectChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"删除失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataTable.RejectChanges();
            }
        }

        private void BtnModify_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择要修改的行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            DataRowView rowView = (DataRowView)selectedRow.DataBoundItem;

            var entryForm = new DataEntryForm(tableName, columnNames, columnCaptions, rowView);
            if (entryForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    int affectedRows = dataAdapter.Update(dataTable);

                    if (affectedRows > 0)
                    {
                        MessageBox.Show("修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("修改失败，请重试！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dataTable.RejectChanges();
                    }
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"修改失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dataTable.RejectChanges();
                }
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
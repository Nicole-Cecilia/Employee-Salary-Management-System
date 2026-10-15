using System;
using System.Windows.Forms;

namespace EmployeeSalaryManagementSystem
{
    public partial class InputForm : Form
    {
        public string InputValue { get; private set; } = string.Empty;
        private string promptText;
        private string inputLabel;

        private Label? label1;
        private TextBox? txtInput;
        private Button? btnOK;
        private Button? btnCancel;

        public InputForm(string promptText, string inputLabel)
        {
            this.promptText = promptText;
            this.inputLabel = inputLabel;
            InitializeComponent();
            EnsureControlsInitialized();
        }

        private void EnsureControlsInitialized()
        {
            label1 ??= new Label();
            txtInput ??= new TextBox();
            btnOK ??= new Button();
            btnCancel ??= new Button();
        }

        private void InitializeComponent()
        {
            // 初始化 Label
            label1 = new Label
            {
                AutoSize = true,
                Location = new System.Drawing.Point(30, 30),
                Name = "label1",
                Size = new System.Drawing.Size(119, 13),
                Text = promptText
            };

            // 初始化 TextBox
            txtInput = new TextBox
            {
                Location = new System.Drawing.Point(33, 60),
                Name = "txtInput",
                Size = new System.Drawing.Size(250, 20),
                TabIndex = 1
            };

            // 初始化 确定按钮
            btnOK = new Button
            {
                Location = new System.Drawing.Point(70, 100),
                Name = "btnOK",
                Size = new System.Drawing.Size(75, 23),
                Text = "确定",
                UseVisualStyleBackColor = true
            };
            btnOK.Click += BtnOK_Click;

            // 初始化 取消按钮
            btnCancel = new Button
            {
                Location = new System.Drawing.Point(170, 100),
                Name = "btnCancel",
                Size = new System.Drawing.Size(75, 23),
                Text = "取消",
                UseVisualStyleBackColor = true
            };
            btnCancel.Click += BtnCancel_Click;

            // 窗体布局
            ClientSize = new System.Drawing.Size(316, 145);
            Controls.AddRange(new Control[] { label1, txtInput, btnOK, btnCancel });
            Name = "InputForm";
            Text = inputLabel;
            ResumeLayout(false);
            PerformLayout();
        }

        // 确定按钮事件
        private void BtnOK_Click(object? sender, EventArgs? e)
        {
            if (txtInput != null)
            {
                InputValue = txtInput.Text;
                DialogResult = DialogResult.OK;
            }
            Close();
        }

        // 取消按钮事件
        private void BtnCancel_Click(object? sender, EventArgs? e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
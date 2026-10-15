using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace EmployeeSalaryManagementSystem
{
    public partial class ChangePasswordForm : Form
    {
        private string _username;
        private bool _isAdmin;
        private string _connectionString = "Server=localhost;Database=employ_salary;Uid=root;Pwd=YOUR_PASSWORD;";

        private Label? _label1;
        private Label? _label2;
        private Label? _label3;
        private Label? _labelHint;
        private TextBox? _txtOldPassword;
        private TextBox? _txtNewPassword;
        private TextBox? _txtConfirmPassword;
        private Button? _btnOK;
        private Button? _btnCancel;

        public ChangePasswordForm(string username, bool isAdmin)
        {
            _username = username;
            _isAdmin = isAdmin;
            InitializeComponent();
            EnsureControlsInitialized();
        }

        private void EnsureControlsInitialized()
        {
            _label1 ??= new Label();
            _label2 ??= new Label();
            _label3 ??= new Label();
            _labelHint ??= new Label();
            _txtOldPassword ??= new TextBox();
            _txtNewPassword ??= new TextBox();
            _txtConfirmPassword ??= new TextBox();
            _btnOK ??= new Button();
            _btnCancel ??= new Button();
        }

        private void InitializeComponent()
        {
            // Label1 初始化
            _label1 = new Label
            {
                AutoSize = true,
                Location = new System.Drawing.Point(30, 30),
                Name = "label1",
                Size = new System.Drawing.Size(65, 13),
                TabIndex = 0,
                Text = "当前密码:"
            };

            // Label2 初始化
            _label2 = new Label
            {
                AutoSize = true,
                Location = new System.Drawing.Point(30, 60),
                Name = "label2",
                Size = new System.Drawing.Size(67, 13),
                TabIndex = 1,
                Text = "新密码:"
            };

            // Label3 初始化
            _label3 = new Label
            {
                AutoSize = true,
                Location = new System.Drawing.Point(30, 90),
                Name = "label3",
                Size = new System.Drawing.Size(73, 13),
                TabIndex = 2,
                Text = "确认新密码:"
            };

            // 添加提示标签
            _labelHint = new Label
            {
                AutoSize = true,
                Location = new System.Drawing.Point(30, 115),
                Name = "labelHint",
                Size = new System.Drawing.Size(113, 13),
                TabIndex = 8,
                Text = "初始密码为123456",
                ForeColor = System.Drawing.Color.Red
            };

            // 旧密码文本框初始化（使用 * 隐藏输入）
            _txtOldPassword = new TextBox
            {
                Location = new System.Drawing.Point(120, 30),
                Name = "txtOldPassword",
                PasswordChar = '*',
                Size = new System.Drawing.Size(150, 20),
                TabIndex = 3
            };

            // 新密码文本框初始化（使用 * 隐藏输入）
            _txtNewPassword = new TextBox
            {
                Location = new System.Drawing.Point(120, 60),
                Name = "txtNewPassword",
                PasswordChar = '*',
                Size = new System.Drawing.Size(150, 20),
                TabIndex = 4
            };

            // 确认密码文本框初始化（使用 * 隐藏输入）
            _txtConfirmPassword = new TextBox
            {
                Location = new System.Drawing.Point(120, 90),
                Name = "txtConfirmPassword",
                PasswordChar = '*',
                Size = new System.Drawing.Size(150, 20),
                TabIndex = 5
            };

            // 确认按钮初始化
            _btnOK = new Button
            {
                Location = new System.Drawing.Point(70, 150),
                Name = "btnOK",
                Size = new System.Drawing.Size(75, 23),
                TabIndex = 6,
                Text = "确定",
                UseVisualStyleBackColor = true
            };
            _btnOK.Click += BtnOK_Click;

            // 取消按钮初始化
            _btnCancel = new Button
            {
                Location = new System.Drawing.Point(170, 150),
                Name = "btnCancel",
                Size = new System.Drawing.Size(75, 23),
                TabIndex = 7,
                Text = "取消",
                UseVisualStyleBackColor = true
            };
            _btnCancel.Click += BtnCancel_Click;

            // 窗体初始化
            ClientSize = new System.Drawing.Size(300, 200);
            Controls.AddRange(new Control[]
            {
                _label1, _label2, _label3, _labelHint,
                _txtOldPassword, _txtNewPassword, _txtConfirmPassword,
                _btnOK, _btnCancel
            });
            Name = "ChangePasswordForm";
            Text = "修改密码";
            ResumeLayout(false);
            PerformLayout();
        }

        private void BtnOK_Click(object? sender, EventArgs? e)
        {
            // 空值检查
            if (_txtOldPassword == null || _txtNewPassword == null || _txtConfirmPassword == null)
            {
                MessageBox.Show("表单控件初始化异常，请重试！");
                return;
            }

            string oldPassword = _txtOldPassword.Text.Trim();
            string newPassword = _txtNewPassword.Text.Trim();
            string confirmPassword = _txtConfirmPassword.Text.Trim();

            // 基本校验
            if (string.IsNullOrEmpty(oldPassword) ||
                string.IsNullOrEmpty(newPassword) ||
                string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("密码不能为空，请完整填写！");
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("两次输入的新密码不一致，请重新输入！");
                _txtNewPassword.Clear();
                _txtConfirmPassword.Clear();
                _txtNewPassword.Focus();
                return;
            }

            try
            {
                // 验证旧密码
                bool isOldPasswordValid = false;
                string checkQuery = _isAdmin
                    ? "SELECT COUNT(*) FROM admin_login WHERE admin_id = @Username AND admin_pass = @Password"
                    : "SELECT COUNT(*) FROM emp_login WHERE Eno = @Username AND Epass = @Password";

                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    MySqlCommand command = new MySqlCommand(checkQuery, connection);
                    command.Parameters.AddWithValue("@Username", _username);
                    command.Parameters.AddWithValue("@Password", oldPassword);

                    connection.Open();
                    long count = (long)command.ExecuteScalar();
                    isOldPasswordValid = count > 0;
                }

                if (!isOldPasswordValid)
                {
                    MessageBox.Show("当前密码验证失败，请检查后重新输入！");
                    _txtOldPassword.Clear();
                    _txtOldPassword.Focus();
                    return;
                }

                // 更新新密码
                string updateQuery = _isAdmin
                    ? "UPDATE admin_login SET admin_pass = @NewPassword WHERE admin_id = @Username"
                    : "UPDATE emp_login SET Epass = @NewPassword WHERE Eno = @Username";

                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    MySqlCommand command = new MySqlCommand(updateQuery, connection);
                    command.Parameters.AddWithValue("@NewPassword", newPassword);
                    command.Parameters.AddWithValue("@Username", _username);

                    connection.Open();
                    int affectedRows = command.ExecuteNonQuery();

                    if (affectedRows > 0)
                    {
                        MessageBox.Show("密码修改成功！");
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("密码修改失败，请稍后重试！");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"修改密码时发生错误：{ex.Message}");
            }
        }

        private void BtnCancel_Click(object? sender, EventArgs? e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
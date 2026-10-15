using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace EmployeeSalaryManagementSystem
{
    public partial class LoginForm : Form
    {
        // MySQL 连接字符串
        private string connectionString = "Server=localhost;Database=employ_salary;Uid=root;Pwd=YOUR_PASSWORD;";

        private Label? label1;
        private Label? label2;
        private TextBox? txtUsername;
        private TextBox? txtPassword;
        private Button? btnLogin;
        private RadioButton? radioAdmin;
        private RadioButton? radioEmployee;

        public LoginForm()
        {
            InitializeComponent();
            EnsureControlsInitialized();
        }

        private void EnsureControlsInitialized()
        {
            label1 ??= new Label();
            label2 ??= new Label();
            txtUsername ??= new TextBox();
            txtPassword ??= new TextBox();
            btnLogin ??= new Button();
            radioAdmin ??= new RadioButton();
            radioEmployee ??= new RadioButton();
        }

        private void InitializeComponent()
        {
            // 初始化 Label1（用户名标签）
            label1 = new Label
            {
                AutoSize = true,
                Location = new System.Drawing.Point(30, 30),
                Name = "label1",
                Size = new System.Drawing.Size(58, 13),
                Text = "用户名:"
            };

            // 初始化 Label2（密码标签）
            label2 = new Label
            {
                AutoSize = true,
                Location = new System.Drawing.Point(30, 60),
                Name = "label2",
                Size = new System.Drawing.Size(56, 13),
                Text = "密码:"
            };

            // 初始化 用户名输入框
            txtUsername = new TextBox
            {
                Location = new System.Drawing.Point(100, 30),
                Name = "txtUsername",
                Size = new System.Drawing.Size(150, 20),
                TabIndex = 2
            };

            // 初始化 密码输入框
            txtPassword = new TextBox
            {
                Location = new System.Drawing.Point(100, 60),
                Name = "txtPassword",
                PasswordChar = '*',
                Size = new System.Drawing.Size(150, 20),
                TabIndex = 3
            };

            // 初始化 登录按钮
            btnLogin = new Button
            {
                Location = new System.Drawing.Point(100, 120),
                Name = "btnLogin",
                Size = new System.Drawing.Size(75, 23),
                Text = "登录",
                UseVisualStyleBackColor = true
            };
            btnLogin.Click += BtnLogin_Click;

            // 初始化 管理员单选框
            radioAdmin = new RadioButton
            {
                AutoSize = true,
                Location = new System.Drawing.Point(60, 90),
                Name = "radioAdmin",
                Size = new System.Drawing.Size(65, 17),
                TabStop = true,
                Text = "管理员",
                UseVisualStyleBackColor = true
            };

            // 初始化 员工单选框
            radioEmployee = new RadioButton
            {
                AutoSize = true,
                Location = new System.Drawing.Point(150, 90),
                Name = "radioEmployee",
                Size = new System.Drawing.Size(67, 17),
                TabStop = true,
                Text = "员工",
                UseVisualStyleBackColor = true
            };

            // 窗体布局
            ClientSize = new System.Drawing.Size(284, 262);
            Controls.AddRange(new Control[] { label1, label2, txtUsername, txtPassword, btnLogin, radioAdmin, radioEmployee });
            Name = "LoginForm";
            Text = "员工工资管理系统 - 登录";
            ResumeLayout(false);
            PerformLayout();
        }

        // 登录按钮事件
        private void BtnLogin_Click(object? sender, EventArgs? e)
        {
            // 防御性检查：确保输入框和单选框非空
            if (txtUsername == null || txtPassword == null || radioAdmin == null || radioEmployee == null)
            {
                MessageBox.Show("控件未初始化，请重试！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (radioAdmin.Checked)
            {
                // 管理员登录验证
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = "SELECT COUNT(*) FROM admin_login WHERE admin_id = @Username AND admin_pass = @Password";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    try
                    {
                        connection.Open();
                        long count = (long)command.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show("管理员登录成功！");
                            AdminForm adminForm = new AdminForm(username);
                            Hide();
                            adminForm.ShowDialog();
                            Show();
                        }
                        else
                        {
                            MessageBox.Show("用户名或密码错误！");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("登录时发生错误：" + ex.Message);
                    }
                }
            }
            else if (radioEmployee.Checked)
            {
                // 员工登录验证
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = "SELECT COUNT(*) FROM emp_login WHERE Eno = @Username AND Epass = @Password";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    try
                    {
                        connection.Open();
                        long count = (long)command.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show("员工登录成功！");
                            EmployeeForm employeeForm = new EmployeeForm(username);
                            Hide();
                            employeeForm.ShowDialog();
                            Show();
                        }
                        else
                        {
                            MessageBox.Show("用户名或密码错误！");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("登录时发生错误：" + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择用户类型！");
            }
        }
    }
}
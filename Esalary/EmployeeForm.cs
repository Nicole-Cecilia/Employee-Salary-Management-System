using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace EmployeeSalaryManagementSystem
{
    public partial class EmployeeForm : Form
    {
        private string _username;
        private string _connectionString = "Server=localhost;Database=employ_salary;Uid=root;Pwd=YOUR_PASSWORD;";

        private Button? _btnClockIn;
        private Button? _btnClockOut;
        private Button? _btnAnnualSalary;
        private Button? _btnAnnualBonus;
        private Button? _btnChangePassword;
        private Button? _btnLogout;
        private Label? _labelWelcome;

        public EmployeeForm(string username)
        {
            _username = username;
            InitializeComponent();
            EnsureControlsInitialized();
        }

        private void EnsureControlsInitialized()
        {
            _btnClockIn ??= new Button();
            _btnClockOut ??= new Button();
            _btnAnnualSalary ??= new Button();
            _btnAnnualBonus ??= new Button();
            _btnChangePassword ??= new Button();
            _btnLogout ??= new Button();
            _labelWelcome ??= new Label();
        }

        private void InitializeComponent()
        {
            // 初始化上班打卡按钮
            _btnClockIn = new Button
            {
                Location = new System.Drawing.Point(50, 80),
                Name = "btnClockIn",
                Size = new System.Drawing.Size(120, 40),
                Text = "上班打卡",
                UseVisualStyleBackColor = true
            };
            _btnClockIn.Click += BtnClockIn_Click;

            // 初始化下班打卡按钮
            _btnClockOut = new Button
            {
                Location = new System.Drawing.Point(200, 80),
                Name = "btnClockOut",
                Size = new System.Drawing.Size(120, 40),
                Text = "下班打卡",
                UseVisualStyleBackColor = true
            };
            _btnClockOut.Click += BtnClockOut_Click;

            // 初始化年度薪资按钮
            _btnAnnualSalary = new Button
            {
                Location = new System.Drawing.Point(50, 150),
                Name = "btnAnnualSalary",
                Size = new System.Drawing.Size(120, 40),
                Text = "年度薪资",
                UseVisualStyleBackColor = true
            };
            _btnAnnualSalary.Click += BtnAnnualSalary_Click;

            // 初始化年终奖金按钮
            _btnAnnualBonus = new Button
            {
                Location = new System.Drawing.Point(200, 150),
                Name = "btnAnnualBonus",
                Size = new System.Drawing.Size(120, 40),
                Text = "年终奖金",
                UseVisualStyleBackColor = true
            };
            _btnAnnualBonus.Click += BtnAnnualBonus_Click;

            // 初始化修改密码按钮
            _btnChangePassword = new Button
            {
                Location = new System.Drawing.Point(50, 220),
                Name = "btnChangePassword",
                Size = new System.Drawing.Size(120, 40),
                Text = "修改密码",
                UseVisualStyleBackColor = true
            };
            _btnChangePassword.Click += BtnChangePassword_Click;

            // 初始化退出登录按钮
            _btnLogout = new Button
            {
                Location = new System.Drawing.Point(200, 220),
                Name = "btnLogout",
                Size = new System.Drawing.Size(120, 40),
                Text = "退出登录",
                UseVisualStyleBackColor = true
            };
            _btnLogout.Click += BtnLogout_Click;

            // 初始化欢迎标签
            _labelWelcome = new Label
            {
                AutoSize = true,
                Location = new System.Drawing.Point(50, 30),
                Name = "labelWelcome",
                Size = new System.Drawing.Size(100, 13),
                Text = "欢迎，员工 + " + _username
            };

            // 窗体布局
            ClientSize = new System.Drawing.Size(400, 300);
            Controls.Add(_labelWelcome);
            Controls.Add(_btnClockIn);
            Controls.Add(_btnClockOut);
            Controls.Add(_btnAnnualSalary);
            Controls.Add(_btnAnnualBonus);
            Controls.Add(_btnChangePassword);
            Controls.Add(_btnLogout);
            Text = "员工界面";
        }

        // 上班打卡
        private void BtnClockIn_Click(object? sender, EventArgs? e)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    // 插入上班打卡记录
                    string insertQuery = "INSERT INTO ea1 (Eno, Atime1) VALUES (@Eno, @Atime1)";
                    using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Eno", _username);
                        command.Parameters.AddWithValue("@Atime1", DateTime.Now);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("上班打卡成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("上班打卡失败，请重试！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"MySQL错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"打卡出错：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 下班打卡
        private void BtnClockOut_Click(object? sender, EventArgs? e)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    // 插入下班打卡记录
                    string insertQuery = "INSERT INTO ea2 (Eno, Atime2) VALUES (@Eno, @Atime2)";
                    using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Eno", _username);
                        command.Parameters.AddWithValue("@Atime2", DateTime.Now);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("下班打卡成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("下班打卡失败，请重试！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"MySQL错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"打卡出错：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 年度薪资
        private void BtnAnnualSalary_Click(object? sender, EventArgs? e)
        {
            InputForm inputForm = new InputForm("请输入查询年份（格式：YYYY）", "年度薪资");
            if (inputForm.ShowDialog() == DialogResult.OK)
            {
                string year = inputForm.InputValue.Trim();
                if (!System.Text.RegularExpressions.Regex.IsMatch(year, @"^\d{4}$"))
                {
                    MessageBox.Show("输入格式错误，请严格按照 YYYY 格式输入，如 2025", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SalaryReportForm form = new SalaryReportForm(
                    "年度员工薪资查询",
                    year,
                    _username,
                    isAnnualEmp: true
                );
                form.ShowDialog();
            }
        }

        // 年终奖金
        private void BtnAnnualBonus_Click(object? sender, EventArgs? e)
        {
            InputForm inputForm = new InputForm("请输入查询年份（格式：YYYY）", "年终奖金");
            if (inputForm.ShowDialog() == DialogResult.OK)
            {
                string year = inputForm.InputValue.Trim();
                if (!System.Text.RegularExpressions.Regex.IsMatch(year, @"^\d{4}$"))
                {
                    MessageBox.Show("输入格式错误，请严格按照 YYYY 格式输入，如 2025", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    // 查询年终奖信息
                    string bonusAmount = GetEmployeeBonus(year, _username);

                    if (!string.IsNullOrEmpty(bonusAmount))
                    {
                        MessageBox.Show($"{year}年度获得年终奖{bonusAmount}元！", "年终奖金", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("请在年末查询！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"查询年终奖时出错：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private string GetEmployeeBonus(string year, string employeeId)
        {
            string bonusAmount = null;
            string connectionString = "Server=localhost;Database=employ_salary;Uid=root;Pwd=YOUR_PASSWORD;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT Bonus FROM bonus WHERE Byear = @Year AND Eno = @EmployeeId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Year", year);
                command.Parameters.AddWithValue("@EmployeeId", employeeId);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        bonusAmount = result.ToString();
                    }
                }
                catch (MySqlException ex)
                {
                    throw new Exception($"数据库错误: {ex.Message}", ex);
                }
            }

            return bonusAmount;
        }

        // 修改密码事件
        private void BtnChangePassword_Click(object? sender, EventArgs? e)
        {
            var changePwdForm = new ChangePasswordForm(_username, false);
            changePwdForm.ShowDialog();
        }

        // 退出登录事件
        private void BtnLogout_Click(object? sender, EventArgs? e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
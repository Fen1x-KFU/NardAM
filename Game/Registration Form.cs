using Guna.UI2.WinForms;

namespace Game
{
    public partial class RegistrationForm : Form
    {
        //private UserGame us;
        private AppDbContext db = new AppDbContext();
        private MainForm mainForm;
        public RegistrationForm()
        {
            InitializeComponent();
            ItemsComboBox();
            Validate();
        }

        private void btn_Reg_Click(object sender, EventArgs e)
        {
            var userName = db.Users.FirstOrDefault(u => u.Name == reg_Name.Text);
            var userPass = db.Users.FirstOrDefault(u => u.Password == reg_Pass.Text);

            if (userName != null)
            {
                MessageBox.Show("Пользователь с таким именем уже существует!");
            }
            else if (userPass != null)
            {
                MessageBox.Show("Пользователь с таким паролем уже существует");
            }
            else
            {
                var newUser = new UserGame
                {
                    Name = reg_Name.Text,
                    Password = reg_Pass.Text,
                    Reiting = int.Parse(comboBox.Text),
                    IsReady = false
                };
                //us.Name = reg_Name.Text;
                //us.Password = reg_Pass.Text;
                //us.Reiting = int.Parse(comboBox.Text);
                //us.IsReady = false;
                db.Add(newUser);
                db.SaveChanges();
                MessageBox.Show("Пользователь добавлен!");
                TabControl.TabPages.Remove(tabPage1);
            }
        }



        private void ItemsComboBox()
        {
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            for (int i = 0; i < 500; i += 100)
            {
                comboBox.Items.Add(i.ToString());
            }
        }

        private void ValidateInputsReg()
        {
            if (string.IsNullOrWhiteSpace(reg_Name.Text) ||
            string.IsNullOrWhiteSpace(reg_Pass.Text) ||
            string.IsNullOrWhiteSpace(comboBox.Text))
            {
                btn_Reg.Enabled = false;
            }
            else
            {
                btn_Reg.Enabled = true;
            }
        }

        private void ValidateInputsEnter()
        {
            if (string.IsNullOrWhiteSpace(enter_Name.Text) ||
            string.IsNullOrWhiteSpace(enter_Pass.Text))
            {
                btn_Enter.Enabled = false;
            }
            else
            {
                btn_Enter.Enabled = true;
            }
        }

        private void TextReg(object sender, EventArgs e)
        {
            ValidateInputsReg();
        }

        private void TextEnter(object sender, EventArgs e)
        {
            ValidateInputsEnter();
        }

        private void Validate()
        {
            btn_Reg.Enabled = false;
            btn_Enter.Enabled = false;

            reg_Name.TextChanged += TextReg;
            reg_Pass.TextChanged += TextReg;
            comboBox.SelectedIndexChanged += TextReg;

            enter_Name.TextChanged += TextEnter;
            enter_Pass.TextChanged += TextEnter;
            checkPass_Enter.CheckedChanged += checkPass_Enter_CheckedChanged;
        }

        private void checkPass_Enter_CheckedChanged(object sender, EventArgs e)
        {
            if (checkPass_Enter.Checked)
            {
                enter_Pass.PasswordChar = '*';
            }
            else
            {
                enter_Pass.PasswordChar = '\0';
            }
        }

        private void ApplyLanguage(string lang)
        {
            if (lang is "en")
            {
                lal_Name_Log.Text = Eng.Username.ToString();
                lal_Password_Log.Text = Eng.Password.ToString();
                lal_Rat.Text = Eng.Rating.ToString() + " " + Eng.player.ToString();
                lal_speak.Text = Eng.English.ToString();
                lal_Name_Reg.Text = Eng.Username.ToString();
                lal_Password_Reg.Text = Eng.Password.ToString();
                btn_Reg.Text = Eng.REGISTER.ToString();
                btn_Enter.Text = Eng.ENTER.ToString();
                tabPage1.Text = Eng.Registration.ToString();
                tabPage2.Text = Eng.Authorization.ToString();
                this.Text = Eng.Form.ToString() + " " + Eng.Registration.ToString();
                checkPass_Enter.Text = Eng.Hide.ToString() + " " + Eng.password.ToString();
            }
            else
            {
                lal_Name_Log.Text = Rus.Имя.ToString() + " " + Rus.пользователя.ToString();
                lal_Password_Log.Text = Rus.Пароль.ToString();
                lal_Rat.Text = Rus.Рейтинг.ToString() + " " + Rus.игрока.ToString();
                lal_speak.Text = Rus.Русский.ToString();
                lal_Name_Reg.Text = Rus.Имя.ToString() + " " + Rus.пользователя.ToString();
                lal_Password_Reg.Text = Rus.Пароль.ToString();
                btn_Reg.Text = Rus.ЗАРЕГИСТРИРОВАТЬСЯ.ToString();
                btn_Enter.Text = Rus.ВОЙТИ.ToString();
                tabPage1.Text = Rus.Регистрация.ToString();
                tabPage2.Text = Rus.Авторизация.ToString();
                this.Text = Rus.Форма.ToString() + " " + Rus.Регистрации.ToString();
                checkPass_Enter.Text = Rus.Скрыть.ToString() + " " + Rus.пароль.ToString();
            }
        }

        private void Switch_Language_CheckedChanged(object sender, EventArgs e)
        {
            if (switch_Language.Checked)
            {
                ApplyLanguage("en");
            }
            else
            {
                ApplyLanguage("ru");
            }
        }

        private void btn_Enter_Click(object sender, EventArgs e)
        {
            //us.Name = enter_Name.Text;
            //us.
            var user = db.Users.FirstOrDefault(u => u.Name == enter_Name.Text);

            if (user != null)
            {
                if (user.Password == enter_Pass.Text)
                {
                    mainForm = new MainForm(user);
                    //MessageBox.Show(user.Name);
                    MessageBox.Show("Успешно!");
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Проверьте пароль!");
                }
            }
            else
            {
                MessageBox.Show("Пользователя с таким именем не существует!");
            }
        }
    }


}
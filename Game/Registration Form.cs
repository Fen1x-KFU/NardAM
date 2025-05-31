namespace Game
{
    public partial class RegistrationForm : Form
    {
        private UserGame us = new UserGame();
        private AppDbContext db = new AppDbContext();
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
                us.Name = reg_Name.Text;
                us.Password = reg_Pass.Text;
                us.Reiting = int.Parse(comboBox.Text);
                db.Add(us);
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

        
    }


}
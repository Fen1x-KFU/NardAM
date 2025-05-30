using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    public partial class RegistrationForm : Form
    {
        private UserGame us = new UserGame();
        private AppDbContext db;
        public RegistrationForm()
        {
            InitializeComponent();
            Forms();
        }

        private void Forms()
        {
            MainForm formPlayer1 = new MainForm();
            //MainForm formPlayer2 = new MainForm();
            formPlayer1.Show();
            //formPlayer2.Show();
        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btn_Reg_Click(object sender, EventArgs e)
        {
            us.Name = Reg_Name.Text;
            us.Password = Reg_Pass.Text;
            us.Reiting = 500;
            db = new AppDbContext();
            db.Users.Add(us);
            db.SaveChanges();
        }
    }
}

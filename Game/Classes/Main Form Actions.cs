using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Game
{
    public partial class MainForm : Form
    {
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (var freshDb = new AppDbContext())
            {
                var currentUser = freshDb.Users.Find(user_this.Id);
                if (currentUser != null)
                {
                    currentUser.IsReady = "NIG";
                    freshDb.SaveChanges();
                }
            }
            Application.Exit();
        }

        private void LoadImageToBox(PictureBox pictureBox, string imagePath)
        {
            if (pictureBox == null) return;

            if (!File.Exists(imagePath))
            {
                MessageBox.Show($"Файл не найден: {imagePath}");
                return;
            }

            try
            {
                pictureBox.Image?.Dispose(); // Освобождаем старое изображение
                pictureBox.Image = new Bitmap(imagePath);
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки изображения: {ex.Message}");
            }
        }
    }
}

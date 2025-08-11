using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public partial class MainForm : Form
    {
        private void CreateChips(Color color_this)
        {
            int chipSize = 32;       // Размер фишки
            int overlap = chipSize / 4;

            for (int i = 0; i < 15; i++)
            {
                Guna2CircleButton chip = new Guna2CircleButton();
                chip.Width = chipSize;
                chip.Height = chipSize;
                chip.FillColor = color_this;
                chip.BackColor = Color.Transparent;

                chip.Location = new Point(
                    board.Left,
                    board.Top + board.Height / 2 - chipSize - i * 10
                    );

                board.Controls.Add(chip);
                chip.BringToFront();
            }

            for (int i = 0; i < 15; i++)
            {
                Guna2CircleButton chip = new Guna2CircleButton();
                chip.Width = chipSize;
                chip.Height = chipSize;
                chip.FillColor = Color.Black;
                chip.BackColor = Color.Transparent;

                chip.Location = new Point(
                    board.Right - 118,
                    board.Top - board.Height / 2 + chipSize + i * 10
                    );

                board.Controls.Add(chip);
                chip.BringToFront();
            }
        }

        private void CreateButtonMove()
        {
            for (int i = 0; i < 6; i--)
            {
                var but = new Button()
                {
                    Name = $"but{i + 13}",
                    Height = 40,
                    Width = 40,
                    Location = new Point(
                        board.Left + 43 + i * 67,
                        board.Top - 50
                    ),
                    BackColor = Color.White,
                    Text = $"{i + 13}"
                };

                this.Controls.Add(but);
                but.BringToFront();
            }

            for (int i = 0; i < 6; i--)
            {
                var but = new Button()
                {
                    Name = $"but{i + 19}",
                    Height = 40,
                    Width = 40,
                    Location = new Point(
                        board.Left + 43 + (i + 6) * 67 + 55,
                        board.Top - 50
                    ),
                    BackColor = Color.White,
                    Text = $"{i + 19}"
                };

                this.Controls.Add(but);
                but.BringToFront();
            }

            for (int i = 0; i < 6; i++)
            {
                var but = new Button()
                {
                    Name = $"but{i + 1}",
                    Height = 40,
                    Width = 40,
                    Location = new Point(
                        board.Left + 43 + i * 67,
                        board.Top + board.Height + 18
                    ),
                    BackColor = Color.White,
                    Text = $"{i + 1}"
                };

                this.Controls.Add(but);
                but.BringToFront();
            }

            for (int i = 0; i < 6; i++)
            {
                var but = new Button()
                {
                    Name = $"but{i + 7}",
                    Height = 40,
                    Width = 40,
                    Location = new Point(
                        board.Left + 43 + (i + 6) * 67 + 55,
                        board.Top + board.Height + 18
                    ),
                    BackColor = Color.White,
                    Text = $"{i + 7}"
                };

                this.Controls.Add(but);
                but.BringToFront();
            }

        }


    }
}

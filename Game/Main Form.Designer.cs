namespace Game
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            board = new PictureBox();
            btn_Ready = new Guna.UI2.WinForms.Guna2CircleButton();
            lPlayer = new Label();
            lPlayer2 = new Label();
            btn_Roll = new Guna.UI2.WinForms.Guna2Button();
            cube2 = new PictureBox();
            cube1 = new PictureBox();
            cube3 = new PictureBox();
            cube4 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)board).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cube2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cube1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cube3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cube4).BeginInit();
            SuspendLayout();
            // 
            // board
            // 
            board.Image = (Image)resources.GetObject("board.Image");
            board.Location = new Point(41, 250);
            board.Margin = new Padding(3, 4, 3, 4);
            board.Name = "board";
            board.Size = new Size(920, 535);
            board.SizeMode = PictureBoxSizeMode.StretchImage;
            board.TabIndex = 0;
            board.TabStop = false;
            // 
            // btn_Ready
            // 
            btn_Ready.BackColor = Color.Transparent;
            btn_Ready.DisabledState.BorderColor = Color.DarkGray;
            btn_Ready.DisabledState.CustomBorderColor = Color.DarkGray;
            btn_Ready.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btn_Ready.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btn_Ready.FillColor = Color.LimeGreen;
            btn_Ready.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btn_Ready.ForeColor = Color.White;
            btn_Ready.Location = new Point(436, 448);
            btn_Ready.Name = "btn_Ready";
            btn_Ready.ShadowDecoration.CustomizableEdges = customizableEdges1;
            btn_Ready.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            btn_Ready.Size = new Size(126, 125);
            btn_Ready.TabIndex = 1;
            btn_Ready.Text = "Готов";
            btn_Ready.Click += btn_Ready_Click;
            // 
            // lPlayer
            // 
            lPlayer.AutoSize = true;
            lPlayer.BackColor = Color.Transparent;
            lPlayer.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lPlayer.ForeColor = SystemColors.Control;
            lPlayer.Location = new Point(66, 905);
            lPlayer.Name = "lPlayer";
            lPlayer.Size = new Size(84, 28);
            lPlayer.TabIndex = 2;
            lPlayer.Text = "Игрок 1";
            // 
            // lPlayer2
            // 
            lPlayer2.AutoSize = true;
            lPlayer2.BackColor = Color.Transparent;
            lPlayer2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lPlayer2.ForeColor = SystemColors.Control;
            lPlayer2.Location = new Point(793, 905);
            lPlayer2.Name = "lPlayer2";
            lPlayer2.Size = new Size(87, 28);
            lPlayer2.TabIndex = 3;
            lPlayer2.Text = "Игрок 2";
            // 
            // btn_Roll
            // 
            btn_Roll.BackColor = Color.Transparent;
            btn_Roll.BorderRadius = 20;
            btn_Roll.CustomizableEdges = customizableEdges2;
            btn_Roll.DisabledState.BorderColor = Color.DarkGray;
            btn_Roll.DisabledState.CustomBorderColor = Color.DarkGray;
            btn_Roll.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btn_Roll.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btn_Roll.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btn_Roll.ForeColor = Color.White;
            btn_Roll.Location = new Point(390, 51);
            btn_Roll.Name = "btn_Roll";
            btn_Roll.ShadowDecoration.CustomizableEdges = customizableEdges3;
            btn_Roll.Size = new Size(225, 69);
            btn_Roll.TabIndex = 4;
            btn_Roll.Text = "Бросить кубик";
            btn_Roll.Click += btn_RollStart_Click;
            // 
            // cube2
            // 
            cube2.Location = new Point(253, 31);
            cube2.Name = "cube2";
            cube2.Size = new Size(110, 110);
            cube2.TabIndex = 5;
            cube2.TabStop = false;
            // 
            // cube1
            // 
            cube1.Location = new Point(106, 31);
            cube1.Name = "cube1";
            cube1.Size = new Size(110, 110);
            cube1.TabIndex = 6;
            cube1.TabStop = false;
            // 
            // cube3
            // 
            cube3.Location = new Point(644, 31);
            cube3.Name = "cube3";
            cube3.Size = new Size(110, 110);
            cube3.TabIndex = 7;
            cube3.TabStop = false;
            // 
            // cube4
            // 
            cube4.Location = new Point(793, 31);
            cube4.Name = "cube4";
            cube4.Size = new Size(110, 110);
            cube4.TabIndex = 8;
            cube4.TabStop = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1003, 965);
            Controls.Add(cube4);
            Controls.Add(cube3);
            Controls.Add(cube1);
            Controls.Add(cube2);
            Controls.Add(btn_Roll);
            Controls.Add(lPlayer2);
            Controls.Add(lPlayer);
            Controls.Add(btn_Ready);
            Controls.Add(board);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "MainForm";
            Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)board).EndInit();
            ((System.ComponentModel.ISupportInitialize)cube2).EndInit();
            ((System.ComponentModel.ISupportInitialize)cube1).EndInit();
            ((System.ComponentModel.ISupportInitialize)cube3).EndInit();
            ((System.ComponentModel.ISupportInitialize)cube4).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox board;
        private Guna.UI2.WinForms.Guna2CircleButton btn_Ready;
        private Label lPlayer;
        private Label lPlayer2;
        private Guna.UI2.WinForms.Guna2Button btn_Roll;
        private PictureBox cube2;
        private PictureBox cube1;
        private PictureBox cube3;
        private PictureBox cube4;
    }
}

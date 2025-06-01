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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            board = new PictureBox();
            btn_Ready = new Guna.UI2.WinForms.Guna2CircleButton();
            ((System.ComponentModel.ISupportInitialize)board).BeginInit();
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
            btn_Ready.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btn_Ready.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            btn_Ready.Size = new Size(126, 125);
            btn_Ready.TabIndex = 1;
            btn_Ready.Text = "Готов";
            btn_Ready.Click += btn_Ready_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1003, 965);
            Controls.Add(btn_Ready);
            Controls.Add(board);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "MainForm";
            Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)board).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox board;
        private Guna.UI2.WinForms.Guna2CircleButton btn_Ready;
    }
}

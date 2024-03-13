namespace TestowanieOprogramowania
{
    partial class FormInitial
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelslide = new Panel();
            buttonLogin = new Button();
            buttonZarzadzaj = new Button();
            mainpanel = new Panel();
            panelslide.SuspendLayout();
            SuspendLayout();
            // 
            // panelslide
            // 
            panelslide.BackColor = Color.Gray;
            panelslide.Controls.Add(buttonLogin);
            panelslide.Controls.Add(buttonZarzadzaj);
            panelslide.Dock = DockStyle.Left;
            panelslide.Location = new Point(0, 0);
            panelslide.Name = "panelslide";
            panelslide.Size = new Size(200, 763);
            panelslide.TabIndex = 0;
            panelslide.Paint += panelslide_Paint;
            // 
            // buttonLogin
            // 
            buttonLogin.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonLogin.Location = new Point(12, 698);
            buttonLogin.Name = "buttonLogin";
            buttonLogin.Size = new Size(171, 53);
            buttonLogin.TabIndex = 1;
            buttonLogin.Text = "Login";
            buttonLogin.UseVisualStyleBackColor = true;
            buttonLogin.Click += buttonLogin_Click;
            // 
            // buttonZarzadzaj
            // 
            buttonZarzadzaj.Location = new Point(12, 12);
            buttonZarzadzaj.Name = "buttonZarzadzaj";
            buttonZarzadzaj.Size = new Size(171, 53);
            buttonZarzadzaj.TabIndex = 0;
            buttonZarzadzaj.Text = "Zarządzaj Użytkownikami";
            buttonZarzadzaj.UseVisualStyleBackColor = true;
            buttonZarzadzaj.Click += buttonZarzadzaj_Click;
            // 
            // mainpanel
            // 
            mainpanel.Dock = DockStyle.Fill;
            mainpanel.Location = new Point(200, 0);
            mainpanel.Name = "mainpanel";
            mainpanel.Size = new Size(1015, 763);
            mainpanel.TabIndex = 1;
            mainpanel.Paint += mainpanel_Paint;
            // 
            // FormInitial
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1215, 763);
            Controls.Add(mainpanel);
            Controls.Add(panelslide);
            Name = "FormInitial";
            Text = "FormInitial";
            WindowState = FormWindowState.Maximized;
            panelslide.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelslide;
        private Button buttonZarzadzaj;
        private Panel mainpanel;
        private Button buttonLogin;
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestowanieOprogramowania
{
    public partial class FormInitial : Form
    {
        public FormInitial()
        {
            InitializeComponent();
            //this.FormBorderStyle = FormBorderStyle.None;
            //this.WindowState = FormWindowState.Maximized;
        }

        private void mainpanel_Paint(object sender, PaintEventArgs e)
        {

        }
        public void loadform(object Form)
        {
            if (this.mainpanel.Controls.Count > 0)
                this.mainpanel.Controls.RemoveAt(0);
            Form f = Form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.mainpanel.Controls.Add(f);
            this.mainpanel.Tag = f;
            f.Show();
        }

        private void buttonZarzadzaj_Click(object sender, EventArgs e)
        {
            loadform(new Form1());
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            //loadform(new FormDodajUzytkownika());
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            loadform(new FormLogin());
        }
    }
}

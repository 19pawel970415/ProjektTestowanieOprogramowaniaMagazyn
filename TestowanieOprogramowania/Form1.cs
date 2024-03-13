using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.VisualBasic.ApplicationServices;

namespace TestowanieOprogramowania
{
    public partial class Form1 : Form
    {



        private ZarzadzanieVoidami zarzadzanieVoidami;

        public Form1()
        {
            InitializeComponent();

            zarzadzanieVoidami = new ZarzadzanieVoidami();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            string StringPolaczeniowy = PolaczenieBazyDanych.StringPolaczeniowy();

        }



        private void Form1_Load(object sender, EventArgs e)
        {
            OdswiezDataGridView();
        }

        private void OdswiezDataGridView()
        {
            var listaUzytkownikow = zarzadzanieVoidami.PobierzUzytkownikow();
            dataGridView1.DataSource = listaUzytkownikow;
        }

        private void buttonUsunUzytkownika_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var result = MessageBox.Show("Czy na pewno chcesz usun¹æ zaznaczonego u¿ytkownika?", "Potwierdzenie", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    int userId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["UzytkownikID"].Value);

                    zarzadzanieVoidami.UsunUzytkownikaZBazy(userId);

                    OdswiezDataGridView();
                }
            }
            else
            {
                MessageBox.Show("Proszê zaznaczyæ wiersz do usuniêcia", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void buttonSzukaj_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Wyszukiwanie narazie tylko po Login Imie Nazwisko - resztê trzeba dodaæ");
            zarzadzanieVoidami.WyszukajUzytkownikow(textBoxSzukaj.Text);
        }




        private void buttonDodajUzytkownika_Click_1(object sender, EventArgs e)
        {
            using (var formDodaj = new FormDodajUzytkownika())
            {
                var result = formDodaj.ShowDialog();
                if (result == DialogResult.OK)
                {
                    OdswiezDataGridView();
                }
            }
        }



        private void buttonEdytujUzytkownika_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int userId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["UzytkownikID"].Value);
                using (var formEdytuj = new FormEdytujUzytkownika(userId))
                {
                    var result = formEdytuj.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        OdswiezDataGridView(); // Zak³adaj¹c, ¿e ta metoda istnieje w formularzu
                    }
                }
            }
            else
            {
                MessageBox.Show("Proszê zaznaczyæ wiersz do edycji", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


    }
}
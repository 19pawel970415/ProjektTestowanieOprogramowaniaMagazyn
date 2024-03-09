using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Data;

namespace TestowanieOprogramowania
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

        }

        public string conString = "Data Source=LAPTOP-72SPAJ8D;Initial Catalog=MagazynTestowanieOprogramowania;Integrated Security=True; TrustServerCertificate=True;";

        private void Form1_Load(object sender, EventArgs e)
        {
            OdswiezDataGridView();
        }

        private void buttonUsunUzytkownika_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var result = MessageBox.Show("Czy na pewno chcesz usun�� zaznaczonego u�ytkownika?", "Potwierdzenie", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    int userId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["UzytkownikID"].Value);

                    UsunUzytkownikaZBazy(userId);

                    OdswiezDataGridView();
                }
            }
            else
            {
                MessageBox.Show("Prosz� zaznaczy� wiersz do usuni�cia", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }





        private void UsunUzytkownikaZBazy(int userId)
        {
            string connectionString = "Data Source=LAPTOP-72SPAJ8D;Initial Catalog=MagazynTestowanieOprogramowania;Integrated Security=True; TrustServerCertificate=True;";
            UsunPowiazaneUprawnienia(userId);
            string query = "DELETE FROM dbo.Uzytkownicy WHERE UzytkownikID = @userId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    // SQL Injection
                    cmd.Parameters.AddWithValue("@userId", userId);

                    connection.Open();
                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("U�ytkownik zosta� usuni�ty.");
                    }
                    else
                    {
                        MessageBox.Show("Nie znaleziono u�ytkownika o podanym ID.");
                    }
                    connection.Close();
                }
            }
        }

        private void UsunPowiazaneUprawnienia(int userId)
        {
            string connectionString = "Data Source=LAPTOP-72SPAJ8D;Initial Catalog=MagazynTestowanieOprogramowania;Integrated Security=True; TrustServerCertificate=True;";
            string query = "DELETE FROM dbo.Uprawnienia WHERE UzytkownikID = @userId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@userId", SqlDbType.Int)).Value = userId;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        private void OdswiezDataGridView()
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            string query = "SELECT * FROM dbo.Uzytkownicy";
            List<Uzytkownik> listaUzytkownikow = new List<Uzytkownik>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Uzytkownik uzytkownik = new Uzytkownik
                            {
                                UzytkownikID = Convert.ToInt32(reader["UzytkownikID"]),
                                Login = reader["Login"].ToString(),
                                Imie = reader["Imie"].ToString(),
                                Nazwisko = reader["Nazwisko"].ToString(),
                            };
                            listaUzytkownikow.Add(uzytkownik);
                        }
                    }
                }
            }
            dataGridView1.DataSource = listaUzytkownikow;
        }

        private void buttonSzukaj_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Wyszukiwanie narazie tylko po Login Imie Nazwisko - reszt� trzeba doda�");
            WyszukajUzytkownikow(textBoxSzukaj.Text);
        }

        private void WyszukajUzytkownikow(string szukanyTekst)
        {
            string connectionString = "Data Source=LAPTOP-72SPAJ8D;Initial Catalog=MagazynTestowanieOprogramowania;Integrated Security=True; TrustServerCertificate=True;";
            string query = "SELECT * FROM dbo.Uzytkownicy WHERE Login LIKE @szukanyTekst OR Imie LIKE @szukanyTekst OR Nazwisko LIKE @szukanyTekst";
            List<Uzytkownik> listaUzytkownikow = new List<Uzytkownik>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(new SqlParameter("@szukanyTekst", SqlDbType.NVarChar)).Value = "%" + szukanyTekst + "%";

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Uzytkownik uzytkownik = new Uzytkownik
                            {
                                UzytkownikID = Convert.ToInt32(reader["UzytkownikID"]),
                                Login = reader["Login"].ToString(),
                                Imie = reader["Imie"].ToString(),
                                Nazwisko = reader["Nazwisko"].ToString(),
                            };
                            listaUzytkownikow.Add(uzytkownik);
                        }
                    }
                }
            }
            dataGridView1.DataSource = listaUzytkownikow;
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
    }
}
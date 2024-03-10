using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Data;

namespace TestowanieOprogramowania
{
    public partial class Form1 : Form
    {
        public string conString;
        //Zmieñ dataSource na swoj¹ nazwê serwera
        private string dataSource = "LAPTOP-72SPAJ8D";
        public Form1()
        {
            InitializeComponent();
            conString = $"Data Source={dataSource};Initial Catalog=MagazynTestowanieOprogramowania;Integrated Security=True; TrustServerCertificate=True;";
        }
        
       

        private void Form1_Load(object sender, EventArgs e)
        {
            OdswiezDataGridView();
        }

        private void buttonUsunUzytkownika_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var result = MessageBox.Show("Czy na pewno chcesz usun¹æ zaznaczonego u¿ytkownika?", "Potwierdzenie", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    int userId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["UzytkownikID"].Value);

                    UsunUzytkownikaZBazy(userId);

                    OdswiezDataGridView();
                }
            }
            else
            {
                MessageBox.Show("Proszê zaznaczyæ wiersz do usuniêcia", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }





        private void UsunUzytkownikaZBazy(int userId)
        {
            UsunPowiazaneUprawnienia(userId);
            string query = "DELETE FROM dbo.Uzytkownicy WHERE UzytkownikID = @userId";

            using (SqlConnection connection = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    // SQL Injection
                    cmd.Parameters.AddWithValue("@userId", userId);

                    connection.Open();
                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("U¿ytkownik zosta³ usuniêty.");
                    }
                    else
                    {
                        MessageBox.Show("Nie znaleziono u¿ytkownika o podanym ID.");
                    }
                    connection.Close();
                }
            }
        }

        private void UsunPowiazaneUprawnienia(int userId)
        {
            string query = "DELETE FROM dbo.Uprawnienia WHERE UzytkownikID = @userId";

            using (SqlConnection conn = new SqlConnection(conString))
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


                                Miejscowosc = reader["Miejscowosc"].ToString(),
                                KodPocztowy= reader["KodPocztowy"].ToString(),
                                Ulica= reader["Ulica"].ToString(),
                                NumerPosesji = reader["NumerPosesji"].ToString(),
                                NumerLokalu = reader["NumerLokalu"].ToString(),
                                PESEL = reader["PESEL"].ToString(),
                                DataUrodzenia = reader["DataUrodzenia"].ToString(),
                                Plec = reader["Plec"].ToString(),
                                Email = reader["Email"].ToString(),
                                NumerTelefonu = reader["NumerTelefonu"].ToString()
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
            MessageBox.Show("Wyszukiwanie narazie tylko po Login Imie Nazwisko - resztê trzeba dodaæ");
            WyszukajUzytkownikow(textBoxSzukaj.Text);
        }

        private void WyszukajUzytkownikow(string szukanyTekst)
        {
            string query = "SELECT * FROM dbo.Uzytkownicy WHERE Login LIKE @szukanyTekst OR Imie LIKE @szukanyTekst OR Nazwisko LIKE @szukanyTekst";
            List<Uzytkownik> listaUzytkownikow = new List<Uzytkownik>();

            using (SqlConnection connection = new SqlConnection(conString))
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

                                Miejscowosc = reader["Miejscowosc"].ToString(),
                                KodPocztowy= reader["KodPocztowy"].ToString(),
                                Ulica= reader["Ulica"].ToString(),
                                NumerPosesji = reader["NumerPosesji"].ToString(),
                                NumerLokalu = reader["NumerLokalu"].ToString(),
                                PESEL = reader["PESEL"].ToString(),
                                DataUrodzenia = reader["DataUrodzenia"].ToString(),
                                Plec = reader["Plec"].ToString(),
                                Email = reader["Email"].ToString(),
                                NumerTelefonu = reader["NumerTelefonu"].ToString()
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
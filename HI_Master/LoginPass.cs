using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HI_Master
{
    public partial class LoginPass : Form
    {
        public LoginPass()
        {
            InitializeComponent();
            mySqlConnector();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = comboBoxUsers.Text;
            string password = passBox.Text;

            if (passChecker(login, password))
            {
                Main main = new Main();
                main.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Enter a valid credentials!");
                passBox.Text = "";
                return;
            }                        
        }

        public void mySqlConnector()
        {
            MySqlConnection connection = DBUtils.GetDBConnection();
            string queryUsersTable = $"SELECT login, password FROM hi_master_users";
            
            connection.Open();

            MySqlCommand command = new MySqlCommand(queryUsersTable, connection);
            MySqlDataReader reader = command.ExecuteReader();

            List<string[]> data = new List<string[]>();

            while (reader.Read())
            {
                data.Add(new string[10]);

                data[data.Count - 1][0] = reader[0].ToString();
            }

            reader.Close();

            connection.Close();

            foreach (string[] s  in data)
            {
                comboBoxUsers.Items.Add(s[0]);
            }
        }
        private bool passChecker(string login, string password)
        {
            if (passBox.Text != "" && comboBoxUsers.Text != "")
            {
                string loginDB = "";
                string passDB = "";
                string query = $"SELECT login, password FROM hi_master_users WHERE login='{comboBoxUsers.Text}'";

                MySqlConnection connection = DBUtils.GetDBConnection();
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                reader.Read();

                loginDB = reader[0].ToString();
                passDB = reader[1].ToString();

                if (loginDB == login && passDB == password)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                reader.Close();
                connection.Close();
            }
            else
            {
                return false;
            }
        }
    }
}

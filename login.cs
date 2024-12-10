using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AKADEMINE_Arnas_Globys_PI23B
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }

            try
            {
                
                DBdetails db = new DBdetails("localhost", "akademineis", "root", "");
               
                User loggedInUser = GetUserByUsernameAndPassword(username, password, db);

                if (loggedInUser != null)
                {
                   
                    menu menu = new menu(loggedInUser);
                    menu.Show();

                    
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid username or password. Please try again.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }


        private User GetUserByUsernameAndPassword(string username, string password, DBdetails db)
        {
            
            string query = @"
            SELECT UserID 
            FROM user 
            WHERE Name = @Username AND Surname = @Password;";

            var parameters = new Dictionary<string, object>
            {
                { "@Username", username }, 
                { "@Password", password }  
            };

            using (var reader = db.ExecuteQuery(query, parameters))
            {
                if (reader.Read())
                {
                    int userId = reader.GetInt32("UserID");
                    return User.GetUserFromDatabase(userId, db); 
                }
            }

            return null;
        }
    }
}

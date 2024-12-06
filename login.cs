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
                SELECT u.UserID, u.academicSystemID, u.Name, u.Surname, u.Email, u.PhoneNumber, u.Username, u.Password, u.CreatedAt,
                       CASE 
                           WHEN s.StudentID IS NOT NULL THEN 'Student'
                           WHEN p.ProfessorID IS NOT NULL THEN 'Professor'
                           WHEN a.AdminID IS NOT NULL THEN 'Admin'
                       END AS UserType,
                       s.StudentID, s.groupID, s.StudyProgramID,
                       p.ProfessorID,
                       a.AdminID
                FROM user u
                LEFT JOIN student s ON u.UserID = s.UserID
                LEFT JOIN professor p ON u.UserID = p.UserID
                LEFT JOIN admin a ON u.UserID = a.UserID
                WHERE u.Username = @Username AND u.Password = @Password;";

            var parameters = new Dictionary<string, object>
            {
                { "@Username", username },
                { "@Password", password }
            };

            using (var reader = db.ExecuteQuery(query, parameters))
            {
                if (reader.Read())
                {
                    string userType = reader["UserType"].ToString();
                    int userId = reader.GetInt32("UserID");
                    int academicSystemId = reader.GetInt32("academicSystemID");
                    string name = reader["Name"].ToString();
                    string surname = reader["Surname"].ToString();
                    string email = reader["Email"].ToString();
                    string phoneNumber = reader["PhoneNumber"].ToString();
                    string userPassword = reader["Password"].ToString();
                    DateTime createdAt = reader.GetDateTime("CreatedAt");

                    if (userType == "Student")
                    {
                        int studentId = reader.GetInt32("StudentID");
                        int groupId = reader.GetInt32("groupID");
                        int studyProgramId = reader.GetInt32("StudyProgramID");
                        return new Student(studentId, userId, academicSystemId, name, surname, email, phoneNumber, username, userPassword, createdAt, groupId, studyProgramId);
                    }
                    else if (userType == "Professor")
                    {
                        int professorId = reader.GetInt32("ProfessorID");
                        return new Professor(professorId, userId, academicSystemId, name, surname, email, phoneNumber, username, userPassword, createdAt);
                    }
                    else if (userType == "Admin")
                    {
                        int adminId = reader.GetInt32("AdminID");
                        return new Admin(adminId, userId, academicSystemId, name, surname, email, phoneNumber, username, userPassword, createdAt);
                    }
                }
            }

            return null; 
        }
    }
}

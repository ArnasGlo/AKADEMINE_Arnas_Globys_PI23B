using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKADEMINE_Arnas_Globys_PI23B
{

    public abstract class User
    {
        public int UserId { get; private set; }
        public int AcademicSystemId { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public DateTime CreatedAt { get; private set; }

        protected User(int userId, int academicSystemId, string name, string surname, string email, string phoneNumber, string username, string password, DateTime createdAt)
        {
            UserId = userId;
            AcademicSystemId = academicSystemId;
            Name = name;
            Surname = surname;
            Email = email;
            PhoneNumber = phoneNumber;
            Username = username;
            Password = password;
            CreatedAt = createdAt;
        }
        public static User GetUserFromDatabase(int userId, DBdetails db)
        {
            string query = @"
    SELECT u.UserID, u.academicSystemID, u.Name, u.Surname, u.Email, u.PhoneNumber, u.Username, u.Password, u.CreatedAt,s.StudentID, s.groupID, s.StudyProgramID, p.ProfessorID, a.AdminID,
           CASE  
               WHEN s.StudentID IS NOT NULL THEN 'Student'
               WHEN p.ProfessorID IS NOT NULL THEN 'Professor'
               WHEN a.AdminID IS NOT NULL THEN 'Admin'
           END AS UserType
    FROM user u
    LEFT JOIN student s ON u.UserID = s.UserID
    LEFT JOIN professor p ON u.UserID = p.UserID
    LEFT JOIN admin a ON u.UserID = a.UserID
    WHERE u.UserID = @UserId;";

            var parameters = new Dictionary<string, object> { { "@UserId", userId } };

            using (var reader = db.ExecuteQuery(query, parameters))
            {
                if (reader.Read())
                {
                    string userType = reader["UserType"].ToString();
                    int academicSystemId = reader.GetInt32("academicSystemID");
                    string name = reader["Name"].ToString();
                    string surname = reader["Surname"].ToString();
                    string email = reader["Email"].ToString();
                    string phoneNumber = reader["PhoneNumber"].ToString();
                    string username = reader["Username"].ToString();
                    string password = reader["Password"].ToString();
                    DateTime createdAt = reader.GetDateTime("CreatedAt");

                    if (userType == "Student")
                    {
                        int studentId = reader.GetInt32("StudentID");
                        int groupId = reader.GetInt32("groupID");
                        int studyProgramId = reader.GetInt32("StudyProgramID");
                        return new Student(studentId, userId, academicSystemId, name, surname, email, phoneNumber, username, password, createdAt, groupId, studyProgramId);
                    }
                    else if (userType == "Professor")
                    {
                        int professorId = reader.GetInt32("ProfessorID");
                        return new Professor(professorId, userId, academicSystemId, name, surname, email, phoneNumber, username, password, createdAt);
                    }
                    else if (userType == "Admin")
                    {
                        int adminId = reader.GetInt32("AdminID");
                        return new Admin(adminId, userId, academicSystemId, name, surname, email, phoneNumber, username, password, createdAt);
                    }
                }
            }

            throw new Exception("User not found or unsupported user type.");
        }

    }

}

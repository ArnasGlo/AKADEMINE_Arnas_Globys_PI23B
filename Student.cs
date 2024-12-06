using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKADEMINE_Arnas_Globys_PI23B
{
    public class Student : User
    {
        public int StudentId { get; private set; }
        public int GroupId { get; private set; }
        public int StudyProgramId { get; private set; }
        public int Grade { get; set; } 

        public Student(int studentId, int userId, int academicSystemId, string name, string surname, string email, string phoneNumber, string username, string password, DateTime createdAt, int groupId, int studyProgramId)
            : base(userId, academicSystemId, name, surname, email, phoneNumber, username, password, createdAt)
        {
            StudentId = studentId;
            GroupId = groupId;
            StudyProgramId = studyProgramId;
        }

        public static Student GetStudentByUserId(int userId, DBdetails db)
        {
            string query = "SELECT * FROM student WHERE UserID = @UserId;";
            var parameters = new Dictionary<string, object> { { "@UserId", userId } };

            using (var reader = db.ExecuteQuery(query, parameters))
            {
                if (reader.Read())
                {
                    return new Student(
                        reader.GetInt32("StudentID"),
                        reader.GetInt32("UserID"),
                        1, 
                        reader.GetString("Name"),
                        reader.GetString("Surname"),
                        reader.GetString("Email"),
                        reader.GetString("PhoneNumber"),
                        reader.GetString("Username"),
                        reader.GetString("Password"),
                        reader.GetDateTime("CreatedAt"),
                        reader.GetInt32("GroupID"),
                        reader.GetInt32("StudyProgramID")
                    );
                }
            }

            throw new Exception("Student not found.");
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKADEMINE_Arnas_Globys_PI23B
{
    public class Professor : User
    {
        public int ProfessorId { get; private set; }

        public Professor(int professorId, int userId, int academicSystemId, string name, string surname, string email, string phoneNumber, string username, string password, DateTime createdAt)
            : base(userId, academicSystemId, name, surname, email, phoneNumber, username, password, createdAt)
        {
            ProfessorId = professorId;
        }

        public static List<Group> GetGroupsForProfessor(int professorId, DBdetails db)
        {
            string query = @"
        SELECT g.groupID, g.title, g.CreatedAt
        FROM `professor group` pg
        INNER JOIN `group` g ON pg.groupID = g.groupID
        WHERE pg.ProfessorID = @ProfessorId;";

            var parameters = new Dictionary<string, object>
            {
                { "@ProfessorId", professorId }
            };

            var groups = new List<Group>();

            using (var reader = db.ExecuteQuery(query, parameters))
            {
                while (reader.Read())
                {
                    groups.Add(new Group(
                        reader.GetInt32("groupID"),
                        reader.GetString("title"),
                        reader.GetDateTime("CreatedAt")
                    ));
                }
            }

            return groups;
        }

        public static List<StudyProgram> GetStudyProgramsForProfessor(int professorId, DBdetails db)
        {
            string query = @"
        SELECT sp.StudyProgramID, sp.academicSystemID, sp.studies, sp.semester
        FROM `professor program` pp
        INNER JOIN `study program` sp ON pp.StudyProgramID = sp.StudyProgramID
        WHERE pp.ProfessorID = @ProfessorId;";

            var parameters = new Dictionary<string, object>
            {
                { "@ProfessorId", professorId }
            };

            var studyPrograms = new List<StudyProgram>();

            using (var reader = db.ExecuteQuery(query, parameters))
            {
                while (reader.Read())
                {
                    int programId = reader.GetInt32("StudyProgramID");
                    int academicSystemId = reader.GetInt32("academicSystemID");
                    string studies = reader.GetString("studies");
                    int semester = reader.GetInt32("semester");

                    studyPrograms.Add(new StudyProgram(programId, academicSystemId, studies, semester));
                }
            }

            return studyPrograms;
        }
        public List<Student> GetStudentsForSubject(int subjectId, DBdetails db)
        {
            string query = @"
        SELECT s.StudentID, s.UserID, u.Name, u.Surname, ss.Grade
        FROM `student subject` ss
        INNER JOIN `student` s ON ss.StudentID = s.StudentID
        INNER JOIN `user` u ON s.UserID = u.UserID
        WHERE ss.SubjectID = @SubjectID AND ss.ProfessorID = @ProfessorID;";

            var parameters = new Dictionary<string, object>
            {
                { "@SubjectID", subjectId },
                { "@ProfessorID", this.ProfessorId }
            };

            var students = new List<Student>();

            using (var reader = db.ExecuteQuery(query, parameters))
            {
                while (reader.Read())
                {
                    int studentId = reader.GetInt32("StudentID");
                    int userId = reader.GetInt32("UserID");
                    string name = reader["Name"].ToString();
                    string surname = reader["Surname"].ToString();
                    int grade = reader.IsDBNull(reader.GetOrdinal("Grade")) ? 0 : reader.GetInt32("Grade");

                    
                    var student = new Student(studentId, userId, this.AcademicSystemId, name, surname, "", "", "", "", DateTime.Now, 0, 0)
                    {
                        Grade = grade 
                    };

                    students.Add(student);
                }
            }

            return students;
        }

    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKADEMINE_Arnas_Globys_PI23B
{
    public class Subject
    {
        public int SubjectID { get; private set; }
        public int StudyProgramID { get; private set; }
        public string NameOfSubject { get; private set; }
        public int CreditAmount { get; private set; }
        public int Grade { get; private set; }

        public Subject(int subjectId, int studyProgramId, string nameOfSubject, int creditAmount, int grade)
        {
            SubjectID = subjectId;
            StudyProgramID = studyProgramId;
            NameOfSubject = nameOfSubject;
            CreditAmount = creditAmount;
            Grade = grade;
        }

        public static List<Subject> GetSubjectsByStudyProgramID(int studyProgramId, DBdetails db)
        {
            string query = @"
                SELECT SubjectID, StudyProgramID, NameOfSubject, creditAmount, grade
                FROM subject
                WHERE StudyProgramID = @StudyProgramID;";

            var parameters = new Dictionary<string, object>
            {
                { "@StudyProgramID", studyProgramId }
            };

            var subjects = new List<Subject>();

            using (var reader = db.ExecuteQuery(query, parameters))
            {
                while (reader.Read())
                {
                    int subjectId = reader.GetInt32("SubjectID");
                    int programId = reader.GetInt32("StudyProgramID");
                    string nameOfSubject = reader["NameOfSubject"].ToString();
                    int creditAmount = reader.GetInt32("creditAmount");
                    int grade = reader.IsDBNull(reader.GetOrdinal("grade")) ? 0 : reader.GetInt32("grade");

                    subjects.Add(new Subject(subjectId, programId, nameOfSubject, creditAmount, grade));
                }
            }

            return subjects;
        }
        public static List<Subject> GetSubjectsByProfessorId(int professorId, DBdetails db)
        {
            string query = @"
        SELECT DISTINCT s.SubjectID, s.NameOfSubject, s.CreditAmount
        FROM `subject` s
        INNER JOIN `student subject` ss ON s.SubjectID = ss.SubjectID
        WHERE ss.ProfessorID = @ProfessorID;";

            var parameters = new Dictionary<string, object>
            {
                { "@ProfessorID", professorId }
            };

            var subjects = new List<Subject>();

            using (var reader = db.ExecuteQuery(query, parameters))
            {
                while (reader.Read())
                {
                    int subjectId = reader.GetInt32("SubjectID");
                    string nameOfSubject = reader["NameOfSubject"].ToString();
                    int creditAmount = reader.GetInt32("CreditAmount");

                    subjects.Add(new Subject(subjectId, 0, nameOfSubject, creditAmount, 0));
                }
            }

            return subjects;
        }
    }
}

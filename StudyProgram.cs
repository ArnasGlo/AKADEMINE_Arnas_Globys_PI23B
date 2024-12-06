using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKADEMINE_Arnas_Globys_PI23B
{
    public class StudyProgram
    {
        public int StudyProgramID { get; private set; }
        public int AcademicSystemID { get; private set; }
        public string Studies { get; private set; }
        public int Semester { get; private set; }

        public StudyProgram(int studyProgramId, int academicSystemId, string studies, int semester)
        {
            StudyProgramID = studyProgramId;
            AcademicSystemID = academicSystemId;
            Studies = studies;
            Semester = semester;
        }

       
        public static StudyProgram GetById(int studyProgramId, DBdetails db)
        {
            string query = @"
                SELECT StudyProgramID, academicSystemID, studies, semester
                FROM `study program`
                WHERE StudyProgramID = @StudyProgramID;";

            var parameters = new Dictionary<string, object>
            {
                { "@StudyProgramID", studyProgramId }
            };

            using (var reader = db.ExecuteQuery(query, parameters))
            {
                if (reader.Read())
                {
                    int programId = reader.GetInt32("StudyProgramID");
                    int academicSystemId = reader.GetInt32("academicSystemID");
                    string studies = reader["studies"].ToString();
                    int semester = reader.GetInt32("semester");

                    return new StudyProgram(programId, academicSystemId, studies, semester);
                }
            }

            throw new Exception("Study program not found.");
        }

       
        public static List<StudyProgram> GetStudyProgramsForProfessor(int professorId, DBdetails db)
        {
            string query = @"
                SELECT sp.StudyProgramID, sp.academicSystemID, sp.studies, sp.semester
                FROM `study program` sp
                INNER JOIN `professor program` pp ON sp.StudyProgramID = pp.StudyProgramID
                WHERE pp.ProfessorID = @ProfessorID;";

            var parameters = new Dictionary<string, object>
            {
                { "@ProfessorID", professorId }
            };

            var programs = new List<StudyProgram>();

            using (var reader = db.ExecuteQuery(query, parameters))
            {
                while (reader.Read())
                {
                    int programId = reader.GetInt32("StudyProgramID");
                    int academicSystemId = reader.GetInt32("academicSystemID");
                    string studies = reader["studies"].ToString();
                    int semester = reader.GetInt32("semester");

                    programs.Add(new StudyProgram(programId, academicSystemId, studies, semester));
                }
            }

            return programs;
        }
    }
}


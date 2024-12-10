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

    }

}

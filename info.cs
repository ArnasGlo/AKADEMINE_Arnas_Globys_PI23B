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
    
    public partial class info : Form
    {
        private User loggedInUser;
        private DBdetails db;
        public info(User user)
        {
            InitializeComponent(); 
            loggedInUser = user;
            db = new DBdetails("localhost", "akademineis", "root", "");
            DisplayUserInfo();
        }

        private void DisplayUserInfo()
        {
            lblUserInfo.Text = $"{loggedInUser.GetType().Name} ID: {loggedInUser.UserId}, Created At: {loggedInUser.CreatedAt}";

            if (loggedInUser is Admin admin)
            {
                lblGroups.Text = "";
                lblStudyPrograms.Text = "";
            }
            else if (loggedInUser is Student student)
            {
                Group group = Group.GetGroupById(student.GroupId, db);
                lblGroups.Text = $"Group: {group.Title}, Created At: {group.CreatedAt}";
                
                StudyProgram program = StudyProgram.GetById(student.StudyProgramId, db);
                lblStudyPrograms.Text = $"Study Program: {program.Studies}, Semester: {program.Semester}";
            }
            else if (loggedInUser is Professor professor)
            {
                var groups = Professor.GetGroupsForProfessor(professor.ProfessorId, db);
                lblGroups.Text = "Groups:\n" + string.Join("\n", groups.Select(g => $"{g.Title}, Created At: {g.CreatedAt}"));

                var studyPrograms = Professor.GetStudyProgramsForProfessor(professor.ProfessorId, db);
                lblStudyPrograms.Text = "Study Programs:\n" + string.Join("\n", studyPrograms.Select(sp => $"{sp.Studies}, Semester: {sp.Semester}"));
                lblStudyPrograms.Location = new System.Drawing.Point(
                   lblGroups.Location.X,
                   lblGroups.Location.Y + lblGroups.Height + 20
                   );
            }
        }


    }
}


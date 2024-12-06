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

        public info(User user)
        {
            InitializeComponent(); 
            loggedInUser = user;
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
                Group group = Group.GetGroupById(student.GroupId, new DBdetails("localhost", "akademineis", "root", ""));
                lblGroups.Text = $"Group: {group.Title}, Created At: {group.CreatedAt}";

                StudyProgram program = StudyProgram.GetById(student.StudyProgramId, new DBdetails("localhost", "akademineis", "root", ""));
                lblStudyPrograms.Text = $"Study Program: {program.Studies}";
            }
            else if (loggedInUser is Professor professor)
            {
                List<Group> groups = Professor.GetGroupsForProfessor(professor.ProfessorId, new DBdetails("localhost", "akademineis", "root", ""));
                lblGroups.Text = "Groups:\n" + string.Join("\n", groups.Select(g => $"{g.Title}, Created At: {g.CreatedAt}"));

                List<StudyProgram> programs = Professor.GetStudyProgramsForProfessor(professor.ProfessorId, new DBdetails("localhost", "akademineis", "root", ""));
                lblStudyPrograms.Text = "Study Programs:\n" + string.Join("\n", programs.Select(p => p.Studies));

                    lblStudyPrograms.Location = new System.Drawing.Point(
                    lblGroups.Location.X,
                    lblGroups.Location.Y + lblGroups.Height + 20 
                    );
            }
        }


    }
}


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
    public partial class Form4 : Form
    {
        private User loggedInUser;

        public Form4(User user)
        {
            InitializeComponent();
            loggedInUser = user;

            if (loggedInUser is Student student)
            {
                DisplaySubjectsForStudent(student);
            }
            else if (loggedInUser is Professor professor)
            {
                DisplaySubjectsForProfessor(professor);
            }
            else
            {
                MessageBox.Show("Only students and professors can access this interface.");
                this.Close();
            }
        }

       
        private void DisplaySubjectsForStudent(Student student)
        {
            DBdetails db = new DBdetails("localhost", "akademineis", "root", "");
            int currentY = 20;

            try
            {
                
                StudyProgram program = StudyProgram.GetById(student.StudyProgramId, db);

                
                List<Subject> subjects = Subject.GetSubjectsByStudyProgramID(student.StudyProgramId, db);

                
                DisplayStudyProgramTable(program, subjects, ref currentY);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading student subjects: {ex.Message}");
            }
        }
        private void DisplaySubjectsForProfessor(Professor professor)
        {
            DBdetails db = new DBdetails("localhost", "akademineis", "root", "");

            try
            {
          
                List<Subject> subjects = Subject.GetSubjectsByProfessorId(professor.ProfessorId, db);

                cbSubjects.DataSource = subjects;
                cbSubjects.DisplayMember = "NameOfSubject";
                cbSubjects.ValueMember = "SubjectID";

                cbSubjects.Visible = true; 
                dgvStudentGrades.Visible = true; 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading professor subjects: {ex.Message}");
            }
        }

        private void CbSubjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSubjects.SelectedValue is int selectedSubjectId)
            {
                DBdetails db = new DBdetails("localhost", "akademineis", "root", "");

                try
                {
                    if (loggedInUser is Professor professor)
                    {
                       
                        List<Student> students = professor.GetStudentsForSubject(selectedSubjectId, db);

                       
                        dgvStudentGrades.DataSource = students.ConvertAll(s => new
                        {
                            Name = $"{s.Name} {s.Surname}",
                            Grade = s.Grade
                        });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading students for subject: {ex.Message}");
                }
            }
        }

       
        private void DisplayStudyProgramTable(StudyProgram program, List<Subject> subjects, ref int currentY)
        {
            Label lblStudyProgram = new Label
            {
                Text = $"{program.Studies}, {program.Semester} semester:",
                Font = new Font("Arial", 12, FontStyle.Bold),
                Location = new Point(20, currentY),
                AutoSize = true
            };
            this.Controls.Add(lblStudyProgram);
            currentY += 30;

            Label lblNameHeader = new Label { Text = "Name of Subject", Location = new Point(20, currentY), AutoSize = true };
            Label lblCreditHeader = new Label { Text = "Credit Amount", Location = new Point(220, currentY), AutoSize = true };
            Label lblGradeHeader = new Label { Text = "Grade", Location = new Point(420, currentY), AutoSize = true };
            this.Controls.Add(lblNameHeader);
            this.Controls.Add(lblCreditHeader);
            this.Controls.Add(lblGradeHeader);
            currentY += 20;

            foreach (var subject in subjects)
            {
                Label lblName = new Label { Text = subject.NameOfSubject, Location = new Point(20, currentY), AutoSize = true };
                Label lblCredit = new Label { Text = subject.CreditAmount.ToString(), Location = new Point(220, currentY), AutoSize = true };
                Label lblGrade = new Label { Text = subject.Grade == 0 ? "N/A" : subject.Grade.ToString(), Location = new Point(420, currentY), AutoSize = true };

                this.Controls.Add(lblName);
                this.Controls.Add(lblCredit);
                this.Controls.Add(lblGrade);
                currentY += 20;
            }
        }
    }
}

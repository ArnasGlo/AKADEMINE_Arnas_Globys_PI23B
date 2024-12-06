using System;
using System.Collections.Generic;
using System.Windows.Forms;
namespace AKADEMINE_Arnas_Globys_PI23B
{
    partial class Form4
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox cbSubjects; // Dropdown for professors
        private System.Windows.Forms.DataGridView dgvStudentGrades; // Table for student grades

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.cbSubjects = new System.Windows.Forms.ComboBox();
            this.dgvStudentGrades = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudentGrades)).BeginInit();
            this.SuspendLayout();
            // 
            // cbSubjects
            // 
            this.cbSubjects.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSubjects.FormattingEnabled = true;
            this.cbSubjects.Location = new System.Drawing.Point(20, 20);
            this.cbSubjects.Name = "cbSubjects";
            this.cbSubjects.Size = new System.Drawing.Size(300, 21);
            this.cbSubjects.TabIndex = 0;
            this.cbSubjects.Visible = false; // Initially hidden for students
            this.cbSubjects.SelectedIndexChanged += new System.EventHandler(this.CbSubjects_SelectedIndexChanged);
            // 
            // dgvStudentGrades
            // 
            this.dgvStudentGrades.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStudentGrades.Location = new System.Drawing.Point(20, 60);
            this.dgvStudentGrades.Name = "dgvStudentGrades";
            this.dgvStudentGrades.Size = new System.Drawing.Size(500, 300);
            this.dgvStudentGrades.TabIndex = 1;
            this.dgvStudentGrades.Visible = false; // Initially hidden for students
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.cbSubjects);
            this.Controls.Add(this.dgvStudentGrades);
            this.Name = "Form4";
            this.Text = "Grades of Subjects";
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudentGrades)).EndInit();
            this.ResumeLayout(false);
        }
    }

}
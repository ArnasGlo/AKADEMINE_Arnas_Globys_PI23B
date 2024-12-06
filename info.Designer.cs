using System;
using System.Windows.Forms;
namespace AKADEMINE_Arnas_Globys_PI23B
   
{
    partial class info
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Declare the controls
        private System.Windows.Forms.Label lblUserInfo;
        private System.Windows.Forms.Label lblGroups;
        private System.Windows.Forms.Label lblStudyPrograms;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.lblUserInfo = new System.Windows.Forms.Label();
            this.lblGroups = new System.Windows.Forms.Label();
            this.lblStudyPrograms = new System.Windows.Forms.Label();
            this.SuspendLayout();
          
            this.lblUserInfo.AutoSize = true;
            this.lblUserInfo.Location = new System.Drawing.Point(12, 20);
            this.lblUserInfo.Name = "lblUserInfo";
            this.lblUserInfo.Size = new System.Drawing.Size(0, 13);
            this.lblUserInfo.TabIndex = 0;
          
            this.lblGroups.AutoSize = true;
            this.lblGroups.Location = new System.Drawing.Point(12, 50);
            this.lblGroups.Name = "lblGroups";
            this.lblGroups.Size = new System.Drawing.Size(0, 13);
            this.lblGroups.TabIndex = 1;
           
            this.lblStudyPrograms.AutoSize = true;
            this.lblStudyPrograms.Location = new System.Drawing.Point(12, 80);
            this.lblStudyPrograms.Name = "lblStudyPrograms";
            this.lblStudyPrograms.Size = new System.Drawing.Size(0, 13);
            this.lblStudyPrograms.TabIndex = 2;
           
            this.ClientSize = new System.Drawing.Size(600, 300);
            this.Controls.Add(this.lblStudyPrograms);
            this.Controls.Add(this.lblGroups);
            this.Controls.Add(this.lblUserInfo);
            this.Name = "Form2";
            this.Text = "User Information";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
    }
}
namespace AKADEMINE_Arnas_Globys_PI23B
{
    partial class menu
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnUserInfo;
        private System.Windows.Forms.Button btnGrades;

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
            this.btnGrades = new System.Windows.Forms.Button();
            this.btnUserInfo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            
            this.btnGrades.Location = new System.Drawing.Point(100, 180);
            this.btnGrades.Name = "btnGrades";
            this.btnGrades.Size = new System.Drawing.Size(200, 50);
            this.btnGrades.TabIndex = 1;
            this.btnGrades.Text = "Grades of Subject";
            this.btnGrades.UseVisualStyleBackColor = true;
            this.btnGrades.Click += new System.EventHandler(this.BtnGrades_Click); 

            this.btnUserInfo.Location = new System.Drawing.Point(100, 100); 
            this.btnUserInfo.Name = "btnUserInfo";
            this.btnUserInfo.Size = new System.Drawing.Size(200, 50);
            this.btnUserInfo.TabIndex = 0;
            this.btnUserInfo.Text = "Information of User";
            this.btnUserInfo.UseVisualStyleBackColor = true;
            this.btnUserInfo.Click += new System.EventHandler(this.BtnUserInfo_Click);
            
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Controls.Add(this.btnGrades);
            this.Controls.Add(this.btnUserInfo);
            this.Name = "menu";
            this.Text = "Menu";
            this.ResumeLayout(false);
        }
    }
}
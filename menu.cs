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
    public partial class menu : Form
    {
        private User loggedInUser;

        public menu(User user)
        {
            InitializeComponent();
            loggedInUser = user; 
        }
        private void BtnGrades_Click(object sender, EventArgs e)
        {
           
            Form4 form4 = new Form4(loggedInUser);
            form4.Show();
        }
        private void BtnUserInfo_Click(object sender, EventArgs e)
        {
            
            info form2 = new info(loggedInUser);
            form2.Show();
        }
    }
}

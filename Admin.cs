using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKADEMINE_Arnas_Globys_PI23B
{
    public class Admin : User
    {
        public int AdminId { get; private set; }

        public Admin(int adminId, int userId, int academicSystemId, string name, string surname, string email, string phoneNumber, string username, string password, DateTime createdAt)
            : base(userId, academicSystemId, name, surname, email, phoneNumber, username, password, createdAt)
        {
            AdminId = adminId;
        }

    }
}


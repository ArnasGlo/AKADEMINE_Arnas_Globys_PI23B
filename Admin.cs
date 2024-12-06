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

        
        public static Admin GetAdminByUserId(int userId, DBdetails db)
        {
            string query = "SELECT * FROM admin WHERE UserID = @UserId;";
            var parameters = new Dictionary<string, object> { { "@UserId", userId } };

            using (var reader = db.ExecuteQuery(query, parameters))
            {
                if (reader.Read())
                {
                    return new Admin(
                        reader.GetInt32("AdminID"),
                        reader.GetInt32("UserID"),
                        1, 
                        reader.GetString("Name"),
                        reader.GetString("Surname"),
                        reader.GetString("Email"),
                        reader.GetString("PhoneNumber"),
                        reader.GetString("Username"),
                        reader.GetString("Password"),
                        DateTime.MinValue
                    );
                }
            }

            throw new Exception("Admin not found.");
        }
    }
}


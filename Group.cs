using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKADEMINE_Arnas_Globys_PI23B
{
    public class Group
    {
        public int GroupId { get; private set; }
        public string Title { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Group(int groupId, string title, DateTime createdAt)
        {
            GroupId = groupId;
            Title = title;
            CreatedAt = createdAt;
        }

        public static Group GetGroupById(int groupId, DBdetails db)
        {
            string query = "SELECT * FROM `group` WHERE GroupID = @GroupId;";
            var parameters = new Dictionary<string, object> { { "@GroupId", groupId } };

            using (var reader = db.ExecuteQuery(query, parameters))
            {
                if (reader.Read())
                {
                    return new Group(
                        reader.GetInt32("GroupID"),
                        reader.GetString("title"),
                        reader.GetDateTime("CreatedAt")
                    );
                }
            }

            throw new Exception("Group not found.");
        }
    }
}

using System.Data.SQLite;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Scruffy
{
    [TestClass]
    public class ScruffyFullFrameworkTests
    {
        private class UserDto
        {
            public string Name { get; set; }
            public string Age { get; set; }
        }

        [TestMethod]
        public void SimpleQuery()
        {
            using (var con = new SQLiteConnection("data source=:memory:"))
            {
                con.Open();

                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS Users (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT, Age INTEGER)";
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Users (Name, Age) VALUES ('Arthur Dent', 42);";
                    cmd.ExecuteNonQuery();
                }
                
                const string sql = "SELECT Name, Age FROM Users";
                var result = con
                    .Query<UserDto>(sql) // Making the assumption that ID is 1
                    .Single();

                Assert.AreEqual("Arthur Dent", result.Name);
                Assert.AreEqual("42", result.Age);
            }
        }
    }
}

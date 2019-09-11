using System.Data;
using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ScruffyNetCore
{
    [TestClass]
    public class ScruffyNetCoreTests
    {
        private class UserDto
        {
            public string Name { get; set; }
            public string Age { get; set; }
        }

        [TestMethod]
        public void SimpleQuery()
        {
            using (var factory = new TestDbFactory())
            using (var db = factory.CreateContext())
            {
                var arthur = new User { Name = "Arthur Dent", Age = 42 };
                db.Users.Add(arthur);
                db.SaveChanges();
                Assert.AreEqual(1, db.Users.Count());

                const string sql = "SELECT Name, Age FROM Users WHERE Id=@id";
                var result = db.Database
                    .Query<UserDto>(sql, new SqliteParameter("@id", SqlDbType.Int) {Value = arthur.Id})
                    .Single();

                Assert.AreEqual(arthur.Name, result.Name);
                Assert.AreEqual(arthur.Age.ToString(), result.Age);
            }
        }
    }
}
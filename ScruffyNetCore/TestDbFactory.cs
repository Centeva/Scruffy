using System;
using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace ScruffyNetCore
{
    public class TestDbFactory : IDisposable
    {
        private DbConnection _connection;

        private DbContextOptions<MyDb> CreateOptions()
        {
            return new DbContextOptionsBuilder<MyDb>()
                .UseSqlite(_connection).Options;
        }

        public MyDb CreateContext()
        {
            if (_connection == null)
            {
                _connection = new SqliteConnection("DataSource=:memory:");
                _connection.Open();

                var options = CreateOptions();
                using (var context = new MyDb(options))
                {
                    context.Database.EnsureCreated();
                }
            }

            return new MyDb(CreateOptions());
        }

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }
    }
}
# Scruffy

Extremely simple tool to map results of a ADO query to an object.

The hardest part of using the low-level ADO.Net IDataReader is handling the
type casting and null semantics. `Scruffy` handles all of the messy bits
which lets you quickly and easily create custom SQL queries without worrying
about the low-level type mapping details.

The design of `Scruffy` was inspired by the excellent open-source project
`Dapper` (https://github.com/StackExchange/Dapper). I created `Scruffy`
because I couldn't use `Dapper` but really needed a simple way to map query
results to objects. `Scruffy` will never have all the features of `Dapper`,
and that's OK. If you need that feature set, then please use `Dapper`!

## Usage

`Scruffy` is simply a set of extension methods that take a SQL query and return
a list of objects.

There are two implementations to choose from:

1. ADO.Net
2. EF Core

They all have very similar APIs and the usage will be nearly identical.

The easiest thing to do is to simply copy the relevant `Scruffy` class into your
project and use it directly.

## ADO.Net

The ADO.Net implementation extends the `SqlConnection` object.

> Note: The SQL connection that is used for the query must be opened before calling `Query`.

```csharp

public class MyClass {
    public string Name { get; set; }
    public int Age { get; set; }
}

const string _connectionString = "...";

public MyClass ReadData(MyDbContext db, int id) {
    string sql = "SELECT Name, Age FROM MyTable WHERE ID = @id";

    using (var connection = new SqlConnection(_connectionString)){
        connection.Open();

        MyClass result = db.Database.Query<MyClass>(sql, new SqlParameter("@id", SqlDbType.Int) {Value = id});
        return result;
    }
}
```

## EF Core

The EF Core implementation extends the `DatabaseFacade` that is provided by the
`Database` property on your context.

```csharp

public class MyClass {
    public string Name { get; set; }
    public int Age { get; set; }
}

public MyClass ReadData(MyDbContext db, int id) {
    string sql = "SELECT Name, Age FROM MyTable WHERE ID = @id";

    MyClass result = db.Database.Query<MyClass>(sql, new SqlParameter("@id", SqlDbType.Int) {Value = id});
    return result;
}

```

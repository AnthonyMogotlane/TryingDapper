using Dapper;
using Microsoft.Data.Sqlite;
using SqliteUsingDapper;

string cs = "Data source=./gatsbies.db";
using SqliteConnection con = new SqliteConnection(cs);
con.Open();

// Creating a table
con.Execute(@"CREATE TABLE if not exists gatsby (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    type TEXT NOT NULL,
    size TEXT NOT NULL,
    price REAL NOT NULL
);");

// Inserting data into the table
con.Execute(@"
    INSERT INTO gatsby (type, size, price)
    VALUES (@Type, @Size, @Price);", 
    new object[] {
        new Gatsby {
            Type = "Chicken",
            Size = "Full",
            Price = 95
        },
        new Gatsby {
            Type = "Russian",
            Size = "Half",
            Price = 30
        },
        new Gatsby {
            Type = "Polony",
            Size = "Full",
            Price = 50
        }
    }
);

var gatsbys = con.Query<Gatsby>(@"SELECT * FROM gatsby");
Console.WriteLine(gatsbys.Count()); // => Output number of rows/inserted data into the table

var totalPerSize = con.Query<GatsbysGrouped>(@"SELECT size AS grouping, SUM(price) AS total FROM gatsby GROUP BY size");

foreach (var item in totalPerSize)
{
    Console.WriteLine($"size: {item.Grouping} - total {item.Total}");
}






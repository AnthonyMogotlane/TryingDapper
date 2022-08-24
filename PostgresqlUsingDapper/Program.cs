using Dapper;
using Npgsql;
using PostgresqlUsingDapper;

string cs = "Server=heffalump.db.elephantsql.com;Port=5432;Database=xbixatua;UserId=xbixatua;Password=MZpFuYnavsnJw65QqMIG9JtHM29yqMz6";

using NpgsqlConnection con = new NpgsqlConnection(cs);
con.Open();

// Creating a table
con.Execute(@"CREATE TABLE if NOT EXISTS members (
	id serial PRIMARY KEY,
	firstname VARCHAR(45) NOT NULL,
	lastname VARCHAR(45) NOT NULL,
	location VARCHAR(45) NOT NULL
);");

// Inserting data into the table
con.Execute(@"
	INSERT INTO 
		members (firstname, lastname, location)
	VALUES (@FirstName, @LastName, @Location);",
	new Object [] {
		new Members() {
			FirstName = "Joel",
			LastName = "Mog",
			Location = "Joburg"
		},
		new Members() {
			FirstName = "Thando",
			LastName = "Thabethe",
			Location = "Zinyola"
		},
		new Members() {
			FirstName = "John",
			LastName = "Kay",
			Location = "New York"
		}
});

// Returning a list of object in the table
var members = con.Query<Members>(@"SELECT * FROM members");
Console.WriteLine(members.Count());

// Outputing data from the table
Console.WriteLine($"ID| Firstname | Lastname | Location");
foreach (var item in members)
{
	Console.WriteLine($"-----------------------------------");
	Console.WriteLine($"{item.ID} | {item.FirstName} | {item.LastName} | {item.Location}");
}




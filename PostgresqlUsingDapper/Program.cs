using Dapper;
using Npgsql;
using PostgresqlUsingDapper;

string connectionString = "Server=heffalump.db.elephantsql.com;Port=5432;Database=xbixatua;UserId=xbixatua;Password=MZpFuYnavsnJw65QqMIG9JtHM29yqMz6";
using NpgsqlConnection connection = new NpgsqlConnection(connectionString);

connection.Open();

string MEMBERS = @"CREATE TABLE if NOT EXISTS members (
	id serial PRIMARY KEY,
	first_name VARCHAR(45) NOT NULL,
	last_name VARCHAR(45) NOT NULL,
    location VARCHAR(45) NOT NULL
);" ;

connection.Execute(MEMBERS);

connection.Execute(@"
	INSERT INTO 
		members (first_name, last_name, location)
	VALUES 
		(@FistName, @LastName, @Location);",
		new Members() {
		FistName = "Rock",
		LastName = "Dudula",
		Location = "Mthatha"
});

var members = connection.Query<Members>(@"select * from members");
Console.WriteLine(members.Count());

using (var cmd = new NpgsqlCommand("SELECT * FROM members", connection))
using (var reader = cmd.ExecuteReader())
{
    while (reader.Read())
    {
        for(int i = 0; i < reader.FieldCount; i++)
        {
            Console.WriteLine(reader.GetValue(i));
        }
        Console.WriteLine();
    }
}

using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TCSS.Console.Flashcards.Models;

public class DataAccess
{
    IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

    private string ConnectionString;

    public DataAccess()
    {
        ConnectionString = configuration.GetSection("ConnectionStrings")["DefaultConnection"];
    }
    internal void CreateTables()
    {
        try
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string createStackTableSql =
                    @"IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Stacks')
                    CREATE TABLE Stacks (
                        Id int IDENTITY(1,1) NOT NULL,
                        Name NVARCHAR(30) NOT NULL UNIQUE,
                        PRIMARY KEY (Id)
                    );";
                conn.Execute(createStackTableSql);

                string createFlashcardTableSql =
                    @"IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Flashcards')
                    CREATE TABLE Flashcards (
                        Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
                        Question NVARCHAR(30) NOT NULL,
                        Answer NVARCHAR(30) NOT NULL,
                        StackId int NOT NULL 
                            FOREIGN KEY 
                            REFERENCES Stacks(Id) 
                            ON DELETE CASCADE 
                            ON UPDATE CASCADE
                    );";
                conn.Execute(createFlashcardTableSql);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"There was a problem creating the tables: {ex.Message}");
        }
    }

    internal void InsertStack(Stack stack)
    {
        try
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string insertQuery = @"
            INSERT INTO Stacks (Name) VALUES (@Name)";

                connection.Execute(insertQuery, new { stack.Name });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"There was a problem inserting the stack: {ex.Message}");
        }
    }

    internal IEnumerable<Stack> GetAllStacks()
    {
        try
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string selectQuery = "SELECT * FROM stacks";

                var records = connection.Query<Stack>(selectQuery);

                return records;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"There was a problem retrieving stacks: {ex.Message}");
            return new List<Stack>();
        }
    }

    internal void InsertFlashcard(Flashcard flashcard)
    {
        try
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string insertQuery = @"
            INSERT INTO Flashcards (Question, Answer, StackId) VALUES (@Question, @Answer, @StackId)";

                connection.Execute(insertQuery, new { flashcard.Question, flashcard.Answer, flashcard.StackId });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"There was a problem inserting the flashcard: {ex.Message}");
        }
    }

    internal void BulkInsertRecords(List<Stack> stacks, List<Flashcard> flashcards)
    {
        SqlTransaction transaction = null; // Declare the transaction variable outside the try block
        try
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                transaction = connection.BeginTransaction();

                connection.Execute("INSERT INTO Stacks (Name) VALUES (@Name)", stacks, transaction: transaction);
                connection.Execute("INSERT INTO Flashcards (Question, Answer, StackId) VALUES (@Question, @Answer, @StackId)", flashcards, transaction: transaction);

                transaction.Commit(); // Commit the transaction if everything is successful
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"There was a problem bulk inserting records: {ex.Message}");

            if (transaction != null)
            {
                transaction.Rollback(); // Rollback the transaction if an exception occurs
            }
        }
    }

    internal void DeleteTables()
    {
        try
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string dropFlashcardsTableSql = @"DROP TABLE Flashcards";
                connection.Execute(dropFlashcardsTableSql);

                string dropStacksTableSql = @"DROP TABLE Stacks";
                connection.Execute(dropStacksTableSql);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"There was a problem deleting tables: {ex.Message}");
        }
    }
}

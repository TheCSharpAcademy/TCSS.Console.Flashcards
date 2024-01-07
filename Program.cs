using TCSS.Console.Flashcards;

var DataAccess = new DataAccess();
DataAccess.CreateTables();
SeedData.SeedRecords();
UserInterface.MainMenu();
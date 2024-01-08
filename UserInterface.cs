using Spectre.Console;
using TCSS.Console.Flashcards.Models;
using static TCSS.Console.Flashcards.Enums;

namespace TCSS.Console.Flashcards;

internal class UserInterface
{
    internal static void MainMenu()
    {
        var isMenuRunning = true;

        while (isMenuRunning)
        {
            var usersChoice = AnsiConsole.Prompt(
                   new SelectionPrompt<MainMenuChoices>()
                    .Title("What would you like to do?")
                    .AddChoices(
                       MainMenuChoices.ManageStacks,
                       MainMenuChoices.ManageFlashcards,
                       MainMenuChoices.Quit)
                    );

            switch (usersChoice)
            {
                case MainMenuChoices.ManageStacks:
                    StacksMenu();
                    break;
                case MainMenuChoices.ManageFlashcards:
                    FlashcardsMenu();
                    break;
                case MainMenuChoices.Quit:
                    System.Console.WriteLine("Goodbye");
                    isMenuRunning = false;
                    break;
            }
        }
    }

    internal static void StacksMenu()
    {
        var isMenuRunning = true;

        while (isMenuRunning)
        {
            var usersChoice = AnsiConsole.Prompt(
                   new SelectionPrompt<StackChoices>()
                    .Title("What would you like to do?")
                    .AddChoices(
                       StackChoices.ViewStacks,
                       StackChoices.AddStack,
                       StackChoices.UpdateStack,
                       StackChoices.DeleteStack,
                       StackChoices.ReturnToMainMenu)
                    );

            switch (usersChoice)
            {
                case StackChoices.ViewStacks:
                    ViewStacks();
                    break;
                case StackChoices.AddStack:
                    AddStack();
                    break;
                case StackChoices.DeleteStack:
                    DeleteStack();
                    break;
                case StackChoices.UpdateStack:
                    UpdateStack();
                    break;
                case StackChoices.ReturnToMainMenu:
                    isMenuRunning = false;
                    break;
            }
        }
    }

    private static void UpdateStack()
    {
        throw new NotImplementedException();
    }

    private static void DeleteStack()
    {
        var id = ChooseStack("Choose stack to delete");

        if (!AnsiConsole.Confirm("Are you sure?"))
            return;

        var dataAccess = new DataAccess();
        dataAccess.DeleteStack(id);
    }

    private static void AddStack()
    {
        Stack stack = new();

        stack.Name = AnsiConsole.Ask<string>("Insert Stack's Name.");

        while (string.IsNullOrEmpty(stack.Name))
        {
            stack.Name = AnsiConsole.Ask<string>("Stack's name can't be empty. Try again.");
        }

        var dataAccess = new DataAccess();
        dataAccess.InsertStack(stack);
    }

    private static void ViewStacks()
    {
        throw new NotImplementedException();
    }

    private static int ChooseStack(string message)
    {
        var dataAccess = new DataAccess();
        var stacks = dataAccess.GetAllStacks();

        var stacksArray = stacks.Select(x => x.Name).ToArray();
        var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title(message)
            .AddChoices(stacksArray));

        var stackId = stacks.Single(x => x.Name == option).Id;

        return stackId;
    }

    private static int ChooseFlashcard(string message, int stackId)
    {
        var dataAccess = new DataAccess();
        var flashcards = dataAccess.GetFlashcards(stackId);

        var flashcardsArray = flashcards.Select(x => x.Question).ToArray();
        var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title(message)
            .AddChoices(flashcardsArray));

        var flashcardId = flashcards.Single(x => x.Question == option).Id;

        return flashcardId;
    }

    internal static void FlashcardsMenu()
    {
        var isMenuRunning = true;

        while (isMenuRunning)
        {
            var usersChoice = AnsiConsole.Prompt(
                   new SelectionPrompt<FlashcardChoices>()
                    .Title("What would you like to do?")
                    .AddChoices(
                       FlashcardChoices.ViewFlashcards,
                       FlashcardChoices.AddFlashcard,
                       FlashcardChoices.UpdateFlashcard,
                       FlashcardChoices.DeleteFlashcard,
                       FlashcardChoices.ReturnToMainMenu)
                    );

            switch (usersChoice)
            {
                case FlashcardChoices.ViewFlashcards:
                    ViewFlashcards();
                    break;
                case FlashcardChoices.AddFlashcard:
                    AddFlashcard();
                    break;
                case FlashcardChoices.DeleteFlashcard:
                    DeleteFlashcard();
                    break;
                case FlashcardChoices.UpdateFlashcard:
                    UpdateFlashcard();
                    break;
                case FlashcardChoices.ReturnToMainMenu:
                    isMenuRunning = false;
                    break;
            }
        }
    }

    private static void UpdateFlashcard()
    {
        throw new NotImplementedException();
    }

    private static void DeleteFlashcard()
    {
        var stackId = ChooseStack("Where's the flashcard you want to delete?");
        var flashcard = ChooseFlashcard("Choose flashcard to delete", stackId);

        if (!AnsiConsole.Confirm("Are you sure?"))
            return;

        var dataAccess = new DataAccess();
        dataAccess.DeleteFlashcard(flashcard);
    }

    private static void AddFlashcard()
    {
        Flashcard flashcard = new();

        flashcard.StackId = ChooseStack("Choose a stack for the new flashcard");
        flashcard.Question = AnsiConsole.Ask<string>("Insert Question.");

        while (string.IsNullOrEmpty(flashcard.Question))
        {
            flashcard.Question = AnsiConsole.Ask<string>("Question can't be empty. Try again.");
        }

        flashcard.Answer = AnsiConsole.Ask<string>("Insert Answer.");

        while (string.IsNullOrEmpty(flashcard.Answer))
        {
            flashcard.Answer = AnsiConsole.Ask<string>("Answer can't be empty. Try again.");
        }

        var dataAccess = new DataAccess();
        dataAccess.InsertFlashcard(flashcard);
    }

    private static void ViewFlashcards()
    {
        throw new NotImplementedException();
    }
}

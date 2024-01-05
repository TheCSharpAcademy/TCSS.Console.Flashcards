using Spectre.Console;
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
        throw new NotImplementedException();
    }

    private static void AddStack()
    {
        throw new NotImplementedException();
    }

    private static void ViewStacks()
    {
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }

    private static void AddFlashcard()
    {
        throw new NotImplementedException();
    }

    private static void ViewFlashcards()
    {
        throw new NotImplementedException();
    }
}

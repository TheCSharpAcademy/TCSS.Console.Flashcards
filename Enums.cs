﻿using System.ComponentModel.DataAnnotations;

namespace TCSS.Console.Flashcards;

internal class Enums
{
    internal enum MainMenuChoices
    {
        ManageStacks,
        ManageFlashcards,
        StudyArea,
        Quit
    }

    internal enum StackChoices
    {
        ViewStacks,
        AddStack,
        DeleteStack,
        UpdateStack,
        ReturnToMainMenu
    }

    internal enum FlashcardChoices
    {
        ViewFlashcards,
        AddFlashcard,
        DeleteFlashcard,
        UpdateFlashcard,
        ReturnToMainMenu
    }
}
using System;
using System.Collections.Generic;

public class Translator
{
    public static void Run()
    {
        var englishToGerman = new Translator();

        // Build dictionary
        englishToGerman.AddWord("House", "Haus");
        englishToGerman.AddWord("Car", "Auto");
        englishToGerman.AddWord("Plane", "Flugzeug");

        // Test translations
        Console.WriteLine(englishToGerman.Translate("Car"));   // Auto
        Console.WriteLine(englishToGerman.Translate("Plane")); // Flugzeug
        Console.WriteLine(englishToGerman.Translate("Train")); // ???
    }

    private Dictionary<string, string> _words = new Dictionary<string, string>();

    /// <summary>
    /// Add the translation from 'fromWord' to 'toWord'
    /// </summary>
    public void AddWord(string fromWord, string toWord)
    {
        // Add or update the word in the dictionary
        _words[fromWord] = toWord;
    }

    /// <summary>
    /// Translate a word or return "???" if not found
    /// </summary>
    public string Translate(string fromWord)
    {
        if (_words.TryGetValue(fromWord, out string translation))
        {
            return translation;
        }

        return "???";
    }
}
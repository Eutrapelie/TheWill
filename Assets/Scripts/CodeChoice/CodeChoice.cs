
using UnityEngine;
using System;
using System.Collections.Generic;

[System.Serializable]
public class CodeChoice
{
    public Genre genre;
    public Character character;
    public string line;

    public CodeChoice(Genre g, Character c, string l)
    {
        genre = g;
        character = c;
        line = l;
    }
}

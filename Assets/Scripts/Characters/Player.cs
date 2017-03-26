using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    public string playerName;

    //public QuestLog questLog;
    public List<CodeLine> codeLines;

    public List<int> inventory;

    public Genre genre;

    public Player(string player, List<CodeLine> cL, List<int> i, Genre g)
    {
        playerName = player;
        codeLines = cL;
        inventory = new List<int>();
        inventory = i;
        genre = g;
    }
}

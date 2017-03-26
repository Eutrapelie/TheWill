using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestLog
{
    // TODO : probably need to hash this, cuz searching in string is ugly :|
    public List<CodeChoice> choices;

    public QuestLog()
    {
        choices = new List<CodeChoice>();
    }

    public QuestLog(List<CodeChoice> lC)
    {
        choices = lC;
    }

    public bool ComposeAndSaveChoice(CodeChoice newCode)
    {
        if (!choices.Contains(newCode))
        {
            choices.Add(newCode);
            return true;
        }

        Debug.Log("[QuestLog] Save failed : Choice already taken");
        return false;
    }

    public bool HasChoiceTaken(CodeChoice code)
    {
        if (choices.Contains(code))
        {
            return true;
        }
        return false;
    }    
}

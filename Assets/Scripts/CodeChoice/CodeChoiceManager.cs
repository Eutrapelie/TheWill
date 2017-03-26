using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CodeChoiceManager", menuName = "InGameManager/CodeChoiceManager", order = 4)]
public class CodeChoiceManager : ScriptableObject
{
    [SerializeField]
    private List<CodeChoice> _codeChoices;

    public List<CodeChoice> CodeChoices
    {
        get
        {
            return _codeChoices;
        }

        set
        {
            _codeChoices = value;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CodeChoiceController : MonoBehaviour
{
    public CodeChoiceManager _codeChoiceManager;

    public List<string> _codeChoice;

    public void OnEnable()
    {
        if(_codeChoiceManager != null)
        {
            _codeChoice = new List<string>();
            foreach (CodeChoice item in _codeChoiceManager.CodeChoices)
            {
                string code = item.genre.ToString() + item.character.ToString() + item.line;
                _codeChoice.Add(code);
            }
        }
    }
    
	
}

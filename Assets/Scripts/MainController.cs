using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using NodeCanvas.BehaviourTrees;
using NodeCanvas.DialogueTrees;

public class MainController : MonoBehaviour
{
    [SerializeField]
    private Room _room;

    [SerializeField]
    private GameObject _charactersGameobject;

    [SerializeField]
    private Blackboard _blackBoard;

    [SerializeField]
    private BehaviourTreeOwner _behaviourTree;

    //[SerializeField]
    //private bool _cheatMode;

    //[SerializeField]
    //private CodeLine _cheatCodeLine;
    
    private List<GameObject> _charactersInScene;

    void Awake()
    {
        //Game.Current.Test();
        LoadBehaviourTree();
    }

    void Start()
    {
        // TODO
        // Load progression... way before this start though.
        // find level Controller corresponding to this level
        // END TODO

        _charactersInScene = new List<GameObject>();

        LoadCharactersInvolved();
        LoadBlackBoard();
        LoadPlayerInfo();
    }

    private void LoadBehaviourTree()
    {
        string level = _room.ToString() + "Act" + Game.Current.acteNumber.ToString() + "Day" + Game.Current.dayNumber.ToString();
        Debug.Log("[MainController] Loading level : " + level);
        BehaviourTree tree = ((BehaviourTree) Resources.Load("Tree/" + level));
        if(tree == null)
        {
            Debug.Log("[MainController] Resources load failed");
        }
        else
        {
            Debug.Log("[MainController] Resources load succeed");
        }
        _behaviourTree.behaviour = tree;
        GameManager.Instance.ActeNumber = Game.Current.acteNumber;
        GameManager.Instance.DayNumber = Game.Current.dayNumber;
    }

    private void LoadPlayerInfo()
    {
        GameManager.Instance.PlayerController.PlayerChoices = Game.Current.player.codeLines;
        if (GameManager.Instance.PlayerController.PlayerCard != null)
        {
            GameManager.Instance.PlayerController.PlayerCard.Player = Game.Current.player;
        }
    }

    private void LoadBlackBoard()
    {
        foreach (GameObject go in _charactersInScene)
        {
            BoxCollider2D collider = go.GetComponent<BoxCollider2D>();
            CharacterCard characterCard = go.GetComponent<CharacterCard>();
            if(collider)
            {
                Variable found = _blackBoard.GetVariable(characterCard.CharacterInfo.characterName + "Collider");
                if (found != null)
                {
                    found.value = collider;
                }
            }

            DialogueActor dialog = go.GetComponent<DialogueActor>();
            if(dialog)
            {
                //_blackBoard.GetVariable<DialogueActor>(go.name + "Dialog").SetValue(dialog);
            }
        }
    }

    public void LoadCharactersInvolved()
    {            
        foreach (CharacterInfo characterInfo in Game.Current.charactersInfos)
        {
            if (characterInfo.currentRoom == _room)
            {
                GameObject characterObject = ((GameObject)Instantiate(Resources.Load("Characters/" + characterInfo.characterName)));
                if (characterObject)
                {
                    _charactersInScene.Add(characterObject);

                    CharacterCard characterCard = characterObject.GetComponent<CharacterCard>();
                    characterCard.CharacterInfo = characterInfo;

                    GameManager.Instance.MyCharacterController.Characters.Add(characterCard);
                    GameManager.Instance.MyCharacterController.AddCharacterCardToGlobalBB(characterCard);

                    characterObject.transform.parent = _charactersGameobject.transform;
                    characterObject.transform.localScale = new Vector3(1f, 1f);
                }
            }
        }        
    }
}

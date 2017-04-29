﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using NodeCanvas.BehaviourTrees;
using NodeCanvas.DialogueTrees;

public class MainController : MonoBehaviour
{
    [SerializeField]
    private int _acteNumber = 1;

    [SerializeField]
    private int _dayNumber = 1;

    [SerializeField]
    private Room _room;

    [SerializeField]
    private List<RoomSpotView> _characterSpotsGameObject;

    [SerializeField]
    private Blackboard _blackBoard;

    [SerializeField]
    private BehaviourTreeOwner _behaviourTree;
    
    
    private List<GameObject> _charactersInScene;

    void Awake()
    {
        Game.Current.LoadStartLevel(_acteNumber, _dayNumber);
        LoadBehaviourTree();
    }

    void Start()
    {
        // TODO
        // Load progression... way before this start though.
        // find level Controller corresponding to this level
        // END TODO

        _charactersInScene = new List<GameObject>();

        LoadCharactersObjectInvolved();
        LoadBlackBoard();
    }

    private void LoadBehaviourTree()
    {
        string level = _room.ToString() + "Act" + Game.Current.acteNumber.ToString() + "Day" + Game.Current.dayNumber.ToString();
        Debug.Log("[MainController] Loading level : " + level);
        BehaviourTree tree = ((BehaviourTree) Resources.Load("Tree/SallesBehavior/" + level));
        if(tree == null)
        {
            Debug.Log("[MainController] Resources load failed");
        }
        else
        {
            Debug.Log("[MainController] Resources load succeed");
        }
        _behaviourTree.behaviour = tree;
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

    public void LoadCharactersObjectInvolved()
    {
        if(_characterSpotsGameObject.Count < Game.Current.charactersInfos.Count)
        {
            Debug.LogError("[MainController] There's more characters than spots on the scene.");
            return;
        }

        GameManager.Instance.MyCharacterController.Characters.Clear();
        foreach (CharacterInfo info in Game.Current.charactersInfos)
        {
            if (info.currentRoom == _room)
            {
                GameObject characterObject = ((GameObject)Instantiate(Resources.Load("Characters/" + info.characterName)));
                if (characterObject)
                {
                    _charactersInScene.Add(characterObject);
                    CharacterCard characterCard = characterObject.GetComponent<CharacterCard>();
                    characterCard.CharacterInfo = info;

                    GameManager.Instance.MyCharacterController.Characters.Add(characterCard);
                    GameManager.Instance.MyCharacterController.AddCharacterCardToGlobalBB(characterCard);

                    RoomSpotView newSpot = GetRoomSpotForCharacter(info);
                    if (newSpot)
                    {
                        characterObject.transform.parent = newSpot.gameObject.transform;
                        characterObject.transform.localPosition = Vector3.zero;
                        characterObject.transform.localScale = Vector3.one;
                    }
                }
            }
        }      
    }

    public RoomSpotView GetRoomSpotForCharacter(CharacterInfo info)
    {
        RoomSpotView newSpot = null;
        foreach(RoomSpotView spotControl in _characterSpotsGameObject)
        {
            if(info.currentRoomSpot.Equals(spotControl.RoomSpots))
            {
                if(spotControl.SpotAvailable == false)
                {
                    Debug.LogError("[MainController] Spot your aiming for " + info.characterName + " is already occupied.");
                    return null;
                }
                spotControl.SpotAvailable = false;
                newSpot = spotControl;
                break;
            }
        }
        return newSpot;
    }
}

using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using NodeCanvas.BehaviourTrees;
using NodeCanvas.DialogueTrees;
using NodeCanvas.DialogueTrees.UI.Examples;

namespace TheWill
{
    public class MainController : MonoBehaviour
    {
        static MainController _instance;
        public static MainController Instance { get { return _instance; } }

        public static string EVT_UPSPOT_CHARACTER = "MainController.EVT_UPSPOT_CHARACTER";

        [SerializeField] int _acteNumber = 1;
        [SerializeField] int _dayNumber = 1;
        [SerializeField] Room _room;
        [SerializeField] string _roomName;
        public string RoomName { get { return _roomName; } }

        [SerializeField] List<RoomSpotView> _characterSpotsGameObject;
        [SerializeField] RoomSpotView _upSpot;

        [SerializeField] Blackboard _blackBoard;
        [SerializeField] BehaviourTreeOwner _behaviourTree;

        [SerializeField] ItemCard[] _itemCards;

        List<CharacterCard> _charactersInScene;
        GameManager _gameManager;

        /*1/bool _isNewLevel
        public void SetNewLevel(bool a_bool) { _isNewLevel = a_bool; }*/

            
    ///////////////////////////////////////////////////////////////
    /// GENERAL FUNCTIONS /////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
        void Awake()
        {
            _instance = this;
            SaveLoad.LoadSaves();
            /*1/if (_isNewLevel)
                Game.Current.LoadStartLevel(_acteNumber, _dayNumber);*/
            LoadBehaviourTree();
            _gameManager = GameManager.Instance;

            EventManager.StartListening(EVT_UPSPOT_CHARACTER, UpdateUpspotWithCharacter);
            EventManager.StartListening(VNFinishNode.EVT_FINISH_DIALOG, UpdateUpspotWithCharacter);

            if (_itemCards != null && _itemCards.Length > 0)
                for (int i = 0; i < _itemCards.Length; i++)
                {
                    EventManager.StartListening(_itemCards[i].Info.name + "OnClick", SelectItem);
                }
        }
        /*********************************************************/

        void Start()
        {
            // TODO
            // Load progression... way before this start though.
            // find level Controller corresponding to this level
            // END TODO

            _charactersInScene = new List<CharacterCard>();

            LoadCharactersObjectInvolved();
            LoadBlackBoard();
        }
        /*********************************************************/

        void OnDestroy()
        {
            EventManager.StopListening(EVT_UPSPOT_CHARACTER, UpdateUpspotWithCharacter);
            EventManager.StopListening(VNFinishNode.EVT_FINISH_DIALOG, UpdateUpspotWithCharacter);

            if (_itemCards != null && _itemCards.Length > 0)
                for (int i = 0; i < _itemCards.Length; i++)
                {
                    EventManager.StopListening(_itemCards[i].Info.name + "OnClick", SelectItem);
                }
        }
        /*********************************************************/
        
    ///////////////////////////////////////////////////////////////
    /// GENERAL FUNCTIONS /////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
        void LoadBehaviourTree(bool a_items = false)
        {
            string level = _room.ToString() + "Act" + Game.Current.acteNumber.ToString() + "Day" + Game.Current.dayNumber.ToString();
            Debug.Log("[MainController] Loading level : " + level);
            BehaviourTree tree = ((BehaviourTree)Resources.Load("Tree/SallesBehavior/" + level + (a_items ? "Item" : string.Empty)));
            if (tree == null)
            {
                Debug.Log("[MainController] Tree load failed");
            }
            else
            {
                Debug.Log("[MainController] Tree load succeed");
            }
            if (_behaviourTree)
                _behaviourTree.StopBehaviour();
            _behaviourTree.behaviour = tree;
        }
        /*********************************************************/

        void LoadBlackBoard()
        {
            foreach (CharacterCard card in _charactersInScene)
            {
                BoxCollider2D collider = card.GetComponent<BoxCollider2D>();
                if (collider)
                {
                    Variable found = _blackBoard.GetVariable(card.CharacterInfo.characterName + "Collider");
                    if (found != null)
                    {
                        found.value = collider;
                    }
                }

                DialogueActor dialog = card.GetComponent<DialogueActor>();
                if (dialog)
                {
                    //_blackBoard.GetVariable<DialogueActor>(go.name + "Dialog").SetValue(dialog);
                }
            }
        }
        /*********************************************************/

        void SelectItem(object a_value)
        {
            ItemCard card = (ItemCard)a_value;
            if (!card)
            {
                Debug.LogError("[MyCharacterController]Error from event to selectItem");
                return;
            }

            Debug.Log("SelectItem: " + card.name);
        }
        /*********************************************************/
        
    ///////////////////////////////////////////////////////////////
    /// PUBLIC FUNCTIONS //////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
        public void LoadCharactersObjectInvolved()
        {
            int cptSpot = 0;
            _gameManager.MyCharacterController.Characters.Clear();
            foreach (CharacterInfo info in Game.Current.charactersInfos)
            {
                Debug.Log("<color=blue>-- " + info.characterName + ", " + info.currentRoom +"</color>");
                if (info.currentRoom == _room)
                {
                    if (cptSpot >= _characterSpotsGameObject.Count)
                    {
                        Debug.Log("<color=blue>[MainController] Not enough spots in the scene.</color>");
                        return;
                    }

                    RoomSpotView newSpot = GetRoomSpotForCharacter(info);
                    if (newSpot)
                    {
                        Debug.Log("<color=blue>--- New spot !</color>");
                        GameObject characterObject = ((GameObject)Instantiate(Resources.Load("Characters/" + info.characterName)));
                        if (characterObject)
                        {
                            Debug.Log("<color=blue>--- Add " + characterObject.name + "</color>");
                            _charactersInScene.Add(characterObject.GetComponent<CharacterCard>());
                            CharacterCard characterCard = characterObject.GetComponent<CharacterCard>();
                            characterCard.CharacterInfo = info;

                            _gameManager.MyCharacterController.Characters.Add(characterCard);
                            _gameManager.MyCharacterController.AddCharacterCardToGlobalBB(characterCard);
                            characterObject.transform.parent = newSpot.gameObject.transform;
                            characterObject.transform.localPosition = Vector3.zero;
                            characterObject.transform.localScale = Vector3.one;
                        }
                        cptSpot++;
                        Debug.Log("<color=blue>[MainController] Character " + info.characterName + " is in the scene.</color>");
                    }
                }
            }
        }
        /*********************************************************/

        public RoomSpotView GetRoomSpotForCharacter(CharacterInfo info)
        {
            RoomSpotView newSpot = null;
            foreach (RoomSpotView spotControl in _characterSpotsGameObject)
            {
                Debug.Log("<color=gray>GetRoomSpotForCharacter: " + info.characterName + " in " + spotControl.RoomSpots + " (info.currentRoomSpot: " + info.currentRoomSpot + ")</color>");
                if (info.currentRoomSpot.Equals(spotControl.RoomSpots))
                {
                    if (spotControl.SpotAvailable == false)
                    {
                        Debug.LogError("[MainController] Spot (" + spotControl.RoomSpots + ") your aiming for " + info.characterName + " is already occupied.");
                        return null;
                    }
                    spotControl.SpotAvailable = false;
                    newSpot = spotControl;
                    break;
                }
            }
            return newSpot;
        }
        /*********************************************************/

        public void ActivateAllCharactersColliders(bool a_enabled)
        {
            for (int i = 0; i < _charactersInScene.Count; i++)
            {
                _charactersInScene[i].GetComponent<Collider2D>().enabled = a_enabled;
            }
        }
        /*********************************************************/

        public void DisplayAllCharacters(bool a_show)
        {
            foreach (CharacterCard character in _charactersInScene)
            {
                character.ToggleVisibility(a_show);
            }
        }
        /*********************************************************/

        public void DisplayAndActivateAllItems(bool a_show)
        {
            foreach (ItemCard item in _itemCards)
            {
                item.ToggleInteractibilityAndVisibility(a_show);
            }
        }
        /*********************************************************/

        public void UpdateUpspotWithCharacter(object a_value)
        {
            CharacterCard card = (CharacterCard)a_value;
            if (card && _upSpot)
            {
                Debug.Log("[MainController] UpdateUpspotWithCharacter " + card.CharacterInfo.characterName.ToString());
                SpriteRenderer renderer = _upSpot.GetComponentInChildren<SpriteRenderer>();
                if (renderer)
                {
                    Debug.Log("[MainController] UpdateUpspotWithCharacter has renderer and sprite " + card.Sprite.sprite.ToString());
                    renderer.sprite = card.Sprite.sprite;
                }
            }
            else
            {
                Debug.Log("[MainController] UpdateUpspotWithCharacter hide sprite upspot");
                SpriteRenderer renderer = _upSpot.GetComponentInChildren<SpriteRenderer>();
                if (renderer)
                {
                    renderer.sprite = null;
                }
            }
        }
        /*********************************************************/

        public void ActivateExploreMode(bool a_activate)
        {
            DisplayAndActivateAllItems(a_activate);
            DisplayAllCharacters(!a_activate);
            ActivateAllCharactersColliders(!a_activate);
            LoadBehaviourTree(a_activate);
            _behaviourTree.StartBehaviour();
        }
        /*********************************************************/
    }
}

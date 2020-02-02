using System;
using System.Collections.Generic;
using UnityEngine;

namespace TheWill
{
    [CreateAssetMenu(fileName = "LevelController", menuName = "InGameControllers/LevelController", order = 1)]
    [Serializable]
    public class LevelController : ScriptableObject
    {
        [SerializeField] LevelControllerData _levelControllerData;
        public LevelControllerData Data {
            get { return _levelControllerData; }
            set { _levelControllerData = new LevelControllerData(value); }
        }

        public LevelController(LevelController a_other)
        {
            //CreateInstance("newLevel");
            _levelControllerData = new LevelControllerData(a_other.Data);
        }

        public LevelController(LevelData a_data)
        {
            _levelControllerData = new LevelControllerData(a_data);
        }
    } // Class
} // Namespace

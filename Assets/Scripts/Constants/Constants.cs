using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheWill
{
    public static class Constants
    {
        public static float INFO_MESSAGE_DURATION = 3.0f;
        public static readonly string LOAD_SLOT_FORMAT = "Sauvegarde {0}";
        public static readonly string GAME_DATA_SAVED_FILE = "/savedGames.gd";
        public static readonly string OPTIONS_SAVED_FILE = "/options.gd";

        public static readonly int FONT_SIZE_LITTLE = 14;
        public static readonly int FONT_SIZE_STANDARD = 18;
        public static readonly int FONT_SIZE_BIG = 22;

        public static readonly float DIALOGUE_CHARACTER_DELAY_FAST = 0.025f;
        public static readonly float DIALOGUE_CHARACTER_DELAY_STANDARD = 0.05f;
        public static readonly float DIALOGUE_CHARACTER_DELAY_SLOW = 0.075f;
        public static readonly float DIALOGUE_SENTENCE_DELAY_FAST = 0.25f;
        public static readonly float DIALOGUE_SENTENCE_DELAY_STANDARD = 0.5f;
        public static readonly float DIALOGUE_SENTENCE_DELAY_SLOW = 0.75f;
        public static readonly float DIALOGUE_COMMA_DELAY_FAST = 0.05f;
        public static readonly float DIALOGUE_COMMA_DELAY_STANDARD = 0.1f;
        public static readonly float DIALOGUE_COMMA_DELAY_SLOW = 0.2f;
        public static readonly float DIALOGUE_FINAL_DELAY_FAST = 0.8f;
        public static readonly float DIALOGUE_FINAL_DELAY_STANDARD = 1.2f;
        public static readonly float DIALOGUE_FINAL_DELAY_SLOW = 1.6f;


        public static void Initialise()
        {

        }
    }
}

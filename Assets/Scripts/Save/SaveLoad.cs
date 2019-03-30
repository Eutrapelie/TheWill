using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

namespace TheWill
{
    public static class SaveLoad
    {
        public static Game[] savedGames = new Game[81];
        public static Options options;
        static bool _areSaveLoaded;


        public static void Save(int a_index = 0)
        {
            Debug.Log("Save...");
            Game.Current.SaveGameFromManager();
            savedGames[a_index] = Game.Current;
            Debug.Log(savedGames[a_index].DebugGameData());
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + Constants.GAME_DATA_SAVED_FILE);
            bf.Serialize(file, SaveLoad.savedGames);
            file.Close();
            Debug.Log("Save complete.");
        }

        public static void Load(int a_index = 0)
        {
            if (_areSaveLoaded == false)
            {
                LoadSaves();
            }

            //Debug.Log("Load " + a_index + ": ");
            //Debug.Log("Load " + a_index + ": " + savedGames[a_index].DebugGameData());
            Game.Current.LoadGame(savedGames[a_index]);
        }

        public static void LoadSaves()
        {
            if (File.Exists(Application.persistentDataPath + Constants.GAME_DATA_SAVED_FILE))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
                //Debug.Log(Application.persistentDataPath + Constants.GAME_DATA_SAVED_FILE);
                SaveLoad.savedGames = (Game[])bf.Deserialize(file);
                file.Close();
                _areSaveLoaded = true;
                //Debug.Log("LoadSaves");
            }
        }

        public static void SaveOptions(Options a_options)
        {
            Debug.Log("Save option...");
            Options.Current.SaveOption(a_options);
            options = Options.Current;
            Debug.Log(options.DebugData());
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + Constants.OPTIONS_SAVED_FILE);
            bf.Serialize(file, options);
            file.Close();
            Debug.Log("Save complete.");
        }

        public static void LoadOptions()
        {
            if (File.Exists(Application.persistentDataPath + Constants.OPTIONS_SAVED_FILE))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + Constants.OPTIONS_SAVED_FILE, FileMode.Open);
                SaveLoad.options = (Options)bf.Deserialize(file);
                file.Close();
                //Debug.Log("Load Options");
                Options.Current.SaveOption(options);
                //Debug.Log(Options.Current.DebugData());
            }
        }

        /*public static void StartNewGame()
        {
            Game.Current.LoadStartLevel(1, 1);
        }*/
    }
}

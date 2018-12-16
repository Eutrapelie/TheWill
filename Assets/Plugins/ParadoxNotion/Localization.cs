using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Utils
{
    public enum Lang { fr, en }

    public static class Localization
    {
        static List<string> _paths = new List<string>();
        static Dictionary<Lang, Dictionary<string, string>> _globalLangDictionary = new Dictionary<Lang, Dictionary<string, string>>();
        static Dictionary<string, string> _currentLangDictionary;

        public static void InitializeLangDictionaries(Lang a_lang = Lang.fr)
        {
            _paths.Clear();
            _paths.Add(Application.streamingAssetsPath + "/TABLE_REFERENCE_LOCALISATION_00.csv");
            _paths.Add(Application.streamingAssetsPath + "/TABLE_REFERENCE_LOCALISATION_01.csv");
            _globalLangDictionary.Clear();
            ParseCSV();
            _currentLangDictionary = _globalLangDictionary[a_lang];
        }

        static void ParseCSV()
        {
            Dictionary<string, string> frenchData = new Dictionary<string, string>();
            Dictionary<string, string> englishData = new Dictionary<string, string>();

            for (int j = 0; j < _paths.Count; j++)
            {
                Debug.Log("ParseCSV -- File exists? " + File.Exists(_paths[j]));
                string fileData = File.ReadAllText(_paths[j]);
                //Debug.Log(fileData);
                //fileData = fileData.Replace(',', '.');

                string[] lines = fileData.Split("\n"[0]);
                //Debug.Log(lines.Length);

                for (int i = 1; i < lines.Length; i++)
                {
                    string[] lineData = (lines[i]).Split(new char[] { ';' });

                    if (lineData.Length > 6)
                    {
                        string id = lineData[0];
                        string frValue = lineData[5];
                        string enValue = lineData[6];

                        frenchData.Add(id, frValue);
                        englishData.Add(id, enValue);
                        //Debug.Log("[" + id + "] French: " + frenchData[id] + " // English: " + englishData[id]);
                    }
                }
            }

            Debug.Log(frenchData.Count + " -- " + englishData.Count);
            _globalLangDictionary.Add(Lang.fr, frenchData);
            _globalLangDictionary.Add(Lang.en, englishData);
        }
        /*********************************************************/

        public static string GetLocalized(string a_id)
        {
            return _currentLangDictionary[a_id];
        }
        /*********************************************************/
    }
}

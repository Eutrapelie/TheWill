using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public enum Lang { fr, en }

    public static class Localization
    {
        static string _path = Application.streamingAssetsPath + "/Test.csv";
        static Dictionary<Lang, Dictionary<string, string>> _globalLangDictionary = new Dictionary<Lang, Dictionary<string, string>>();
        static Dictionary<string, string> _currentLangDictionary;

        public static void InitializeLangDictionaries(Lang a_lang = Lang.fr)
        {
            ParseCSV();
            _currentLangDictionary = _globalLangDictionary[a_lang];
        }

        static void ParseCSV()
        {
            string fileData = System.IO.File.ReadAllText(_path);
            //fileData = fileData.Replace(',', '.');

            string[] lines = fileData.Split("\n"[0]);
            Debug.Log(lines.Length);

            Dictionary<string, string> frenchData = new Dictionary<string, string>();
            Dictionary<string, string> englishData = new Dictionary<string, string>();

            for (int i = 1; i < lines.Length; i++)
            {
                string[] lineData = (lines[i]).Split(new char[] { ';' });

                if (lineData.Length == 3)
                {
                    string id = lineData[0];
                    string frValue = lineData[1];
                    string enValue = lineData[2];

                    frenchData.Add(id, frValue);
                    englishData.Add(id, enValue);
                    Debug.Log("[" + id + "] French: " + frenchData[id] + " // Enlish: " + englishData[id]);
                }
            }
            _globalLangDictionary.Add(Lang.fr, frenchData);
            _globalLangDictionary.Add(Lang.en, englishData);

        }
        /*********************************************************/

        public static string GetLocalized(string a_id)
        {
            return _currentLangDictionary[a_id];
        }
    }
}

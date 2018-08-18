using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheWill
{
    [System.Serializable]
    public class Options
    {
        static Options current;
        public static Options Current
        {
            get
            {
                if (current == null)
                {
                    current = new Options();
                }
                return current;
            }
        }

        public Options()
        {
            if (current != null)
            {
                volume = current.volume;
                language = current.language;
                resolution = current.resolution;
                isFullscreen = current.isFullscreen;
                fontSize = current.fontSize;
                readingSpeed = current.readingSpeed;
            }
            else
            {
                volume = 50f;
                language = Language.French;
                resolution = Resolution._1920_1080;
                isFullscreen = true;
                fontSize = FontSize.Standard;
                readingSpeed = ReadingSpeed.Standard;
            }
        }

        public float volume;
        public Language language;
        public Resolution resolution;
        public bool isFullscreen;
        public FontSize fontSize;
        public ReadingSpeed readingSpeed;
        
        public void SaveOption(Options a_options)
        {
            volume = a_options.volume;
            language = a_options.language;
            resolution = a_options.resolution;
            isFullscreen = a_options.isFullscreen;
            fontSize = a_options.fontSize;
            readingSpeed = a_options.readingSpeed;
        }

        public string DebugData()
        {
            string data = "";
            data = "{Volume: " + volume + " -- language: " + language + " -- readingSpeed: " + readingSpeed + "\n" +
                "resolution: " + resolution + " -- isFullscreen: " + isFullscreen + " -- fontSize: " + fontSize + "}";
            return data;
        }
    }
}

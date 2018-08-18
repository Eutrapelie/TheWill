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

            switch (resolution)
            {
                case Resolution._1280_720:
                    Screen.SetResolution(1280, 720, isFullscreen);
                    return;
                case Resolution._1920_1080:
                    Screen.SetResolution(1920, 1080, isFullscreen);
                    return;
            }
        }

        public string DebugData()
        {
            string data = "";
            data = "{Volume: " + volume + " -- language: " + language + " -- readingSpeed: " + readingSpeed + "\n" +
                "resolution: " + resolution + " -- isFullscreen: " + isFullscreen + " -- fontSize: " + fontSize + "}";
            return data;
        }

        public int GetFontSizeInPixels()
        {
            switch (fontSize)
            {
                case FontSize.Petite:
                    return Constants.FONT_SIZE_LITTLE;
                case FontSize.Standard:
                    return Constants.FONT_SIZE_STANDARD;
                case FontSize.Grande:
                    return Constants.FONT_SIZE_BIG;
            }
            return Constants.FONT_SIZE_STANDARD;
        }

        public float GetCharacterDelay()
        {
            switch (readingSpeed)
            {
                case ReadingSpeed.Lente:
                    return Constants.DIALOGUE_CHARACTER_DELAY_SLOW;
                case ReadingSpeed.Standard:
                    return Constants.DIALOGUE_CHARACTER_DELAY_STANDARD;
                case ReadingSpeed.Rapide:
                    return Constants.DIALOGUE_CHARACTER_DELAY_FAST;
            }
            return Constants.DIALOGUE_CHARACTER_DELAY_STANDARD;
        }

        public float GetSentenceDelay()
        {
            switch (readingSpeed)
            {
                case ReadingSpeed.Lente:
                    return Constants.DIALOGUE_SENTENCE_DELAY_SLOW;
                case ReadingSpeed.Standard:
                    return Constants.DIALOGUE_SENTENCE_DELAY_STANDARD;
                case ReadingSpeed.Rapide:
                    return Constants.DIALOGUE_SENTENCE_DELAY_FAST;
            }
            return Constants.DIALOGUE_SENTENCE_DELAY_STANDARD;
        }

        public float GetCommaDelay()
        {
            switch (readingSpeed)
            {
                case ReadingSpeed.Lente:
                    return Constants.DIALOGUE_COMMA_DELAY_SLOW;
                case ReadingSpeed.Standard:
                    return Constants.DIALOGUE_COMMA_DELAY_STANDARD;
                case ReadingSpeed.Rapide:
                    return Constants.DIALOGUE_COMMA_DELAY_FAST;
            }
            return Constants.DIALOGUE_COMMA_DELAY_STANDARD;
        }

        public float GetFinalDelay()
        {
            switch (readingSpeed)
            {
                case ReadingSpeed.Lente:
                    return Constants.DIALOGUE_FINAL_DELAY_SLOW;
                case ReadingSpeed.Standard:
                    return Constants.DIALOGUE_FINAL_DELAY_STANDARD;
                case ReadingSpeed.Rapide:
                    return Constants.DIALOGUE_FINAL_DELAY_FAST;
            }
            return Constants.DIALOGUE_FINAL_DELAY_STANDARD;
        }
    }
}

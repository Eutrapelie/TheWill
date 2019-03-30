using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheWill
{
    public class MusicManager : MonoBehaviour
    {
        static MusicManager _instance;
        public static MusicManager Instance { get {
                if (_instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = "MusicManager";
                    if (go != null)
                        _instance = go.AddComponent<MusicManager>();
                }
                return _instance;
            } }

        public const string MAIN_MUSIC = "VN-Celeste";
        

        bool _launchAudioSourcesMixing;
        AudioSource _previousAmbiantSource;
        AudioSource _currentAmbiantSource;
        float _mixingTime;
        float _mixingDeltaVolumePerDeltaTime;
        const float MUSIC_TRANSITION_DURATION = 3f;


        void Awake()
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            //AddFirstAudioSource("VN-Leontine-WIP-2.aif");
        }
        /*********************************************************/

        void Start()
        {
        }
        /*********************************************************/

        public void PlayAudioSource(string a_musicName)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            DontDestroyOnLoad(audioSource);
            _currentAmbiantSource = audioSource;
            _currentAmbiantSource.volume = Options.Current.volume / 100f;
            _currentAmbiantSource.loop = true;

            AudioClip clip = Resources.Load<AudioClip>("Sounds/Musics/" + a_musicName);
            if (_currentAmbiantSource.clip == null || clip.name != _currentAmbiantSource.clip.name)
            {
                _currentAmbiantSource.clip = clip;
                _currentAmbiantSource.Play();
            }
        }
        /*********************************************************/


        /*void Update()
        {
            if (_launchAudioSourcesMixing)
            {
                if (_mixingTime < MUSIC_TRANSITION_DURATION)
                {
                    _mixingTime += Time.deltaTime;
                    if (_previousAmbiantSource)
                        _previousAmbiantSource.volume -= Time.deltaTime * _mixingDeltaVolumePerDeltaTime;
                    _currentAmbiantSource.volume += Time.deltaTime * _mixingDeltaVolumePerDeltaTime;
                }
                else
                {
                    _currentAmbiantSource.volume = MUSIC_TRANSITION_DURATION;
                    Destroy(_previousAmbiantSource);
                    _mixingTime = 0.0f;
                    _launchAudioSourcesMixing = false;
                }
            }
        }
        /*********************************************************/


        /*void PlayAmbiantMusic(ref string a_musicName)
        {
            if (_soundObject.GetComponents<AudioSource>().Length == 0)
            {
                AddFirstAudioSource(a_musicName);
            }
            else if (_soundObject.GetComponents<AudioSource>().Length == 1)
            {
                if (_currentAmbiantSource != null && _currentAmbiantSource.clip != null && _currentAmbiantSource.clip.name == a_musicName)
                    return;
                AddNewAudioSource(a_musicName);
            }
            else
            {
                Destroy(_soundObject.GetComponents<AudioSource>()[0]);
                AddNewAudioSource(a_musicName);
            }
        }
        /*********************************************************/
        /*void AddFirstAudioSource(string a_musicName)
        {
            _soundObject.AddComponent<AudioSource>();
            _currentAmbiantSource = _soundObject.GetComponent<AudioSource>();
            _currentAmbiantSource.volume = 100f / 100f;
            _currentAmbiantSource.loop = true;

            AudioClip clip = Resources.Load<AudioClip>("Sounds/Music/" + a_musicName);
            if (_currentAmbiantSource.clip == null || clip.name != _currentAmbiantSource.clip.name)
            {
                _currentAmbiantSource.clip = clip;
                _currentAmbiantSource.Play();
            }
        }
        /*********************************************************/
        /*void AddNewAudioSource(string a_musicName)
        {
            _soundObject.AddComponent<AudioSource>();
            _previousAmbiantSource = _soundObject.GetComponents<AudioSource>()[0];
            _currentAmbiantSource = _soundObject.GetComponents<AudioSource>()[1];
            _currentAmbiantSource.volume = 0;
            _currentAmbiantSource.loop = true;

            AudioClip clip = Resources.Load<AudioClip>("Music/" + a_musicName);
            if (_currentAmbiantSource.clip == null || clip.name != _currentAmbiantSource.clip.name)
            {
                _currentAmbiantSource.clip = clip;
                _currentAmbiantSource.Play();


                _mixingDeltaVolumePerDeltaTime = (_previousAmbiantSource.volume - 0.0f) / MUSIC_TRANSITION_DURATION;
                _mixingTime = 0.0f;
                _launchAudioSourcesMixing = true;
            }
        }
        /*********************************************************/

        /*public void LaunchGame()
        {
            AddNewAudioSource("VN-Celeste.m4a");
        }
        /*********************************************************/

        /*public void ResumeToMenu()
        {
            AddNewAudioSource("VN-Leontine-WIP-2.aif");
        }
        /*********************************************************/
    }
}
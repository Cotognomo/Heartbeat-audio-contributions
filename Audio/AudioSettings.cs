using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace Audio
{
    /// <summary>
    /// Audio settings API.
    /// </summary>
    public static class AudioSettings
    {
        private static Bus _masterBus;
        private static VCA _sfxVca;
        private static VCA _musicVca;

        private const float DefaultMasterVolume = 1.0f;
        private const float DefaultMusicVolume = 1.0f;
        private const float DefaultSfxVolume = 1.0f;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void InitVca()
        {
            Application.quitting += ApplicationQuitting;
            return;
            
            void ApplicationQuitting()
            {
                Application.quitting -= ApplicationQuitting;
                SaveMasterBus();
                SaveSfxVca();
                SaveMusicVca();
            }
        }

        public static void Setup()
        {
            RuntimeUtils.EnforceLibraryOrder();
            SetupMasterBus();
            SetupSfxVca();
            SetupMusicVca();
        }
        
        /// <summary>
        /// Set the master volume.
        /// </summary>
        public static void SetMasterVolume(float volume)
        {
            _masterBus = GetMasterBus();
            if (_masterBus.isValid() == false) return;
            _masterBus.setVolume(volume);
        }

        /// <summary>
        /// Get the master volume.
        /// </summary>
        public static float GetMasterVolume()
        {
            _masterBus = GetMasterBus();
            if (_masterBus.isValid() == false) return DefaultMasterVolume;
            _masterBus.getVolume(out var volume);
            return volume;
        }
        
        /// <summary>
        /// Set the sfx volume.
        /// </summary>
        public static void SetSfxVolume(float volume)
        {
            _sfxVca = GetSfxVca();
            if (_sfxVca.isValid() == false) return;
            _sfxVca.setVolume(volume);
        }
        
        /// <summary>
        /// Get the sfx volume.
        /// </summary>
        public static float GetSfxVolume()
        {
            _sfxVca = GetSfxVca();
            if (_sfxVca.isValid() == false) return DefaultSfxVolume;
            _sfxVca.getVolume(out var volume);
            return volume;
        }

        /// <summary>
        /// Set the music volume.
        /// </summary>
        public static void SetMusicVolume(float volume)
        {
            _musicVca = GetMusicVca();
            if (_musicVca.isValid() == false) return;
            _musicVca.setVolume(volume);
        }

        /// <summary>
        /// Get the music volume.
        /// </summary>
        public static float GetMusicVolume()
        {
            _musicVca = GetMusicVca();
            if (_musicVca.isValid() == false) return DefaultMusicVolume;
            _musicVca.getVolume(out var musicVolume);
            return musicVolume;
        }

        /// <summary>
        /// Mute or unmute SFX.
        /// </summary>
        public static void MuteSfx(bool mute)
        {
            _sfxVca = GetSfxVca();
            if (_sfxVca.isValid() == false) return;
            _sfxVca.setVolume(mute ? 0.0f : 1.0f);
        }
        
        /// <summary>
        /// Mute or unmute Music.
        /// </summary>
        public static void MuteMusic(bool mute)
        {
            _musicVca = GetMusicVca();
            if (_musicVca.isValid() == false) return;
            _musicVca.setVolume(mute ? 0.0f : 1.0f);
        }

        /// <summary>
        /// Check if the sfx are muted.
        /// </summary>
        public static bool AreSfxMuted()
        {
            _sfxVca = GetSfxVca();
            if (_sfxVca.isValid() == false) return false;
            _sfxVca.getVolume(out var sfxVolume);
            return sfxVolume == 0.0f;
        }

        /// <summary>
        /// Check if the music is muted.
        /// </summary>
        public static bool IsMusicMuted()
        {
            _musicVca = GetMusicVca();
            if (_musicVca.isValid() == false) return false;
            _musicVca.getVolume(out var musicVolume);
            return musicVolume == 0.0f;
        }
        
        /// <summary>
        /// Get the Master Bus.
        /// </summary>
        private static Bus GetMasterBus()
        {
            if (_masterBus.isValid()) return _masterBus;
            SetupMasterBus();
            return _masterBus;
        }
        
        /// <summary>
        /// Get the Sfx VCA.
        /// </summary>
        private static VCA GetSfxVca()
        {
            if (_sfxVca.isValid()) return _sfxVca;
            SetupSfxVca();
            return _sfxVca;
        }
        
        /// <summary>
        /// Get the Music VCA.
        /// </summary>
        private static VCA GetMusicVca()
        {
            if (_musicVca.isValid()) return _musicVca;
            SetupMusicVca();
            return _musicVca;
        }
        
        /// <summary>
        /// Set up the Master Bus.
        /// </summary>
        private static void SetupMasterBus()
        {
            _masterBus = RuntimeManager.GetBus("bus:/");
            _masterBus.setVolume(PlayerPrefs.GetFloat("MasterVolume", DefaultMasterVolume));
        }
        
        /// <summary>
        /// Set up the Sfx VCA.
        /// </summary>
        private static void SetupSfxVca()
        {
            _sfxVca = RuntimeManager.GetVCA("vca:/Sfx");
            _sfxVca.setVolume(PlayerPrefs.GetFloat("SfxVolume", DefaultSfxVolume));
        }
        
        /// <summary>
        /// Set up the Music VCA.
        /// </summary>
        private static void SetupMusicVca()
        {
            _musicVca = RuntimeManager.GetVCA("vca:/Music");
            _musicVca.setVolume(PlayerPrefs.GetFloat("MusicVolume", DefaultMusicVolume));
        }

        /// <summary>
        /// Save the Master Bus.
        /// </summary>
        private static void SaveMasterBus()
        {
            _masterBus = GetMasterBus();
            if (_masterBus.isValid() == false) return;
            _masterBus.getVolume(out var masterVolume);
            PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        }
        
        /// <summary>
        /// Save the Sfx VCA.
        /// </summary>
        private static void SaveSfxVca()
        {
            _sfxVca = GetSfxVca();
            if (_sfxVca.isValid() == false) return;
            _sfxVca.getVolume(out var sfxVolume);
            PlayerPrefs.SetFloat("SfxVolume", sfxVolume);
        }

        /// <summary>
        /// Save the Music VCA.
        /// </summary>
        private static void SaveMusicVca()
        {
            _musicVca = GetMusicVca();
            if (_musicVca.isValid() == false) return;
            _musicVca.getVolume(out var musicVolume);
            PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        }
        
    }
}
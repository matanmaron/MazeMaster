using System;
using System.IO;
using UnityEngine;

namespace MaronByteStudio.MazeMaster
{
    public class AudioManager : Singleton<AudioManager>
    {
        [SerializeField] AudioSource MusicSource;
        [SerializeField] AudioSource SoundSource;

        private bool isMusicOn = true;
        private bool isSoundOn = true;

        public bool IsMusicOn
        {
            get => isMusicOn;
            set
            {
                isMusicOn = value;
                MusicSource.mute = !isMusicOn;
            }
        }

        public bool IsSoundOn
        {
            get => isSoundOn;
            set
            {
                isSoundOn = value;
                SoundSource.mute = !isSoundOn;
            }
        }

        public void PlayMusic(MusicTracks track)
        {
            switch (track)
            {
                case MusicTracks.MenuTrack:
                    MusicSource.clip = Resources.Load<AudioClip>(Path.Combine("Music", "final6"));
                    break;
                case MusicTracks.GameTrack:
                    MusicSource.clip = Resources.Load<AudioClip>(Path.Combine("Music", "game3"));
                    break;
                default:
                    Debug.LogError("Music track not found");
                    return;
            }
            MusicSource.Play();
        }

        internal void Refresh()
        {
            IsMusicOn = !Settings.MuteMusic;
            isSoundOn = !Settings.MuteSFX;
        }
    }

    public enum MusicTracks
    {
        MenuTrack,
        GameTrack
    }
}

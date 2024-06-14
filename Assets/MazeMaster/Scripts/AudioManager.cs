using System.IO;
using UnityEngine;

namespace MaronByteStudio.MazeMaster
{
    public class AudioManager : Singleton<AudioManager>
    {
        [SerializeField] AudioSource MusicSource;

        private bool isMusicOn = true;

        public bool IsMusicOn
        {
            get => isMusicOn;
            set
            {
                isMusicOn = value;
                MusicSource.mute = !isMusicOn;
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
        }
    }

    public enum MusicTracks
    {
        MenuTrack,
        GameTrack
    }
}

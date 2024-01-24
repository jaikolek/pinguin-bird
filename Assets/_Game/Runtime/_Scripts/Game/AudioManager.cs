using System;
using System.Collections.Generic;
using UnityEngine;

namespace PinguinBird.Game
{
    public class AudioManager : AppSingleton<AudioManager>
    {
        [Serializable]
        public class Audio
        {
            public string name;
            public AudioClip clip;
        }

        [SerializeField] private AudioSource sfx;
        [SerializeField] private List<Audio> audioList;

        public bool isPlayingBGM { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            isPlayingBGM = true;
        }

        public void PlaySFXOneShot(string name, float volume = 1f)
        {
            foreach (Audio data in audioList)
            {
                if (data.name == name)
                {
                    sfx.PlayOneShot(data.clip, volume);
                    return;
                }
            }
        }
    }
}
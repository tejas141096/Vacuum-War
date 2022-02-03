using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VWPrototype
{
    public class VacuumGunFrameAudioFunction : AVacuumGunFunction
    {
        public SerializableDictionary<HeadMode, AudioSource> HeadSwitchAudios;

        new void Start()
        {
            base.Start();
            if (gunFrame)
            {
                gunFrame.OnHeadSwitched += new VacuumGunFrame.OnHeadSwitchedHandler(this.PlayHeadSwitchAudio);
            }
        }

        public void PlayHeadSwitchAudio()
        {
            HeadMode mode = gunFrame.currentMode;
            AudioSource audioSource;
            HeadSwitchAudios.TryGetValue(mode, out audioSource);
            if (audioSource)
            {
                audioSource.Play();
            }
        }

    }
}
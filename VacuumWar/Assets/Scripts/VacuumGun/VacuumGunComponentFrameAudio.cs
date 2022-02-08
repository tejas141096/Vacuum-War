using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VacuumWar
{
    public class VacuumGunComponentFrameAudio : VacuumGunComponentBase
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VacuumWar
{
    [RequireComponent(typeof(Collider))]
    public class VacuumGunAccessoryRollerHead : VacuumGunAccessoryBase
    {
        public Collider detectionCollider;
        public AudioSource audioSourceOnGoing;
        public AudioSource audioSourceCollecting;

        private bool isSucking = false;

        new void Start()
        {
            base.Start();
            if (!detectionCollider)
            {
                detectionCollider = GetComponent<Collider>();
            }
            if (!detectionCollider.isTrigger)
            {
                Debug.Log("Roller head collision is not a trigger.");
            }
        }

        public override void Activate()
        {
            isSucking = true;
            if (audioSourceOnGoing)
            {
                audioSourceOnGoing.Play();
            }
        }

        public override void Deactivate()
        {
            isSucking = false;
            if (audioSourceOnGoing)
            {
                audioSourceOnGoing.Stop();
            }
        }

        public override void OnGoing()
        {
            isSucking = true;

        }

        private void OnTriggerEnter(Collider other)
        {
            if (isSucking && other.CompareTag("Collectable"))
            { 
                collect(other); 
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (isSucking && other.CompareTag("Collectable"))
            { 
                collect(other); 
            }
        }

        private void collect(Collider other)
        { 
            {
                var c = other.GetComponent<Collectable>();
                if (!c)
                {
                    c = other.GetComponentInParent<Collectable>();
                }
                if (c)
                {
                    var ammoFunc = gunFrame.GetComponent<VacuumGunComponentAmmo>();
                    if(ammoFunc)
                    {
                        var num = ammoFunc.TryChangeAmmo(c.ammoGain);
                        if (num > 0 && audioSourceCollecting) 
                        {
                            audioSourceCollecting.Play();
                        }
                    }
                    c.OnCollected();
                }
            }
        }
    }
}
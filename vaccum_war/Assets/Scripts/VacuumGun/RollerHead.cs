using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VWPrototype
{
    [RequireComponent(typeof(Collider))]
    public class RollerHead : AVacuumGunHead
    {
        public Collider detectionCollider;
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
        }

        public override void Deactivate()
        {
            isSucking = false;
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
                    var ammoFunc = gunFrame.GetComponent<VacuumGunAmmoFunction>();
                    if(ammoFunc)
                    {
                        ammoFunc.TryChangeAmmo(c.ammoGain);
                    }
                    c.OnCollected();
                }
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VacuumWar
{
    public abstract class VacuumGunAccessoryBase : MonoBehaviour
    {
        // Default gun frame is the parent gameObject
        [SerializeField]
        protected VacuumGunFrame gunFrame;
        public HeadMode mode {
            get {
                if(VacuumGunCommon.GetHeadModeFromType(this.GetType()) == HeadMode.Empty)
                {
                    throw new MissingReferenceException($"{this.GetType().Name} cannot be found in VacuumGumCommon.HeadModeTypePairs.");
                }
                return VacuumGunCommon.GetHeadModeFromType(this.GetType());
            }
        }

        protected void Start()
        {
            if (!this.gunFrame && this.GetComponentInParent<VacuumGunFrame>())
            {
                gunFrame = this.GetComponentInParent<VacuumGunFrame>();
            }
            else
            {
                // Todo: fall back if this is a "wild" accessory
            }
        }

        public abstract void Activate();
        public abstract void OnGoing();
        public abstract void Deactivate();

        public void EnableHead()
        {
            this.gameObject.SetActive(true);
        }

        public void DisableHead()
        {
            this.gameObject.SetActive(false);
        }

    }
}
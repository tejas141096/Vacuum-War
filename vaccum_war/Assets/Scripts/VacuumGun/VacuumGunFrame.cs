using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VWPrototype
{

    public class VacuumGunFrame : MonoBehaviour
    {
        public HeadMode initialHeadMode = HeadMode.Empty;
        public bool allowToSwitchHead = true;
        // Defaultly find heads component in children gameobject.
        [SerializeField]
        private List<AVacuumGunHead> heads;

        private HeadMode currentMode = HeadMode.Empty;
        private AVacuumGunHead currentHead;



        // Start is called before the first frame update
        void Start()
        {
            if(heads.Count == 0)
            {
                heads = new List<AVacuumGunHead>(GetComponentsInChildren<AVacuumGunHead>());
            }
            enableHead(initialHeadMode);
        }

        public void Activate()
        {
            if(currentMode == HeadMode.Empty)
            {
                return;
            }
            else
            {
                currentHead.Activate();
            }
        }

        public void OnGoing()
        {
            if (currentMode == HeadMode.Empty)
            {
                return;
            }
            else
            {
                currentHead.OnGoing();
            }
        }

        public void Deactivate()
        {
            if (currentMode == HeadMode.Empty)
            {
                return;
            }
            else
            {
                currentHead.Deactivate();
            }
        }

        public bool SwitchHead(HeadMode newMode)
        {
            if (newMode == currentMode || !allowToSwitchHead)
            {
                return false;
            }
            enableHead(newMode);
            return true;
        }

        private void enableHead(HeadMode newMode)
        {
            currentMode = newMode;

            foreach (var h in heads)
            {
                if (h.mode != newMode)
                {
                    h.DisableHead();
                }
                else
                {
                    h.EnableHead();
                    currentHead = h;
                }
            }

            if (newMode == HeadMode.Empty)
            {
                currentHead = null;
            }
        }
    }
}

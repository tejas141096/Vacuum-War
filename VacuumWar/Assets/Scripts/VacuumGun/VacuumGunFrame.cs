using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VacuumWar
{

    public class VacuumGunFrame : MonoBehaviour
    {
        public HeadMode initialHeadMode = HeadMode.Empty;
        public bool allowToSwitchHead = true;
        /// <summary>
        /// Defaultly find heads component in children gameobject.
        /// </summary>
        [SerializeField]
        private List<VacuumGunAccessoryBase> heads;

        private HeadMode _currentMode = HeadMode.Empty;
        public HeadMode currentMode
        {
            get => _currentMode;
        }
        private VacuumGunAccessoryBase currentHead;

        public delegate void OnHeadSwitchedHandler();
        public event OnHeadSwitchedHandler OnHeadSwitched;

        public static VacuumGunFrame defaultInstance;

        private void Awake()
        {
            defaultInstance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            if(heads.Count == 0)
            {
                heads = new List<VacuumGunAccessoryBase>(GetComponentsInChildren<VacuumGunAccessoryBase>());
            }
            enableHead(initialHeadMode);
        }

        public void Activate()
        {
            if(_currentMode == HeadMode.Empty)
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
            if (_currentMode == HeadMode.Empty)
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
            if (_currentMode == HeadMode.Empty)
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
            if (newMode == _currentMode || !allowToSwitchHead)
            {
                return false;
            }
            enableHead(newMode);
            OnHeadSwitched?.Invoke();
            return true;
        }

        private void enableHead(HeadMode newMode)
        {
            _currentMode = newMode;

            foreach (var h in heads)
            {
                if (h.mode != newMode)
                {
                    h.Deactivate();
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

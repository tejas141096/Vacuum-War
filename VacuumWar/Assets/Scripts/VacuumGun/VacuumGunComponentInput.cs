using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace VacuumWar
{
    public class VacuumGunComponentInput : VacuumGunComponentBase
    {
        public InputActionProperty activeHeadActionProperty;
        public bool enableDebugInput = false;
        public InputActionProperty SwitchEmptyHeadAction;
        public InputActionProperty SwitchShooterHeadAction;
        public InputActionProperty SwitchRollerHeadAction;
        public InputActionProperty FillAmmoAction;
        private InputAction activeHeadActionInUse;

        new void Start()
        {
            base.Start();
            activeHeadActionInUse = activeHeadActionProperty.action;
            if (activeHeadActionInUse.bindings.Count <= 0)
            {
                Debug.Log("Vacuum Gun Input initialized without bindings.");
            }
            activeHeadActionInUse.Enable();
            if (enableDebugInput)
            {
                SwitchEmptyHeadAction.action.Enable();
                SwitchShooterHeadAction.action.Enable();
                SwitchRollerHeadAction.action.Enable();
                FillAmmoAction.action.Enable();
            }
            else
            {
                SwitchEmptyHeadAction.action.Disable();
                SwitchShooterHeadAction.action.Disable();
                SwitchRollerHeadAction.action.Disable();
                FillAmmoAction.action.Disable();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (activeHeadActionInUse.WasPerformedThisFrame())
            {
                gunFrame.Activate();
            }
            if (activeHeadActionInUse.IsPressed())
            {
                gunFrame.OnGoing();
            }
            if (activeHeadActionInUse.WasReleasedThisFrame())
            {
                gunFrame.Deactivate();
            }
            if (SwitchEmptyHeadAction.action.WasPerformedThisFrame())
            {
                //gunFrame.allowToSwitchHead = true;
                gunFrame.SwitchHead(HeadMode.Empty);
                //gunFrame.allowToSwitchHead = false;
            }
            if (SwitchShooterHeadAction.action.WasPerformedThisFrame())
            {
                //gunFrame.allowToSwitchHead = true;
                gunFrame.SwitchHead(HeadMode.Shooter);
                //gunFrame.allowToSwitchHead = false;
            }
            if (SwitchRollerHeadAction.action.WasPerformedThisFrame())
            {
                //gunFrame.allowToSwitchHead = true;
                gunFrame.SwitchHead(HeadMode.Roller);
                //gunFrame.allowToSwitchHead = false;
            }
            if (FillAmmoAction.action.WasPerformedThisFrame())
            {
                var ammoFunction = gunFrame.GetComponent<VacuumGunComponentAmmo>();
                if (ammoFunction)
                {
                    ammoFunction.TryChangeAmmo(ammoFunction.maxAmmo);
                }
            }
        }
    }
}
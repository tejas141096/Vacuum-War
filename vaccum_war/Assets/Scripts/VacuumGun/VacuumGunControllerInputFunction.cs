using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace VWPrototype
{
    public class VacuumGunControllerInputFunction : AVacuumGunFunction
    {
        public InputActionProperty activeHeadActionProperty;
        public bool enableOverrideHeadSwitchInput = false;
        public InputActionProperty SwitchEmptyHeadAction;
        public InputActionProperty SwitchShooterHeadAction;
        public InputActionProperty SwitchRollerHeadAction;
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
            if (enableOverrideHeadSwitchInput)
            {
                //gunFrame.SwitchHead(HeadMode.Empty);
                //gunFrame.allowToSwitchHead = false;
                SwitchEmptyHeadAction.action.Enable();
                SwitchShooterHeadAction.action.Enable();
                SwitchRollerHeadAction.action.Enable();
            }
            else
            {
                SwitchEmptyHeadAction.action.Disable();
                SwitchShooterHeadAction.action.Disable();
                SwitchRollerHeadAction.action.Disable();
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
        }
    }
}
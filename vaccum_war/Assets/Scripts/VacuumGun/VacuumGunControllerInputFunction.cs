using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace VWPrototype
{
    public class VacuumGunControllerInputFunction : AVacuumGunFunction
    {
        public InputActionProperty activeHeadActionProperty;
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
        }
    }
}
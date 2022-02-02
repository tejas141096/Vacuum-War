using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VWPrototype
{
    [RequireComponent(typeof(VacuumGunFrame))]
    public class AVacuumGunFunction : MonoBehaviour
    {
        protected VacuumGunFrame gunFrame;
        // Start is called before the first frame update
        protected void Start()
        {
            gunFrame = this.GetComponent<VacuumGunFrame>();
            if (!gunFrame)
            {
                this.enabled = false;
            }
        }
    }
}
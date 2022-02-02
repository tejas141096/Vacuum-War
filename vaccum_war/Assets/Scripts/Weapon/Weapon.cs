using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

namespace VWPrototypeLegacy
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(XRGrabInteractable))]
    public class Weapon : MonoBehaviour
    {
        //========================================================================================================================================
        /// <summary>
        /// Variables
        /// </summary>
        [SerializeField] protected float shootingForce;
        [SerializeField] protected Transform bulletSpawn;
        [SerializeField] protected BoolVariable gunHead;
        [SerializeField] protected GameObject vaccumHead;
        [SerializeField] private float recoilForce;
        [SerializeField] private float damage;

        protected int bullets = 0;
        protected TextMeshProUGUI bulletText;
        private bool timer = true;
        private InputDevice targetDevice;
        private Rigidbody rigidBody;
        private XRGrabInteractable interactableWeapon;

        //========================================================================================================================================


        //========================================================================================================================================
        /// <summary>
        /// General Functions
        /// </summary>
        [Obsolete]
        protected virtual void Awake()
        {
            interactableWeapon = GetComponent<XRGrabInteractable>();
            rigidBody = GetComponent<Rigidbody>();
            gunHead.value = false;
            SetupInteractableWeaponEvents();
        }

        private void Start()
        {
            bulletText = FindObjectOfType<TextMeshProUGUI>();
            print(bulletText);
        }

        [Obsolete]
        private void Update()
        {
            List<InputDevice> devices = new List<InputDevice>();
            InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right;
            InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);
            if (devices.Count > 0)
            {
                targetDevice = devices[0];
            }


            targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue);
            if (primaryButtonValue == true && timer)
            {
                timer = false;
                print("Changed gun head");
                if (!gunHead.value)
                {
                    gunHead.value = true;
                    vaccumHead.SetActive(false);
                }
                else
                {
                    gunHead.value = false;
                    vaccumHead.SetActive(true);
                }
                ////SetupInteractableWeaponEvents();
                StartCoroutine(WaitForIt(3.0F));
            }
        }
        //========================================================================================================================================

        //========================================================================================================================================
        IEnumerator WaitForIt(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            timer = true;
        }
        //========================================================================================================================================


        //========================================================================================================================================
        /// <summary>
        /// Input Handler
        /// </summary>
        [Obsolete]
        private void SetupInteractableWeaponEvents()
        {
            interactableWeapon.onActivate.AddListener(StartShooting);
            interactableWeapon.onDeactivate.AddListener(StopShooting);

            interactableWeapon.onActivate.AddListener(StartCollecting);
            interactableWeapon.onDeactivate.AddListener(StopCollecting);
        }
        //========================================================================================================================================


        //========================================================================================================================================
        /// <summary>
        /// Gun shooting
        /// </summary>
        protected virtual void StartShooting(XRBaseInteractor xRBaseInteractor)
        {
            //throw new NotImplementedException();
        }

        protected virtual void StopShooting(XRBaseInteractor xRBaseInteractor)
        {
            //throw new NotImplementedException();
        }

        protected virtual void Shoot()
        {
            ApplyRecoil();
        }

        private void ApplyRecoil()
        {
            rigidBody.AddRelativeForce(Vector3.back * recoilForce, ForceMode.Impulse);
        }
        //========================================================================================================================================


        //========================================================================================================================================
        /// <summary>
        /// Vaccum Collecting
        /// </summary>
        protected virtual void StartCollecting(XRBaseInteractor xRBaseInteractor)
        {
            //throw new NotImplementedException();
        }

        protected virtual void StopCollecting(XRBaseInteractor xRBaseInteractor)
        {
            //throw new NotImplementedException();
        }

        protected virtual void Collect()
        {

        }
        //========================================================================================================================================


        //========================================================================================================================================
        /// <summary>
        /// Global Functions
        /// </summary>
        public float GetShootingForce()
        {
            return shootingForce;
        }

        public float GetDamage()
        {
            return damage;
        }
        //========================================================================================================================================
    }
}



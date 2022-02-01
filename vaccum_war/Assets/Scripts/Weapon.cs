using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

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
        gunHead.value = true;
        SetupInteractableWeaponEvents();
    }

    private void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);
        if (devices.Count > 0)
        {
            targetDevice = devices[0];
        }
        print(targetDevice);
    }

    [Obsolete]
    private void Update()
    {
        targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue);
        if (primaryButtonValue == true && timer)
        {
            gunHead.value = !gunHead.value;
            timer = false;
            print("Changed gun head");
            if (gunHead.value)
                vaccumHead.SetActive(false);

            else
                vaccumHead.SetActive(true);
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
        //interactableWeapon.onSelectEntered.AddListener(PickupWeapon);
        //interactableWeapon.onSelectExited.AddListener(DropWeapon);

        //interactableWeapon.onActivate.RemoveAllListeners();
        //interactableWeapon.onDeactivate.RemoveAllListeners();
        //if (gunHead.value)
        //{
        print("Gun listener");
        interactableWeapon.onActivate.AddListener(StartShooting);
        interactableWeapon.onDeactivate.AddListener(StopShooting);
        //}
        //else
        //{
        print("Vaccum Listener");
        interactableWeapon.onActivate.AddListener(StartCollecting);
        interactableWeapon.onDeactivate.AddListener(StopCollecting);
        //}
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

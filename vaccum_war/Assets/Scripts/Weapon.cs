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


    ////========================================================================================================================================
    ///// <summary>
    ///// List of current collisions
    ///// </summary>
    //List<GameObject> currentCollisions = new List<GameObject>();

    //void OnCollisionEnter(Collision col)
    //{

    //    // Add the GameObject collided with to the list.
    //    currentCollisions.Add(col.gameObject);

    //    // Print the entire list to the console.
    //    foreach (GameObject gObject in currentCollisions)
    //    {
    //        print(gObject.name);
    //    }
    //}

    //void OnCollisionExit(Collision col)
    //{

    //    // Remove the GameObject collided with from the list.
    //    currentCollisions.Remove(col.gameObject);

    //    // Print the entire list to the console.
    //    foreach (GameObject gObject in currentCollisions)
    //    {
    //        print(gObject.name);
    //    }
    //}
    ////========================================================================================================================================


    //========================================================================================================================================
    /// <summary>
    /// General Functions
    /// </summary>
    [Obsolete]
    protected virtual void Awake()
    {
        interactableWeapon = GetComponent<XRGrabInteractable>();
        rigidBody = GetComponent<Rigidbody>();
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
        print(gunHead.value);
    }

    private void Update()
    {
        targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue);
        if (primaryButtonValue == true && timer)
        {
            print("1");
            gunHead.value = !gunHead.value;
            timer = false;
            print("Changed gun head");
            if (!gunHead.value)
            {
                vaccumHead.SetActive(true);
            }
            else
            {
                vaccumHead.SetActive(false);
            }
            StartCoroutine(WaitForIt(3.0F));
        }
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
        interactableWeapon.onActivate.AddListener(StartShooting);
        interactableWeapon.onDeactivate.AddListener(StopShooting);

        interactableWeapon.onActivate.AddListener(StartCollecting);
        interactableWeapon.onDeactivate.AddListener(StopCollecting);

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

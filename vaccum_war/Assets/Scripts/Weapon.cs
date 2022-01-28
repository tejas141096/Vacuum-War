using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(XRGrabInteractable))]
public class Weapon : MonoBehaviour
{
    [SerializeField] protected float shootingForce;
    [SerializeField] protected Transform bulletSpawn;
    [SerializeField] private float recoilForce;
    [SerializeField] private float damage;

    private Rigidbody rigidBody;
    private XRGrabInteractable interactableWeapon;

    [Obsolete]
    protected virtual void Awake()
    {
        interactableWeapon = GetComponent<XRGrabInteractable>();
        rigidBody = GetComponent<Rigidbody>();
        SetupInteractableWeaponEvents();
    }

    [Obsolete]
    private void SetupInteractableWeaponEvents()
    {
        interactableWeapon.onSelectEntered.AddListener(PickupWeapon);
        interactableWeapon.onSelectExited.AddListener(DropWeapon);
        interactableWeapon.onActivate.AddListener(StartShooting);
        interactableWeapon.onDeactivate.AddListener(StopShooting);
    }

    private void PickupWeapon(XRBaseInteractor xRBaseInteractor)
    {
        xRBaseInteractor.GetComponent<MeshHidder>().Hide();
    }

    private void DropWeapon(XRBaseInteractor xRBaseInteractor)
    {
        xRBaseInteractor.GetComponent<MeshHidder>().Show();
    }

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

    public float GetShootingForce()
    {
        return shootingForce;
    }

    public float GetDamage()
    {
        return damage;
    }
}

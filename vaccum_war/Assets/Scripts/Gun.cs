using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Gun : Weapon
{
    [SerializeField] private Projectile bulletPrefab;
    protected override void StartShooting(XRBaseInteractor xRBaseInteractor)
    {
        base.StartShooting(xRBaseInteractor);
        Shoot();
    }

    protected override void Shoot()
    {
        base.Shoot();
        Projectile projectileInstance = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        projectileInstance.Init(this);
        projectileInstance.Launch();
    }

    protected override void StopShooting(XRBaseInteractor xRBaseInteractor)
    {
        base.StopShooting(xRBaseInteractor);
    }
}

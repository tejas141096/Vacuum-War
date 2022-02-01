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
        if (gunHead.value)
        {
            Shoot();
        }
    }

    protected override void Shoot()
    {
        if (gunHead.value)
        {
            base.Shoot();
            if (bullets > 0)
            {
                Projectile projectileInstance = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
                projectileInstance.Init(this);
                projectileInstance.Launch();
                bullets--;
            }
            else
            {
                print("Collect more objects");
            }
        }
    }

    protected override void StopShooting(XRBaseInteractor xRBaseInteractor)
    {
        base.StopShooting(xRBaseInteractor);
    }



    // Declare and initialize a new List of GameObjects called currentCollisions.
    List<GameObject> currentCollisions = new List<GameObject>();

    void OnCollisionEnter(Collision col)
    {
        // Add the GameObject collided with to the list.
        currentCollisions.Add(col.gameObject);
    }

    void OnCollisionExit(Collision col)
    {
        // Remove the GameObject collided with from the list.
        currentCollisions.Remove(col.gameObject);
    }

    protected override void StartCollecting(XRBaseInteractor xRBaseInteractor)
    {
        base.StartCollecting(xRBaseInteractor);
        if (!gunHead.value)
        {
            Collect();
        }
    }

    protected override void StopCollecting(XRBaseInteractor xRBaseInteractor)
    {
        base.StopCollecting(xRBaseInteractor);
    }

    protected override void Collect()
    {
        if (!gunHead.value)
        {
            base.Collect();
            print(currentCollisions.Count);
            bullets += (int)(currentCollisions.Count);
            print(bullets);
            foreach (GameObject gObject in currentCollisions)
            {
                Destroy(gObject);
            }
            currentCollisions.Clear();
        }
    }
}

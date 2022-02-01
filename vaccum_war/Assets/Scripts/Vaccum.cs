using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Vaccum : Weapon
{
    // Declare and initialize a new List of GameObjects called currentCollisions.
    List<GameObject> currentCollisions = new List<GameObject>();

    void OnCollisionEnter(Collision col)
    {
        print("col enter");
        // Add the GameObject collided with to the list.
        currentCollisions.Add(col.gameObject);

        // Print the entire list to the console.
        foreach (GameObject gObject in currentCollisions)
        {
            print(gObject.name);
        }
    }

    void OnCollisionExit(Collision col)
    {
        print("col exit");
        // Remove the GameObject collided with from the list.
        currentCollisions.Remove(col.gameObject);

        // Print the entire list to the console.
        foreach (GameObject gObject in currentCollisions)
        {
            print(gObject.name);
        }
    }

    protected override void StartCollecting(XRBaseInteractor xRBaseInteractor)
    {
        base.StartCollecting(xRBaseInteractor);
        Collect();
    }

    protected override void StopCollecting(XRBaseInteractor xRBaseInteractor)
    {
        base.StopCollecting(xRBaseInteractor);
    }

    protected override void Collect()
    {
        base.Collect();
        bullets += (int)(currentCollisions.Count);
        print(bullets);
        foreach (GameObject gObject in currentCollisions)
        {
            print(gObject.name);
            gObject.SetActive(false);
        }
    }

    //protected override void StartShooting(XRBaseInteractor xRBaseInteractor)
    //{
    //    base.StartShooting(xRBaseInteractor);
    //    Shoot();
    //}

    //protected override void Shoot()
    //{
    //    base.Shoot();
    //    Projectile projectileInstance = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
    //    projectileInstance.Init(this);
    //    projectileInstance.Launch();
    //}

    //protected override void StopShooting(XRBaseInteractor xRBaseInteractor)
    //{
    //    base.StopShooting(xRBaseInteractor);
    //}
}

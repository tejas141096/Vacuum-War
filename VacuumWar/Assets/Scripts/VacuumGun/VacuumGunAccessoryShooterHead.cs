using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VacuumWar
{
    public class VacuumGunAccessoryShooterHead : VacuumGunAccessoryBase
    {
        public VacuumGunAccessoryShooterHeadParams Params;
        public Transform projectileGeneratingPoint;

        public AudioSource audioSourceFire;
        public AudioSource audioSourceFireWhenOutOfAmmo;

        public GameObject muzzleFlameVFX;

        public override void Activate()
        {
            var ammoFunction = gunFrame.GetComponent<VacuumGunComponentAmmo>();
            if (ammoFunction)
            {
                if (ammoFunction.TryChangeAmmo(-Params.ammoConsumePerRound) < 0)
                {
                    // Todo: Fire mode and Round mode
                    shoot();
                }
                else
                {
                    if (audioSourceFireWhenOutOfAmmo)
                    {
                        audioSourceFireWhenOutOfAmmo.Play();
                    }
                }
            }
            else
            {
                shoot();
            }
        }

        public override void Deactivate()
        {
        }

        public override void OnGoing()
        {
        }

        private void shoot()
        {
            // Todo: Fire-Mode
            if (Params.projectile)
            {
                var p = Instantiate(Params.projectile.gameObject, projectileGeneratingPoint.position, projectileGeneratingPoint.rotation) as GameObject;
                p.GetComponent<Rigidbody>().AddForce(projectileGeneratingPoint.forward * Params.launchForce, ForceMode.Impulse);
            }

            if (audioSourceFire)
            {
                audioSourceFire.Play();
            }

            if (muzzleFlameVFX)
            {
                Instantiate(muzzleFlameVFX, projectileGeneratingPoint);
            }
        }

        new void Start()
        {
            base.Start();
            if (!projectileGeneratingPoint)
            {
                projectileGeneratingPoint = this.GetComponent<Transform>();
            }
        }
    }
}
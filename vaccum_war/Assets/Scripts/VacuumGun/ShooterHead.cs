using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VWPrototype
{
    public class ShooterHead : AVacuumGunHead
    {
        public ShooterHeadParams Params;
        public Transform projectileGeneratingPoint;
        public override void Activate()
        {
            var ammoFunction = gunFrame.GetComponent<VacuumGunAmmoFunction>();
            if (ammoFunction)
            {
                if (ammoFunction.TryChangeAmmo(-Params.ammoConsumePerRound) < 0)
                {
                    // Todo: Fire mode and Round mode
                    shoot();
                }
                else
                {
                    // Todo: empty ammo audio
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
                var p = Instantiate(Params.projectile.gameObject, projectileGeneratingPoint) as GameObject;
                p.GetComponent<Rigidbody>().AddForce(projectileGeneratingPoint.forward * Params.launchForce, ForceMode.Impulse);
            }

            // Todo: Audio and Particles
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace VWPrototype
{
    public class VacuumGunAmmoFunction : AVacuumGunFunction
    {
        public int initAmmo = 10;
        public int maxAmmo = 30;
        [SerializeField]
        private TextMeshPro ammoIndicator;

        private int currentAmmo;

        new void Start()
        {
            base.Start();
            ResetAmmo();
        }

        // Return the value that was succesfully changed
        public int TryChangeAmmo(int delta)
        {
            if(currentAmmo == 0 && delta < 0)
            {
                // Todo: some visualization for empty shooting on the indicator
                return 0;
            }
            else if (currentAmmo == maxAmmo && delta > 0)
            {
                // Todo: some visualization for the full magazine collecting on the indicator
                return 0;
            }
            else
            {
                var r = 0;
                if (delta > 0)
                {
                    r = Mathf.Min(maxAmmo - currentAmmo, delta);
                }
                else if (delta < 0)
                {
                    r = Mathf.Max(0 - currentAmmo, delta);
                }
                currentAmmo += delta;
                currentAmmo = Mathf.Clamp(currentAmmo, 0, maxAmmo);
                refreshIndicator();
                return r;
            }
        }

        public void ResetAmmo()
        {
            currentAmmo = initAmmo;
            refreshIndicator();
        }

        void refreshIndicator()
        {
            if (ammoIndicator)
            {
                ammoIndicator.text = $"Ammo\n{currentAmmo}/{maxAmmo}";
            }
        }
    }
}
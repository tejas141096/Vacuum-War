using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VWPrototype
{
    public class Collectable : MonoBehaviour
    {
        public int ammoGain = 1;

        public void OnGenerated()
        {

        }

        public void OnCollected()
        {
            this.gameObject.SetActive(false);
            foreach(var c in GetComponentsInChildren<Transform>())
            {
                Destroy(c.gameObject);
            }
            Destroy(this.gameObject);
        }

        public void OnDisappear()
        {
            this.gameObject.SetActive(false);
            foreach (var c in GetComponentsInChildren<Transform>())
            {
                Destroy(c.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}
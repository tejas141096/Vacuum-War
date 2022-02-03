using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VWPrototype
{
    public class Collectable : MonoBehaviour
    {
        public int ammoGain = 1;
        /// <summary>
        /// Will find in this GameObject if not assigned.
        /// </summary>
        public AudioSource audioSource;
        public List<AudioClip> floorCollisionAudioClips;
        public float playAudioPossibility = 0.05f;

        bool hasPlayedFloorCollisionAudio = false;

        private void Start()
        {
            if (!audioSource)
            {
                audioSource = this.GetComponent<AudioSource>();
            }
        }

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

        private void OnCollisionEnter(Collision collision)
        {
            //Debug.Log($"{collision.collider.name}, {collision.collider.tag}");
            if (!hasPlayedFloorCollisionAudio && collision.collider.CompareTag("Floor"))
            {
                if(audioSource && 
                    floorCollisionAudioClips.Count>0 && 
                    Random.value < playAudioPossibility )
                {
                    audioSource.PlayOneShot(floorCollisionAudioClips[Random.Range(0, floorCollisionAudioClips.Count)]);
                    hasPlayedFloorCollisionAudio = true;
                }
            }
        }
    }
}
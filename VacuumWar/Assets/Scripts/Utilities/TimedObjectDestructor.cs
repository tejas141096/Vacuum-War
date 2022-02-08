using UnityEngine;
using System.Collections;

public class TimedObjectDestructor : MonoBehaviour {

	public float timeOut = 1.0f;
	public bool detachChildren = false;
	public bool destroyChildren = true;

	// Use this for initialization
	void Awake () {
		// invote the DestroyNow funtion to run after timeOut seconds
		Invoke ("DestroyNow", timeOut);
	}
	

	void DestroyNow ()
	{
		if (detachChildren) { // detach the children before destroying if specified
			transform.DetachChildren ();
		}

		if (destroyChildren)
        {
			gameObject.SetActive(false);
			foreach(var c in GetComponentsInChildren<Transform>())
            {
				Destroy(c.gameObject);
            }
        }

		// destory the game Object
		Destroy(gameObject);
	}
}

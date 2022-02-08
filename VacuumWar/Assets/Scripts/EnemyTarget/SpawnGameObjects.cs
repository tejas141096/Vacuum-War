using UnityEngine;
using System.Collections;

// Spawn objects from a pool of prefabs at regular intervals
public class SpawnGameObjects : MonoBehaviour
{
	// public variables
	public float secondsBetweenSpawning = 0.1f;
	public float xMinRange = -25.0f;
	public float xMaxRange = 25.0f;
	public float yMinRange = 8.0f;
	public float yMaxRange = 25.0f;
	public float zMinRange = -25.0f;
	public float zMaxRange = 25.0f;

	public bool randomSpeed = false;  // XY-plane speed only
	public float randomSpeedMin = 0f;
	public float randomSpeedMax = 3f;

	public GameObject[] spawnObjects; // what prefabs to spawn

	private float nextSpawnTime;

	// Use this for initialization
	void Start ()
	{
		// determine when to spawn the next object
		nextSpawnTime = Time.time+secondsBetweenSpawning;
	}
	
	// Update is called once per frame
	void Update ()
	{
		// exit if there is a game manager and the game is over
		//if (GameManager.gm) {
		//	if (GameManager.gm.gameIsOver)
		//		return;
		//}

		// if time to spawn a new game object
		if (Time.time  >= nextSpawnTime) {
			// Spawn the game object through function below
			MakeThingToSpawn ();

			// determine the next time to spawn the object
			nextSpawnTime = Time.time+secondsBetweenSpawning;
		}	
	}

	void MakeThingToSpawn ()
	{
		Vector3 spawnPosition;

		// get a random position between the specified ranges
		spawnPosition.x = this.transform.position.x + Random.Range (xMinRange, xMaxRange);
		spawnPosition.y = this.transform.position.y + Random.Range (yMinRange, yMaxRange);
		spawnPosition.z = this.transform.position.z + Random.Range (zMinRange, zMaxRange);

		// determine which object to spawn
		int objectToSpawn = Random.Range (0, spawnObjects.Length);

		// actually spawn the game object
		GameObject spawnedObject = Instantiate (spawnObjects [objectToSpawn], spawnPosition, transform.rotation) as GameObject;

		// make the parent the spawner so hierarchy doesn't get super messy
		spawnedObject.transform.parent = gameObject.transform;

        if (randomSpeed)
        {
			var rb = spawnedObject.GetComponent<Rigidbody>();
            if (rb)
            {
				rb.velocity = new Vector3(Random.value-0.5f, 0, Random.value-0.5f).normalized * Random.Range(randomSpeedMin, randomSpeedMax);
            }
        }
	}
}

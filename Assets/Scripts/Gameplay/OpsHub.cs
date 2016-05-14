using UnityEngine;
using System.Collections;
using System;

public class OpsHub : MonoBehaviour {

	public GameObject[] waypoints;
	private GameObject[] targetComponents;

    private float bytes;
    float bitRate;
    public float maxBitRate;

    public int droneSpawnCount = 5;

    public GameObject[] obstacles;

	// Use this for initialization
	void Start () {
		//the nodes for pathfinding
		waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
	}

    public float GetBytes() {
        return bytes;
    }

    public float GetBitRate() {
        return bitRate;
    }
	
	// Update is called once per frame
	void Update () {
        bitRate = maxBitRate + Mathf.PerlinNoise(Time.time * .5f, 0.0F) * (maxBitRate / 5f) - (maxBitRate / 5f);
        //Generate Points
        bytes += bitRate * Time.deltaTime;

		//find obstacles
		obstacles = GameObject.FindGameObjectsWithTag("Solid");
	}

	public void SpawnDrones(int numDrones)
	{
		float degreeIncrement = 360 / numDrones;
		float spawnDist = 10;

		for (int d = 0; d < numDrones; d++)
		{
			float x = transform.position.x + Mathf.Cos(degreeIncrement * d) * spawnDist;
			float z = transform.position.z + Mathf.Sin(degreeIncrement * d) * spawnDist;
			float y = Terrain.activeTerrain.SampleHeight(new Vector3(x, 0, z));

			Vector3 position = new Vector3(x, y + 3, z);

			Instantiate(Resources.Load("Drone", typeof(GameObject)), position, Quaternion.identity);
		}
	}

	public GameObject GetTargetComponent()
	{
        return GameObject.Find("CPU");
	}
}

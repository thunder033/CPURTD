using UnityEngine;
using System.Collections;
using System;

public class ColonyHub : MonoBehaviour {

	public float mineralCount;

	public GameObject[] waypoints;
	private GameObject[] targetComponents;

	public int droneSpawnCount = 5;

	Camera[] cameras;
	int curCameraIndex;

	public GameObject[] obstacles;

	// Use this for initialization
	void Start () {
		//the nodes for pathfinding
		waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
		cameras = FindObjectsOfType<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		//switch camera on space bar press
		if(Input.GetKeyDown("space"))
		{
			if(curCameraIndex++ >= cameras.Length)
			{
				curCameraIndex = 0;
			}

			for (int c = 0; c < cameras.Length; c++)
			{
				cameras[c].enabled = false;
				cameras[c].tag = "Untagged";

				if(c == curCameraIndex)
				{
					cameras[c].enabled = true;
					cameras[c].tag = "MainCamera";
				}
			}
		}

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

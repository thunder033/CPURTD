using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(OpsHub))]
public class Spawner : MonoBehaviour {

    public GameObject minionPrefab;
    public OpsHub hub;
    public float spawnRate;
    public float spawnCoolDown;

	// Use this for initialization
	void Start () {
        spawnRate = 2;
        spawnCoolDown = spawnRate;
	}
	
	// Update is called once per frame
	void Update () {
        spawnCoolDown -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.S)) {
            Spawn<Minion>();
        }
	}

    public void Spawn<T>() where T : Minion {
        if (spawnCoolDown < 0 && hub.getBytes() >= Minion.BytesCost) {
            GameObject newMinion = Instantiate(minionPrefab);
            newMinion.transform.position = gameObject.transform.position;
            spawnCoolDown = spawnRate;
        }
    }
}

using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(OpsHub))]
public class Spawner : MonoBehaviour {

    public GameObject virusPrefab;
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
        if(spawnCoolDown > 0)
            spawnCoolDown -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.S)) {
            Spawn(Strain.Basic);
        }
	}

    public void Spawn(Strain strain) {
        if (spawnCoolDown < 0 && hub.GetBytes() >= (int)strain) {
            hub.SpendBytes((int)strain);
            GameObject virus = (GameObject)Instantiate(Resources.Load("Virus" + strain.ToString()));
            virus.transform.position = gameObject.transform.position;
            spawnCoolDown = spawnRate;
        }
    }

    public void SpawnBasic() {
        Spawn(Strain.Basic);
    }
    public void SpawnTrojan() {
        Spawn(Strain.Trojan);
    }
    public void SpawnWorm() {
        Spawn(Strain.Worm);
    }
}

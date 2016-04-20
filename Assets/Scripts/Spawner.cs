using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public GameObject minionPrefab;
    public GameManager manager;
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
        if (spawnCoolDown < 0)
        {
            GameObject newMinion = Instantiate(minionPrefab);
            newMinion.transform.position = gameObject.transform.position;
            spawnCoolDown = spawnRate;
        }
	}
}

using UnityEngine;
using System.Collections;

public interface TowerPower
{
    void Attack();
}

public class Tower : MonoBehaviour {

    public GameManager manager;
    public int range;
    public TowerPower attack;

	// Use this for initialization
	void Start () {
        range = int.MaxValue;
	}
	
	// Update is called once per frame
	void Update () {
	    foreach (GameObject drone in manager.minions)
        {
            if (Vector3.Distance(drone.transform.position, gameObject.transform.position) <= range)
            {
                attack.Attack();
            }
        }
	}
}

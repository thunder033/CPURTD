using UnityEngine;
using System.Collections;
using System.Linq;

public interface TowerPower
{
    void Attack(GameObject closestDrone);
}

public enum TowerState
{
    shooting,
    notShooting
}

public class Tower : MonoBehaviour {

    public GameManager manager;
    public GameObject towerIndicator;
    public int range;
    public float attackSpeed;
    public TowerPower attack;
    public TowerState curState;
    public float coolDownTimer;

	// Use this for initialization
	void Start () {
        range = int.MaxValue;
        curState = TowerState.notShooting;
        attackSpeed = 3;
        coolDownTimer = attackSpeed;
        towerIndicator.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (curState == TowerState.notShooting && manager.minions.Count() > 0)
        {
            GameObject closestDrone = manager.minions.OrderBy(a => Vector3.Distance(a.transform.position, gameObject.transform.position)).First();
            if (closestDrone != null && (Vector3.Distance(closestDrone.transform.position, gameObject.transform.position)) <= range)
            {
                attack.Attack(closestDrone);
                towerIndicator.SetActive(true);
                curState = TowerState.shooting;
            }
        }
        else
        {
            coolDownTimer -= Time.deltaTime;
            if (coolDownTimer < 0)
            {
                curState = TowerState.notShooting;
                coolDownTimer = attackSpeed;
                towerIndicator.SetActive(false);
            }
        }
	}
}

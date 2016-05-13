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

[RequireComponent(typeof(TowerPower))]
public class Tower : MonoBehaviour {

    public GameManager manager;
    public GameObject towerIndicator;
    public float range;
    public float attackSpeed;
    public TowerPower attack;
    public TowerState curState;
    public float coolDownTimer;

	// Use this for initialization
	void Start () {
        range = 60;
        curState = TowerState.notShooting;
        attackSpeed = 3;
        coolDownTimer = attackSpeed;
        towerIndicator.SetActive(false);
        attack = GetComponent<TowerPower>();
	}
	
	// Update is called once per frame
	void Update () {
        var minions = FindObjectsOfType<Minion>();
        if (curState == TowerState.notShooting && minions.Count() > 0)
        {
            Minion closestDrone = minions.OrderBy(a => Vector3.Distance(a.transform.position, gameObject.transform.position)).First();
            if (closestDrone != null && (Vector3.Distance(closestDrone.transform.position, gameObject.transform.position)) <= range)
            {
                attack.Attack(closestDrone.gameObject);
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
            }
        }

        if(coolDownTimer < 2.5f && towerIndicator.activeSelf)
        {
            towerIndicator.SetActive(false);
        }
	}
}

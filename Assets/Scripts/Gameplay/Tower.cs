using UnityEngine;
using System.Collections;
using System.Linq;

public interface TowerPower
{
    void Attack(Combatant target);
}

public enum TowerState
{
    shooting,
    notShooting
}

[RequireComponent(typeof(TowerPower))]
[RequireComponent(typeof(Weapon))]
public class Tower : MonoBehaviour {

    public GameManager manager;
    public GameObject towerIndicator;
    public float range;
    public TowerState curState;

    TowerPower attack;
    Weapon weapon;
    Minion target;

	// Use this for initialization
	void Start () {
        range = 60;
        curState = TowerState.notShooting;
        towerIndicator.SetActive(false);
        attack = GetComponent<TowerPower>();
        weapon = GetComponent<Weapon>();
	}
	
	// Update is called once per frame
	void Update () {
        var minions = FindObjectsOfType<Minion>();

        if (minions.Count() == 0)
            return;

        if (target == null || !target.isActiveAndEnabled || Vector3.Distance(target.transform.position, transform.position) > range) {
            target = minions.OrderBy(a => Vector3.Distance(a.transform.position, gameObject.transform.position)).First();
        }
        else if (weapon.IsReady())
            {
            weapon.Fire();
                towerIndicator.SetActive(true);
        }
        else {
            weapon.Aim(target.GetComponent<Combatant>());
            towerIndicator.SetActive(false);
        }
	}
}

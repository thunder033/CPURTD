using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Tower))]
public class Lazer : Weapon, TowerPower {

    public void Attack (Combatant target)
    {
        Aim(target);
        Fire();
    }
}

using UnityEngine;
using System.Collections;

public class HomingProjectile : Projectile {

    Combatant target;

	// Update is called once per frame
	override protected void Update () {
        if(target != null) {
            Vector3 fireDirection = target.transform.position - transform.position;
            Velocity = ((fireDirection.normalized * Velocity.magnitude) + Velocity) / 2.0f;
        }
        

        base.Update();
	}

    public void SetTarget(Combatant target) {
        this.target = target;
    }
}

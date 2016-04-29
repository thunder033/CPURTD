using UnityEngine;
using System.Collections;

enum WeaponState {
    Idle,
    Firing,
    Cooldown
}

public class Weapon : MonoBehaviour {

    Projectile projectilePrefab;

    public float fireRate;
    public float firePower;

    float cooldown;
    WeaponState state;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        switch (state) {
            case WeaponState.Firing:
                cooldown = 60.0f / fireRate;
                state = WeaponState.Cooldown;
                break;
            case WeaponState.Cooldown:
                cooldown -= Time.deltaTime;
                if(cooldown <= 0) {
                    cooldown = 0;
                    state = WeaponState.Idle;
                }
                break;
        }
	}

    public void Aim(Combatant target) {
        transform.LookAt(target.transform);
    }

    public void Fire() {
        if(state == WeaponState.Idle) {
            Projectile projectile = Instantiate(projectilePrefab);

            Entity entity = projectile.GetComponent<Entity>();
            entity.Velocity = transform.forward * firePower / entity.mass;

            state = WeaponState.Firing;
        }
        
    }
}

using UnityEngine;
using System.Collections;

enum WeaponState {
    Idle,
    Firing,
    Cooldown
}

public class Weapon : MonoBehaviour {

    public Projectile projectilePrefab;
    public Transform spawnPoint;

    public float fireRate;
    public float firePower;
    Combatant target;

    float cooldown;
    WeaponState state;

	// Use this for initialization
	void Start () {
	
	}
	
	public void Update () {
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
        //transform.LookAt(target.transform);
        this.target = target;
    }

    public void Fire() {
        if(state == WeaponState.Idle) {
            Projectile projectile = Instantiate(projectilePrefab);

            Vector3 spawnPos = spawnPoint == null ? transform.position : spawnPoint.position;

            Entity entity = projectile.GetComponent<Entity>();
            Vector3 fireDirection = transform.forward;
            if(target != null) {
                Vector3 targetPos = target.transform.position;
                Entity targetEntity = target.GetComponent<Entity>();
                if (targetEntity != null) {
                    targetPos += targetEntity.Velocity * 20 * Time.deltaTime;
                }
                fireDirection = targetPos - spawnPos;
            }
            entity.Velocity = fireDirection.normalized  * (firePower / entity.mass);

            if(projectile.GetComponent<HomingProjectile>() != null) {
                projectile.GetComponent<HomingProjectile>().SetTarget(target);
            }
            
            entity.transform.position = spawnPos;

            state = WeaponState.Firing;
        }
    }

    public bool IsReady() {
        return state == WeaponState.Idle;
    }
}

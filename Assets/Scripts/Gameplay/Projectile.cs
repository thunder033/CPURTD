using UnityEngine;
using System.Collections;

public class Projectile : Entity {

    public Transform explosionPrefab;

    static Vector3 minBounds = new Vector3(-500, -500, -200);
    static Vector3 maxBounds = new Vector3(500, 500, 300);

    override protected void Update() {
        if(OutsideBounds(transform.position, minBounds, maxBounds)) {
            Debug.Log("Destroy at " + transform.position);
            Destroy(gameObject);
        }

        base.Update();
    }

    private bool OutsideBounds(Vector3 position, Vector3 min, Vector3 max) {
        Vector3 p = position;
        return (p.x < min.x || p.y < min.y || p.z < min.z || p.x > max.x || p.y > max.y || p.z > max.z);
    }

    void OnCollisionEnter(Collision collision) {
        Combatant impactee = collision.collider.GetComponent<Combatant>();
        Debug.Log("Impacted object");

        if(impactee != null) {
            foreach(DamageEffect effect in GetComponents<DamageEffect>()) {
                Debug.Log("Apply damage");
                impactee.TakeDamage(effect);
            }
        }

        if(explosionPrefab != null) {
            ContactPoint contact = collision.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;
            Instantiate(explosionPrefab, pos, rot);
        }

        Destroy(gameObject);
    }
}

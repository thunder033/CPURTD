using UnityEngine;
using System.Collections;

public class Projectile : Entity {

    public Transform explosionPrefab;

    void OnCollisionEnter(Collision collision) {
        Combatant impactee = collision.collider.GetComponent<Combatant>();

        if(impactee != null) {
            foreach(DamageEffect effect in GetComponents<DamageEffect>()) {
                impactee.TakeDamage(effect);
            }
        }

        if(explosionPrefab != null) {
            ContactPoint contact = collision.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;
            Instantiate(explosionPrefab, pos, rot);
            Destroy(gameObject);
        }
    }
}

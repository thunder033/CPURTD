using UnityEngine;
using System.Collections;

public class Combatant : MonoBehaviour
{

    public float baseHealth;

    float health;

    Weapon weapon {
        get {
            return GetComponentInChildren<Weapon>();
        }
    }


    // Use this for initialization
    void Start()
    {
        health = baseHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public float PercentHealth()
    {
        return health / baseHealth;
    }

    public float GetHealth()
    {
        return health;
    }

    void Attack(Combatant target)
    {
        if(weapon != null) {
            weapon.Aim(target);
            weapon.Fire();
        }
    }

    public void TakeDamage(DamageEffect effect)
    {
        float damage = effect.baseDamage;

        foreach(DamageModifier modifer in GetComponents<DamageModifier>()) {
            damage = modifer.types.Contains(effect.type) ? modifer.modifier * damage : damage;
        }

        health -= damage;

        Debug.Log("Took Damage: " + PercentHealth());

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}

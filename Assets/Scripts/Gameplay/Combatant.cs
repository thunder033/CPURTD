using UnityEngine;
using System.Collections;

public class Combatant : MonoBehaviour
{

    public float baseHealth;

    float health;
    HealthBar healthBar;
    MeshRenderer renderer;
    bool alive;

    Weapon weapon {
        get {
            return GetComponentInChildren<Weapon>();
        }
    }


    // Use this for initialization
    void Start()
    {
        alive = true;
        health = baseHealth;
        healthBar = ((GameObject)Instantiate(Resources.Load("HealthBar"))).GetComponent<HealthBar>();
        healthBar.SetCombatant(this);
        healthBar.transform.localScale = new Vector3(0.5f, 2.0f, 4.0f);
        renderer = GetComponentInChildren<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.transform.position = new Vector3(transform.position.x, transform.position.y + renderer.bounds.extents.y + 10f, transform.position.z);
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

    public void Die()
    {
        if (!alive)
            return;

        alive = false;
        Destroy(healthBar.gameObject);
        Destroy(gameObject);
    }
}

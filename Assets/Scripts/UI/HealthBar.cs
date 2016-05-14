using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

    Combatant combatant;

    public void SetCombatant(Combatant combatant) {
        this.combatant = combatant;
    }

    public bool HasCombatant() {
        return combatant != null;
    }
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(Camera.main.transform);
        Vector3 rot = transform.localEulerAngles;
        transform.localEulerAngles = new Vector3(0, rot.y + 90, rot.z + 90);

        if (!HasCombatant()) {
            combatant = GetComponentInParent<Combatant>();
        } else {
            Transform current = transform.FindChild("Current");
            current.localScale = new Vector3(.77f, .95f, combatant.PercentHealth() * .95f);
        }   
	}
}

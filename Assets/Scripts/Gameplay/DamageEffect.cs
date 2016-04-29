using UnityEngine;
using System.Collections;

public enum DamageType {
    Base
}

public class DamageEffect : MonoBehaviour {

    public float baseDamage;
    public DamageType type;
}

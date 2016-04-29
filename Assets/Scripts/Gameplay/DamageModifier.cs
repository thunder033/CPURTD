using UnityEngine;
using System.Collections.Generic;

public class DamageModifier {

    public List<DamageType> types = new List<DamageType>(){ DamageType.Base };
    public float modifier = 1;
}

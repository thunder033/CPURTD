using UnityEngine;
using System.Collections.Generic;

public class DamageModifier : Component {

    public List<DamageType> types = new List<DamageType>(){ DamageType.Base };
    public float modifier = 1;
}

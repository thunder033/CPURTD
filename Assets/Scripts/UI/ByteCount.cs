using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

[RequireComponent(typeof(Text))]
public class ByteCount : MonoBehaviour {

    OpsHub hub;
    Text text;

    enum Unit {
        B = 1,
        KB = 1024,
        MB = 1024 * 1024,
        GB = 1024 * 1024 * 1024
    }

	// Use this for initialization
	void Start () {
        hub = FindObjectOfType<OpsHub>();
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(hub != null) {
            float bytes = hub.getBytes();
            var units = Enum.GetValues(typeof(Unit));
            Unit unit = Unit.B;

            var it = units.GetEnumerator();
            while(bytes / 1024.0f > (int)unit) {
                it.MoveNext();
                unit = (Unit)it.Current;
            }

            text.text = string.Format("{0:0.00}", (bytes / (float)unit)) + " " + Enum.GetName(typeof(Unit), unit);
        }

        else {
            text.text = "0 B";
        }
	}
}

using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

[RequireComponent(typeof(Text))]
public class ByteCount : MonoBehaviour {

    OpsHub hub;
    Text text;
    public Field field;

    enum Unit {
        B = 1,
        KB = 1024,
        MB = 1024 * 1024,
        GB = 1024 * 1024 * 1024
    }

    public enum Field {
        Bytes,
        BitRate
    }

	// Use this for initialization
	void Start () {
        hub = FindObjectOfType<OpsHub>();
        text = GetComponent<Text>();
	}

    Unit GetMaxUnit(float number) {
        var units = Enum.GetValues(typeof(Unit));
        Unit unit = Unit.B;

        var it = units.GetEnumerator();
        while (number / 1024.0f > (int)unit) {
            it.MoveNext();
            unit = (Unit)it.Current;
        }

        return unit;
    }

    string FormatSize(float size) {
        Unit unit = GetMaxUnit(size);
        return string.Format("{0:0.00}", (size / (float)unit)) + " " + Enum.GetName(typeof(Unit), unit);
    }

    string FormatRate(float rate) {
        Unit unit = GetMaxUnit(rate);
        return string.Format("{0:0.00}", (rate / (float)unit)) + " " + Enum.GetName(typeof(Unit), unit) + "/s";
    }
	
	// Update is called once per frame
	void Update () {
	    if(hub != null) {
            switch (field) {
                case Field.Bytes:
                    text.text = FormatSize(hub.GetBytes());
                    break;
                case Field.BitRate:
                    text.text = FormatRate(hub.GetBitRate());
                    break;
            }
            
        }

        else {
            text.text = "0 B";
        }
	}
}

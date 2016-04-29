﻿using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

    public Camera camera;
    Combatant combatant;

	// Use this for initialization
	void Start () {
        combatant = GetComponentInParent<Combatant>();
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(camera.transform);
        Vector3 rot = transform.localEulerAngles;
        transform.localEulerAngles = new Vector3(0, rot.y + 90, 90);
        transform.localPosition = new Vector3(0, 2, 0);

        if(combatant != null)
        {
            Transform current = transform.FindChild("Current");
            current.localScale = new Vector3(combatant.PercentHealth(), .95f, .95f);
        }   
	}
}

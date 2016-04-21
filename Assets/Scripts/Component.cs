using UnityEngine;
using System.Collections;

public class Component : MonoBehaviour {

	public float health;
	public float yieldRate = 2;

	// Use this for initialization
	void Start () {
		health = Random.Range(50, 100);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;

public class FixedCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(GameObject.FindGameObjectWithTag("Moving_Player").transform.position.x, transform.position.y, transform.position.z);
	}
}

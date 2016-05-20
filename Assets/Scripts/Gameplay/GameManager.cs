using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public Virus[] minions
    {
        get
        {
            return FindObjectsOfType<Virus>();
        }
    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}

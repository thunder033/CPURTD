using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public Minion[] minions
    {
        get
        {
            return FindObjectsOfType<Minion>();
        }
    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Entity : MonoBehaviour {

	public float mass = 1.0f;
	public float radius = 1.0f;
	public float gravity = 20.0f;

	protected Vector3 acceleration = new Vector3();	//change in velocity per second
	protected Vector3 velocity = new Vector3();		//change in position per second
	
    [SerializeField]
	public Vector3 Velocity
	{
		get { return velocity; }
		set { velocity = value; }
	}

	virtual public void Start()
	{
		acceleration = Vector3.zero;
	}


	// Update is called once per frame
	protected void Update()
	{
        velocity += acceleration * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
	}

	protected void ApplyForce(Vector3 force)
	{
		acceleration += force / mass;
	}

}

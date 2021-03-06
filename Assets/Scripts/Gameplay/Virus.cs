﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum Task
{
	None,
    Explore,
	Attack,
	Build,
	DropOff
}

public enum Strain {
    Basic = 300,
    Worm = 500, 
    Trojan = 700
}

public class Virus : SmartEntity {

    public static float BytesCost = 50;

	public float bytesCost;
	public float attack;

	private Node immediateTarget;
	private Node _target;
	public Node target {
		set
		{
			Node start = Node.FindClosestVisible(this.transform.position);
			Node dest = value;
			path = FindPath(start, dest);

			_target = value;
		}
		get
		{
			return _target;
		}
	}

	public Task task;
	private Path path;

	private OpsHub GM;

	private GameObject[] obstacles;

	// These weights will be exposed in the Inspector window
	public float seekWt = 50.0f;
	public float avoidWt = 100.0f;
	public float avoidDist = 10.0f;
	public float wanderWt = 8.0f;

    private float arrivalDist = 7;
    private float attackDist = 50;

	float stuckCount = 0;
	float stuckLimit = 50;
	Vector3 prevPos;

	// Use this for initialization
	public override void Start () {

		base.Start();
		//gm = GameObject.Find("MainGO").GetComponent<GameManager>();
		GM = GameObject.Find("SpawnPoint").GetComponent<OpsHub>();
        obstacles = GameObject.FindGameObjectsWithTag("Solid");

		task = Task.None;

		//target = GameObject.Find("Waypoint16").GetComponent<Node>();
	}

	// Update is called once per frame
	new public void Update () {


		if(prevPos == transform.position)
		{
			stuckCount++;
			if(stuckCount > stuckLimit)
			{
				Debug.Log("were stuck!");

				Node start = Node.FindClosestVisible(transform.position);
				Node dest = target;

				path = FindPath(start, dest);
				immediateTarget = path.nodes[0].node;
				stuckCount = 0;
			}
		}
		else
		{
			stuckCount = 0;
		}
		prevPos = transform.position;
		GameObject targetComponent;

		//if the drone has a task
		switch (task)
		{
			//determine what to do based on that task

			//If we don't have a task, determine one
			case Task.None:
			
				//if not find an open resource node
				if ((targetComponent = GM.GetTargetComponent()) != null)
				{
					target = targetComponent.GetComponent<Node>();
					task = Task.Attack;
				}
				//if there's no known nodes, explore
				else
				{
					task = Task.Explore;
				}

				//if there's no undiscovered nodes, idle or wander
				break;

			//Explore
			case Task.Explore:
				//Pathfind to nearest unvisited node

				//if we arrive at a newly discovered node, end explore task
				break;
			case Task.Attack:
				//if we have a mineral node and our inventory isn't full
				if(target.GetComponent<BoardComponent>() != null)
				{
					//determine if were near the mineral node
					if ((transform.position - target.position).magnitude < attackDist)
					{
						//gather
						if(!Attack(target.GetComponent<BoardComponent>()))
						{
							task = Task.DropOff;
						}
					}

				}
				//if we don't have an attack target
				else if (target.GetComponent<BoardComponent>() == null && target.GetComponent<OpsHub>() == null)
				{
					//find one or explore
					if ((target = GM.GetTargetComponent().GetComponent<Node>()) != null)
					{
						task = Task.Attack;
					}
					else
					{
						task = Task.Explore;
					}
				}

				break;
			}

		if(path != null && immediateTarget == null)
		{
			immediateTarget = path.nodes[0].node;
			Debug.Log("iTarget:" + immediateTarget.ToString());
		}

        if(path != null && path.nodes.Count > 1)
        {
            for (int n = 1; n < path.nodes.Count; n++)
            {
                Debug.DrawLine(path.nodes[n - 1].node.position, path.nodes[n].node.position, Color.green);
            }
        }

        if(immediateTarget != null)
        {
			double distance = 0;

			if (immediateTarget.gameObject != null && immediateTarget.gameObject.GetComponent<Collider>())
			{
				distance = (immediateTarget.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position) - transform.position).magnitude;
			}
			else
			{
				distance = (immediateTarget.position - transform.position).magnitude;
			}


            if(distance < arrivalDist || stuckCount > stuckLimit)
            {
                Debug.Log("Arrived at target");
                Node start = path.nodes[1].node;
                Node dest = target;

				if(!CanSee(target.position) || stuckCount > stuckLimit)
				{
					path = FindPath(start, dest);
					stuckCount = 0;
				}
				else
				{
					path = new Path();
					path.nodes.Add(new PathNode(target));
				}
                

                immediateTarget = path.nodes[0].node;
            }
        }

		base.Update();

        transform.position = new Vector3(transform.position.x, 1.0f, transform.position.z);
	}

	public bool Attack(BoardComponent node)
	{
        //Weapon weapon = GetComponent<Weapon>();
        //weapon.Aim(node);
        //weapon.Fire();
        node.GetComponent<Combatant>().TakeDamage(GetComponent<DamageEffect>());
        GetComponent<Combatant>().Die();
        return true;
    }

	public bool CanSee(Vector3 pos)
	{
		RaycastHit hit;

		float checkHeight = 10;
		Vector3 raisedStart = new Vector3(transform.position.x, checkHeight, transform.position.z);
		Vector3 raisedDest = new Vector3(pos.x, checkHeight, pos.z);

		//Draw a line from the starting position to the desired node
		Physics.Linecast(raisedStart, raisedDest, out hit);

		//If this line intersects the terrain, it is obstructed and we move on
		if (hit.collider != null)
		{
			return !(hit.collider.GetType() == typeof(TerrainCollider) || hit.collider.CompareTag("Solid"));
		}

		return true;
	}

	public Path FindPath(Node start, Node dest)
	{
		if (start != null && dest != null)
		{
			Path path = new Path();

			Node curNode = null;
			Node nextNode = null;

			int runCount = 0;

			List<Node> open = new List<Node>();
			open.Add(start);
			//Debug.Log(dest.ToString());

			List<Node> closed = new List<Node>();

			while (!closed.Contains(dest))
			{
				runCount++;
				Node cheapest = null;
				//int nodeIndex = 0;

				//cheapest open node
				for (int o = 0; o < open.Count; o++)
				{
					if (cheapest == null || open[o].f < cheapest.f)
					{
						cheapest = open[o];
						//nodeIndex = o;
					}
				}
				curNode = cheapest;
				closed.Add(curNode);

				//if we found the destination, quit searching
				if (curNode == dest)
				{
					Debug.Log("found destination");
					break;
				}


				//remove the node were visiting from open
				open.Remove(curNode);
				if (open.Count == 0 && runCount > 1)
				{
					Debug.Log("Pathfind Failed: no open nodes");
					break;
				}


				for (int c = 0; c < curNode.connections.Length; c++)
				{
					nextNode = curNode.connections[c];

					if (!closed.Contains(nextNode))
					{
						if (!open.Contains(nextNode))
						{
							//total cost to traverse path to this node
							nextNode.g = (nextNode.transform.position - curNode.transform.position).magnitude + curNode.g;
							//distance from node to destination node
							nextNode.h = (dest.transform.position - nextNode.transform.position).magnitude;
							nextNode.f = nextNode.g + nextNode.h;
							nextNode.parent = curNode;

							//add to open nodes
							open.Add(nextNode);
						}
						else
						{
							double g = (nextNode.transform.position - curNode.transform.position).magnitude + curNode.g;

							if (g < nextNode.g)
							{
								nextNode.g = g;
								nextNode.f = g + nextNode.h;
								nextNode.parent = curNode;
							}
						}
					}

					if (nextNode == dest)
					{
						Debug.Log("found destination: " + nextNode.g + " " + nextNode.h + " " + nextNode.f);
					}
				}

				if (open.Count > 30 || runCount > 400)
				{
					Debug.Log("Pathfind failed!");
					break;
				}
			}

			Debug.Log(closed.ToArray().ToString());

			path.nodes.Add(new PathNode(curNode));
			while (curNode.parent != null)
			{
				path.nodes.Insert(0, new PathNode(curNode.parent));
				curNode = curNode.parent;
			}

			Node.ResetAllNodeHeuristics();

			return path;
		}
		else
		{
			return null;
		}
	}

	protected override void CalcSteeringForce()
	{
		Vector3 force = Vector3.zero;

		//Not everyone has wander working
		/*
		if (characterController.velocity.magnitude < 1.0f) {
			force += Wander() * wanderWt;
		} 
		else {*/

		//seek target
		if(immediateTarget != null)
		{
			force += seekWt * Seek(immediateTarget.position);
		}

		//We don't really need flocking
		//-------------- flocking changes ----------------------
		//call other Flocking methods here
		//force += alignmentWt * Alignment(gm.flockDirection);
		//force += separationWt * Separation(gm.Flock, separationDist);
		//force += cohesionWt * Cohesion(gm.Centroid);
		//force += inBoundsWt * StayInBounds(30, new Vector3(0, 1, 0));
		//---------------------------------------------------------

		//avoid obstacles
        for (int i = 0; i < GM.obstacles.Length; i++)
		{
			force += avoidWt * AvoidObstacle(GM.obstacles[i], avoidDist);
		}

		//limit force to maxForce and apply
		force = Vector3.ClampMagnitude(force, maxForce);
        //Debug.Log(force.magnitude);
		ApplyForce(force);

		//show force as a blue line pushing the guy like a jet stream
		Debug.DrawLine(transform.position, transform.position - force, Color.blue);

		if(immediateTarget != null)
		{
			//red line to the target which may be out of sight
			Debug.DrawLine(transform.position, immediateTarget.position, Color.red);
		}
		
	}
}

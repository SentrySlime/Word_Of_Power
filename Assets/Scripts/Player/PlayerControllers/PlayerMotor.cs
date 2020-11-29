using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(PlayerController))]
public class PlayerMotor : MonoBehaviour
{

	public Transform target;
	NavMeshAgent agent;     // Reference to our NavMeshAgent
	public CharacterStats characterStats;

	public float speed;


	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		characterStats = GetComponent<CharacterStats>(); 

		SetCharacterSpeed();
	}

	public void MoveToPoint(Vector3 point)
	{
		agent.SetDestination(point);
	}

	void Update()
	{
		if (target != null)
		{
			MoveToPoint(target.position);
			FaceTarget();
		}
	}


	void FaceTarget()
	{
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
	}

	public void ResetPath()
	{
		agent.ResetPath();
	}

	public void SetCharacterSpeed()
	{

		agent.speed = characterStats.speed/4;
		speed = agent.speed;
	}
}

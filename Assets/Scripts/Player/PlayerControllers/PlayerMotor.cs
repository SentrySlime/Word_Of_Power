using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(PlayerController))]
public class PlayerMotor : MonoBehaviour
{

	public Transform target;
	public NavMeshAgent agent;     // Reference to our NavMeshAgent
	public CharacterStats characterStats;

	public float speed;

	public AudioSource walkSource;
	AnimationController animationController;
	ProjectileShootTriggerable projectileTrigger;

	public List<AudioClip> footStepList = new List<AudioClip>();

	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		characterStats = GetComponent<CharacterStats>();
		animationController = GetComponent<AnimationController>();
		projectileTrigger = GetComponent<ProjectileShootTriggerable>();
		StartCoroutine(SpeedNumerator());
		//SetCharacterSpeed();
	}

	public void MoveToPoint(Vector3 point)
	{
		if (animationController.animator.GetCurrentAnimatorStateInfo(0).IsName("arthur_attack_01"))
		{
			ResetPath();
		}
		else if (animationController.animator.GetCurrentAnimatorStateInfo(0).IsName("arthur_active_01"))
		{
			ResetPath();
		}
		else if (animationController.animator.GetCurrentAnimatorStateInfo(0).IsName("arthur_passive_01"))
		{
			ResetPath();
		}
		else
		{
			agent.SetDestination(point);
		}

		
	}

	void Update()
	{
		if(agent.hasPath)
		{
			animationController.WalkAnimation();
			int randomStep = Random.Range(0, footStepList.Count);
			if (!walkSource.isPlaying)
			{
				projectileTrigger.StopAllCoroutines();
				walkSource.clip = footStepList[randomStep];
				walkSource.Play();
			}
		}
		else
		{
			animationController.IdleAnimation();
			walkSource.Stop();
		}

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
		walkSource.Stop();
	}

	public void SetCharacterSpeed()
	{

		agent.speed = characterStats.finalSpeed;
		speed = agent.speed;
	}

	public IEnumerator SpeedNumerator()
	{
		yield return new WaitForSeconds(.2f);

		SetCharacterSpeed();
	}

}

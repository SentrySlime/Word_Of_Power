using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public string animationName;
    public Animator animator;
    public Animation animation_One;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            TestAnimation();

        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            AttackTwo();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            AttackThree();
        }

    }

    public void IdleAnimation()
    {
        
        animator.SetBool("WalkBool", false);
        animator.SetBool("IdleBool", true);
        animator.ResetTrigger("AttackOneTrigger");
        animator.ResetTrigger("AttackTwoTrigger");
        animator.ResetTrigger("AttackThreeTrigger");
    }

    public void WalkAnimation()
    {
        animator.SetBool("IdleBool", false);
        animator.SetBool("WalkBool", true);
        animator.ResetTrigger("AttackOneTrigger");
        animator.ResetTrigger("AttackTwoTrigger");
        animator.ResetTrigger("AttackThreeTrigger");
    }

    public void AttackOne()
    {

        animator.SetBool("WalkBool", false);
        animator.SetBool("IdleBool", false);
        animator.Play("TestAnimation", 0, 0.5f);
        //animator.SetTrigger("AttackOneTrigger");
    }

    public void AttackTwo()
    {

        animator.SetBool("WalkBool", false);
        animator.SetBool("IdleBool", false);
        animator.SetTrigger("AttackTwoTrigger");
    }

    public void AttackThree()
    {

        animator.SetBool("WalkBool", false);
        animator.SetBool("IdleBool", false);
        animator.SetTrigger("AttackThreeTrigger");
    }

    public void TestAnimation()
    {
        
    }
}

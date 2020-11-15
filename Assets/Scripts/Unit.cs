using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Unit : MonoBehaviour
{
    NavMeshAgent nav;
    public Transform targetToFollow;
    Animator animator;

    const string ANIMATOR_SPEED = "Speed",
        ANIMATOR_DIE = "Die",
        ANIMATOR_ATTACK = "Basic Attack";
     
    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        if(targetToFollow)
        {
            nav.SetDestination(targetToFollow.position);
        }
        Animate();
    }
    protected virtual void Animate()
    {
        var speedVector = nav.velocity;
        speedVector.y = 0;
        float speed = speedVector.magnitude;
        animator.SetFloat(ANIMATOR_SPEED, speed);

    }
}

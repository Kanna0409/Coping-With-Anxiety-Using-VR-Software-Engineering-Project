using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
public class TigerMovement : MonoBehaviour
{
    public float wanderRadius = 400f; 
    public float timeBetweenMoves = 5f; 
    private float timer;
    private NavMeshAgent navMeshAgent;
    private Animator tigerAnimator;
    void Start(){
        navMeshAgent=GetComponent<NavMeshAgent>();
        tigerAnimator=GetComponent<Animator>();
        timer=timeBetweenMoves;
    }
    void Update(){
        timer-=Time.deltaTime;
        if(timer<=0){
            MoveToRandomPosition();
            timer = timeBetweenMoves;
        }
        if(navMeshAgent.remainingDistance>0){
               tigerAnimator.SetBool("IsWalking",true);
               tigerAnimator.SetBool("IsIdle", false);
        }
        else{
            tigerAnimator.SetBool("IsWalking",false);
            tigerAnimator.SetBool("IsIdle",true);
        }
    }
    void MoveToRandomPosition(){
        Vector3 randomDirection=Random.insideUnitSphere*wanderRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        if(NavMesh.SamplePosition(randomDirection,out hit,wanderRadius,NavMesh.AllAreas)){
            navMeshAgent.SetDestination(hit.position);
        }
    }
}


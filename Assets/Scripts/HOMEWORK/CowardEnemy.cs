using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CowardEnemy : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    Animator animator;
    Target[] targets;

    [SerializeField] Transform target;
    [SerializeField] Transform playerPosition;
    [SerializeField] Vector3 currentPlayerPosition;
    [SerializeField] Target farthestTarget;
    [SerializeField] float farthestDistance;
    [SerializeField] bool hasPath;
    [SerializeField] float rotationSpeed;


    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        targets = target.GetComponentsInChildren<Target>(false);
        currentPlayerPosition = playerPosition.position;
        SelectNewTarget();
    }

    // Update is called once per frame
    void Update()
    {
        hasPath = navMeshAgent.hasPath;
        if (hasPath)
        {
            FaceDestination();
        }

        GameObject playerObject = GameObject.Find("Player");
        currentPlayerPosition = playerObject.transform.position;
        SelectNewTarget();
    }

    private void SelectNewTarget()
    {
        farthestDistance = 0;
        foreach(Target target in targets)
        {
            float targetDisatance = Vector3.Distance(target.transform.position, currentPlayerPosition);
            if (targetDisatance > farthestDistance)
            {
                farthestDistance = targetDisatance;
                farthestTarget = target;
            }
        }

        navMeshAgent.SetDestination(farthestTarget.transform.position);
    }

    private void FaceDestination()
    {
        Vector3 directionToDestination = (navMeshAgent.destination - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToDestination.x, 0, directionToDestination.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }
}

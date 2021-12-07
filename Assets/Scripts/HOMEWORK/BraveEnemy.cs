using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BraveEnemy : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    Animator animator;
    Target[] targets;
    float closestDistance;

    [SerializeField] Transform target;
    [SerializeField] Transform playerPosition;
    [SerializeField] Vector3 currentPlayerPosition;
    [SerializeField] Target closestTarget;
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

    void SelectNewTarget()
    {
        closestDistance = 10000000;
        foreach(Target target in targets)
        {
            float targetDistance = Vector3.Distance(target.transform.position, currentPlayerPosition);
            if(targetDistance < closestDistance)
            {
                closestDistance = targetDistance;
                closestTarget = target;
            }
        }

        navMeshAgent.SetDestination(closestTarget.transform.position);
    }

    void FaceDestination()
    {
        Vector3 directionToPosition = (navMeshAgent.destination - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPosition.x, 0, directionToPosition.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }
}

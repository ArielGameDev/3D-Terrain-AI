using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasingEnemy : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    Animator animator;

    [SerializeField] Transform playerPosition;
    [SerializeField] Vector3 currentPlayerPosition;
    [SerializeField] bool hasPath;
    [SerializeField] float rotationSpeed;


    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

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
        navMeshAgent.SetDestination(currentPlayerPosition);
    }

    private void FaceDestination()
    {
        Vector3 directionToDestination = (navMeshAgent.destination - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToDestination.x, 0, directionToDestination.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EngineEnemy : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    Animator animator;

    [SerializeField] Transform enginePosition;
    [SerializeField] float rotationSpeed;
    [SerializeField] Transform enemyPosition;
    [SerializeField] Vector3 currentEnemyPosition;
    [SerializeField] float maxDistance;
    [SerializeField] GameObject engineObject;
    [SerializeField] GameObject enemyObject;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        currentEnemyPosition = enemyPosition.position;
        SelectTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if (navMeshAgent.hasPath)
        {
            FaceDestination();
        }

        currentEnemyPosition = enemyPosition.transform.position;
        float distatnce = Vector3.Distance(currentEnemyPosition, enginePosition.position);

        if(distatnce <= maxDistance)
        {
            Destroy(enemyObject);
            Destroy(engineObject);
        }
    }

    void SelectTarget()
    {
        navMeshAgent.SetDestination(enginePosition.position);
    }

    void FaceDestination()
    {
        Vector3 directionToDestination = (navMeshAgent.destination - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToDestination.x, 0, directionToDestination.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }
}

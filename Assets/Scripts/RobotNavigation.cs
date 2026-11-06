using UnityEngine;
using UnityEngine.AI;

public class RobotNavigation : MonoBehaviour
{
    public Transform mixingPoint;

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void GoToMixingPoint()
    {
        if (mixingPoint != null)
        {
            agent.SetDestination(mixingPoint.position);
        }
    }

    public bool HasReachedMixingPoint()
    {
        if (agent.pathPending)
            return false;

        return agent.remainingDistance <= agent.stoppingDistance;
    }
}
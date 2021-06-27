using UnityEngine;
using UnityEngine.AI;

namespace NavMeshCallbackSystem
{
    public enum NavMeshAgentStatus
    {
        CALCULATING_PATH,
        PATH_CALCULATED,
        PATH_FINISHED
    }
    [RequireComponent(typeof(NavMeshAgent))]
    public class TrackableNavMeshAgent : MonoBehaviour
    {
        private NavMeshAgent agent;
        public Vector3 Velocity => agent.velocity;
        public float RemainingDistance => agent.remainingDistance;

        public NavMeshAgentCallback onPathStart;
        public NavMeshAgentCallback onPathChanged;
        public NavMeshAgentCallback onPathComplete;

        private NavMeshAgentStatus status = NavMeshAgentStatus.PATH_FINISHED;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            switch (status)
            {
                case NavMeshAgentStatus.CALCULATING_PATH:
                    if (!agent.pathPending)
                    {
                        onPathStart?.Invoke();
                        onPathStart = null;
                        status = NavMeshAgentStatus.PATH_CALCULATED;
                    }
                    break;
                case NavMeshAgentStatus.PATH_CALCULATED:
                    if (agent.remainingDistance <= agent.stoppingDistance)
                    {
                        if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                        {
                            onPathComplete?.Invoke();
                            onPathComplete = null;
                            status = NavMeshAgentStatus.PATH_FINISHED;
                        }
                    }
                    break;
            }
        }

        public bool SetDestination(Vector3 target)
        {
            if(status != NavMeshAgentStatus.PATH_FINISHED)
            {
                onPathChanged?.Invoke();
                onPathChanged = null;
            }
            bool result = agent.SetDestination(target);
            if (result) status = NavMeshAgentStatus.CALCULATING_PATH;
            return result;
        }
    }
}


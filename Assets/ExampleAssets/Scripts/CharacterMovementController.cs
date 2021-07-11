using NavMeshCallbackSystem;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    [SerializeField] private RaycastProvider raycastProvider;
    private TrackableNavMeshAgent agent;
    private Animator animator;
    private string characterState = "Idle";
    private void Awake()
    {
        agent = GetComponent<TrackableNavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (raycastProvider.RayFromCameraInteractable(out Interactable interactable))
            {
                agent.SetDestination(interactable.transform.position);
                agent
                    .OnPathStart   (() => characterState = "Running towards: " + interactable.gameObject.name)
                    .OnPathComplete(() => characterState = "Arrived to: " + interactable.gameObject.name);
            }else if (raycastProvider.RayFromCamera(out RaycastHit hit))
            {
                agent.SetDestination(hit.point);
                agent
                    .OnPathStart   (() => characterState = "Starting a path with length of: " + agent.RemainingDistance)
                    .OnPathChanged (() => characterState = "Changing path") 
                    .OnPathComplete(() => characterState = "Path completed!");
            }
        }

        animator.SetBool("Run", agent.Velocity.sqrMagnitude > 0f);
    }

    void OnGUI()
    {
        GUI.Box(new Rect(200, 200, 300, 25), characterState);
    }
}

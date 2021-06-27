using NavMeshCallbackSystem;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    [SerializeField] private RaycastProvider raycastProvider;
    private TrackableNavMeshAgent agent;
    private Animator animator;
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
                    .OnPathStart(() => print("Running towards: " + interactable.gameObject.name))
                    .OnPathComplete(() => interactable.Interact());
            }else if (raycastProvider.RayFromCamera(out RaycastHit hit))
            {
                agent.SetDestination(hit.point);
                agent
                    .OnPathStart   (() => print("Starting a path with length of: " + agent.RemainingDistance))
                    .OnPathChanged (() => print("Changing path"))
                    .OnPathComplete(() => print("Path completed!"));
            }
        }

        animator.SetBool("Run", agent.Velocity.sqrMagnitude > 0f);
    }
}

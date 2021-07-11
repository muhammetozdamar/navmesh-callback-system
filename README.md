# NavMesh-Callback-System
Simple runtime callbacks for Unity NavMesh system and NavMeshAgent.

## Motivation
In top-down games that use the NavMesh system, as developers, we have to constantly track the state of the NavMeshAgent. This creates a bit of need, such as calling functions and taking action when certain events occur. The simplest of these actions is to call a function when the NavMeshAgent completes a path. For example, when we click on a chest far from the NavMeshAgent character, we expect the character to first generate a path using the NavMesh system, then complete the path and interact with the chest. NavMeshAgentCallbackSystem solves this problem using native C# events.

## Usage
Import  `NavMeshCallbackSystem`  namespace to be able to use extensions for callbacks. Add TrackableNavMeshAgent component to your agent. If your agent doesn't have NavMeshAgent component it will be added automatically. 
Then simply reference the TrackableNavMeshAgent component and start adding callbacks to spesific events. You can also chain callbacks.

## Example
Example scene with simple top-down NavMeshAgent character that can move to the mouse position on click, interacts with the clicked  interactable object.

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



https://user-images.githubusercontent.com/39065803/125205583-9e5a9480-e28b-11eb-8f77-54c0eb0785db.mp4


using UnityEngine;

public class RaycastProvider : MonoBehaviour
{
    [SerializeField] private LayerMask movableLayerMask;
    [SerializeField] private LayerMask interactableLayerMask;

    private Camera mainCam;
    private void Start()
    {
        mainCam = Camera.main;
    }
    
    public bool RayFromCamera(out RaycastHit hit, float maxDistance = 100f)
    {
        if (Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition),out hit, maxDistance, movableLayerMask))
        {
            return true;
        }
        return false;
    }

    public bool RayFromCameraInteractable(out Interactable interactable, float maxDistance = 100f)
    {
        if (Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, maxDistance, interactableLayerMask))
        {
            if (hit.transform.TryGetComponent<Interactable>(out interactable))
            {
                return true;
            }
        }
        interactable = null;
        return false;
    }
}

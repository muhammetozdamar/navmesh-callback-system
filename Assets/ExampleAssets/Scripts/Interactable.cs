using UnityEngine;

public class Interactable : MonoBehaviour
{
    public virtual void Interact()
    {
        print("Interacting with " + gameObject.name);
    }
}

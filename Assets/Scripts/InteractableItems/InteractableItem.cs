using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour, IInteractable
{
    InteractableData beforeInteraction;
    InteractableData afterInteraction;

    GameObject childObject;
    // Start is called before the first frame update
    void Start()
    {
        childObject = Instantiate(beforeInteraction.prefab);
        childObject = GetComponentInChildren<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        Destroy(childObject);
        childObject = Instantiate(afterInteraction.prefab);
    }
}

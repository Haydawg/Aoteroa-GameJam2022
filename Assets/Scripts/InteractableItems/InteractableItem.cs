using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour, IInteractable
{
    [SerializeField]
    InteractableData beforeInteraction;
    [SerializeField]
    InteractableData afterInteraction;

    GameObject childObject;
    // Start is called before the first frame update
    void Start()
    {
        childObject = Instantiate(beforeInteraction.prefab, transform.position, Quaternion.identity, transform);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        Destroy(childObject);
        childObject = Instantiate(afterInteraction.prefab, transform.position, Quaternion.identity, transform);
    }
}

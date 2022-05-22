using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    [SerializeField]
    public EquipableItem[] equipableItems;
    [SerializeField]
    public EquipableItem currentItem;
    [SerializeField]
    public Transform itemHandTransform;
    [SerializeField]
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void TakeHit(float damage)
    {

    }
}

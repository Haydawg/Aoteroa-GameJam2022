using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipableItem : MonoBehaviour
{
    public Character player;
    public bool isEquiped;
    public MeshRenderer mesh;
    public GameObject parent;
    

    private void Start()
    {
        isEquiped = false;
        
       
    }

    private void Update()
    {
        parent.transform.position = player.itemHandTransform.position;
        parent.transform.rotation = player.itemHandTransform.rotation;
        mesh.enabled = isEquiped;
    }

    public virtual void Draw()
    {

    }

    public virtual void Sheath()
    {

    }
    public virtual void Equip()
    {

        isEquiped = true;
    }

    public virtual void Unequip()
    {
        isEquiped = false;
        player.currentItem = null;
    }

    public virtual void Attack()
    {

    }
}

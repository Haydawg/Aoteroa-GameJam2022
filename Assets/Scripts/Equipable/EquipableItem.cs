using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipableItem : MonoBehaviour
{
    public PlayerController player;
    public bool isEquiped;
    public MeshRenderer mesh;

    private void Start()
    {
        mesh = GetComponentInChildren<MeshRenderer>();
        isEquiped = false;
        player = FindObjectOfType<PlayerController>();
       
    }

    private void Update()
    {
        transform.position = player.itemHandTransform.position;
        transform.rotation = player.itemHandTransform.rotation;
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

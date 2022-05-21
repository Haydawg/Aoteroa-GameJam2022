using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipableItem : MonoBehaviour
{
    public PlayerController player;
    public bool isEquiped;
    MeshRenderer mesh;

    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        isEquiped = false;
        player = FindObjectOfType<PlayerController>();

    }

    private void Update()
    {
        mesh.enabled = isEquiped;
    }
    public virtual void Equip()
    {

    }

    public virtual void Unequip()
    {

    }

    public virtual void Attack()
    {

    }
}

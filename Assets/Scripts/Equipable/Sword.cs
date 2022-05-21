using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : EquipableItem
{
    [SerializeField]
    Weapon weapon;


    public override void Equip()
    {
        player.currentItem = this;
        isEquiped = true;
    }

    public override void Unequip()
    {
        player.currentItem = null;
        isEquiped = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<NpcController>())
        {
            NpcController npc = collision.gameObject.GetComponent<NpcController>();
            npc.TakeHit(weapon.damage);

        }
    }
}

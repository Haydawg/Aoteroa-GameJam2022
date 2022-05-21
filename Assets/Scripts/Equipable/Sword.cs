using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : EquipableItem
{
    [SerializeField]
    Weapon weapon;

    public override void Draw()
    {
        player.currentItem = this;
        player.anim.ResetTrigger("DrawSword");
        player.anim.SetTrigger("DrawSword");

    }
    public override void Sheath()
    {

        player.anim.ResetTrigger("SheathSword");
        player.anim.SetTrigger("SheathSword");

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

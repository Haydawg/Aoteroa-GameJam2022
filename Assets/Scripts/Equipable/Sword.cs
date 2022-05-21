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

    public override void Attack()
    {
        player.anim.ResetTrigger("Attack");
        player.anim.SetTrigger("Attack");
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if(collision.gameObject.GetComponent<NpcController>())
        {
            NpcController npc = collision.gameObject.GetComponent<NpcController>();
            npc.TakeHit(weapon.damage);

        }
    }
}

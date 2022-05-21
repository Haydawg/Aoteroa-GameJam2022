using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGun : EquipableItem
{

    public bool portalReady = false;

    [SerializeField]
    GameObject portalPrefab;
    // Start is called before the first frame update
    public override void Draw()
    {
        player.currentItem = this;
        player.anim.ResetTrigger("DrawGun");
        player.anim.SetTrigger("DrawGun");

    }
    public override void Sheath()
    {
        player.anim.ResetTrigger("SheathGun");
        player.anim.SetTrigger("SheathGun");

    }

    public override void Attack()
    {
        if(portalReady)
        {
            RaycastHit hit;
            if(Physics.Raycast(player.transform.position, player.transform.forward, out hit , 100))
            {
                Instantiate(portalPrefab, hit.point, player.transform.rotation *= Quaternion.Euler(0, 180, 0));
            }
        }
    }
}

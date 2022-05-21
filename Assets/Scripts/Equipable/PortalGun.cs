using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGun : EquipableItem
{

    public bool portalReady = false;

    [SerializeField]
    GameObject portalPrefab;
    // Start is called before the first frame update

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

    public override void Attack()
    {
        Debug.Log('1');
        if(portalReady)
        {
            RaycastHit hit;
            if(Physics.Raycast(player.transform.position, player.transform.forward, out hit , 100))
            {
                Debug.Log(hit.collider.name);
                Instantiate(portalPrefab, hit.point, player.transform.rotation *= Quaternion.Euler(0, 180, 0));
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryConsumer : MonoBehaviour {

	// Use this for initialization
	void Start () {
        EventManager.instance.ItemConsumed += Consume;
    }

    void Consume(DBinventoryItem item) {
        switch (item.category)
        {
            case INVENTORYITEM_CATEGORY.stamina:
                EventManager.instance.DispatchChargeStamina(item);
                break;
            case INVENTORYITEM_CATEGORY.ammo:
                EventManager.instance.DispatchChargeAmmo(item.type, (int)item.amount, true);
                break;
            case INVENTORYITEM_CATEGORY.bomb:
                DBinventoryBomb bomb = (DBinventoryBomb)item;
                EventManager.instance.DispatchChargeBomb(item.type, item.amount, bomb.radius, bomb.duration);
                break;
            case INVENTORYITEM_CATEGORY.shield:
                EventManager.instance.DispatchChargeShield(item.type, item.amount, true);
                break;
            case INVENTORYITEM_CATEGORY.weapon:
                EventManager.instance.DispatchChargeWeapon(item.type, (int)item.amount, true);
                break;
            default:
                break;
        }
    }
}

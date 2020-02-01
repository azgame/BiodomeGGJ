using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource: MonoBehaviour, IInteractable
{
    
    // Is a player with inventory space close?
    bool isInProximity = false;
    bool isActive = true;

    bool wasConsumed = false;

    InventoryItem inventoryType = InventoryItem.RED;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.tag = "InteractiveObject";
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(this.transform.position);
    }

    public bool wasTriggered(IInteractable inventory)
    {
        if (inventory == null) {
            this.isInProximity = true;
            return true;
        }
        return false;
    }

    public void wasUntriggered()
    {
        this.isInProximity = false;
    }

    public IInteractable activated(IInteractable inventory)
    {
        if (inventory == null) {
            this.isActive = true;
            return this;
        }
        return null;
    }

    public bool deactivated()
    {
        this.isActive = false;
        return wasConsumed;
    }

    public InventoryItem getInventoryType()
    {
        return this.inventoryType;
    }

    public void consumed()
    {
        this.wasConsumed = true;
    }


}

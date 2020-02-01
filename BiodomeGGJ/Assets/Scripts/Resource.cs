using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource: MonoBehaviour, IInteractable
{
    MeshRenderer mr;

    // Is a player with inventory space close?
    bool isInProximity = false;
    bool isActive = true;

    bool wasConsumed = false;

    InventoryItem inventoryType = InventoryItem.RED;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.tag = "Resource";
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.parent != null)
        { 
            this.transform.position = this.transform.parent.position;
        }
    }

    public void SetInventoryType(InventoryItem type)
    {
        mr = GetComponent<MeshRenderer>();
        inventoryType = type;
        switch (type)
        {
            case InventoryItem.RED:
                mr.material = Resources.Load("Materials/RedMat", typeof(Material)) as Material;
                break;
            case InventoryItem.GREEN:
                mr.material = Resources.Load("Materials/GreenMat", typeof(Material)) as Material;
                break;
            case InventoryItem.BLUE:
                mr.material = Resources.Load("Materials/BlueMat", typeof(Material)) as Material;
                break;
            default:
                break;
        }
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

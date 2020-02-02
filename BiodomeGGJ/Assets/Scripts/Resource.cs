using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceStates
{
    SPAWNING,
    INUSE,
    DROPPED
}

public class Resource: MonoBehaviour, IInteractable
{
    MeshRenderer mr;

    // Is a player with inventory space close?
    bool isInProximity = false;
    bool isActive = true;

    bool wasConsumed = false;

    InventoryItem inventoryType;
    ResourceStates rState;

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

    public void SetInventoryType(ResourceStates state, InventoryItem type)
    {
        mr = GetComponent<MeshRenderer>();
        inventoryType = type;
        switch (type)
        {
            case InventoryItem.RED:
                UpdateMaterial(state, "RedMat");
                break;
            case InventoryItem.GREEN:
                UpdateMaterial(state, "GreenMat");
                break;
            case InventoryItem.BLUE:
                UpdateMaterial(state, "BlueMat");
                break;
            default:
                break;
        }

        //mr.material.color = new Color(mr.material.color.r, mr.material.color.g, mr.material.color.b, 0.5f);
        rState = state;
    }

    void UpdateMaterial(ResourceStates state, string matPath)
    {
        mr = GetComponent<MeshRenderer>();
        switch (state)
        {
            case ResourceStates.SPAWNING:
                mr.material = Resources.Load("Materials/" + matPath + "HalfA", typeof(Material)) as Material;
                break;
            case ResourceStates.INUSE:
                mr.material = Resources.Load("Materials/" + matPath, typeof(Material)) as Material;
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
        if (inventory == null && rState != ResourceStates.SPAWNING) {
            this.isActive = true;
            return this;
        }
        return null;
    }

    public bool deactivated()
    {
        this.isActive = false;
        if (wasConsumed) {
            Destroy(this.gameObject);
        }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// FIXME! A bunch of this should be refactored with Tower!
public class Refinery: MonoBehaviour, IInteractable
{
    // Is a player with inventory space close?
    bool isInProximity = false;
    bool isActive = true;

    InventoryItem[] refinerySlots = new InventoryItem[3];

    int fillCount = 0;

    List<Image> fillUI = new List<Image>();

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void initialize()
    {
        foreach(Transform child in this.transform) {
            if (child.tag == "FillUI") {
                foreach(Transform gChild in child) {
                    fillUI.Add(gChild.GetComponent<Image>());
                }
            }
        }
    }

    private void onFill(InventoryItem resourceType) {
        this.refinerySlots[this.fillCount] = resourceType;
        Color newColor = new Color();
        switch (resourceType) {
            case InventoryItem.RED:
                {
                    newColor = new Color(255, 0, 0, 100);
                    break;
                }
            case InventoryItem.GREEN:
                {
                    newColor = new Color(0, 255, 0, 100);
                    break;
                }
            case InventoryItem.BLUE:
                {
                    newColor = new Color(0, 0, 255, 100);
                    break;
                }
        }
        this.fillUI[this.fillCount].color = newColor;
        this.fillCount++;
        if (this.fillCount == 3) {
            this.onFillComplete();
        }

    }

    private void onFillComplete() {
        //
        this.fillCount = 0;
    }

    public bool wasTriggered(IInteractable inventory)
    {
        if (inventory.getInventoryType() != InventoryItem.TOWER) {
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
        if (inventory.getInventoryType() != InventoryItem.TOWER) {
            this.isActive = true;
            this.onFill(inventory.getInventoryType());
            inventory.consumed();

        }
        return null;
    }

    public bool deactivated()
    {
        return false;
    }

    public InventoryItem getInventoryType()
    {
        // WOMP
        return InventoryItem.TOWER;
    }

    public void consumed()
    {
        // Nope
    }
}

using UnityEngine;
public interface IInteractable //input interface for ACT game
{

    bool wasTriggered(IInteractable item);

    void wasUntriggered();

    IInteractable activated(IInteractable item);

    // returns true if consumed;
    bool deactivated();

    void consumed();
    InventoryItem getInventoryType();
}
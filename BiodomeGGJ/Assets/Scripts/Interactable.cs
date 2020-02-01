public interface IInteractable //input interface for ACT game
{

    bool wasTriggered(IInteractable item);

    void wasUntriggered();

    IInteractable activated(IInteractable item);

    void deactivated();

    void consumed();
    InventoryItem getInventoryType();
}
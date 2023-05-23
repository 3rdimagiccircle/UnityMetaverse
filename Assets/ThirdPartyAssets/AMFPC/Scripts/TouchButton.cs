using UnityEngine.Events;

public class TouchButton : PickupItem
{
    public UnityEvent OnInteract;
    public override void Interact()
    {
        OnInteract?.Invoke();
    }
}

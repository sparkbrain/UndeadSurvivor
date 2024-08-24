using UnityEngine;

public class UpPicker : MonoBehaviour
{
    public float pickupRange;
    public float pickupMovementSpeed;
    public float pickupInterval;
    public LayerMask pickupLayerMask;

    [SerializeField] private Health _health;
    [SerializeField] private PlayerFire _attack;
    [SerializeField] private ExperienceManager _levelManager;

    private void Awake()
    {
        InvokeRepeating(nameof(CheckForPickups), 0, pickupInterval);
    }

    private void CheckForPickups()
    {
        var hits = Physics2D.OverlapCircleAll(transform.position, pickupRange, pickupLayerMask);
        foreach (var hit in hits)
        {
            hit.GetComponent<IPickupable>().PickUp(this);
        }
    }

    public void GrantPickup(PickupSettings pickup)
    {
        switch (pickup._type)
        {
            case PickupType.HEALTH:
                _health.RestoreHealth(pickup.value);
                break;
            case PickupType.AMMO:
                _attack.GiveAmmo(pickup.value);
                break;
            case PickupType.EXPERIENCE:
                _levelManager.GiveExperience(pickup.value);
                break;
        }
    }
}

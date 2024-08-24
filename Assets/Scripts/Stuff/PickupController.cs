using UnityEngine;

public class PickupController : MonoBehaviour
{
    [SerializeField] private float _pickupRange;
    [SerializeField] private float _pickupInterval;
    [SerializeField] private LayerMask _pickupLayerMask;

    [SerializeField] private Health _health;
    [SerializeField] private PlayerFire _attack;
    [SerializeField] private ExperienceController _levelManager;

    private void Awake()
    {
        InvokeRepeating(nameof(CheckForPickups), 0, _pickupInterval);
    }

    private void CheckForPickups()
    {
        var hits = Physics2D.OverlapCircleAll(transform.position, _pickupRange, _pickupLayerMask);
        foreach (var hit in hits)
        {
            hit.GetComponent<IPickupable>().PickUp(this);
        }
    }

    public void GrantPickup(PickupSettings pickup)
    {
        switch (pickup.pickupType)
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

using System.Collections;
using UnityEngine;


public enum PickupType
{
    EXPERIENCE,
    HEALTH,
    AMMO,
}

public class Pickup : MonoBehaviour, IPickupable
{
    [SerializeField] PickupSettings _pickupSettings;
    [SerializeField] private float pickupSpeed;

    private UpPicker target;


    public void PickUp(UpPicker picker)
    {
        gameObject.layer = 0;

        target = picker;
        StartCoroutine(PickUpSequence());
    }

    private IEnumerator PickUpSequence()
    {
        float distance = Mathf.Infinity;

        while (distance > Mathf.Epsilon)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, pickupSpeed * Time.deltaTime);
            distance = (transform.position - target.transform.position).sqrMagnitude;

            yield return null;
        }

        target.GrantPickup(_pickupSettings);
        Destroy(gameObject);
    }
}
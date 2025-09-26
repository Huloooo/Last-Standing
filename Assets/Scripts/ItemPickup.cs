using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Transform holdPoint;
    private GameObject heldItem;
    private GameObject itemToPickup;
    private bool canPickup = false;

    void Update()
    {
        if (canPickup && Input.GetKeyDown(KeyCode.E) && heldItem == null)
        {
            Debug.Log("Picked up item: " + itemToPickup.name);
            PickupItem();
        }
        else if (Input.GetKeyDown(KeyCode.R) && heldItem != null)
        {
            Debug.Log("Dropped item: " + heldItem.name);
            DropItem();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            Debug.Log("Entered trigger: " + other.name);
            itemToPickup = other.gameObject;
            canPickup = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            Debug.Log("Exited trigger: " + other.name);
            canPickup = false;
            itemToPickup = null;
        }
    }

    void PickupItem()
    {
        heldItem = itemToPickup;
        heldItem.transform.SetParent(holdPoint);
        heldItem.transform.localPosition = Vector3.zero;
        heldItem.transform.localRotation = Quaternion.identity;

        Rigidbody rb = heldItem.GetComponent<Rigidbody>();
        if (rb) rb.isKinematic = true;
    }

    void DropItem()
    {
        heldItem.transform.SetParent(null);

        Rigidbody rb = heldItem.GetComponent<Rigidbody>();
        if (rb) rb.isKinematic = false;

        heldItem = null;
    }
}

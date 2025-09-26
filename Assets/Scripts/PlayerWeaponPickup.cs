using UnityEngine;

public class PlayerWeaponPickup : MonoBehaviour
{
    public float pickupRange = 2f;
    public Transform weaponHolder; // empty object in front of camera
    private Camera playerCam;
    private GameObject currentWeapon;

    void Start()
    {
        playerCam = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPickup();
        }
    }

    void TryPickup()
    {
        Ray ray = new Ray(playerCam.transform.position, playerCam.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            if (hit.collider.CompareTag("Weapon"))
            {
                // If already holding something, drop it
                if (currentWeapon != null)
                {
                    Destroy(currentWeapon);
                }

                // Spawn the weapon in the holder
                GameObject weaponPrefab = hit.collider.gameObject;
                currentWeapon = Instantiate(weaponPrefab, weaponHolder);
                currentWeapon.transform.localPosition = Vector3.zero;
                currentWeapon.transform.localRotation = Quaternion.identity;
                currentWeapon.transform.localScale = Vector3.one;

                Destroy(hit.collider.gameObject); // remove from world
                Debug.Log("Picked up weapon: " + currentWeapon.name);
            }
        }
    }
}

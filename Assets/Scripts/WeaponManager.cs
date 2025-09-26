using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Transform weaponHolder;       // empty GameObject under the camera
    public GameObject startingWeapon;    // assign Glock prefab here
    private GameObject currentWeapon;

    void Start()
    {
        if (startingWeapon != null)
        {
            EquipWeapon(startingWeapon);
        }
    }

    public void EquipWeapon(GameObject weaponPrefab)
    {
        // Remove old weapon
        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
        }

        // Spawn weapon into holder
        currentWeapon = Instantiate(weaponPrefab, weaponHolder);
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.identity;
        currentWeapon.transform.localScale = Vector3.one;

        Debug.Log("Equipped: " + currentWeapon.name);
    }

    public GameObject GetCurrentWeapon()
    {
        return currentWeapon;
    }
}

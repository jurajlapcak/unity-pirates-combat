using System;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public UnitController myUnitController;
    public GameObject owner;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == owner || collision.gameObject.GetComponent<UnitController>() == null)
        {
            return;
        }

        try
        {
            if (myUnitController.isAttacking)
            {
                collision.gameObject.GetComponent<UnitController>().TakeDamage(myUnitController);
            }
        }
        catch (NullReferenceException e)
        {
            Debug.Log(collision.gameObject.name + " | " + e.Message);
        }
    }
}
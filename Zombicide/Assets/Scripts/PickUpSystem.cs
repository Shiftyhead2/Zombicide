using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSystem : MonoBehaviour
{
    public GameObject Gun;
    public int GunStoredAmmo;
    public bool HasBeenPickedUp = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("We collided with " + collision.name);
        if (collision.CompareTag("Player"))
        {
            GameObject WeaponHolder = GameObject.FindGameObjectWithTag("WeaponHolder");
            if(WeaponHolder != null)
            {
                //Debug.Log("Player has a weapon holder");
                GameObject gun = GameObject.FindGameObjectWithTag("Weapon");
                if(gun == null){
                    //Debug.Log("Player has no weapons equipped");
                    gun = Instantiate(Gun, WeaponHolder.transform.position, WeaponHolder.transform.rotation) as GameObject;
                    gun.transform.SetParent(WeaponHolder.transform);
                    //Debug.Log(gun.GetComponent<Weapon>().Weaponstats.name + " equipped");
                    if(HasBeenPickedUp)
                    {
                        gun.GetComponent<Weapon>().Ammo = GunStoredAmmo;
                    }
                    
                    Destroy(this.gameObject);
                }
                else
                {
                    //Debug.Log("You already have a weapon equipped. It's " + gun.name);
                    return;
                }
                
                
            }
        }
    }
}

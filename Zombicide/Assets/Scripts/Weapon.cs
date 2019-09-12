using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponStats Weaponstats;
    public Transform ShootPoint;
    public GameObject BulletPrefab;
    public GameObject GunPickupPrefab;
    public float force = 20f;

    private float FireRate;
    public int Ammo;
    private float Damage;


    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("This weapon has " + Ammo + " ammo in the magazine");
        FireRate = Weaponstats.firerate;
        Damage = Weaponstats.damage;
       
        
    }

    // Update is called once per frame
    void Update()
    {
        FireRate -= Time.deltaTime;
        if(Input.GetButton("Fire1") && FireRate <= 0f && Ammo != 0) {

            Shoot();

        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ThrowWeapon();
        }
        
    }

    void Shoot()
    {
       GameObject bullet = Instantiate(BulletPrefab, ShootPoint.position, ShootPoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(ShootPoint.up * force, ForceMode2D.Impulse);
        FireRate = Weaponstats.firerate;
        Ammo--;
    }

    void ThrowWeapon()
    {
        GameObject Weaponpickup = Instantiate(GunPickupPrefab, ShootPoint.position, ShootPoint.rotation);
        Weaponpickup.GetComponent<PickUpSystem>().GunStoredAmmo = Ammo;
        Weaponpickup.GetComponent<PickUpSystem>().HasBeenPickedUp = true;
        Destroy(this.gameObject);
    }
}

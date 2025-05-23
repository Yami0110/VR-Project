using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public GameObject projectile;
    public float power = 10.0f;
    public GameObject shootPoint;
    public GameObject grabPoint;

    public void Shoot() {
        GameObject newProjectile = Instantiate(projectile, 
                                    shootPoint.transform.position, 
                                    shootPoint.transform.rotation) as GameObject;

        newProjectile.GetComponent<Rigidbody>().AddForce(
                                                    grabPoint.transform.forward * power, 
                                                    ForceMode.VelocityChange);
    }

}

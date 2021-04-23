using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
  public float damg = 20;

  public float fireRate = 1f;
  public AudioSource gunSound;
  public Camera FPSCam;
  public ParticleSystem muzzleFlash;
  public GameObject impactEffect;
  private float nextTimeToFire = 0f;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
    {
      nextTimeToFire = Time.time + 1f / fireRate;
      gunSound.Play();
      Shooting(damg);
    }

    }
  public void Shooting(float damg)
  {
    muzzleFlash.Play();
    RaycastHit hit;
    float originDamg = damg;
    if(Physics.Raycast(FPSCam.transform.position, FPSCam.transform.forward, out hit, Mathf.Infinity))
    {
      muzzleFlash.Play();
      DestroyAbleObj target = hit.transform.GetComponent<DestroyAbleObj>();
      //if(target != null)
      
        GameObject hitGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(hitGO, 1f);
      if(target != null)
      {
        target.ItemDestoyed();
      }
    }
  }

}



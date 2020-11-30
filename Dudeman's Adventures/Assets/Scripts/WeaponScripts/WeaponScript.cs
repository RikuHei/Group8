using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    //The shootpoint where the bullet is instantiated
    public Transform shootPoint;
    public GameObject bulletPrefab;
    public float fireRate = 10;
    float nextTimeToFire = 0;

    public AudioClip[] shoot;
    public AudioSource audioSource;
    public AudioListener audioListener;
    private AudioClip shootClip;
    
    void Start()
    {
        audioListener = GetComponent<AudioListener>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1"))
        {
            //Timer for fire rate
            if(Time.time > nextTimeToFire)
            {
                nextTimeToFire = Time.time+4/fireRate;
                Shoot();
            }
        }
    }

    void Shoot()
    {
       Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
       PlayRandom();
    }

    void PlayRandom()
    {
        int index = Random.Range(0, shoot.Length);
        shootClip = shoot[index];
        audioSource.clip = shootClip;
        audioSource.Play();
    }
}

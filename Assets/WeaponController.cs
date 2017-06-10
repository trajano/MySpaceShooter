using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    public float timeToFire;
    public float repeatRate;
    public GameObject shot;
    public Transform shotSpawn;
    private AudioSource audioSource;

    private Quaternion downwards;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        var rot = new Vector3(0, 180, 0);
        downwards = Quaternion.Euler(rot);
        InvokeRepeating("Fire", timeToFire, repeatRate);

    }

    void Fire()
    {
        Instantiate(shot, shotSpawn.position, downwards);
        audioSource.Play();
    }
}

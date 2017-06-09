using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour
{

    private Rigidbody rb;
    // Use this for initialization
    public float tumble;
    public GameObject explosion;
    public GameObject playerExplosion;
    private GameController gameController;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameController = FindObjectOfType<GameController>();
        rb.angularVelocity = Random.insideUnitSphere * tumble;
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary")
        {
            return;
        }
        Instantiate(explosion, transform.position, transform.rotation);
        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            //gameController.GameOver();
        }
        if (other.tag == "Bolt")
        {
            gameController.AddScore(5);
        }
        Destroy(other.gameObject);
        Destroy(gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManeuver : MonoBehaviour
{

    public float startWaitMin;
    public float startWaitMax;
    private float targetManeuver;
    public float dodge;
    public float maneuverTimeMin;
    public float maneuverTimeMax;
    public float maneuverWaitMin;
    public float maneuverWaitMax;
    public float smoothing;
    public Boundary boundary;
    private Rigidbody rb;
    private float currentSpeed;
    public float tilt;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = rb.velocity.z;
        StartCoroutine(Evade());
    }

    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWaitMin, startWaitMax));
        for (;;)
        {
            targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
            yield return new WaitForSeconds(Random.Range(maneuverTimeMin, maneuverTimeMax));
            targetManeuver = 0;
            yield return new WaitForSeconds(Random.Range(maneuverWaitMin, maneuverWaitMax));
        }
    }
    private void FixedUpdate()
    {
        var newManeuver = Mathf.MoveTowards(rb.velocity.x, targetManeuver, Time.deltaTime * smoothing);
        rb.velocity = new Vector3(newManeuver, 0.0f, currentSpeed);
        rb.position = new Vector3(Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax));
        //rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }
}

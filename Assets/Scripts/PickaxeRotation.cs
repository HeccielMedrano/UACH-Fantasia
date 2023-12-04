using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class PickaxeRotation : MonoBehaviour
{
    public float rotationSpeed = 1000f;

    /*public float minLaunchForce = 5f;
    public float maxLaunchForce = 7f;
    public float upwardsModifier = 2f;
    public float launchForce = 10f;
    public float horizontalImpulse = 1f;*/

    public float launchForce = 2f;
    public float upwardsForce = 3f;

    void Start()
    {
        Launch();
    }

    void Update()
    {
        float rotationAngle = Time.deltaTime * rotationSpeed;

        transform.Rotate(Vector3.forward, rotationAngle);

        if (Time.time % 1f < Time.deltaTime)
        {
            transform.rotation = Quaternion.identity;
        }
    }

    public void Launch()
    {
        /*Vector3 launchDirection;

        Vector3 forwardDirection = transform.forward;
        float randomUpwardsImpulse = Random.Range(1f, upwardsModifier);

        launchDirection = forwardDirection + Vector3.up * randomUpwardsImpulse - Vector3.left * horizontalImpulse;

        launchDirection.Normalize();*/

        Vector3 launchDirection = Random.Range(0, 2) == 0 ? Vector3.right : -Vector3.right;
        launchDirection += Vector3.up * upwardsForce;
        launchDirection.Normalize();

        GetComponent<Rigidbody2D>().AddForce(launchDirection * launchForce, ForceMode2D.Impulse);
    }
}

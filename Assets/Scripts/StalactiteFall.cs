using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalactiteFall : MonoBehaviour
{
    private GameObject parentObject;
    private Rigidbody2D rb;

    private float xCoords;
    private float yCoords;

    void Start()
    {
        parentObject = transform.parent.gameObject;
        rb = parentObject.GetComponent<Rigidbody2D>();

        xCoords = parentObject.transform.position.x;
        yCoords = parentObject.transform.position.y;
    }

    public void resetPosition()
    {
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;

        parentObject.transform.position = new Vector2(xCoords, yCoords);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            rb.isKinematic = false;
            rb.gravityScale = 2;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinecartMove : MonoBehaviour
{
    private BoxCollider2D coll;

    bool onRails;
    public Vector3 colliderOffset = new Vector3(0.1f, 0.18f, 0f);
    float railLength = 2f;
    public LayerMask railLayer;

    float moveSpeed = 0.05f;

    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        onRails = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, railLength, railLayer);

        if (onRails)
        {
            transform.position = new Vector2(transform.position.x + moveSpeed, transform.position.y);
        }
        else
        {
            flipMinecart();
            moveSpeed *= -1;
        }
    }

    public void flipMinecart()
    {
        colliderOffset = new Vector3(colliderOffset.x * -1, 0.18f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMinecartMovement : MonoBehaviour
{
    bool onRails;
    private BoxCollider2D coll;
    public Vector3 colliderOffset = new Vector3(0.1f, 0.18f, 0f);
    float railLength = 2f;
    public LayerMask railLayer;
    public float moveSpeed = 0.02f;

    public GameObject projectilePrefab;

    public float projectileInterval;


    void Start()
    {
        StartCoroutine(SpawnProjectilesRepeatedly());
    }

    IEnumerator SpawnProjectilesRepeatedly()
    {
        while (true)
        {
            SpawnProjectile();

            yield return new WaitForSeconds(projectileInterval);
        }
    }

    void SpawnProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        projectile.GetComponent<SpriteRenderer>().sortingOrder = 999;

        Destroy(projectile, 4f);
    }


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
}

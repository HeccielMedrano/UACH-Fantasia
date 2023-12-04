using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    void Start()
    {
        Physics2D.gravity = new Vector3(0f, -1.625f, 0f);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Physics2D.gravity = new Vector3(0f, -9.81f, 0f);
        }
        else
        {
            Physics2D.gravity = new Vector3(0f, -1.625f, 0f);
        }
    }
}

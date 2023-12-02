using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawner : MonoBehaviour
{
    // Variables correspondientes al respawn y checkpoints
    [SerializeField] GameObject[] checkPoints;
    GameObject currentCheckPoint;
    int currentCheckPointIndex = 0;

    void Start()
    {
        currentCheckPoint = checkPoints[currentCheckPointIndex];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hazard"))
        {
            // Animacion respawneo
            transform.position = currentCheckPoint.transform.position;
        }

        if (other.CompareTag("CheckPointMarker"))
        {
            currentCheckPointIndex++;
            currentCheckPoint = checkPoints[currentCheckPointIndex];
        }
    }
}

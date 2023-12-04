using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalFallTrigger : MonoBehaviour
{
    public GameObject playerObject;
    public GameObject deactivate;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            deactivate.SetActive(false);
            playerObject.transform.GetChild(2).gameObject.SetActive(true);
        }
    }
}

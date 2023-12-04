using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawner : MonoBehaviour
{
    public Animator respawnAnimator;
    public MinesLevelResetter resetter;

    // Variables correspondientes al respawn y checkpoints
    [SerializeField] GameObject[] checkPoints;
    GameObject currentCheckPoint;
    int currentCheckPointIndex = 0;

    public AudioSource audio;
    public AudioClip clip;

    void Start()
    {
        currentCheckPoint = checkPoints[currentCheckPointIndex];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hazard"))
        {
            // Animacion respawneo
            audio.clip = clip;
            audio.Play();
            
            StartCoroutine(TriggerAnimation());
        }

        if (other.CompareTag("CheckPointMarker"))
        {
            currentCheckPointIndex++;
            currentCheckPoint = checkPoints[currentCheckPointIndex];

            other.enabled = false;
        }
    }

    private IEnumerator TriggerAnimation()
    {
        respawnAnimator.SetTrigger("FadeBlack");
        yield return new WaitForSeconds(0.4f);

        transform.position = currentCheckPoint.transform.position;
        resetter.resetScreens();

        respawnAnimator.SetTrigger("FadeReturn");
        yield return new WaitForSeconds(0.16f);
        respawnAnimator.SetTrigger("ReturnToNull");
    }
}

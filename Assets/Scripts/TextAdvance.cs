using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextAdvance : MonoBehaviour
{
    public GameObject[] textObjects;
    public Animator anim;

    private int index = 0;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Advance();
        }
    }

    private void Advance()
    {
        index++;

        if (index >= textObjects.Length)
            StartCoroutine(NextScene());
        else
        {
            textObjects[index - 1].SetActive(false);
            textObjects[index].SetActive(true);
        }
    }

    private IEnumerator NextScene()
    {
        Time.timeScale = 0.33f;
        anim.SetTrigger("FadeBlack");

        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

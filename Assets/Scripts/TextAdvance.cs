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

        if (Input.anyKeyDown)
        {
            Advance();
        }
    }

    private void Advance()
    {
        textObjects[index].SetActive(false);
        index++;

        if (index >= textObjects.Length)
            StartCoroutine(NextScene());
        else
            textObjects[index].SetActive(true);
    }

    private IEnumerator NextScene()
    {
        anim.SetTrigger("FadeBlack");

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

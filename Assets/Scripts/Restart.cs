using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    // Start is called before the first frame update
  

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space"))
        {
            StartCoroutine("esperar");
        }
    }
    public IEnumerator esperar()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(1);
    }
}

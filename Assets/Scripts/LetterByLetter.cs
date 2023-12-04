using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LetterByLetter : MonoBehaviour
{
    //public float revealSpeed = 0f; // Adjust this to control the speed of text reveal
    private TextMeshProUGUI textMeshPro;
    private string fullText;
    private string currentText = "";

    private void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        fullText = textMeshPro.text;
        textMeshPro.text = "";
        StartCoroutine(RevealText());
    }

    private IEnumerator RevealText()
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i + 1);
            textMeshPro.text = currentText;
            yield return new WaitForSeconds(0.05f);
        }
    }
}

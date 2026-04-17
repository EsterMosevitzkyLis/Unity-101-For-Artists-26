using System.Collections;
using UnityEngine;
using TMPro;

public class MatrixText : MonoBehaviour
{
    public TMP_Text textComponent;
    [TextArea(5, 10)]
    public string fullText;
    public float delay = 0.05f;

    void OnEnable()
    {
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        textComponent.text = "";
        foreach (char c in fullText)
        {
            textComponent.text += c;
            yield return new WaitForSeconds(delay);
        }
    }
}

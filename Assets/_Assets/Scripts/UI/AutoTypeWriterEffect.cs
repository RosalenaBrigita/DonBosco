using System.Collections;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class AutoTypewriterEffect : MonoBehaviour
{
    private TMP_Text textComponent;
    private string fullText;

    public float delay = 0.05f;

    private void Awake()
    {
        textComponent = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        fullText = textComponent.text; // pindahkan ke sini!
        textComponent.text = "";
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            textComponent.text += fullText[i];
            yield return new WaitForSeconds(delay);
        }
    }
}

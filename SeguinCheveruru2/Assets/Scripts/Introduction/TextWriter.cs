using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextWriter : MonoBehaviour
{
    public TMP_Text tmp;
    public List<StringValue> texts;
    public float timeBetweenLetters;
    public float timeBetweenTexts;
    public float timeToDissapear;

    private WaitForSeconds textWait;
    private WaitForSeconds letterWait;
    private WaitForSeconds dissapearTime;

    private int currentWriteIndex = 0;

    private void Awake()
    {
        textWait = new WaitForSeconds(timeBetweenTexts);
        letterWait = new WaitForSeconds(timeBetweenLetters);
        dissapearTime = new WaitForSeconds(timeToDissapear);
    }

    public IEnumerator WriteAllTexts(Action onFinish)
    {
        for (int i = 0; i < texts.Count; i++)
        {
            yield return Write(texts[i].Value);
            yield return textWait;
        }

        onFinish?.Invoke();
    }

    public void WriteText()
    {
        StartCoroutine(Write(texts[currentWriteIndex].Value));
        currentWriteIndex++;
    }

    private IEnumerator Write(string text)
    {
        char[] charArray1 = text.ToCharArray();

        string completeText = "";
        for (int i = 0; i < charArray1.Length; i++)
        {
            completeText = completeText + charArray1[i];
            tmp.text = completeText;
            yield return letterWait;
        }
        yield return new WaitForSeconds(timeToDissapear);
        tmp.text = "";
    }
}

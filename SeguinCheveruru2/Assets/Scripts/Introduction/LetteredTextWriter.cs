using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LetteredTextWriter
{
    public IEnumerator WriteText(TextMeshProUGUI tmp, float timeBetweenletters, string text, Action onFinish)
    {
        char[] charArray = text.ToCharArray();
        string completeText = "";

        for (int i = 0; i < charArray.Length; i++)
        {
            completeText = completeText + charArray[i];
            tmp.text = completeText;
            yield return new WaitForSeconds(timeBetweenletters);
        }

        onFinish?.Invoke();
    }
}


using UnityEngine;
using TMPro;
using System;

public class GameTimer : MonoBehaviour
{
    public TMP_Text timerText;
    private float elapsedTime;

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (timerText != null)
        {
            TimeSpan time = TimeSpan.FromSeconds(elapsedTime);
            timerText.text = "Time: " +
                time.ToString(@"mm\:ss");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHighscores : MonoBehaviour 
{
    public TMPro.TextMeshProUGUI[] rNames;
    public TMPro.TextMeshProUGUI[] rScores;
    HighScores myScores;

    float timer = 0.0f;

    public void Start() //Fetches the Data at the beginning
    {
        for (int i = 0; i < rNames.Length; i++)
        {
            rNames[i].text = i + 1 + ".";
            rScores[i].text = "0000.0";
        }
    }

    public void SetScoresToMenu() //Assigns proper name and score for each text value
    {
        for (int i = 0; i < rNames.Length;i ++)
        {
            rNames[i].text = i + 1 + ". ";
            if (Scr_LootLockerManager.instance.scores.Count > i)
            {
                rScores[i].text = Scr_LootLockerManager.instance.scores[i];

                //Adding one decimal place
                rScores[i].text = rScores[i].text.Remove(rScores[i].text.Length - 1);
                rScores[i].text += ".";
                rScores[i].text += (Scr_LootLockerManager.instance.scores[i])[Scr_LootLockerManager.instance.scores[i].Length - 1];

                rNames[i].text += Scr_LootLockerManager.instance.names[i];
            }
        }
    }
    private void Update()
    {
        if(timer <= 0.0f)
        {
            SetScoresToMenu();
            timer = 2.0f;
        }
        else
        {
            timer -= Time.unscaledDeltaTime;
        }
    }
}

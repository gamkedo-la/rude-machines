using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Scr_GameoverCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameoverText;
    [SerializeField] private TextMeshProUGUI bestText;
    [SerializeField] private float faderDelay = 2.0f;
    [SerializeField] private Image fader;

    void Start()
    {
        string newBestStr = (Scr_GameManager.instance.surviveTime >= PlayerPrefs.GetFloat("bestTime") ? "NEW! " : "");
        gameoverText.text = "GAMEOVER!\n" + newBestStr + ((float)Mathf.FloorToInt(Scr_GameManager.instance.surviveTime * 10.0f) / 10.0f).ToString() + "s";

        bestText.text = "BEST: " + (Mathf.Floor(PlayerPrefs.GetFloat("bestTime", 0.0f) * 10.0f) / 10.0f).ToString() + "s";
    }

    void Update()
    {
        if(faderDelay <= 0.0f)
            fader.color = Color.Lerp(fader.color, Color.black, 8.0f * Time.deltaTime);
        else
            faderDelay -= Time.deltaTime;
    }
}

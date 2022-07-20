using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scr_GameoverCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameoverText;
    [SerializeField] private TextMeshProUGUI bestText;
    [SerializeField] private Image fader;
    public bool back = false;

    public void Back() { back = true; }

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        string newBestStr = (Scr_GameManager.instance.surviveTime >= PlayerPrefs.GetFloat("bestTime") ? "NEW! " : "");
        gameoverText.text = "GAMEOVER!\n" + newBestStr + ((float)Mathf.FloorToInt(Scr_GameManager.instance.surviveTime * 10.0f) / 10.0f).ToString() + "s";

        bestText.text = "BEST: " + (Mathf.Floor(PlayerPrefs.GetFloat("bestTime", 0.0f) * 10.0f) / 10.0f).ToString() + "s";
    }

    void Update()
    {
        if (back)
        {
            fader.color = Color.Lerp(fader.color, Color.black, 12.0f * Time.deltaTime);

            if(fader.color == Color.black)
                SceneManager.LoadScene(0);
        }
    }
}

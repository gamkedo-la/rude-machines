using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Scr_StartCanvas : MonoBehaviour
{
    [SerializeField] private GameObject gameplayCanvas;
    [Space]
    [SerializeField] private Scr_GameManager gameManager;
    [SerializeField] private TextMeshProUGUI bestText;
    [SerializeField] private Image fader;
    [Space]
    public GameObject startQuad;

    [ContextMenu("Reset Best Time")]
    public void ResetBestTime()
    {
        PlayerPrefs.SetFloat("bestTime", 0.0f);
    }

    void Start()
    {
        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.None;

        gameplayCanvas.SetActive(false);

        bestText.text = "BEST: " + (Mathf.Floor(PlayerPrefs.GetFloat("bestTime", 0.0f) * 10.0f) / 10.0f).ToString() + "s";
    }

    void Update()
    {
        fader.color = Color.Lerp(fader.color, Color.clear, 4.0f * Time.deltaTime);

        if(gameManager.enabled)
        {
            startQuad.transform.position = Vector3.Lerp(startQuad.transform.position, new Vector3(0.0f, 4.0f, 0.0f), Time.deltaTime * 4.0f);
            
            if(startQuad.transform.position.y >= 3.8f)
            {
                startQuad.SetActive(false);
                gameObject.SetActive(false);

                for(int i = 0; i < transform.childCount; i++)
                    transform.GetChild(i).gameObject.SetActive(true);

                gameplayCanvas.SetActive(true);
            }
        }
    }

    public void Play()
    {
        gameManager.enabled = true;
        GetComponent<AudioSource>().Play();

        for(int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(false);
    }

    public void Settings()
    {

    }
}

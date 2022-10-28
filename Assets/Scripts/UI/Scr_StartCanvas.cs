using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Scr_StartCanvas : MonoBehaviour
{
    [SerializeField] private GameObject startCanvas;
    [SerializeField] private GameObject gameplayCanvas;
    [Space]
    [SerializeField] private Scr_GameManager gameManager;
    [SerializeField] private TextMeshProUGUI bestText;
    [SerializeField] private Image fader;
    [Space]
    public AudioClip clip;
    [Space]
    public GameObject startQuad;
    [Space]
    public Camera cam;
    public LayerMask normalLayers;
    public LayerMask lowResLayers;
    public GameObject[] lowResObjects;
    [Space]
    public GameObject crosshair;
    public static Scr_StartCanvas instance;

    [ContextMenu("Reset Best Time")]
    public void ResetBestTime()
    {
        PlayerPrefs.SetFloat("bestTime", 0.0f);
    }

    public void Quit()
    {
        if (!Application.isEditor)
            Application.Quit();

        Debug.Log("Quit");
    }

    void Start()
    {
        gameManager.pausedCanvas.GetComponent<Scr_SettingsCanvas>().InitializeSettings();
        Time.timeScale = 1.0f;
        Scr_State.block = false;
        instance = this;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

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
                startCanvas.SetActive(false);

                for(int i = 0; i < startCanvas.transform.childCount; i++)
                    startCanvas.transform.GetChild(i).gameObject.SetActive(true);

                gameplayCanvas.SetActive(true);

                enabled = false;
            }
        }
    }

    public void Play()
    {
        gameManager.enabled = true;
        GetComponent<AudioSource>().Play();

        for(int i = 0; i < startCanvas.transform.childCount; i++)
            startCanvas.transform.GetChild(i).gameObject.SetActive(false);
    }

    public void ChangeStyle(GameObject valueTextObject)
    {
        if (!lowResObjects[0].activeSelf)
        {
            for (int i = 0; i < lowResObjects.Length; i++)
                lowResObjects[i].SetActive(true);
            cam.cullingMask = lowResLayers;
            valueTextObject.GetComponent<TextMeshProUGUI>().text = "Low Res.";
            PlayerPrefs.SetInt("style", 1);
        }
        else
        {
            for (int i = 0; i < lowResObjects.Length; i++)
                lowResObjects[i].SetActive(false);
            cam.cullingMask = normalLayers;
            valueTextObject.GetComponent<TextMeshProUGUI>().text = "Standard";
            PlayerPrefs.SetInt("style", 0);
        }
    }

    public void ToggleCrosshair(GameObject valueTextObject)
    {
        if(crosshair.activeSelf)
        {
            crosshair.SetActive(false);
            valueTextObject.GetComponent<TextMeshProUGUI>().text = "Disabled";
            PlayerPrefs.SetInt("crosshair", 0);
        }
        else
        {
            crosshair.SetActive(true);
            valueTextObject.GetComponent<TextMeshProUGUI>().text = "Enabled";
            PlayerPrefs.SetInt("crosshair", 1);
        }
    }

    public void PlaySound()
    {
        GetComponent<AudioSource>().PlayOneShot(clip);
    }
}

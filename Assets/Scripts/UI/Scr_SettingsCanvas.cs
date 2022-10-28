using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Scr_SettingsCanvas : MonoBehaviour
{
    public Slider brightnessSlider;
    public Slider sensitivitySlider;
    public Slider volumeSlider;
    public TextMeshProUGUI crosshairValue;
    public TextMeshProUGUI styleValue;
    [Space]
    public Scr_StartCanvas startCanvas;

    public void InitializeSettings()
    {
        brightnessSlider.value = PlayerPrefs.GetFloat("brightness", 0.4f);
        brightnessSlider.onValueChanged.Invoke(brightnessSlider.value);

        sensitivitySlider.value = PlayerPrefs.GetFloat("sensitivity", 0.2f);
        sensitivitySlider.onValueChanged.Invoke(sensitivitySlider.value);

        volumeSlider.value = PlayerPrefs.GetFloat("volume", -10.0f);
        volumeSlider.onValueChanged.Invoke(volumeSlider.value);

        crosshairValue.text = PlayerPrefs.GetInt("crosshair", 0) == 1 ? "Enabled" : "Disabled";
       // startCanvas.crosshair.SetActive(crosshairValue.text == "Enabled");

        styleValue.text = PlayerPrefs.GetInt("style", 0) == 1 ? "Low Res." : "Standard";
        //for (int i = 0; i < startCanvas.lowResObjects.Length; i++)
         //   startCanvas.lowResObjects[i].SetActive(styleValue.text == "Low Res.");
        //startCanvas.cam.cullingMask = styleValue.text == "Low Res." ? startCanvas.lowResLayers : startCanvas.normalLayers;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Scr_GameplayCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI surviveTimeText;
    [SerializeField] private Image wavePanel;
    [SerializeField] private Color wavePanelColor;
    [SerializeField] private TextMeshProUGUI waveText;

    void Start()
    {
        
    }

    void Update()
    {
        surviveTimeText.text = ((float)Mathf.FloorToInt(Scr_GameManager.instance.surviveTime * 10.0f) / 10.0f).ToString() + "s";

        wavePanel.color = Color.Lerp(wavePanel.color, Scr_GameManager.instance.waveDisplayTimer > 0.0f ? wavePanelColor : Color.clear, 8.0f * Time.deltaTime);
        waveText.color = Color.Lerp(waveText.color, Scr_GameManager.instance.waveDisplayTimer > 0.0f ? Color.white : Color.clear, 12.0f * Time.deltaTime);

        waveText.text = "Wave " + (Scr_EnemyManager.instance.currentWave + 1).ToString() + "\n" + Scr_EnemyManager.instance.GetWaveName();
    }
}

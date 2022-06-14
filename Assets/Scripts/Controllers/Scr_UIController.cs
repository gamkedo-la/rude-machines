using UnityEngine;
using UnityEngine.UIElements;

public class Scr_UIController : MonoBehaviour
{
    public Scr_GameManager gameManager;
    [Space]
    public VisualTreeAsset startMenuUI;
    public VisualTreeAsset gameplayUI;
    [Space]
    public GameObject startQuad;

    private AudioSource audSrc;

    private UIDocument doc;
    private GameObject player;
    private Button playButton;

    private Label surviveTime;
    private VisualElement waveDisplay;
    private Label waveName;

    void Start()
    {
        audSrc = GetComponent<AudioSource>();

        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.None;

        doc = GetComponent<UIDocument>();
        var root = doc.rootVisualElement;
        playButton = root.Q<Button>("playbutton");

        playButton.clicked += Play;
    }

    void Update()
    {
        if(doc.visualTreeAsset == gameplayUI)
        {
            if(Scr_GameManager.instance == null) return;
            surviveTime.text = ((float)Mathf.FloorToInt(Scr_GameManager.instance.surviveTime * 10.0f) / 10.0f).ToString() + "s";
            waveDisplay.style.opacity = Mathf.Lerp(waveDisplay.style.opacity.value, Scr_GameManager.instance.waveDisplayTimer > 0.0f ? 1.0f : 0.0f, 8.0f * Time.deltaTime);
            waveName.text = "Wave " + (Scr_EnemyManager.instance.currentWave + 1).ToString() + "\n" + Scr_EnemyManager.instance.GetWaveName();

            if(startQuad.activeSelf)
            {
                startQuad.transform.position = Vector3.Lerp(startQuad.transform.position, new Vector3(0.0f, 4.0f, 0.0f), Time.deltaTime * 4.0f);
                if(startQuad.transform.position.y >= 3.25f) startQuad.SetActive(false);
            }
        }
    }

    void Play()
    {
        gameManager.enabled = true;

        doc.visualTreeAsset = gameplayUI;

        surviveTime = doc.rootVisualElement.Q<Label>("surviveTime");
        waveDisplay = doc.rootVisualElement.Q<VisualElement>("waveDisplay");
        waveName = doc.rootVisualElement.Q<Label>("waveName");

        audSrc.Play();
    }
}

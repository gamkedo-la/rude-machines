using UnityEngine;
using UnityEngine.UIElements;

public class Scr_UIController : MonoBehaviour
{
    public VisualTreeAsset startMenuUI;
    public VisualTreeAsset gameplayUI;
    [Space]
    public GameObject startQuad;
    public GameObject enemyManager;

    private UIDocument doc;
    private GameObject player;
    private Button playButton;

    private Label surviveTime;
    private VisualElement waveDisplay;
    private Label waveName;

    void Start()
    {
        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.None;

        doc = GetComponent<UIDocument>();
        var root = doc.rootVisualElement;
        playButton = root.Q<Button>("playbutton");

        playButton.clicked += Play;

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if(doc.visualTreeAsset == gameplayUI)
        {
            surviveTime.text = ((float)Mathf.FloorToInt(player.GetComponent<Scr_PlayerController>().SurviveTime * 10.0f) / 10.0f).ToString() + "s";
            waveDisplay.style.opacity = 0.0f;
            waveName.text = "Wave " + "1" + " " + "waveName";

            if(startQuad.activeSelf)
            {
                startQuad.transform.position = Vector3.Lerp(startQuad.transform.position, new Vector3(0.0f, 4.0f, 0.0f), Time.deltaTime * 4.0f);
                if(startQuad.transform.position.y >= 3.25f) startQuad.SetActive(false);
            }
        }
    }

    void Play()
    {
        player.GetComponent<Scr_PlayerController>().enabled = true;
        player.GetComponent<Scr_HandController>().enabled = true;
        enemyManager.SetActive(true);

        doc.visualTreeAsset = gameplayUI;

        surviveTime = doc.rootVisualElement.Q<Label>("surviveTime");
        waveDisplay = doc.rootVisualElement.Q<VisualElement>("waveDisplay");
        waveName = doc.rootVisualElement.Q<Label>("waveName");
    }
}

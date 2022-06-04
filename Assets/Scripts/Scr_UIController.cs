using UnityEngine;
using UnityEngine.UIElements;

public class Scr_UIController : MonoBehaviour
{
    public VisualTreeAsset startMenuUI;
    public VisualTreeAsset gameplayUI;

    private UIDocument doc;
    private GameObject player;
    private Button playButton;

    void Start()
    {
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
            var surviveTime = doc.rootVisualElement.Q<Label>("surviveTime");
            surviveTime.text = ((float)Mathf.FloorToInt(player.GetComponent<Scr_PlayerController>().SurviveTime * 10.0f) / 10.0f).ToString() + "s";
        }
    }

    void Play()
    {
        player.GetComponent<Scr_PlayerController>().enabled = true;
        player.GetComponent<Scr_HandController>().enabled = true;
        doc.visualTreeAsset = gameplayUI;
    }
}

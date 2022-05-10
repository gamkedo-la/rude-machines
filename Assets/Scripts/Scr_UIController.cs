using UnityEngine;
using UnityEngine.UIElements;

public class Scr_UIController : MonoBehaviour
{
    private Button playButton;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        playButton = root.Q<Button>("playbutton");

        playButton.clicked += Play;
    }

    void Play()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Scr_PlayerController>().enabled = true;
        player.GetComponent<Scr_HandController>().enabled = true;
        GetComponent<UIDocument>().enabled = false;
    }
}

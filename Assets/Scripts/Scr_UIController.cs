using System.Collections;
using System.Collections.Generic;
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
        GameObject.FindGameObjectWithTag("Player").GetComponent<Scr_PlayerController>().enabled = true;
        GetComponent<UIDocument>().enabled = false;
    }
}

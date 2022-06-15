using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Scr_GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject enemyManager;
    public Transform patrolPointGroups;
    [SerializeField] private Volume stuckTimeVolume;
    [Space]
    [SerializeField] private float endTime = 3.0f;
    [SerializeField] private GameObject gameplayCanvas;
    [SerializeField] private GameObject gameoverCanvas;

    [HideInInspector] public float surviveTime = 0.0f;
    [HideInInspector] public float waveDisplayTimer = 3.0f;

    private float preStuckTime = 0.0f;
    private float stuckTime = 0.0f;

    public static Scr_GameManager instance = null;

    public void Die()
    {
        float bestTime = PlayerPrefs.GetFloat("bestTime", 0.0f);
        if(surviveTime > bestTime) PlayerPrefs.SetFloat("bestTime", surviveTime);

        GameObject cameraObject = player.transform.GetChild(0).gameObject;
        cameraObject.transform.parent = null;
        for(int i = 0; i < cameraObject.transform.childCount; i++)
            Destroy(cameraObject.transform.GetChild(i).gameObject);
        Destroy(player);

        gameplayCanvas.SetActive(false);
        gameoverCanvas.SetActive(true);
    }

    public void SetStuckTime(float time, float pre = 0)
    {
        stuckTime = time;
        preStuckTime = pre;
    }

    void Start()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Scr_PlayerController>().enabled = true;
        player.GetComponent<Scr_HandController>().enabled = true;
        enemyManager.SetActive(true);
    }

    void Update()
    {
        if(player == null)
        {
            if(endTime <= 0.0f)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                endTime -= Time.deltaTime;
            }
            return;
        }

        if(preStuckTime <= 0.0f)
        {
            if(stuckTime <= 0.0f)
            {
                Time.timeScale = 1.0f;
                stuckTimeVolume.weight = 0.0f;
            }
            else
            {
                Time.timeScale = 0.0f;
                stuckTimeVolume.weight = 1.0f;
                stuckTime -= Time.unscaledDeltaTime;
            }
        }
        else
        {
            preStuckTime -= Time.deltaTime;
        }

        surviveTime += Time.deltaTime;

        if(waveDisplayTimer > 0.0f) waveDisplayTimer -= Time.deltaTime;
    }
}

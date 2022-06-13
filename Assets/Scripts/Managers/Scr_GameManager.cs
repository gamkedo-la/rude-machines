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

    [HideInInspector] public float surviveTime = 0.0f;
    [HideInInspector] public float waveDisplayTimer = 3.0f;

    private float preStuckTime = 0.0f;
    private float stuckTime = 0.0f;

    public static Scr_GameManager instance = null;

    public void Die()
    {
        float bestTime = PlayerPrefs.GetFloat("bestTime", 0.0f);
        if(surviveTime > bestTime) PlayerPrefs.SetFloat("bestTime", surviveTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

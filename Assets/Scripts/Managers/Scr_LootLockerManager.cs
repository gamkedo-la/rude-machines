using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using Newtonsoft.Json;

public class Scr_LootLockerManager : MonoBehaviour
{
    private float timer = 30.0f;

    public static Scr_LootLockerManager instance;

    public List<string> names;
    public List<string> scores;

    public static void UpdateLeaderboard()
    {
        instance._UpdateLeaderboard();
    }

    public void _UpdateLeaderboard()
    {
        timer = 0.0f;
    }

    void Start()
    {
        instance = this;

        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (!response.success)
            {
                Debug.Log("error starting LootLocker session");

                return;
            }

            Debug.Log("successfully started LootLocker session");
        });
    }

    void Update()
    {
        if(timer <= 0.0f)
        {
            int leaderboardID = 4836;
            int count = 15;
            int after = 0;
            LootLockerSDKManager.GetScoreList(leaderboardID, count, after, (response) =>
            {
                if (response.statusCode == 200)
                {
                    Debug.Log("Successful");
                    //Debug.Log(response.text);

                    scores.Clear();
                    names.Clear();

                    JsonTextReader reader = new JsonTextReader(new System.IO.StringReader(response.text));
                    string prevValue = "";
                    while(reader.Read())
                    {
                        if(reader.Value != null)
                        {
                            if (prevValue == "score") scores.Add(reader.Value.ToString());
                            else if (prevValue == "name") names.Add(reader.Value.ToString());
                            prevValue = reader.Value.ToString();
                        }
                    }
                }
                else
                {
                    Debug.Log("failed: " + response.Error);
                }
            });
            timer = 30.0f;
        }
        else
        {
            timer -= Time.unscaledDeltaTime;
        }
    }
}

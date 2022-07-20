using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;

public class Scr_SubmitScore : MonoBehaviour
{
    public TMPro.TextMeshProUGUI myName;
    public GameObject activate;

    public void SendScore()
    {
        if (!myName.text.Contains('*') && myName.text.Length > 0)
        {
            string name = myName.text.Substring(0, myName.text.Length > 8 ? 8 : myName.text.Length);
            name = name.ToUpper();
            while (name.Length < 6) name += "_";

            int score = (int)(PlayerPrefs.GetFloat("bestTime", 0.0f) * 10);

            Debug.Log(name + ", " + score);

            //HighScores.UploadScore(myName.text.Substring(0, myName.text.Length > 8 ? 8 : myName.text.Length), (int)(PlayerPrefs.GetFloat("bestTime", 0.0f) * 10));

            LootLockerSDKManager.SetPlayerName(name, (response) =>
            {
                if (response.success)
                {
                    int leaderboardID = 4836;
                    LootLockerSDKManager.SubmitScore(name, score, leaderboardID, (response) =>
                    {
                        if (response.statusCode == 200)
                        {
                            Debug.Log("Successful");
                            Scr_LootLockerManager.UpdateLeaderboard();
                            gameObject.SetActive(false);
                            activate.SetActive(true);
                        }
                        else
                        {
                            Debug.Log("Failed to submit score: " + response.Error);
                        }
                    });
                }
                else
                {
                    Debug.Log("Failed to set name: " + response.Error);
                }
            });
              
        }
    }
}

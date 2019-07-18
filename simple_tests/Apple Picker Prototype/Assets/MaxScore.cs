using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaxScore : MonoBehaviour
{
    static public int maxScore = 0;

    private void Awake() {
        //If the Platerefs HighScore already exists, read it
        if(PlayerPrefs.HasKey("MaxScore")){
            maxScore = PlayerPrefs.GetInt("MaxScore");
        }    
        //Assign the high score to MaxScore
        PlayerPrefs.SetInt("MaxScore", maxScore);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Text counter = GetComponent<Text>();
        counter.text = maxScore.ToString();

        //Update PlayerPrefs MaxScore if necessary
        if(maxScore > PlayerPrefs.GetInt("MaxScore")){
            PlayerPrefs.SetInt("MaxScore", maxScore);
        }
    }
}

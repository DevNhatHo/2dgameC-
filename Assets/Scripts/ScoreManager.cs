using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public Text win;
    public int score;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (score > 60)
        {
            win.text = "GEtting there!!!";
        }
        scoreText.text = score.ToString();

        if (score > 80)
        {
            win.text = "NICE MOVE!!!";
        }
        scoreText.text = score.ToString();

        if (score > 120)
        {
            win.text = "yOUR ABOUT TO BEAT ME!!!";
        }
        scoreText.text = score.ToString();

        if (score > 150)
        {
            win.text = "WINNER!!! Keep Going!!";
        }

        if (score > 200)
        {
            win.text = "WOW";
        }

        if (score > 250)
        {
            win.text = "^_^";
        }
        scoreText.text = score.ToString();
        
        
    }

    public void IncreaseScore(int amountIncrease)
    {
        score += amountIncrease;
    }
}

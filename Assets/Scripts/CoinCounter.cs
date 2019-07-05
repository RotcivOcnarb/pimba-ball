using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{
    Text text;
    public GameManager manager;
    public enum ScoreType
    {
        Coin,
        Score
    }

    public ScoreType type;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (type)
        {
            case ScoreType.Coin:
                text.text = "Coins: " + (int)GlobalVars.coins;
                break;
            case ScoreType.Score:
                text.text = "Score: " + (int)manager.score;
                break;
        }
            

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCounters : MonoBehaviour
{

    public GameManager manager;
    public PimbaBall pimbaBall;
    public Image lifeBar;
    public Text lifeCounter;
    public Text coinCounter;
    public Text scoreCounter;
    public Text stageCounter;
    public LevelCounter levelCounter;

    float initialWidth;
    float lifeTween;

    // Start is called before the first frame update
    void Start()
    {
        initialWidth = lifeBar.rectTransform.rect.width;
        lifeTween = pimbaBall.life;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTween += (pimbaBall.life - lifeTween) / 5f;
        lifeBar.rectTransform.offsetMax = new Vector2(-initialWidth * (1-(lifeTween / pimbaBall.maxLife)), 0);
        lifeCounter.text = (int)((lifeTween / pimbaBall.maxLife) * 100) + "%";
        coinCounter.text = "Coins: " + GlobalVars.getPlayerProfile().coins;
        scoreCounter.text = "Score: " + manager.score;
        stageCounter.text = "Stage " + GlobalVars.getPlayerProfile().stage;
        levelCounter.value = manager.level;
    }
}

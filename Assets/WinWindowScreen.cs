using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WinWindowScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public Text scoresText;
    public GameManager manager;
    public ScrollRect scroll;
    List<string> lines;
    float lineTimer = 2;
    int lineIndex = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lineTimer -= Time.unscaledDeltaTime;
        if(lineTimer < 0){
            if(lineIndex < lines.Count){
                lineTimer = .5f;
                scoresText.text += lines[lineIndex] + "\n";
                lineIndex++;
                scroll.normalizedPosition = new Vector2(0, 0);
            }
        }
    }

    public void ShowScores(){
        scoresText.text = "";
        lineTimer = 1;
        lineIndex = 0;
        lines = new List<string>();
        lines.Add("Score: " + manager.score);
        lines.Add("Multiplier: x" + GlobalVars.getPlayerProfile().GetPlayerCoinMultiplier());
        lines.Add("Coins: " + (manager.score*GlobalVars.getPlayerProfile().GetPlayerCoinMultiplier()));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class WinWindowScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public Text scoresText;
    public GameManager manager;
    public ScrollRect scroll;
    public Button button;
    List<WinList> lines;
    List<WinList> queue;
    float lineTimer = 2;
    int lineIndex = 0;
    public Text coinsText;
    int coinsTween = 0;
    void Start()
    {
        queue = new List<WinList>();
    }

    struct WinList{
        public string line;
        public Action execute;
        public WinList(string line, Action execute){
            this.line = line;
            this.execute = execute;
        }
    }

    // Update is called once per frame
    void Update()
    {
        lineTimer -= Time.unscaledDeltaTime;
        if(lineTimer < 0){
            if(lineIndex < lines.Count){
                lineTimer = .5f;
                scoresText.text += lines[lineIndex].line + "\n";
                lines[lineIndex].execute();
                lineIndex++;
                scroll.normalizedPosition = new Vector2(0, 0);
            }
            else{
                button.interactable = true;
            }
        }

        coinsTween += (int)((GlobalVars.getPlayerProfile().coins+1 - coinsTween)/5f);
        coinsText.text = "" + coinsTween;
    }

    public void addLine(string line, Action execute){
        if(queue == null) queue = new List<WinList>();
        queue.Add(new WinList(line, execute));
    }

    public void ShowScores(){
        scoresText.text = "";
        lineTimer = 1;
        lineIndex = 0;
        lines = new List<WinList>();

        lines.Add(new WinList("Score: " + manager.score, () => {}));
        lines.Add(new WinList("Multiplier: x" + GlobalVars.getPlayerProfile().GetPlayerCoinMultiplier(), () =>{}));
        lines.Add(new WinList("Coins: " + (manager.score*GlobalVars.getPlayerProfile().GetPlayerCoinMultiplier()), () => {
            int coins = manager.score*GlobalVars.getPlayerProfile().GetPlayerCoinMultiplier();
            GlobalVars.getPlayerProfile().coins += coins;
        }));

        lines.AddRange(queue);
    }
}

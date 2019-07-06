using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerProfile : SaveGame
{
    public string playerName = "Player";
    public int coins = 0;
    public int highScore = 0;
    public Dictionary<string, int> upgrades;

    public PlayerProfile(){
        upgrades = new Dictionary<string, int>();
    }

    public float GetImpulseUpgrade(){
        if(upgrades.ContainsKey("estilingue")){
            int cont = upgrades["estilingue"];
            return (Mathf.Log10(cont+1) + 1) * 2 - 1;
        }
        return 1;
    }

}

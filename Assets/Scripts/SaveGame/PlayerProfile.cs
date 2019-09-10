﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization;

[Serializable]
public class PlayerProfile : SaveGame
{
    public string playerName = "Player";
    public int coins = 0;
    public int highScore = 0;
    public StringIntDict upgrades;
    public int stage = 1;

    public PlayerProfile(){
        upgrades = new StringIntDict();
    }

    public float GetImpulseUpgrade(){
        if(upgrades.ContainsKey("estilingue")){
            int cont = upgrades["estilingue"];
            return (Mathf.Log10(cont+1) + 1) * 2 - 1;
        }
        return 1;
    }

    public float GetLinearDampingUpgrade(){
        if(upgrades.ContainsKey("sabao")){
            int cont = upgrades["sabao"];
            return 1/(float)(cont/7f+2);
        }
        return 0.5f;
    }

    public float GetHitDamageMultiplier()
    {
        if (upgrades.ContainsKey("faca")) {
            int cont = upgrades["faca"];
            return 1+ cont;
        }
        return 1;
    }

    public float GetPlayerDeffense(){
        if (upgrades.ContainsKey("armadura")) {
            int cont = upgrades["armadura"];
            return (1 / (float)(cont+5f)) * 5f;
        }
        return 1;
    
    }

    public int GetPlayerCoinMultiplier(){
        if (upgrades.ContainsKey("barganha")) {
            int cont = upgrades["barganha"];
            return cont*cont + 1;
        }
        return 1;
    }

    public Vector2 GetBombValue(){
        Vector2 ret = new Vector2(0, 0);

        if (upgrades.ContainsKey("bomba")) {
            int cont = upgrades["bomba"];
            ret.x = -1/(cont/10f + 1) + 1;
            ret.y = (int) (cont/2f + 1);
        }

        return ret;
    }

    public Vector2 GetChainReactionValue(){
        Vector2 ret = new Vector2(0, 0);
        if (upgrades.ContainsKey("chain-reaction")) {
            int cont = upgrades["chain-reaction"];
            ret.x = -1/(cont/10f + 1) + 1;
            ret.y = (int) (cont/2f + 1);
        }
        return ret;
    }

    public override string ToString(){
        return JsonUtility.ToJson(this, true);
    }

    public int GetValue(string id){
        return upgrades.ContainsKey(id) ? upgrades[id] : 0;
    }

}

[Serializable]
public class StringIntDict : SerializableDictionary<string, int> { }
using System.Collections;
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
    public SerDictStringInt upgrades;
    public int stage = 1;

    public PlayerProfile(){
        upgrades = new SerDictStringInt();
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

}

[Serializable]
public class SerDictStringInt : SerializableDictionary<string, int>{
    public SerDictStringInt() {  }
    protected SerDictStringInt(SerializationInfo info, StreamingContext context)
    {

        try {
            Debug.Log(info.GetInt32("faca"));
        }
        catch(Exception e) {
            Debug.LogError(e);
        }

    }
}

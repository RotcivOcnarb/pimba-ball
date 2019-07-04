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

}

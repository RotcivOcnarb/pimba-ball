using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVars : MonoBehaviour
{
    static PlayerProfile playerProfile;
    public static ShopStructure shopCache;
    static Dictionary<string, Sprite> shopImagesCache;
    public static void Initialize(){
        
        if (!SaveGameSystem.DoesSaveGameExist("pimba_game")){
            Debug.Log("Save not found, creating one");
            playerProfile = new PlayerProfile();
            SaveGameSystem.SaveGame(playerProfile, "pimba_game");
        }
        else{
            Debug.Log("Loaded saved game");
            playerProfile = (PlayerProfile) SaveGameSystem.LoadGame("pimba_game");
        }
    }

    public static Dictionary<string, Sprite> GetShopImagesCache()
    {
        if(shopImagesCache == null) {
            shopImagesCache = new Dictionary<string, Sprite>();
        }
        return shopImagesCache;
    }

    public static PlayerProfile getPlayerProfile(){
        if(playerProfile == null){
            Debug.Log("Player null, initializing");
            Initialize();
        }
        return playerProfile;
    }

    public static void SaveGame(){
        SaveGameSystem.SaveGame(playerProfile, "pimba_game");
        Debug.Log("Game Saved");
    }
}

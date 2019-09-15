﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

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
            SaveGame sg = SaveGameSystem.LoadGame("pimba_game");
            playerProfile = sg as PlayerProfile;
            Debug.Log(playerProfile);
        }
    }

    public static void LoadShop(){
        if(shopCache == null){
            FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://pimba-ball.firebaseio.com/");
            DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("flamelink");

            //LÊ O SHOP DO FIREBASE
            reference.GetValueAsync().ContinueWith((System.Threading.Tasks.Task<DataSnapshot> task) =>
            {
                if (task.IsFaulted)
                {
                    Debug.LogError(task.Exception);
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snap = task.Result;
                    Dictionary<string, object> myDict = (Dictionary<string, object>)snap.Value;

                    shopCache = new ShopStructure(myDict, false);
                }
            });
        }
    }

    public static void LoadImageFromStorage(string URL, System.Action<byte[]> resultTask)
    {
        Firebase.Storage.StorageReference storageReference =
        Firebase.Storage.FirebaseStorage.DefaultInstance.GetReferenceFromUrl(URL);

        storageReference.GetBytesAsync(1024 * 1024).
         ContinueWith((System.Threading.Tasks.Task<byte[]> task) => {
        if (task.IsFaulted || task.IsCanceled) {
            Debug.Log(task.Exception.ToString());
        }
        else {
            resultTask(task.Result);
        }
    });
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
    }

    public static void DeleteSaveGame()
    {
        SaveGameSystem.DeleteSaveGame("pimba_game");
        playerProfile = null;
    }
}

using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveGameSystem
{
    public static bool SaveGame(SaveGame saveGame, string name)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        using (FileStream stream = new FileStream(GetSavePath(name), FileMode.Create))
        {
            try
            {
                formatter.Serialize(stream, saveGame);
                stream.Close();
            }
            catch (Exception)
            {
                return false;
            }
        }

        return true;
    }

    public static SaveGame LoadGame(string name)
    {
        if (!DoesSaveGameExist(name))
        {
            Debug.Log("Save with name " + name + " not found");
            return null;
        }

        BinaryFormatter formatter = new BinaryFormatter();

        using (FileStream stream = new FileStream(GetSavePath(name), FileMode.Open))
        {
            try
            {
                stream.Position = 0;
                SaveGame s = formatter.Deserialize(stream) as SaveGame;
                stream.Close();
                return s;
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                return null;
            }
        }
    }

    public static bool DeleteSaveGame(string name)
    {
        try
        {
            File.Delete(GetSavePath(name));
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }

    public static bool DoesSaveGameExist(string name)
    {
        return File.Exists(GetSavePath(name));
    }

    private static string GetSavePath(string name)
    {
        return Path.Combine(Application.persistentDataPath, name + ".sav");
    }
}
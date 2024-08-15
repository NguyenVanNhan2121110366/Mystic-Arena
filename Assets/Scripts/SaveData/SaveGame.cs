using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor.Overlays;
using UnityEngine;

[Serializable]
public class SaveData
{
    public int[] goldPlayer = new int[5];
}

public class SaveGame : MonoBehaviour
{
    private static SaveGame instance;
    public static SaveGame Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<SaveGame>();
            }
            return instance;
        }
    }

    public SaveData saveData;

    public void Save()
    {
        var dataPath = Application.persistentDataPath + "/saveGame.data";
        var binary = new BinaryFormatter();
        var fileStream = File.Open(dataPath, FileMode.OpenOrCreate);
        var saveDataGame = new SaveData();
        saveDataGame = saveData;
        binary.Serialize(fileStream, saveDataGame);
        fileStream.Close();
    }

    public void Load()
    {
        var dataPath = Application.persistentDataPath + "/saveGame.data";
        if (File.Exists(dataPath))
        {
            var binary = new BinaryFormatter();
            var fileStream = File.Open(dataPath, FileMode.Open);
            saveData = (SaveData)binary.Deserialize(fileStream);
            fileStream.Close();
        }
        else
        {
            Debug.Log("None");
        }
    }




}

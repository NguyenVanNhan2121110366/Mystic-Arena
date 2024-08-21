using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class SaveData
{
    public int[] goldPlayer = new int[5];
    public float[] scoreBlood = new float[5];
    public float[] scoreShield = new float[5];
    public float[] scoreMana = new float[5];
    public int[,] bloodItem = new int[5, 5];
    public bool[] isCheck = new bool[5];
    public bool[] checkBuySkill = new bool[5];
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
        var dataPath = Application.persistentDataPath + "/gamedata.data";
        var binary = new BinaryFormatter();
        var fileStream = File.Open(dataPath, FileMode.OpenOrCreate);
        binary.Serialize(fileStream, saveData);
        fileStream.Close();
    }

    public void Load()
    {
        var dataPath = Application.persistentDataPath + "/gamedata.data";
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

    private void OnEnable()
    {
        Load();
    }




}

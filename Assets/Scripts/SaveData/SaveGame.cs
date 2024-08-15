using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


public class SaveData
{
    public int[] gold = new int[5];
}
public class SaveGame : MonoBehaviour
{
    private SaveGame instance;
    public SaveGame Instance
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
        // 
        var binaryFormatter = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/Game.data", FileMode.OpenOrCreate);
        var saveDataGame = new SaveData();
        saveDataGame = saveData;
        binaryFormatter.Serialize(file, saveDataGame);
        file.Close();
    }


    public void Load()
    {
        var filePath = Application.persistentDataPath + "/Game.data";
        if (File.Exists(filePath))
        {
            var binary = new BinaryFormatter();
            var file = File.Open(filePath, FileMode.Open);
            saveData = (SaveData)binary.Deserialize(file);
            file.Close();
        }
    }
}

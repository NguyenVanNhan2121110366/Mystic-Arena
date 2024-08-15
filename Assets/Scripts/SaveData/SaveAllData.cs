using UnityEngine;

public class SaveAllData : MonoBehaviour
{
    private ShopItem shopItem;
    private void Awake()
    {
        this.shopItem = FindFirstObjectByType<ShopItem>();
    }
    public void SaveDataGoldPlayer()
    {
        SaveGame.Instance.saveData.goldPlayer[0] = Player.Instance.CurrentGold;
        SaveGame.Instance.Save();
        Debug.Log("Save data");
    }

    public void LoadDataGoldPlayer()
    {
        SaveGame.Instance.Load();
        this.shopItem.GoldPlayer = SaveGame.Instance.saveData.goldPlayer[0];
        Debug.Log("Load data");
    }

    public void LoadDataGoldGame()
    {
        SaveGame.Instance.Load();
        Player.Instance.CurrentGold = SaveGame.Instance.saveData.goldPlayer[0];

    }
}

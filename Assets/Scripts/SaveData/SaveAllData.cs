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
        SaveGame.Instance.saveData.goldPlayer[1] = SaveGame.Instance.saveData.goldPlayer[0] + SaveGame.Instance.saveData.goldPlayer[1];

        SaveGame.Instance.Save();
        Debug.Log("Save data");
    }

    public void ResetData()
    {
        SaveGame.Instance.saveData.goldPlayer[0] = 0;
        SaveGame.Instance.saveData.goldPlayer[1] = 0;
    }
}

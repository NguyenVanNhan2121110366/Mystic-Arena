using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
public class ShopManager : MonoBehaviour
{
    protected int goldPlayer;
    [SerializeField] private TextMeshProUGUI txtGold;
    protected Button bntTest;
    protected Item item;
    public int GoldPlayer { get => goldPlayer; set => goldPlayer = value; }

    private void Awake()
    {
        this.txtGold = GameObject.Find("txtGold").GetComponent<TextMeshProUGUI>();
        this.item = FindFirstObjectByType<Item>();
        // this.LoadDataGold();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.LoadGold();
        Debug.Log(goldPlayer);
    }
    private void Update()
    {
        Debug.Log(goldPlayer);
    }


    protected virtual void LoadGold()
    {
        if (txtGold != null)
            this.txtGold.text = this.goldPlayer.ToString();
    }

    protected virtual void CheckClick()
    {
        SceneManager.LoadScene("Menu");
    }
    protected virtual void BuyItem(int index, int itemIndex)
    {
        if (goldPlayer >= this.item.Price)
        {
            this.goldPlayer -= this.item.Price;
            this.SaveGoldBuyPlayer();
            this.LoadGold();
            SaveGame.Instance.saveData.bloodItem[index, itemIndex]++;
            SaveGame.Instance.Save();
            Debug.Log(SaveGame.Instance.saveData.bloodItem[index, itemIndex]);
        }
        else
        {
            Debug.Log("Khong du tien");
        }
    }

    protected virtual void BuySkill(int index, bool isCheck)
    {
        if (goldPlayer >= this.item.Price)
        {
            this.goldPlayer -= this.item.Price;
            this.SaveGoldBuyPlayer();
            this.LoadGold();
            SaveGame.Instance.saveData.checkBuySkill[index] = isCheck;
            SaveGame.Instance.Save();
            Debug.Log(SaveGame.Instance.saveData.checkBuySkill[index]);
        }
        else
        {
            Debug.Log("Khong du tien");
        }
    }



    protected virtual void SaveGoldBuyPlayer()
    {
        SaveGame.Instance.saveData.goldPlayer[1] = goldPlayer;
        Debug.Log("Save Price");
    }

    protected virtual void LoadDataGold()
    {
        SaveGame.Instance.Load();
        this.goldPlayer = SaveGame.Instance.saveData.goldPlayer[1];
    }
}

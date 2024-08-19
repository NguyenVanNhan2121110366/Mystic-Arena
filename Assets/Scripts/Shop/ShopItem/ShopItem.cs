using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
public class ShopItem : MonoBehaviour
{
    [SerializeField] private int goldPlayer;
    [SerializeField] private TextMeshProUGUI txtGold;
    [SerializeField] private Button bntTest;
    [SerializeField] private int currentGold;
    [SerializeField] private Button bntBuyBlood;
    [SerializeField] private int countItemsBlood;
    [SerializeField] private Button bntBuyMana;
    [SerializeField] private Button bntBuyShield;
    private SaveAllData saveAllData;
    private Item item;
    public int GoldPlayer { get => goldPlayer; set => goldPlayer = value; }

    private void Awake()
    {
        this.item = FindFirstObjectByType<Item>();
        this.bntBuyBlood = GameObject.Find("ButtonBlood").GetComponent<Button>();
        this.bntBuyMana = GameObject.Find("ButtonMana").GetComponent<Button>();
        this.bntBuyShield = GameObject.Find("ButtonShield").GetComponent<Button>();
        this.saveAllData = FindAnyObjectByType<SaveAllData>();
        this.txtGold = GameObject.Find("txtGold").GetComponent<TextMeshProUGUI>();
        this.bntTest = GameObject.Find("Button").GetComponent<Button>();
        if (bntTest != null)
            this.bntTest.onClick.AddListener(this.CheckClick);
        this.LoadDataGold();
        this.bntBuyBlood.onClick.AddListener(this.ClickBuyBlood);
        this.bntBuyMana.onClick.AddListener(this.ClickBuyMana);
        this.bntBuyShield.onClick.AddListener(this.ClickBuyShield);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.LoadGold();
        Debug.Log(goldPlayer);
        this.countItemsBlood = 0;
    }
    private void Update()
    {
        Debug.Log(goldPlayer);
    }


    private void LoadGold()
    {
        if (txtGold != null)
            this.txtGold.text = this.goldPlayer.ToString();
    }

    private void CheckClick()
    {
        SceneManager.LoadScene("Game");
    }
    private void BuyItem(int index, int itemIndex)
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


    private void ClickBuyBlood()
    {
        this.BuyItem(0, 0);
    }

    private void ClickBuyMana()
    {
        this.BuyItem(0, 1);
    }

    private void ClickBuyShield()
    {
        this.BuyItem(0, 2);
    }


    private void SaveGoldBuyPlayer()
    {
        SaveGame.Instance.saveData.goldPlayer[1] = goldPlayer;
        Debug.Log("Save Price");
    }

    private void LoadDataGold()
    {
        SaveGame.Instance.Load();
        this.goldPlayer = SaveGame.Instance.saveData.goldPlayer[1];
    }
}

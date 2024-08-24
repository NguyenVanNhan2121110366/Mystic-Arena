using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
public class ShopItem : ShopManager
{
    [SerializeField] private Button bntBuyBlood;
    [SerializeField] private Button bntBuyMana;
    [SerializeField] private Button bntBuyShield;

    private void Awake()
    {
        this.saveAllData = FindFirstObjectByType<SaveAllData>();
        this.item = FindFirstObjectByType<Item>();
        this.bntBuyBlood = GameObject.Find("ButtonBlood").GetComponent<Button>();
        this.bntBuyMana = GameObject.Find("ButtonMana").GetComponent<Button>();
        this.bntBuyShield = GameObject.Find("ButtonShield").GetComponent<Button>();
        this.bntTest = GameObject.Find("Button").GetComponent<Button>();
        this.txtGold = GameObject.Find("txtGold").GetComponent<TextMeshProUGUI>();
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
        this.saveAllData.audioSource.volume = SaveGame.Instance.saveData.saveSound[0];
    }

    private void ClickBuyBlood()
    {
        this.item.Price = 20;
        this.BuyItem(0, 0);
    }

    private void ClickBuyMana()
    {
        this.item.Price = 60;
        this.BuyItem(0, 1);
    }

    private void ClickBuyShield()
    {
        this.item.Price = 40;
        this.BuyItem(0, 2);
    }
}

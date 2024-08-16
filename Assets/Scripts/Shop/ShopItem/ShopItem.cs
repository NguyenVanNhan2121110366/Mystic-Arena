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
    // [SerializeField] private Button bntBuyMana;
    // [SerializeField] private Button bntBuyShield;
    private SaveAllData saveAllData;
    private Item item;
    public int GoldPlayer { get => goldPlayer; set => goldPlayer = value; }

    private void Awake()
    {
        this.item = FindFirstObjectByType<Item>();
        this.bntBuyBlood = GameObject.Find("ButtonBlood").GetComponent<Button>();
        this.saveAllData = FindAnyObjectByType<SaveAllData>();
        this.txtGold = GameObject.Find("txtGold").GetComponent<TextMeshProUGUI>();
        this.bntTest = GameObject.Find("Button").GetComponent<Button>();
        if (bntTest != null)
            this.bntTest.onClick.AddListener(this.CheckClick);
        this.LoadDataGold();
        this.bntBuyBlood.onClick.AddListener(this.ClickBuyBlood);
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


    private void LoadGold()
    {
        if (txtGold != null)
            this.txtGold.text = this.goldPlayer.ToString();
    }

    private void CheckClick()
    {
        SceneManager.LoadScene("Game");
    }
    private void ClickBuyBlood()
    {
        if (goldPlayer >= this.item.Price)
        {
            this.goldPlayer -= this.item.Price;
            this.SaveGoldBuyPlayer();
            Debug.Log("Click Buy");
            Debug.Log(goldPlayer);
            this.LoadGold();
        }
        else
        {
            Debug.Log("Khong du tien");
        }
    }

    private void SaveGoldBuyPlayer()
    {
        SaveGame.Instance.saveData.goldPlayer[1] = goldPlayer;
        SaveGame.Instance.Save();
        Debug.Log("Save Price");
    }


    private void LoadDataGold()
    {
        SaveGame.Instance.Load();
        this.goldPlayer = SaveGame.Instance.saveData.goldPlayer[1];
    }
}

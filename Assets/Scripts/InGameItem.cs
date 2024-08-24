using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameItem : MonoBehaviour
{
    [SerializeField] private Button bntBlood;
    [SerializeField] private Button bntMana;
    [SerializeField] private Button bntShield;
    [SerializeField] private int quantityBlood;
    [SerializeField] private int quantityMana;
    [SerializeField] private int quantityShield;
    [SerializeField] private TextMeshProUGUI txtQuantityBlood;
    [SerializeField] private TextMeshProUGUI txtQuantityMana;
    [SerializeField] private TextMeshProUGUI txtQuantityShield;
    [SerializeField] private int healBlood;
    [SerializeField] private int healMana;
    [SerializeField] private int healShield;
    private Player player;

    private void Awake()
    {
        this.player = FindFirstObjectByType<Player>();
        this.bntBlood = GameObject.Find("BloodItem").GetComponent<Button>();
        this.bntMana = GameObject.Find("ManaItem").GetComponent<Button>();
        this.bntShield = GameObject.Find("ShieldItem").GetComponent<Button>();
        this.txtQuantityBlood = GameObject.Find("txtBloodItem").GetComponent<TextMeshProUGUI>();
        this.txtQuantityMana = GameObject.Find("txtManaItem").GetComponent<TextMeshProUGUI>();
        this.txtQuantityShield = GameObject.Find("txtShieldItem").GetComponent<TextMeshProUGUI>();
        this.LoadDataItem();
        this.bntBlood.onClick.AddListener(this.ClickBlood);
        this.bntMana.onClick.AddListener(this.ClickMana);
        this.bntShield.onClick.AddListener(this.ClickShield);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.healBlood = 200;
        this.healMana = 200;
        this.healShield = 150;
        this.UpdateQuantityItem();

    }

    private void UseItem(int indexItem, int quantity, TextMeshProUGUI txtQuantity, int healAmout, Action<int> updatePlayerScore)
    {
        var item = SaveGame.Instance.saveData.bloodItem[0, indexItem];
        if (item > 0)
        {
            item--;
            quantity = item;
            SaveGame.Instance.saveData.bloodItem[0, indexItem] = quantity;
            txtQuantity.text = quantity.ToString();
            SaveGame.Instance.Save();
            updatePlayerScore(healAmout);
        }
        else
        {
            Debug.Log("Het mana");
        }
    }

    private void ClickBlood()
    {
        UseItem(0, quantityBlood, txtQuantityBlood, healBlood, (healBlood) => this.player.CurrentScoreHeal += healBlood);
    }

    private void ClickMana()
    {
        UseItem(1, quantityMana, txtQuantityMana, healMana, (healMana) => this.player.CurrentScoreMana += healMana);
    }

    private void ClickShield()
    {
        UseItem(2, quantityShield, txtQuantityShield, healShield, (healShield) => this.player.CurrentScoreShield += healShield);
    }

    public void UpdateQuantityItem()
    {
        this.txtQuantityBlood.text = this.quantityBlood.ToString();
        this.txtQuantityMana.text = this.quantityMana.ToString();
        this.txtQuantityShield.text = this.quantityShield.ToString();
    }

    public void LoadDataItem()
    {
        SaveGame.Instance.Load();
        this.quantityBlood = SaveGame.Instance.saveData.bloodItem[0, 0];
        this.quantityMana = SaveGame.Instance.saveData.bloodItem[0, 1];
        this.quantityShield = SaveGame.Instance.saveData.bloodItem[0, 2];
    }
}

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

    private void Awake()
    {
        this.bntBlood = GameObject.Find("BloodItem").GetComponent<Button>();
        this.bntMana = GameObject.Find("ManaItem").GetComponent<Button>();
        this.bntShield = GameObject.Find("ShieldItem").GetComponent<Button>();
        this.txtQuantityBlood = GameObject.Find("txtBloodItem").GetComponent<TextMeshProUGUI>();
        this.txtQuantityMana = GameObject.Find("txtManaItem").GetComponent<TextMeshProUGUI>();
        this.txtQuantityShield = GameObject.Find("txtShieldItem").GetComponent<TextMeshProUGUI>();
        this.LoadData();
        this.bntBlood.onClick.AddListener(this.ClickBlood);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.UpdateQuantityItem();

    }

    private void ClickBlood()
    {

        var updateBlood = SaveGame.Instance.saveData.bloodItem[0, 0];
        if (updateBlood >= 0)
        {
            updateBlood--;
            this.quantityBlood = updateBlood;
            SaveGame.Instance.saveData.bloodItem[0, 0] = quantityBlood;
            SaveGame.Instance.Save();
            this.Updatetxt();
            Debug.Log(quantityBlood);
        }
        else
        {
            Debug.Log("Het mau");
        }


    }

    private void Updatetxt()
    {
        this.txtQuantityBlood.text = quantityBlood.ToString();
    }


    private void ClickMana()
    {

    }

    private void ClickShied()
    {

    }
    private void UpdateQuantityItem()
    {
        this.txtQuantityBlood.text = this.quantityBlood.ToString();
        this.txtQuantityMana.text = this.quantityMana.ToString();
        this.txtQuantityShield.text = this.quantityShield.ToString();
    }

    private void LoadData()
    {
        SaveGame.Instance.Load();
        this.quantityBlood = SaveGame.Instance.saveData.bloodItem[0, 0];
        this.quantityMana = SaveGame.Instance.saveData.bloodItem[0, 1];
        this.quantityShield = SaveGame.Instance.saveData.bloodItem[0, 2];
    }
}

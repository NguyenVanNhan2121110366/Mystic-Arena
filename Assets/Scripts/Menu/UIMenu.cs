using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIMenu : MonoBehaviour
{
    [SerializeField] private Button bntStart;
    [SerializeField] private Button bntShop;
    [SerializeField] private GameObject chooseShop;
    [SerializeField] private Button bntShopItem;
    [SerializeField] private Button bntShopSkill;
    private SaveAllData saveAllData;

    private bool checkChooseShop;

    private void Awake()
    {
        this.saveAllData = FindFirstObjectByType<SaveAllData>();
        this.chooseShop = GameObject.Find("ChooseShop");
        this.bntShop = GameObject.Find("Shop").GetComponent<Button>();
        this.bntShopItem = GameObject.Find("ShopItem").GetComponent<Button>();
        this.bntShopSkill = GameObject.Find("ShopSkill").GetComponent<Button>();
        this.bntStart.onClick.AddListener(ClickStart);
        this.bntShop.onClick.AddListener(ClickShop);
        this.bntShopItem.onClick.AddListener(ClickShopItem);
    }
    private void Start()
    {
        this.checkChooseShop = false;
        this.chooseShop.SetActive(false);
    }
    private void ClickStart()
    {
        SceneManager.LoadScene("Game");
        this.saveAllData.ResetData();
    }

    private void ClickShop()
    {
        if (checkChooseShop)
        {
            this.chooseShop.SetActive(false);
            this.checkChooseShop = false;

        }
        else
        {
            this.chooseShop.SetActive(true);
            this.checkChooseShop = true;
        }

    }
    private void ClickShopItem()
    {
        SceneManager.LoadScene("ShopItem");
    }

    private void ClickShopSkill()
    {

    }
}

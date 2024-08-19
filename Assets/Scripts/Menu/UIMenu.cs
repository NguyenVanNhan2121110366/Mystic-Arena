using System.Diagnostics;
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
    [SerializeField] private Button bntExit;
    [SerializeField] private Button bntContinue;
    [SerializeField] private Button bntVolume;
    [SerializeField] private bool checkNewGame;
    [SerializeField] private bool checkContinue;
    [SerializeField] private Button bntVolumeOff;
    [SerializeField] private GameObject objVolume;
    [SerializeField] private GameObject objVolumeOff;

    private SaveAllData saveAllData;

    private bool checkChooseShop;

    private void Awake()
    {
        this.objVolume = GameObject.Find("Volume");
        this.objVolumeOff = GameObject.Find("VolumeOff");
        this.saveAllData = FindFirstObjectByType<SaveAllData>();
        this.chooseShop = GameObject.Find("ChooseShop");
        this.bntShop = GameObject.Find("Shop").GetComponent<Button>();
        this.bntShopItem = GameObject.Find("ShopItem").GetComponent<Button>();
        this.bntShopSkill = GameObject.Find("ShopSkill").GetComponent<Button>();
        this.bntContinue = GameObject.Find("Continue").GetComponent<Button>();
        this.bntVolume = GameObject.Find("Volume").GetComponent<Button>();
        this.bntExit = GameObject.Find("Exit").GetComponent<Button>();
        this.bntVolumeOff = GameObject.Find("VolumeOff").GetComponent<Button>();
        this.bntStart.onClick.AddListener(ClickStart);
        this.bntShop.onClick.AddListener(ClickShop);
        this.bntShopItem.onClick.AddListener(ClickShopItem);
        this.bntContinue.onClick.AddListener(ClickContinue);
        this.bntVolume.onClick.AddListener(ClickVolume);
        this.bntVolumeOff.onClick.AddListener(ClickVolumeOff);
        this.bntExit.onClick.AddListener(ClickExit);
    }
    private void Start()
    {
        this.checkChooseShop = false;
        this.chooseShop.SetActive(false);
        this.checkContinue = false;
        this.checkNewGame = false;
        this.objVolumeOff.SetActive(false);
    }
    private void ClickStart()
    {
        SceneManager.LoadScene("Game");
        this.saveAllData.ResetData();
        this.checkNewGame = true;
        this.SaveValueIsCheck(0, checkNewGame);
    }

    private void ClickContinue()
    {
        SceneManager.LoadScene("Game");
        this.checkContinue = true;
        this.SaveValueIsCheck(1, checkContinue);
    }

    private void ClickVolume()
    {
        this.objVolume.SetActive(false);
        this.objVolumeOff.SetActive(true);
    }

    private void ClickVolumeOff()
    {
        this.objVolume.SetActive(true);
        this.objVolumeOff.SetActive(false);
    }

    private void SaveValueIsCheck(int value, bool nameCheck)
    {
        SaveGame.Instance.saveData.isCheck[value] = nameCheck;
        SaveGame.Instance.Save();
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
    private void ClickExit()
    {
        Application.Quit();
    }
}

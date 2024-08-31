
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
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject objFillNote;
    [SerializeField] private Button bntNote;
    [SerializeField] private Button bntFillExit;

    private SaveAllData saveAllData;

    private bool checkChooseShop;

    private void Awake()
    {
        //GetComponent
        this.bntFillExit = GameObject.Find("BntExit").GetComponent<Button>();
        this.objFillNote = GameObject.Find("NoteObj");
        this.bntNote = GameObject.Find("bntNote").GetComponent<Button>();
        this.saveAllData = FindFirstObjectByType<SaveAllData>();
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

        //Check click button
        this.bntStart.onClick.AddListener(ClickStart);
        this.bntShop.onClick.AddListener(ClickShop);
        this.bntShopItem.onClick.AddListener(ClickShopItem);
        this.bntContinue.onClick.AddListener(ClickContinue);
        this.bntVolume.onClick.AddListener(ClickVolume);
        this.bntVolumeOff.onClick.AddListener(ClickVolumeOff);
        this.bntExit.onClick.AddListener(ClickExit);
        this.bntShopSkill.onClick.AddListener(this.ClickShopSkill);
        this.bntNote.onClick.AddListener(this.ClickNote);
        this.bntFillExit.onClick.AddListener(this.ClickFillExit);
    }
    private void Start()
    {
        SaveGame.Instance.Load();
        this.objVolume.SetActive(SaveGame.Instance.saveData.isCheckSound[0]);
        this.objVolumeOff.SetActive(SaveGame.Instance.saveData.isCheckSound[1]);
        this.saveAllData.audioSource.volume = SaveGame.Instance.saveData.saveSound[0];
        this.checkChooseShop = false;
        this.chooseShop.SetActive(false);
        this.checkContinue = false;
        this.checkNewGame = false;
        this.objFillNote.SetActive(false);
    }
    private void ClickStart()
    {
        SceneManager.LoadScene("Game");
        this.saveAllData.ResetData();
    }

    private void ClickContinue()
    {
        if (SaveGame.Instance.saveData.isCheck[1])
        {
            SceneManager.LoadScene("Game");
        }
        else
            return;

    }

    private void ClickVolume()
    {
        SoundManager.Instance.audioSrc.PlayOneShot(SoundManager.Instance.SoundClick);
        this.objVolume.SetActive(false);
        this.objVolumeOff.SetActive(true);
        this.saveAllData.audioSource.volume = 0;
        SaveGame.Instance.saveData.saveSound[0] = 0;
        SaveGame.Instance.saveData.isCheckSound[0] = false;
        SaveGame.Instance.saveData.isCheckSound[1] = true;
        SaveGame.Instance.Save();
    }

    private void ClickVolumeOff()
    {
        SoundManager.Instance.audioSrc.PlayOneShot(SoundManager.Instance.SoundClick);
        this.objVolume.SetActive(true);
        this.objVolumeOff.SetActive(false);
        this.saveAllData.audioSource.volume = 0.3f;
        SaveGame.Instance.saveData.saveSound[0] = 0.3f;
        SaveGame.Instance.saveData.isCheckSound[0] = true;
        SaveGame.Instance.saveData.isCheckSound[1] = false;
        SaveGame.Instance.Save();
    }

    private void ClickShop()
    {
        SoundManager.Instance.audioSrc.PlayOneShot(SoundManager.Instance.SoundClick);
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
        SceneManager.LoadScene("ShopSkill");
    }

    private void ClickNote()
    {
        this.objFillNote.SetActive(true);
    }

    private void ClickFillExit()
    {
        this.objFillNote.SetActive(false);
    }
    private void ClickExit()
    {
        Application.Quit();
    }
}

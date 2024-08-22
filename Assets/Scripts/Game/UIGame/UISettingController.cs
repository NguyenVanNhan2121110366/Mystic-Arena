using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UISettingController : MonoBehaviour
{
    private SaveAllData save;

    [SerializeField] private Button reset;
    [SerializeField] private Button bntHome;
    [SerializeField] private Button bntResume;
    [SerializeField] private Button bntSave;
    [SerializeField] private Button bntSetting;
    [SerializeField] private GameObject settingBar;
    [SerializeField] private Button bntLoadGame;
    private bool isCheckSetting;

    private void Awake()
    {
        this.bntHome = GameObject.Find("Home").GetComponent<Button>();
        this.bntResume = GameObject.Find("Resume").GetComponent<Button>();
        this.bntSave = GameObject.Find("SaveGame").GetComponent<Button>();
        this.bntSetting = GameObject.Find("Setting").GetComponent<Button>();
        this.bntLoadGame = GameObject.Find("LoadGame").GetComponent<Button>();
        this.settingBar = GameObject.Find("SettingBar");
        this.save = FindFirstObjectByType<SaveAllData>();
        this.bntSetting.onClick.AddListener(this.ClickSetting);
        this.bntHome.onClick.AddListener(this.ClickHome);
        this.bntResume.onClick.AddListener(this.ClickResume);
        this.bntSave.onClick.AddListener(this.ClickSave);
        this.bntLoadGame.onClick.AddListener(this.ClickLoad);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.isCheckSetting = false;
        this.settingBar.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ClickSetting()
    {
        if (isCheckSetting)
        {
            this.settingBar.SetActive(false);
            this.isCheckSetting = false;
        }
        else
        {
            this.settingBar.SetActive(true);
            this.isCheckSetting = true;
        }
    }

    private void ClickHome()
    {
        SceneManager.LoadScene("Menu");
    }

    private void ClickResume()
    {
        this.settingBar.SetActive(false);
        this.isCheckSetting = false;
    }

    private void ClickSave()
    {
        this.save.SaveAllDataGame();
        this.save.SaveDataGoldPlayer();
    }
    private void ClickLoad()
    {
        this.save.LoadAllDataGame();
    }
}

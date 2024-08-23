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
    [SerializeField] private Button bntRestart;
    [SerializeField] private Button bntSetting;
    [SerializeField] private GameObject settingBar;
    [SerializeField] private GameObject fill;
    private bool isCheckSetting;
    private InGameItem inGameItem;

    private void Awake()
    {
        this.inGameItem = FindAnyObjectByType<InGameItem>();
        this.fill = GameObject.Find("Fill");
        this.bntHome = GameObject.Find("Home").GetComponent<Button>();
        this.bntResume = GameObject.Find("Resume").GetComponent<Button>();
        this.bntRestart = GameObject.Find("Restart").GetComponent<Button>();
        this.bntSetting = GameObject.Find("Setting").GetComponent<Button>();
        this.settingBar = GameObject.Find("SettingBar");
        this.save = FindFirstObjectByType<SaveAllData>();
        this.bntSetting.onClick.AddListener(this.ClickSetting);
        this.bntHome.onClick.AddListener(this.ClickHome);
        this.bntResume.onClick.AddListener(this.ClickResume);
        this.bntRestart.onClick.AddListener(this.ClickRestart);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.isCheckSetting = false;
        this.settingBar.SetActive(false);
        this.fill.SetActive(false);
    }

    private void ClickSetting()
    {
        if (isCheckSetting)
        {
            this.settingBar.SetActive(false);
            this.isCheckSetting = false;
            Time.timeScale = 1;
            this.fill.SetActive(false);
        }
        else
        {
            this.settingBar.SetActive(true);
            this.isCheckSetting = true;
            Time.timeScale = 0;
            this.fill.SetActive(true);
        }
    }

    private void ClickHome()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }

    private void ClickResume()
    {
        this.settingBar.SetActive(false);
        this.isCheckSetting = false;
        Time.timeScale = 1;
        this.fill.SetActive(false);
    }

    private void ClickRestart()
    {
        this.fill.SetActive(false);
        this.settingBar.SetActive(false);
        this.isCheckSetting = false;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        //this.save.SaveDataPlayer();
        this.inGameItem.LoadDataItem();
        this.inGameItem.UpdateQuantityItem();
        this.save.LoadDataPlayer();
    }
}

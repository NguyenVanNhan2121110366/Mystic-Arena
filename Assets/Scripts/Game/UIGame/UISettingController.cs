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
    private AllDotController allDots;
    private bool isCheckSetting;
    private InGameItem inGameItem;

    private void Awake()
    {
        this.allDots = FindFirstObjectByType<AllDotController>();
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
        SoundManager.Instance.audioSrc.PlayOneShot(SoundManager.Instance.SoundClick);
        if (!isCheckSetting)
        {
            this.settingBar.SetActive(true);
            this.isCheckSetting = true;
            Time.timeScale = 0;
            this.fill.SetActive(true);
        }
    }

    private void ClickHome()
    {
        SaveGame.Instance.saveData.isCheck[1] = true;
        SaveGame.Instance.Save();
        this.DestroyAllDot();
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }

    public void DestroyAllDot()
    {
        for (var i = 0; i < this.allDots.Width; i++)
        {
            for (var j = 0; j < this.allDots.Height; j++)
            {
                Destroy(this.allDots.AllDots[i, j]);
                this.allDots.AllDots[i, j] = null;
            }
        }
    }

    private void ClickResume()
    {
        SoundManager.Instance.audioSrc.PlayOneShot(SoundManager.Instance.SoundClick);
        this.settingBar.SetActive(false);
        this.isCheckSetting = false;
        Time.timeScale = 1;
        this.fill.SetActive(false);
    }

    private void ClickRestart()
    {
        SoundManager.Instance.audioSrc.PlayOneShot(SoundManager.Instance.SoundClick);
        this.fill.SetActive(false);
        this.settingBar.SetActive(false);
        this.isCheckSetting = false;
        Time.timeScale = 1;
        this.inGameItem.LoadDataItem();
        this.inGameItem.UpdateQuantityItem();
        this.save.LoadDataPlayer();
    }
}

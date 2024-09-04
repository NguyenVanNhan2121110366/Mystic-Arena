using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGameOver : MonoBehaviour
{
    private SaveAllData allData;
    [SerializeField] private Button bntHome;
    [SerializeField] private Button bntRestart;
    [SerializeField] private GameObject fillGameOver;
    [SerializeField] private GameObject gameOver;
    private UISettingController uISettingController;
    private SaveAllData save;
    private void Awake()
    {
        this.uISettingController = FindFirstObjectByType<UISettingController>();
        this.save = FindAnyObjectByType<SaveAllData>();
        this.allData = FindFirstObjectByType<SaveAllData>();
        this.gameOver = GameObject.Find("GameOver");
        this.fillGameOver = GameObject.Find("Fill");
        this.bntHome = GameObject.Find("gOvHome").GetComponent<Button>();
        this.bntRestart = GameObject.Find("gOvRestart").GetComponent<Button>();
        this.bntHome.onClick.AddListener(this.ClickHome);
        this.bntRestart.onClick.AddListener(this.ClickRestart);
    }

    private void ClickHome()
    {
        SaveGame.Instance.saveData.isCheck[1] = true;
        SaveGame.Instance.Save();
        this.uISettingController.DestroyAllDot();
        this.save.SaveDataGoldPlayer();
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
        
    }

    private void ClickRestart()
    {
        this.uISettingController.DestroyAllDot();
        SoundManager.Instance.audioSrc.PlayOneShot(SoundManager.Instance.SoundClick);
        if (SaveGame.Instance.saveData.scoreBlood[1] > 0)
        {
            this.fillGameOver.SetActive(false);
            this.gameOver.SetActive(false);
            Time.timeScale = 1;
            this.allData.LoadDataPlayer();
        }
        else
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

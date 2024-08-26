using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIWinGame : MonoBehaviour
{
    [SerializeField] private Button bntHome;
    [SerializeField] private Button bntNextGame;
    [SerializeField] private GameObject description;
    [SerializeField] private GameObject backHome;
    [SerializeField] private Button bntBackHome;
    private UISettingController uISettingController;
    private SaveAllData save;
    private SpawnEnemy spawnEnemy;

    private void Awake()
    {
        this.uISettingController = FindAnyObjectByType<UISettingController>();
        this.bntBackHome = GameObject.Find("BackHome").GetComponent<Button>();
        this.backHome = GameObject.Find("BackHome");
        this.description = GameObject.Find("Descripttion");
        this.save = FindFirstObjectByType<SaveAllData>();
        this.spawnEnemy = FindFirstObjectByType<SpawnEnemy>();
        this.bntHome = GameObject.Find("WinHome").GetComponent<Button>();
        this.bntNextGame = GameObject.Find("WinNextGame").GetComponent<Button>();
        this.bntHome.onClick.AddListener(this.ClickHome);
        this.bntNextGame.onClick.AddListener(this.ClickNextGame);
        this.description.SetActive(false);
        this.backHome.SetActive(false);
        this.bntBackHome.onClick.AddListener(this.ClickBackHome);
    }

    private void ClickHome()
    {
        this.uISettingController.DestroyAllDot();
        this.spawnEnemy.CurrentLevel++;
        SaveGame.Instance.saveData.currentLevel[0] = this.spawnEnemy.CurrentLevel;
        SaveGame.Instance.Save();
        this.save.SaveDataPlayer();
        this.save.SaveDataGoldPlayer();
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }
    private void ClickNextGame()
    {
        SoundManager.Instance.audioSrc.PlayOneShot(SoundManager.Instance.SoundClick);
        this.spawnEnemy.CurrentLevel++;
        SaveGame.Instance.saveData.currentLevel[0] = this.spawnEnemy.CurrentLevel;
        SaveGame.Instance.Save();
        this.save.SaveDataPlayer();
        this.save.SaveDataGoldPlayer();
        this.save.LoadDataPlayer();
        this.spawnEnemy.ChooseEnemySpawn();
        Time.timeScale = 1;
        if (SaveGame.Instance.saveData.currentLevel[0] > 2)
        {
            Debug.Log("Win Game");
            Time.timeScale = 1;
            StartCoroutine(DelayDescription());
        }
    }

    private IEnumerator DelayDescription()
    {
        this.description.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        this.backHome.SetActive(true);
    }

    private void ClickBackHome()
    {
        SceneManager.LoadScene("Menu");
    }
}

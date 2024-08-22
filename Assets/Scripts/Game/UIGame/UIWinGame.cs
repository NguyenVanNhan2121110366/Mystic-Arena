using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIWinGame : MonoBehaviour
{
    [SerializeField] private Button bntHome;
    [SerializeField] private Button bntNextGame;
    private SpawnEnemy spawnEnemy;

    private void Awake()
    {
        this.spawnEnemy = FindFirstObjectByType<SpawnEnemy>();
        this.bntHome = GameObject.Find("WinHome").GetComponent<Button>();
        this.bntNextGame = GameObject.Find("WinNextGame").GetComponent<Button>();
        this.bntHome.onClick.AddListener(this.ClickHome);
        this.bntNextGame.onClick.AddListener(this.ClickNextGame);
    }

    private void ClickHome()
    {
        SceneManager.LoadScene("Menu");
    }
    private void ClickNextGame()
    {
        this.spawnEnemy.CurrentLevel++;
        SaveGame.Instance.saveData.currentLevel[0] = this.spawnEnemy.CurrentLevel;
        SaveGame.Instance.Save();
        this.spawnEnemy.ChooseEnemySpawn();
        Time.timeScale = 1;
        if (SaveGame.Instance.saveData.currentLevel[0] > 2)
        {
            Debug.Log("Win Game");
        }
    }
}

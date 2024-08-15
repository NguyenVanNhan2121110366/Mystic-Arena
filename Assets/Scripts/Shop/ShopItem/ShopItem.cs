using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
public class ShopItem : MonoBehaviour
{
    [SerializeField] private int goldPlayer;
    [SerializeField] private TextMeshProUGUI txtGold;
    [SerializeField] private Button bntTest;
    [SerializeField] private int currentGold;
    private SaveAllData saveAllData;
    public int GoldPlayer { get => goldPlayer; set => goldPlayer = value; }

    private void Awake()
    {
        this.saveAllData = FindAnyObjectByType<SaveAllData>();
        this.txtGold = GameObject.Find("txtGold").GetComponent<TextMeshProUGUI>();
        this.bntTest = GameObject.Find("Button").GetComponent<Button>();
        if (bntTest != null)
            this.bntTest.onClick.AddListener(this.CheckClick);
        SaveGame.Instance.Load();
        if (SaveGame.Instance.saveData.goldPlayer[1] == 0)
        {
            goldPlayer = SaveGame.Instance.saveData.goldPlayer[0];
        }
        if (SaveGame.Instance.saveData.goldPlayer[1] > 0)
        {
            goldPlayer = SaveGame.Instance.saveData.goldPlayer[1];
            goldPlayer += SaveGame.Instance.saveData.goldPlayer[0];
        }

        //goldPlayer += SaveGame.Instance.saveData.goldPlayer[1];
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        this.LoadGold();
    }
    private void Update()
    {
        SaveGame.Instance.saveData.goldPlayer[1] = this.goldPlayer;
        SaveGame.Instance.Save();
    }


    private void LoadGold()
    {
        if (txtGold != null)
            this.txtGold.text = this.goldPlayer.ToString();
    }

    private void CheckClick()
    {

        SceneManager.LoadScene("Game");

    }
}

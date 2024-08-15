using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ShopItem : MonoBehaviour
{
    [SerializeField] private int goldPlayer;
    [SerializeField] private TextMeshProUGUI txtGold;
    [SerializeField] private Button bntTest;

    private void Awake()
    {
        this.txtGold = GameObject.Find("txtGold").GetComponent<TextMeshProUGUI>();
        this.bntTest = GameObject.Find("Button").GetComponent<Button>();
        this.bntTest.onClick.AddListener(this.CheckClick);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Player.Instance != null)
            this.goldPlayer = Player.Instance.CurrentGold;
        this.LoadGold();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LoadGold()
    {
        this.txtGold.text = this.goldPlayer.ToString();
    }

    private void CheckClick()
    {
        SceneManager.LoadScene("Game");
    }
}

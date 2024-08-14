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
    private bool checkChooseShop;

    private void Awake()
    {
        this.bntStart.onClick.AddListener(ClickStart);
        this.bntShop.onClick.AddListener(ClickShop);
    }
    private void Start()
    {
        this.checkChooseShop = false;
    }
    private void ClickStart()
    {
        SceneManager.LoadScene("Game");
    }

    private void ClickShop()
    {
        if (checkChooseShop)
            this.chooseShop.SetActive(true);
        else
            this.chooseShop.SetActive(true);
    }
    private void ClickShopItem()
    {

    }

    private void ClickShopSkill()
    {

    }
}

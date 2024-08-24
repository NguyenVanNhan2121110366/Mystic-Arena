using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ShopSkill : ShopManager
{

    [SerializeField] private Button bntBuyAbilityFireBall;
    [SerializeField] private Button bntBuyAbilityHealBlood;
    [SerializeField] private Button bntBuyAbilityLightningBolt;
    [SerializeField] private GameObject fillHealBlood;
    [SerializeField] private GameObject fillFireball;

    private void Awake()
    {
        this.item = FindFirstObjectByType<Item>();
        this.txtGold = GameObject.Find("txtGold").GetComponent<TextMeshProUGUI>();
        this.bntBuyAbilityFireBall = GameObject.Find("ButtonFireBall").GetComponent<Button>();
        this.bntBuyAbilityHealBlood = GameObject.Find("ButtonHealBlood").GetComponent<Button>();
        this.bntBuyAbilityLightningBolt = GameObject.Find("ButtonLighningBolt").GetComponent<Button>();
        this.bntTest = GameObject.Find("Button").GetComponent<Button>();
        if (bntTest != null)
            this.bntTest.onClick.AddListener(this.CheckClick);
        this.LoadDataGold();
        this.LoadDataSkil();
        this.bntBuyAbilityFireBall.onClick.AddListener(this.ClickBuyAbilityFireBall);
        this.bntBuyAbilityHealBlood.onClick.AddListener(this.ClickBuyAbilityHealBlood);
        this.bntBuyAbilityLightningBolt.onClick.AddListener(this.ClickBuyAbilityLightningBolt);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.LoadGold();
    }

    protected override void CheckClick()
    {
        SceneManager.LoadScene("Menu");
    }

    private void ClickBuyAbilityLightningBolt()
    {
        this.item.Price = 200;
        if (!SaveGame.Instance.saveData.checkBuySkill[0])
        {
            if (goldPlayer >= item.Price)
            {
                this.BuySkill(0, true);
                SaveGame.Instance.saveData.checkFill[0] = false;
                this.fillHealBlood.SetActive(SaveGame.Instance.saveData.checkFill[0]);
                SaveGame.Instance.Save();
            }

        }
        else
            AudioManager.Instance.audioSrc.PlayOneShot(AudioManager.Instance.SoundBuyFalse);
    }

    private void ClickBuyAbilityHealBlood()
    {
        this.item.Price = 500;
        if (SaveGame.Instance.saveData.checkBuySkill[0] && !SaveGame.Instance.saveData.checkBuySkill[1])
        {
            this.BuySkill(1, true);
            SaveGame.Instance.saveData.checkFill[1] = false;
            this.fillFireball.SetActive(SaveGame.Instance.saveData.checkFill[1]);
        }
        else if (!SaveGame.Instance.saveData.checkBuySkill[0])
        {
            AudioManager.Instance.audioSrc.PlayOneShot(AudioManager.Instance.SoundBuyFalse);
        }

        else
        {
            AudioManager.Instance.audioSrc.PlayOneShot(AudioManager.Instance.SoundBuyFalse);
        }
    }

    private void ClickBuyAbilityFireBall()
    {
        this.item.Price = 900;
        if (SaveGame.Instance.saveData.checkBuySkill[1] && !SaveGame.Instance.saveData.checkBuySkill[2])
        {
            this.BuySkill(2, true);
        }
        else if (!SaveGame.Instance.saveData.checkBuySkill[1])
        {
            AudioManager.Instance.audioSrc.PlayOneShot(AudioManager.Instance.SoundBuyFalse);
        }
        else
        {
            AudioManager.Instance.audioSrc.PlayOneShot(AudioManager.Instance.SoundBuyFalse);
        }
    }

    private void LoadDataSkil()
    {
        SaveGame.Instance.Load();
        this.fillFireball.SetActive(SaveGame.Instance.saveData.checkFill[0]);
        this.fillHealBlood.SetActive(SaveGame.Instance.saveData.checkFill[1]);
    }
}

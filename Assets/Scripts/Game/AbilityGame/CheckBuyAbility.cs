using UnityEngine;
using UnityEngine.UI;

public class CheckBuyAbility : MonoBehaviour
{
    [SerializeField] private GameObject objFireBall;
    [SerializeField] private GameObject objHealBlood;
    [SerializeField] private GameObject objLightningBolt;

    private void Awake()
    {
        this.objFireBall = GameObject.Find("FireBall");
        this.objHealBlood = GameObject.Find("HealBlood");
        this.objLightningBolt = GameObject.Find("LightnightBolt");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.CheckBuy();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CheckBuy()
    {
        this.objFireBall.SetActive(SaveGame.Instance.saveData.checkBuySkill[0]);
        this.objHealBlood.SetActive(SaveGame.Instance.saveData.checkBuySkill[1]);
        this.objLightningBolt.SetActive(SaveGame.Instance.saveData.checkBuySkill[2]);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class LightingBoltAbility : Ability
{
    [SerializeField] private GameObject lineConnector;
    private void Awake()
    {
        this.ability = GameObject.Find("LightnightBolt").GetComponent<Button>();
        this.ability.onClick.AddListener(ExcuteAbility);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.amoutMana = 50;
    }

    protected override void ExcuteAbility()
    {
        base.ExcuteAbility();
        if (CheckCanUseAbility())
        {
            this.UpdateMana();
            var obj = Instantiate(this.lineConnector);
            obj.SetActive(true);
        }
    }
}

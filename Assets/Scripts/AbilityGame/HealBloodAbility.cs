using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class HealBloodAbility : Ability
{
    [SerializeField] private int healBlood;
    private void Awake()
    {
        this.ability = GameObject.Find("HealBlood").GetComponent<Button>();
        this.ability.onClick.AddListener(ExcuteAbility);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.amoutMana = 50;
        this.healBlood = 300;
    }

    protected override void ExcuteAbility()
    {
        base.ExcuteAbility();
        if (CheckCanUseAbility())
        {
            Player.Instance.animator.SetTrigger("ExcutedSkill");
            this.UpdateMana();
            StartCoroutine(this.HealBlood());
        }
    }

    private IEnumerator HealBlood()
    {
        yield return new WaitForSeconds(1f);
        if (TurnController.Instance.CurrentTurn == GameTurn.Player)
        {
            Player.Instance.CurrentScoreHeal += this.healBlood;
            Enemy.Instance.CurrentScoreHeal -= this.healBlood;
            GameStateController.Instance.CurrentGameState = GameState.FillingDot;
            Enemy.Instance.animator.SetTrigger("Hit");
        }
    }
}

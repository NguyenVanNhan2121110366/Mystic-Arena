using UnityEngine;
using UnityEngine.UI;
public class FireBallAbility : Ability
{
    [SerializeField] private GameObject prebFireBall;
    [SerializeField] private Transform posSpawnFireBallAttackEnemy;
    private void Awake()
    {
        this.ability = GameObject.Find("FireBall").GetComponent<Button>();
        this.ability.onClick.AddListener(this.ExcuteAbility);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.amoutMana = 350;
    }

    protected override void ExcuteAbility()
    {
        base.ExcuteAbility();
        if (CheckCanUseAbility())
        {
            this.UpdateMana();
            this.SpawnFireBall();
            Invoke(nameof(SpawnFireBallAttackDot), 2f);
            Player.Instance.animator.SetTrigger("ExcutedSkill");
        }
    }

    private void SpawnFireBall()
    {
        if (TurnController.Instance.CurrentTurn == GameTurn.Player)
        {
            var fireball = Instantiate(this.prebFireBall);
            fireball.transform.position = posSpawnFireBallAttackEnemy.position;
            fireball.SetActive(true);
            fireball.GetComponent<Fireball>().IsSendDame = true;
        }
    }


    private void SpawnFireBallAttackDot()
    {
        if (TurnController.Instance.CurrentTurn == GameTurn.Player)
        {
            var fireball = Instantiate(this.prebFireBall);
            fireball.transform.position = posSpawnFireBallAttackEnemy.position;
            fireball.SetActive(true);
            fireball.GetComponent<Fireball>().IsAttackDot = true;
        }
    }


}

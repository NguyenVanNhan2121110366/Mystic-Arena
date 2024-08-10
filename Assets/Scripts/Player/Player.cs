using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class Player : Character
{
    private static Player instance;
    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<Player>();
            }
            return instance;
        }
    }
    private ScoreController scoreController;
    private bool isAttack;
    private bool isIdle;
    [SerializeField] private bool isMoving;
    private bool isBackToBase;
    [SerializeField] private Transform targetEnemy;
    [SerializeField] private GameObject attackRate;
    [SerializeField] private Quaternion rotationPlayer;
    [SerializeField] private Vector2 posPlayer;
    public bool IsMoving { get => isMoving; set => isMoving = value; }
    private void Awake()
    {
        this.scoreController = FindFirstObjectByType<ScoreController>();
        this.animator = GetComponent<Animator>();
        this.shieldBar = GameObject.Find("ShieldBarPlayer").GetComponent<Image>();
        this.healBar = GameObject.Find("HealBarPlayer").GetComponent<Image>();
        this.manaBar = GameObject.Find("ManaBarPlayer").GetComponent<Image>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CurrentScoreHeal = MaxScoreHeal;
        CurrentScoreMana = 50f;
        CurrentScoreShield = 0f;
        this.rotationPlayer = transform.rotation;
        this.posPlayer = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.UpdateBar();
        this.Attacking();
    }

    public override void UpdateScorePlayer()
    {
        base.UpdateScorePlayer();
        ScoreHeal = this.scoreController.ScoreHeal;
        ScoreMana = this.scoreController.ScoreMana;
        ScoreAttack = this.scoreController.ScoreAttack;
        ScoreGold = this.scoreController.ScoreGold;
        ScoreShield = this.scoreController.ScoreShield;
    }

    public override void Attacking()
    {
        base.Attacking();
        if (TurnController.Instance.CurrentTurn == GameTurn.Player && GameStateController.Instance.CurrentGameState == GameState.Attacking)
        {
            this.isIdle = false;
            if (this.isMoving && !isAttack && !isBackToBase)
            {
                this.isIdle = false;
                transform.position = Vector2.Lerp(transform.position, targetEnemy.position, 9 * Time.deltaTime);

                if (Vector2.Distance(transform.position, targetEnemy.position) <= 0.1f)
                {
                    this.isAttack = true;
                }
            }
            if (this.isAttack)
            {
                this.attackRate.SetActive(true);
                StartCoroutine(this.DelayAttack());
            }
            if (this.isBackToBase)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                transform.position = Vector2.Lerp(transform.position, posPlayer, 9 * Time.deltaTime);
                if (Vector2.Distance(transform.position, posPlayer) <= 0.1f)
                {
                    isBackToBase = false;

                    transform.rotation = rotationPlayer;
                    GameStateController.Instance.CurrentGameState = GameState.FillingDot;
                }
            }
        }
        else
        {
            isMoving = false;
            isIdle = true;
            isAttack = false;
            isBackToBase = false;
            isMoving = false;
        }
        this.animator.SetBool("Idle", this.isIdle);
        this.animator.SetBool("Moving", this.isMoving);
        this.animator.SetBool("Attack", this.isAttack);
        this.animator.SetBool("BackToBase", this.isBackToBase);
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(0.5f);
        this.isAttack = false;
        this.isIdle = true;
        this.isBackToBase = true;
        this.attackRate.SetActive(false);
    }
}

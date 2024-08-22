using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using System.Collections;
public class Enemy : Character
{
    private static Enemy instance;
    public static Enemy Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<Enemy>();
            }
            return instance;
        }
    }
    private bool isAttack;
    private bool isIdle;
    [SerializeField] private bool isMoving;
    private bool isBackToBase;
    [SerializeField] private Transform targetPlayer;
    [SerializeField] private GameObject attackRate;
    [SerializeField] private Quaternion rotationPlayer;
    [SerializeField] private Vector2 posPlayer;
    [SerializeField] private GameObject winGame;
    [SerializeField] private GameObject fillWinGame;
    [SerializeField] private int dameEnemy;
    private ScoreController scoreController;
    public bool IsMoving { get => isMoving; set => isMoving = value; }
    public int DameEnemy { get => dameEnemy; set => dameEnemy = value; }
    private void Awake()
    {
        this.winGame = GameObject.Find("WinGame");
        this.fillWinGame = GameObject.Find("Fill");
        this.scoreController = FindFirstObjectByType<ScoreController>();
        this.shieldBar = GameObject.Find("ShieldBarEnemy").GetComponent<Image>();
        this.healBar = GameObject.Find("HealBarEnemy").GetComponent<Image>();
        this.manaBar = GameObject.Find("ManaBarEnemy").GetComponent<Image>();
        this.animator = GetComponent<Animator>();
        this.targetPlayer = GameObject.Find("TargetPlayer").GetComponent<Transform>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.winGame.SetActive(false);
        this.fillWinGame.SetActive(false);
        this.CurrentScoreHeal = this.MaxScoreHeal;
        this.CurrentScoreMana = 50f;
        this.rotationPlayer = transform.rotation;
        this.posPlayer = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.UpdateBar();
        this.Attacking();
    }

    public override void Attacking()
    {
        if (TurnController.Instance.CurrentTurn == GameTurn.Enemy)
        {
            this.isIdle = false;
            if (this.isMoving && !this.isAttack && !this.isBackToBase)
            {
                transform.position = Vector2.Lerp(transform.position, this.targetPlayer.position, 5 * Time.deltaTime);
                if (Vector2.Distance(transform.position, this.targetPlayer.position) < 0.1f)
                {
                    transform.position = this.targetPlayer.position;
                    this.isAttack = true;
                    this.isMoving = false;
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
                transform.position = Vector2.Lerp(transform.position, this.posPlayer, 5 * Time.deltaTime);
                if (Vector2.Distance(transform.position, this.posPlayer) < 0.1f)
                {
                    this.isBackToBase = false;
                    transform.rotation = rotationPlayer;
                    GameStateController.Instance.CurrentGameState = GameState.Finish;
                }
            }
        }
        else
        {
            this.isIdle = true;
            this.isAttack = false;
            this.isMoving = false;
            this.isBackToBase = false;
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
        this.attackRate.SetActive(false);
        this.isBackToBase = true;
    }

    public override void UpdateScore()
    {
        base.UpdateScore();
        ScoreHeal = this.scoreController.ScoreHeal;
        ScoreMana = this.scoreController.ScoreMana;
        ScoreAttack = this.scoreController.ScoreAttack;
        ScoreGold = 0;
        ScoreShield = this.scoreController.ScoreShield;
        if (ScoreAttack > 0)
        {
            ScoreAttack += this.dameEnemy;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("AttackEnemy"))
        {

            if (CurrentScoreHeal > 0)
            {
                this.animator.SetTrigger("Hit");
                Debug.Log("Test AttackEnemy");
                if (CurrentScoreShield > 0)
                {
                    this.CurrentScoreShield -= Player.Instance.ScoreAttack;
                    var remainingdame = Player.Instance.ScoreAttack - CurrentScoreShield;
                    if (remainingdame > 0)
                        CurrentScoreHeal -= remainingdame;

                }
                else
                {
                    this.CurrentScoreHeal -= Player.Instance.ScoreAttack;
                }

            }
            if (CurrentScoreHeal <= 0)
            {
                StartCoroutine(this.WinGame());
            }
        }
    }

    private IEnumerator WinGame()
    {
        yield return new WaitForSeconds(0.5f);
        this.winGame.SetActive(true);
        this.fillWinGame.SetActive(true);
        Destroy(gameObject);
        Time.timeScale = 0;
    }
}

using System.Collections;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public GameObject posAttackEnemy;
    private AllDotController allDots;
    private bool isAttackEnemy;
    private bool isSendDame;
    private bool isAttackDot;
    [SerializeField] private int dameFireball;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject dotTarget;
    [SerializeField] private int randomCol, randomRow;

    public bool IsSendDame { get => isSendDame; set => isSendDame = value; }
    public bool IsAttackDot { get => isAttackDot; set => isAttackDot = value; }
    private void Awake()
    {
        this.allDots = FindFirstObjectByType<AllDotController>();
        this.animator = GetComponent<Animator>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.isAttackEnemy = true;
        this.dameFireball = 150;
    }

    // Update is called once per frame
    void Update()
    {
        if (TurnController.Instance.CurrentTurn == GameTurn.Player)
        {
            this.FireBallAttackEnemy();
            this.AttackDot();
        }
    }

    private void FireBallAttackEnemy()
    {
        if (isAttackEnemy && isSendDame)
        {
            Debug.Log("Done");
            if (posAttackEnemy == null)
            {
                this.posAttackEnemy = GameObject.Find("TargetEnemy");
                transform.rotation = GetAngle(this.posAttackEnemy.transform.position);
            }
            if (Vector2.Distance(transform.position, this.posAttackEnemy.transform.position) == 0f)
            {
                Destroy(gameObject, 1f);
                if (isSendDame)
                {
                    this.animator.SetTrigger("Exploision");
                    this.isSendDame = false;
                    this.AttackEnemy();
                    Enemy.Instance.animator.SetTrigger("Hit");
                }
            }
            else
                transform.position = Vector2.MoveTowards(transform.position, this.posAttackEnemy.transform.position, 9 * Time.deltaTime);
        }
    }


    private void AttackDot()
    {
        if (isAttackEnemy && isAttackDot)
        {
            if (dotTarget == null)
            {
                dotTarget = RandomTargetDot();
                transform.rotation = GetAngle(this.dotTarget.transform.position);
                return;
            }
            if (Vector2.Distance(transform.position, dotTarget.transform.position) == 0f)
            {
                Destroy(gameObject, 1f);
                if (isAttackDot)
                {
                    this.animator.SetTrigger("Exploision");
                    this.isAttackDot = false;
                    StartCoroutine(this.DestroyDot());
                }
            }
            else
                transform.position = Vector2.MoveTowards(transform.position, dotTarget.transform.position, 9 * Time.deltaTime);
        }
    }

    private GameObject RandomTargetDot()
    {
        this.randomCol = Random.Range(2, this.allDots.Width - 2);
        this.randomRow = Random.Range(2, this.allDots.Height - 2);
        var targetDot = this.allDots.AllDots[randomCol, randomRow];
        return targetDot;
    }
    private IEnumerator DestroyDot()
    {
        yield return null;
        for (var i = -2; i <= 2; i++)
        {
            for (var j = -2; j <= 2; j++)
            {
                Destroy(this.allDots.AllDots[randomCol + i, randomRow + j]);
                StartCoroutine(this.allDots.DestroyMatched());
            }
        }
        yield return null;
        GameStateController.Instance.CurrentGameState = GameState.FillingDot;
        
    }

    private void AttackEnemy()
    {
        if (Enemy.Instance.CurrentScoreHeal > 0)
        {
            if (Enemy.Instance.CurrentScoreShield == 0)
            {
                Enemy.Instance.CurrentScoreHeal -= dameFireball;
            }
            if (Enemy.Instance.CurrentScoreShield > 0)
            {
                Enemy.Instance.CurrentScoreShield -= dameFireball;
                var remainingdame = dameFireball - Enemy.Instance.CurrentScoreShield;
                if (remainingdame > 0)
                {
                    Enemy.Instance.CurrentScoreHeal -= remainingdame;
                }
            }
        }
    }

    private Quaternion GetAngle(Vector3 pos)
    {
        var positionDirection = pos - transform.position;
        var angle = Mathf.Atan2(positionDirection.y, positionDirection.x) * Mathf.Rad2Deg;
        var quaternion = Quaternion.Euler(0, 0, angle);
        return quaternion;
    }
}

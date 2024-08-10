using UnityEngine;
using UnityEngine.UI;
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
    [SerializeField] private Transform targetEnemy;
    [SerializeField] private GameObject attackRate;
    [SerializeField] private Quaternion rotationPlayer;
    [SerializeField] private Vector2 posPlayer;
    public bool IsMoving { get => isMoving; set => isMoving = value; }
    private void Awake()
    {
        this.shieldBar = GameObject.Find("ShieldBarEnemy").GetComponent<Image>();
        this.healBar = GameObject.Find("HealBarEnemy").GetComponent<Image>();
        this.manaBar = GameObject.Find("ManaBarEnemy").GetComponent<Image>();
        this.animator = GetComponent<Animator>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.CurrentScoreHeal = this.MaxScoreHeal;
        this.CurrentScoreMana = 50f;
        this.rotationPlayer = transform.rotation;
        this.posPlayer = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.UpdateBar();
    }

    public override void Attacking()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("AttackEnemy"))
        {
            Debug.Log("Attack");
            this.animator.SetTrigger("Hit");
        }
    }
}

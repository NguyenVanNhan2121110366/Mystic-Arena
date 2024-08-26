using UnityEngine;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
public class TurnController : MonoBehaviour
{
    private static TurnController instance;
    public static TurnController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<TurnController>();
            }
            return instance;
        }
    }

    [SerializeField] private GameTurn currentTurn;
    [SerializeField] private int turn;
    private EnemyAI enemyAI;
    [SerializeField] private TextMeshProUGUI txtTurnPlayer;
    [SerializeField] private GameObject bgrTextTurnPlayer;
    [SerializeField] private TextMeshProUGUI txtTurnEnemy;
    [SerializeField] private GameObject bgrTextTurnEnemy;
    [SerializeField] private int seconds;
    [SerializeField] private TextMeshProUGUI txtSecondsPlayer;
    [SerializeField] private GameObject bgrSecondsPlayer;
    [SerializeField] private TextMeshProUGUI txtSecondsEnemy;
    [SerializeField] private GameObject bgrSecondsEnemy;

    private bool isCheck;
    public GameTurn CurrentTurn { get => currentTurn; set => currentTurn = value; }

    private void Awake()
    {
        this.bgrSecondsPlayer = GameObject.Find("BackGroundSecondsTurnPlayer");
        this.bgrSecondsEnemy = GameObject.Find("BackGroundSecondsTurnEnemy");
        this.txtSecondsPlayer = GameObject.Find("txtSecondsPlayer").GetComponent<TextMeshProUGUI>();
        this.txtSecondsEnemy = GameObject.Find("txtSecondsEnemy").GetComponent<TextMeshProUGUI>();
        this.bgrTextTurnPlayer = GameObject.Find("BackGroundTurnPlayer");
        this.txtTurnPlayer = GameObject.Find("txtTurnPlayer").GetComponent<TextMeshProUGUI>();
        this.bgrTextTurnEnemy = GameObject.Find("BackGroundTurnEnemy");
        this.txtTurnEnemy = GameObject.Find("txtTurnEnemy").GetComponent<TextMeshProUGUI>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.isCheck = true;
        currentTurn = GameTurn.Player;
        this.turn = 1;
        this.seconds = 10;
        StartCoroutine(DelayTurn());
        StartCoroutine(this.CheckSecondsTurn());
    }

    private IEnumerator DelayTurn()
    {
        this.bgrTextTurnPlayer.SetActive(true);
        this.bgrTextTurnEnemy.SetActive(false);
        yield return new WaitForSeconds(2f);
        this.bgrTextTurnPlayer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        this.SecondTurn();
    }

    private void SecondTurn()
    {
        if (currentTurn == GameTurn.Player)
        {
            this.bgrSecondsPlayer.SetActive(true);
            this.bgrSecondsEnemy.SetActive(false);
            this.txtSecondsPlayer.text = this.seconds.ToString();
        }
        else
        {
            this.bgrSecondsEnemy.SetActive(true);
            this.bgrSecondsPlayer.SetActive(false);
            this.txtSecondsEnemy.text = this.seconds.ToString();
        }
    }
    private IEnumerator TextTurn()
    {

        if (currentTurn == GameTurn.Player)
        {
            if (Player.Instance.CurrentScoreHeal > 0)
            {
                yield return new WaitForSeconds(0.3f);
                this.bgrTextTurnPlayer.SetActive(true);
                this.txtTurnPlayer.text = "Lượt của Enemy";
                Debug.Log("Turn of Enemy");
                yield return new WaitForSeconds(2f);
                this.bgrTextTurnPlayer.SetActive(false);
            }
        }
        else
        {
            if (Enemy.Instance.CurrentScoreHeal > 0)
            {
                yield return new WaitForSeconds(0.3f);
                this.bgrTextTurnEnemy.SetActive(true);
                this.txtTurnEnemy.text = "Lượt của Player";
                Debug.Log("Turn of Player");
                yield return new WaitForSeconds(2f);
                this.bgrTextTurnEnemy.SetActive(false);
            }
        }
    }

    private IEnumerator CheckSecondsTurn()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (GameStateController.Instance.CurrentGameState == GameState.Swipe || GameStateController.Instance.CurrentGameState == GameState.CheckingDot)
                this.MinusSeconds(1);
            if (seconds == 0 && isCheck)
            {
                if (GameStateController.Instance.CurrentGameState == GameState.Swipe)
                {
                    isCheck = false;
                    GameStateController.Instance.CurrentGameState = GameState.Finish;
                    yield return null;
                    this.seconds = 10;
                    isCheck = true;
                }
            }
        }

    }

    private void MinusSeconds(int second) { this.seconds -= second; }

    public IEnumerator CheckTurnAndSwitch()
    {
        this.turn--;
        yield return null;
        if (this.turn <= 0)
        {
            StartCoroutine(this.TextTurn());
            this.SwitchTurn();
            this.seconds = 10;
            this.turn = 1;
            this.SetNewTurn();
        }
        else
        {
            Debug.Log("Con vai luot");
            this.SetNewTurn();
        }
    }

    private void SetNewTurn()
    {
        this.enemyAI = FindFirstObjectByType<EnemyAI>();
        if (this.enemyAI != null) FindFirstObjectByType<EnemyAI>().AutoTurn();
    }

    private void SwitchTurn()
    {
        if (currentTurn == GameTurn.Player)
            currentTurn = GameTurn.Enemy;
        else
            currentTurn = GameTurn.Player;
    }
}
public enum GameTurn
{
    None,
    Player,
    Enemy,
}

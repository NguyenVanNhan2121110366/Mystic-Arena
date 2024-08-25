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
    [SerializeField] private TextMeshProUGUI txtTurn;
    [SerializeField] private GameObject bgrTextTurn;
    [SerializeField] private int seconds;
    [SerializeField]
    private bool isCheck;
    public GameTurn CurrentTurn { get => currentTurn; set => currentTurn = value; }

    private void Awake()
    {
        this.bgrTextTurn = GameObject.Find("BackGroundTurn");
        this.txtTurn = GameObject.Find("txtTurn").GetComponent<TextMeshProUGUI>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.isCheck = true;
        this.bgrTextTurn.SetActive(false);
        currentTurn = GameTurn.Player;
        this.turn = 1;
        this.seconds = 10;
        StartCoroutine(this.CheckSecondsTurn());
    }

    // Update is called once per frame
    void Update()
    {

    }
    private IEnumerator TextTurn()
    {
        if (currentTurn == GameTurn.Player)
        {
            yield return new WaitForSeconds(0.3f);
            this.bgrTextTurn.SetActive(true);
            this.txtTurn.text = "Lượt của Enemy";
            Debug.Log("Turn of Enemy");
        }
        else
        {
            yield return new WaitForSeconds(0.3f);
            this.bgrTextTurn.SetActive(true);
            this.txtTurn.text = "Lượt của Player";
            Debug.Log("Turn of Player");
        }
    }

    private IEnumerator CheckSecondsTurn()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            this.MinusSeconds(1);
            if (seconds == 0 && isCheck)
            {
                isCheck = false;
                GameStateController.Instance.CurrentGameState = GameState.Finish;
                yield return null;
                this.seconds = 10;
                isCheck = true;
            }
        }

    }

    private void MinusSeconds(int second)
    {
        this.seconds -= second;
    }

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
        if (this.enemyAI != null)
            FindFirstObjectByType<EnemyAI>().AutoTurn();
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

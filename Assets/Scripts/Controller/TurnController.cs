using UnityEngine;
using System.Collections;
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
    public GameTurn CurrentTurn { get => currentTurn; set => currentTurn = value; }
    private void Awake()
    {
        this.enemyAI = FindFirstObjectByType<EnemyAI>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTurn = GameTurn.Player;
        this.turn = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator CheckTurnAndSwitch()
    {
        this.turn--;
        yield return null;
        if (this.turn <= 0)
        {
            this.SwitchTurn();
            yield return null;
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
        //this.enemyAI.AutoTurn();
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

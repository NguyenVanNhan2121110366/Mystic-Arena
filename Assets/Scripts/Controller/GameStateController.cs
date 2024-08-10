using UnityEngine;

public class GameStateController : MonoBehaviour
{
    private static GameStateController instance;
    public static GameStateController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<GameStateController>();
            }
            return instance;
        }
    }

    [SerializeField] private GameState currentGameState;
    private ScoreController scoreController;
    public GameState CurrentGameState { get => currentGameState; set => currentGameState = value; }
    private void Awake()
    {
        this.scoreController = FindFirstObjectByType<ScoreController>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.currentGameState = GameState.None;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentGameState == GameState.Finish)
        {
            this.scoreController.ResetScore();
            currentGameState = GameState.Swipe;
        }
    }
}
public enum GameState
{
    None,
    Swipe,
    Attacking,
    ExcuteAbility,
    FillingDot,
    CheckingDot,
    Finish,
}

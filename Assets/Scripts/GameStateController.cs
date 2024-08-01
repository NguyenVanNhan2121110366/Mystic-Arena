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
    public GameState CurrentGameState { get => currentGameState; set => currentGameState = value; }
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

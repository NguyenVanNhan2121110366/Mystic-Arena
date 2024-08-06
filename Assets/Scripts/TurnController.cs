using UnityEngine;

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
    public GameTurn CurrentTurn { get => currentTurn; set => currentTurn = value; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTurn = GameTurn.Player;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
public enum GameTurn
{
    None,
    Player,
    Enemy,
}

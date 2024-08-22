using UnityEngine;
public class ScoreController : MonoBehaviour
{
    [SerializeField] private float scoreHeal;
    [SerializeField] private float scoreMana;
    [SerializeField] private float scoreAttack;
    [SerializeField] private float scoreGold;
    [SerializeField] private float scoreShield;
    [SerializeField] private float scoreLevel;
    [SerializeField] private int dameAttack;
    public float ScoreHeal { get => scoreHeal; set => scoreHeal = value; }
    public float ScoreMana { get => scoreMana; set => scoreMana = value; }
    public float ScoreAttack { get => scoreAttack; set => scoreAttack = value; }
    public float ScoreGold { get => scoreGold; set => scoreGold = value; }
    public float ScoreShield { get => scoreShield; set => scoreShield = value; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateScore()
    {
        if (TurnController.Instance.CurrentTurn == GameTurn.Player)
        {
            Player.Instance.UpdateScore();
            Player.Instance.UpdateScoreBar();
            if (Player.Instance.ScoreAttack > 0)
            {
                GameStateController.Instance.CurrentGameState = GameState.Attacking;
                Player.Instance.IsMoving = true;
            }
        }
        else if (TurnController.Instance.CurrentTurn == GameTurn.Enemy)
        {
            Enemy.Instance.UpdateScore();
            Enemy.Instance.UpdateScoreBar();
            if (Enemy.Instance.ScoreAttack > 0)
            {
                GameStateController.Instance.CurrentGameState = GameState.Attacking;
                Enemy.Instance.IsMoving = true;
            }
        }

    }

    public void ResetScore()
    {
        scoreHeal = 0;
        scoreMana = 0;
        scoreAttack = 0;
        scoreGold = 0;
        scoreShield = 0;
    }
}

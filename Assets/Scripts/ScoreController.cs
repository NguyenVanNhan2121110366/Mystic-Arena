using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private float scoreHeal;
    [SerializeField] private float scoreMana;
    [SerializeField] private float scoreAttack;
    [SerializeField] private float scoreGold;
    [SerializeField] private float scoreShield;
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
        Player.Instance.UpdateScorePlayer();
        Player.Instance.UpdateScoreBar();
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

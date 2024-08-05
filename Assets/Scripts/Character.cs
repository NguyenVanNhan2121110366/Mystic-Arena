using UnityEngine;
using UnityEngine.UI;
public class Character : MonoBehaviour
{
    #region Variable
    [SerializeField] private float scoreHeal;
    [SerializeField] private float scoreMana;
    [SerializeField] private float scoreAttack;
    [SerializeField] private float currentScoreHeal;
    [SerializeField] private float currentScoreMana;
    [SerializeField] private float currentScoreAttack;
    [SerializeField] private int maxScoreHeal;
    [SerializeField] private int maxScoreMana;
    [SerializeField] private int maxScoreAttack;
    [SerializeField] private Image healBar;
    [SerializeField] private Image manaBar;
    ScoreController scoreController;

    #endregion

    #region Public
    public float ScoreHeal { get => scoreHeal; set => scoreHeal = value; }
    public float ScoreMana { get => scoreMana; set => scoreMana = value; }
    public float ScoreAttack { get => scoreAttack; set => scoreAttack = value; }
    public float CurrentScoreHeal { get => currentScoreHeal; set => currentScoreHeal = value; }
    public float CurrentScoreMana { get => currentScoreMana; set => CurrentScoreMana = value; }
    public float CurrentScoreAttack { get => currentScoreAttack; set => currentScoreAttack = value; }
    public int MaxScoreHeal { get => maxScoreHeal; set => maxScoreHeal = value; }
    public int MaxScoreMana { get => maxScoreMana; set => maxScoreMana = value; }
    public int MaxScoreAttack { get => maxScoreAttack; set => maxScoreAttack = value; }
    #endregion
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.scoreController = FindFirstObjectByType<ScoreController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected void UpdateBar()
    {
        healBar.fillAmount = Mathf.Lerp(currentScoreHeal, currentScoreHeal / maxScoreHeal, 9 * Time.deltaTime);
        manaBar.fillAmount = Mathf.Lerp(currentScoreMana, currentScoreMana / maxScoreMana, 9 * Time.deltaTime);
    }

    public void UpdateScoreBar()
    {
        currentScoreHeal += scoreHeal;
        currentScoreAttack += scoreAttack;
        currentScoreMana += scoreMana;
        currentScoreHeal = currentScoreHeal < maxScoreAttack ? currentScoreHeal : maxScoreAttack;
        currentScoreMana = currentScoreMana < maxScoreMana ? currentScoreMana : maxScoreMana;
    }

    public void UpdateScore()
    {
        scoreHeal = this.scoreController.ScoreHeal;
        scoreMana = this.scoreController.ScoreMana;
        scoreAttack = this.scoreController.ScoreAttack;
    }


}

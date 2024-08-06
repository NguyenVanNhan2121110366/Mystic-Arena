using UnityEngine;
using UnityEngine.UI;
public class Character : MonoBehaviour
{
    #region Variable
    [SerializeField] private float scoreHeal;
    [SerializeField] private float scoreMana;
    [SerializeField] private float scoreAttack;
    [SerializeField] private float scoreGold;
    [SerializeField] private float scoreShield;
    [SerializeField] private float currentScoreHeal;
    [SerializeField] private float currentScoreMana;
    [SerializeField] private float currentScoreAttack;
    [SerializeField] private float currentScoreGold;
    [SerializeField] private float currentScoreShield;
    [SerializeField] private float maxScoreHeal;
    [SerializeField] private float maxScoreMana;
    [SerializeField] private float maxScoreAttack;
    [SerializeField] private float maxScoreGold;
    [SerializeField] private float maxScoreShield;
    [SerializeField] private Image healBar;
    [SerializeField] private Image manaBar;
    public Animator animator;

    #endregion

    #region Public
    public float ScoreHeal { get => scoreHeal; set => scoreHeal = value; }
    public float ScoreMana { get => scoreMana; set => scoreMana = value; }
    public float ScoreAttack { get => scoreAttack; set => scoreAttack = value; }
    public float ScoreGold { get => scoreGold; set => scoreGold = value; }
    public float ScoreShield { get => scoreShield; set => scoreShield = value; }
    public float CurrentScoreHeal { get => currentScoreHeal; set => currentScoreHeal = value; }
    public float CurrentScoreMana { get => currentScoreMana; set => currentScoreMana = value; }
    public float CurrentScoreAttack { get => currentScoreAttack; set => currentScoreAttack = value; }
    public float CurrentScoreGold { get => currentScoreGold; set => currentScoreGold = value; }
    public float CurrentScoreShield { get => currentScoreShield; set => currentScoreShield = value; }
    public float MaxScoreHeal { get => maxScoreHeal; set => maxScoreHeal = value; }
    public float MaxScoreMana { get => maxScoreMana; set => maxScoreMana = value; }
    public float MaxScoreAttack { get => maxScoreAttack; set => maxScoreAttack = value; }
    public float MaxScoreGold { get => maxScoreGold; set => maxScoreGold = value; }
    public float MaxScoreShield { get => maxScoreShield; set => maxScoreShield = value; }
    #endregion

    protected void UpdateBar()
    {
        healBar.fillAmount = Mathf.Lerp(healBar.fillAmount, currentScoreHeal / maxScoreHeal, 9 * Time.deltaTime);
        manaBar.fillAmount = Mathf.Lerp(manaBar.fillAmount, currentScoreMana / maxScoreMana, 9 * Time.deltaTime);
    }

    public void UpdateScoreBar()
    {
        currentScoreHeal += scoreHeal;
        currentScoreAttack += scoreAttack;
        currentScoreMana += scoreMana;
        currentScoreGold += scoreGold;
        currentScoreShield += scoreShield;
        currentScoreHeal = currentScoreHeal > maxScoreHeal ? maxScoreHeal : currentScoreHeal;
        currentScoreMana = currentScoreMana > maxScoreMana ? maxScoreMana : currentScoreMana;
        currentScoreHeal = currentScoreHeal < 0 ? 0 : currentScoreHeal;
        currentScoreMana = currentScoreMana < 0 ? 0 : currentScoreMana;
    }

    public virtual void UpdateScorePlayer()
    {

    }

    public virtual void Attacking()
    {

    }


}

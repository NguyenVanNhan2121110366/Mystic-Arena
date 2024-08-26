using UnityEngine;
using TMPro;
public class Dot : MonoBehaviour
{
    private AllDotController alldots;
    private ScoreController scoreController;
    [SerializeField] private string dotName;
    [SerializeField] private string dotTag;
    [SerializeField] private int score;
    [SerializeField] private TextMeshProUGUI txtScoreGold;
    public int scoreGold;
    public int checkScoreGold;

    private void Awake()
    {
        this.alldots = FindFirstObjectByType<AllDotController>();
        this.scoreController = FindFirstObjectByType<ScoreController>();
        this.txtScoreGold = GameObject.Find("ScoreGold").GetComponent<TextMeshProUGUI>();

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.dotTag = gameObject.tag;
        this.checkScoreGold = 0;
    }
    private void OnDestroy()
    {
        if (dotName.Contains("Big"))
        {
            Debug.Log("Destroy Big");
            this.DestroyBig();
        }
        this.UpdateScore();
    }

    public void DestroyEffects()
    {
        if (dotName.Contains("Big") && dotName != null)
        {
            Debug.Log("Destroy Big");
            this.DestroyBig();
        }
    }

    private void UpdateScore()
    {
        if (dotTag == "Blood")
        {
            this.scoreController.ScoreHeal += this.score;
        }
        if (dotTag == "Gold")
        {

            if (Player.Instance != null)
                this.checkScoreGold += Player.Instance.CheckScoreGold();
            this.scoreController.ScoreGold += this.score;
            this.txtScoreGold.text = " X " + checkScoreGold;

        }
        if (dotTag == "Mana")
        {
            this.scoreController.ScoreMana += this.score;
        }
        if (dotTag == "Shield")
        {
            this.scoreController.ScoreShield += this.score;
        }
        if (dotTag == "Sword")
        {
            this.scoreController.ScoreAttack += this.score;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void DestroyBig()
    {
        var dot = GetComponent<DotInteraction>();
        var column = dot.Column;
        var row = dot.Row;
        for (var i = -1; i < 2; i++)
        {
            for (var j = -1; j < 2; j++)
            {
                if (column + i < this.alldots.Width && column + i >= 0 &&
                 row + j >= 0 && row + j < this.alldots.Height &&
                 this.alldots.AllDots[column + i, row + j] != null)
                {
                    this.alldots.SpawnDestroyEffects(column + i, row + j);
                    Destroy(this.alldots.AllDots[column + i, row + j]);
                    this.alldots.AllDots[column + i, row + j] = null;
                }
            }
        }
    }

}

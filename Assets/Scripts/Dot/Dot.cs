using UnityEngine;

public class Dot : MonoBehaviour
{
    private AllDotController alldots;
    private ScoreController scoreController;
    [SerializeField] private string dotName;
    [SerializeField] private string dotTag;
    [SerializeField] private int score;
    private void Awake()
    {
        this.alldots = FindFirstObjectByType<AllDotController>();
        this.scoreController = FindFirstObjectByType<ScoreController>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.dotTag = gameObject.tag;
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

    private void UpdateScore()
    {
        if (dotTag == "Blood")
        {
            this.scoreController.ScoreHeal += this.score;
        }
        if (dotTag == "Gold")
        {
            this.scoreController.ScoreGold += this.score;
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
                    Destroy(this.alldots.AllDots[column + i, row + j]);
                    this.alldots.AllDots[column + i, row + j] = null;
                }

            }
        }
    }

}

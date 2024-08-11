using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private AllDotController allDot;
    [SerializeField] private string[,] allTags;
    [SerializeField] private bool isTurnEnemy;
    [SerializeField] private List<MoveDot> allMoves;

    private void Awake()
    {
        this.allDot = FindFirstObjectByType<AllDotController>();
        this.allTags = new string[this.allDot.Width, this.allDot.Height];
    }
    private void Start()
    {
        this.allMoves = new List<MoveDot>();

    }

    private void Update()
    {
        if (isTurnEnemy)
        {
            this.allMoves.Clear();
            this.GetAllTag();
            StartCoroutine(this.RandomMoveDot());
            this.FindAllMatchedWidth();
            this.isTurnEnemy = false;
        }
    }

    private IEnumerator RandomMoveDot()
    {
        yield return new WaitForSeconds(0.5f);
        var randomIndex = Random.Range(0, this.allMoves.Count);
        var moveDot = this.allMoves[randomIndex];
        var originDot = this.allDot.AllDots[moveDot.OriginDotCol, moveDot.OriginDotRow];
        var targetDot = this.allDot.AllDots[moveDot.TargetDotCol, moveDot.TargetDotRow];
        originDot.GetComponent<DotInteraction>().SetDot(moveDot.TargetDotCol, moveDot.TargetDotRow);
        targetDot.GetComponent<DotInteraction>().SetDot(moveDot.OriginDotCol, moveDot.OriginDotRow);
        GameStateController.Instance.CurrentGameState = GameState.FillingDot;
        StartCoroutine(this.allDot.DestroyMatched());
    }

    private void GetAllTag()
    {
        for (int i = 0; i < this.allDot.Width; i++)
        {
            for (int j = 0; j < this.allDot.Height; j++)
            {
                this.allTags[i, j] = this.allDot.AllDots[i, j].tag;
            }
        }
    }

    private void FindAllMatchedWidth()
    {
        for (var i = 0; i < this.allDot.Width - 1; i++)
        {
            for (var j = 0; j < this.allDot.Height; j++)
            {
                var isOrigin = FindMatchedOriginDot(i, j);
                var isTarget = FindMatchedTargetDot(i + 1, j);
                if (isOrigin || isTarget)
                {
                    var newMove = new MoveDot(i, j, i + 1, j);
                    this.allMoves.Add(newMove);
                }
            }
        }
    }

    #region Find Matched For Width 
    private bool FindMatchedOriginDot(int i, int j)
    {
        var a = FindMatchedRightHorizontal(i, j);
        var b = FindMatchedVertical(i, j, 1);
        return IsMatched(a, b);
    }
    private bool FindMatchedTargetDot(int i, int j)
    {
        var a = FindMatchedLeftHorizontal(i, j);
        var b = FindMatchedVertical(i, j, -1);
        return IsMatched(a, b);
    }

    private bool FindMatchedLeftHorizontal(int i, int j)
    {
        var count = 0;
        var dot = this.allTags[i, j];
        var leftDot = i - 2 > 0 ? this.allTags[i - 2, j] : string.Empty;
        var leftDot2 = i - 3 >= 0 ? this.allTags[i - 3, j] : string.Empty;
        if (dot == leftDot)
        {
            count++;
            if (dot == leftDot2)
                count++;
        }
        return Find3Matched(count);
    }
    private bool FindMatchedRightHorizontal(int i, int j)
    {
        var count = 1;
        var dot = this.allTags[i, j];
        var dotRight = i + 2 < this.allDot.Width ? this.allTags[i + 2, j] : string.Empty;
        var dotRight2 = i + 3 < this.allDot.Width ? this.allTags[i + 3, j] : string.Empty;
        if (dot == dotRight)
        {
            count++;
            if (dot == dotRight2)
                count++;
        }
        return Find3Matched(count);

    }

    private bool FindMatchedVertical(int i, int j, int k)
    {
        var count = 1;
        var dot = this.allTags[i, j];
        var dotUp = j + 1 < this.allDot.Height ? this.allTags[i + k, j + 1] : string.Empty;
        var dotUp2 = j + 2 < this.allDot.Height ? this.allTags[i + k, j + 2] : string.Empty;
        var dotDown = j - 1 > 0 ? this.allTags[i + k, j - 1] : string.Empty;
        var dotDown2 = j - 2 >= 0 ? this.allTags[i + k, j - 2] : string.Empty;
        if (dot == dotUp)
        {
            count++;
            if (dot == dotUp2)
                count++;
        }
        if (dot == dotDown)
        {
            count++;
            if (dot == dotDown2)
                count++;
        }
        return Find3Matched(count);
    }
    #endregion

    private bool Find3Matched(int count)
    {
        if (count == 3)
            return true;
        return false;
    }

    private bool IsMatched(bool a, bool b)
    {
        if (a || b)
            return true;
        return false;
    }
}

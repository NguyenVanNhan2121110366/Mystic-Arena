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
        this.allMoves = new List<MoveDot>();
    }
    private void Start()
    {

        this.isTurnEnemy = false;

    }

    private void Update()
    {
        if (isTurnEnemy && TurnController.Instance.CurrentTurn == GameTurn.Enemy)
        {
            this.allMoves.Clear();
            this.GetAllTag();
            this.FindAllMatchedWidth();
            this.FindAllMatchedHeight();
            StartCoroutine(this.RandomMoveDot());
            this.isTurnEnemy = false;
        }
    }

    private IEnumerator RandomMoveDot()
    {
        yield return new WaitForSeconds(.5f);
        var randomIndex = Random.Range(0, this.allMoves.Count);
        var moveDot = this.allMoves[randomIndex];
        var originDot = this.allDot.AllDots[moveDot.OriginDotCol, moveDot.OriginDotRow];
        var targetDot = this.allDot.AllDots[moveDot.TargetDotCol, moveDot.TargetDotRow];
        originDot.GetComponent<DotInteraction>().SetDot(moveDot.TargetDotCol, moveDot.TargetDotRow);
        targetDot.GetComponent<DotInteraction>().SetDot(moveDot.OriginDotCol, moveDot.OriginDotRow);
        StartCoroutine(this.DelayDestroyEnemy());
    }

    private IEnumerator DelayDestroyEnemy()
    {
        GameStateController.Instance.CurrentGameState = GameState.FillingDot;
        StartCoroutine(this.allDot.DestroyMatched());
        yield return new WaitForSeconds(0.5f);
        SoundManager.Instance.audioSrc.PlayOneShot(SoundManager.Instance.SoundDestroy);
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
        var count = 1;
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

    private void FindAllMatchedHeight()
    {
        for (var i = 0; i < this.allDot.Width; i++)
        {
            for (var j = 0; j < this.allDot.Height - 1; j++)
            {
                var isOriginDot = FindMatchedHeightOriginDot(i, j);
                var isTargetDot = FindMatchedHeightTargetDot(i, j + 1);
                if (isOriginDot || isTargetDot)
                {
                    var newMove = new MoveDot(i, j, i, j + 1);
                    this.allMoves.Add(newMove);
                }
            }
        }
    }
    #region Find All Matched Height
    private bool FindMatchedHeightOriginDot(int i, int j)
    {
        var a = FindMatchedUpVertical(i, j);
        var b = FindMatchedHorizontal(i, j, 1);
        return IsMatched(a, b);
    }
    private bool FindMatchedHeightTargetDot(int i, int j)
    {
        var a = FindMatchedDownVertical(i, j);
        var b = FindMatchedHorizontal(i, j, -1);
        return IsMatched(a, b);
    }

    private bool FindMatchedDownVertical(int i, int j)
    {
        var count = 1;
        var originDot = this.allTags[i, j];
        var downDot = j - 2 > 0 ? this.allTags[i, j - 2] : string.Empty;
        var downDot2 = j - 3 >= 0 ? this.allTags[i, j - 3] : string.Empty;

        if (originDot == downDot)
        {
            count++;
            if (originDot == downDot2)
                count++;
        }
        return Find3Matched(count);
    }

    private bool FindMatchedUpVertical(int i, int j)
    {
        var count = 1;
        var originDot = this.allTags[i, j];
        var upDot = j + 2 < this.allDot.Height ? this.allTags[i, j + 2] : string.Empty;
        var upDot2 = j + 3 < this.allDot.Height ? this.allTags[i, j + 3] : string.Empty;
        if (originDot == upDot)
        {
            count++;
            if (originDot == upDot2)
                count++;
        }
        return Find3Matched(count);
    }

    private bool FindMatchedHorizontal(int i, int j, int k)
    {
        var count = 1;
        var originDot = this.allTags[i, j];
        var leftDot = i - 1 > 0 ? this.allTags[i - 1, j + k] : string.Empty;
        var leftDot2 = i - 2 >= 0 ? this.allTags[i - 2, j + k] : string.Empty;

        var rightDot = i + 1 < this.allDot.Width ? this.allTags[i + 1, j + k] : string.Empty;
        var rightDot2 = i + 2 < this.allDot.Width ? this.allTags[i + 2, j + k] : string.Empty;

        if (originDot == leftDot)
        {
            count++;
            if (originDot == leftDot2)
                count++;
        }

        if (originDot == rightDot)
        {
            count++;
            if (originDot == rightDot2)
                count++;
        }

        return Find3Matched(count);
    }

    #endregion

    private bool Find3Matched(int count)
    {
        if (count >= 3)
            return true;
        return false;
    }

    private bool IsMatched(bool a, bool b)
    {
        if (a || b)
            return true;
        else
            return false;
    }

    public void AutoTurn()
    {
        this.isTurnEnemy = true;
    }
}

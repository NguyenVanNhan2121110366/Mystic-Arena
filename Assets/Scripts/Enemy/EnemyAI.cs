using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private AllDotController allDots;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.allDots = FindFirstObjectByType<AllDotController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FindAllMatchedByWidth()
    {
        for (var i = 0; i < this.allDots.Width; i++)
        {
            for (var j = 0; j < this.allDots.Height; j++)
            {
                //var originDot = FindMatchedOriginDot(i, j);
            }
        }
    }
    #region Find allMatched Width
    // private bool FindMatchedOriginDot(int i, int j)
    // {
    //     var a = FindMatchedWidthRight(i, j);
    //     var b = FindMatchedVertical(i, j, 1);
    // }

    private bool FindMatchedWidthRight(int i, int j)
    {
        var dot = this.allDots.AllDots[i, j];
        var rightDot = this.allDots.AllDots[i + 2, j];
        var rightDot2 = this.allDots.AllDots[i + 3, j];
        var count = 1;
        if (dot.CompareTag(rightDot.tag))
        {
            count++;
            if (dot.CompareTag(rightDot2.tag))
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
}

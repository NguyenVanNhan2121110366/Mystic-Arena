using UnityEngine;

public class FindMatchedObj : MonoBehaviour
{
    private static FindMatchedObj instance;
    public static FindMatchedObj Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<FindMatchedObj>();
            }
            return instance;
        }
    }
    private DotInteraction dot;
    private AllDotController alldots;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.alldots = FindFirstObjectByType<AllDotController>();
        this.dot = FindFirstObjectByType<DotInteraction>();
    }

    private void Update()
    {
        
    }

    // public void Find5Matched(GameObject obj)
    // {
    //     var column = dot.Column;
    //     var row = dot.Row;
    //     if (column + 1 < this.alldots.Width && column - 1 >= 0)
    //     {
    //         var leftDot = this.alldots.AllDots[column - 1, row];
    //         var leftleftDot = this.alldots.AllDots[column - 2, row];
    //         var rightDot = this.alldots.AllDots[column + 1, row];
    //         var righrighttDot = this.alldots.AllDots[column + 2, row];
    //         if (leftDot && rightDot && leftDot.CompareTag(obj.tag) && rightDot.CompareTag(obj.tag) && leftDot != obj && rightDot != obj)
    //         {
    //             obj.GetComponent<DotInteraction>().IsMatched = true;
    //             leftDot.GetComponent<DotInteraction>().IsMatched = true;
    //             rightDot.GetComponent<DotInteraction>().IsMatched = true;
    //         }
    //     }

    //     if (row + 1 < this.alldots.Height && row - 1 >= 0)
    //     {
    //         var upDot = this.alldots.AllDots[column, row - 1];
    //         var downDot = this.alldots.AllDots[column, row + 1];
    //         if (upDot && downDot && upDot.CompareTag(obj.tag) && downDot.CompareTag(obj.tag) && upDot != obj && downDot != obj)
    //         {
    //             obj.GetComponent<DotInteraction>().IsMatched = true;
    //             upDot.GetComponent<DotInteraction>().IsMatched = true;
    //             downDot.GetComponent<DotInteraction>().IsMatched = true;
    //         }
    //     }
    // }
}

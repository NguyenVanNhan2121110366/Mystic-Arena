using System.Collections;
using UnityEngine;

public class AllDotController : MonoBehaviour
{
    #region Variable
    [SerializeField] private int width, height;
    [SerializeField] private GameObject[] dots = new GameObject[9];
    [SerializeField] private Transform parentObj;
    [SerializeField] private GameObject girdPrefab;
    private GameObject[,] allGrids;
    private GameObject[,] allDots;
    #endregion
    #region Public
    public int Width { get => width; set => width = value; }
    public int Height { get => height; set => height = value; }
    public GameObject[,] AllDots { get => allDots; set => allDots = value; }
    public GameObject[,] AllGrids { get => allGrids; set => allGrids = value; }
    #endregion



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.allDots = new GameObject[this.width, this.height];
        this.allGrids = new GameObject[this.width, this.height];
        this.GetAllDotToArray();
        StartCoroutine(this.CreateDotAndGrid());
    }

    private void GetAllDotToArray()
    {
        for (var i = 0; i < this.parentObj.childCount; i++)
        {
            this.dots[i] = this.parentObj.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    

    private IEnumerator CreateDotAndGrid()
    {
        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
            {
                var pos = new Vector2(i, j);
                var grid = Instantiate(this.girdPrefab, pos, Quaternion.identity);
                grid.transform.parent = transform;
                grid.name = "(" + i + " , " + j + ")";
                this.allGrids[i, j] = grid;
            }
        }
        yield return null;
        for (var i = 0; i < width; i++)
        {
            yield return null;
            for (var j = 0; j < height; j++)
            {
                yield return null;
                for (; ; )
                {
                    var dotToUse = DotToUse();
                    var dotTag = this.dots[dotToUse].tag;
                    var leftDot = i - 1 >= 0 ? this.allDots[i - 1, j].tag : string.Empty;
                    var downDot = j - 1 >= 0 ? this.allDots[i, j - 1].tag : string.Empty;
                    if (dotTag == leftDot || dotTag == downDot)
                        continue;
                    else
                    {
                        var pos = this.allGrids[i, j].transform.position;
                        var dotObj = Instantiate(this.dots[dotToUse], pos, Quaternion.identity);
                        dotObj.transform.parent = transform;
                        dotObj.name = "(" + i + " , " + j + ")";
                        dotObj.SetActive(true);
                        this.allDots[i, j] = dotObj;
                        dotObj.GetComponent<DotInteraction>().Column = i;
                        dotObj.GetComponent<DotInteraction>().Row = j;
                        break;
                    }
                }

            }
        }
    }

    private int DotToUse()
    {
        int numberIndex;
        var randomNumber = Random.Range(0, 100);
        if (randomNumber >= 0 && randomNumber < 19)
            numberIndex = 0;
        else if (randomNumber >= 19 && randomNumber < 42)
            numberIndex = 1;
        else if (randomNumber >= 42 && randomNumber < 61)
            numberIndex = 2;
        else if (randomNumber >= 61 && randomNumber < 80)
            numberIndex = 3;
        else if (randomNumber >= 80 && randomNumber < 97)
            numberIndex = 4;
        else if (randomNumber >= 97 && randomNumber < 98)
            numberIndex = 5;
        else if (randomNumber >= 98 && randomNumber < 99)
            numberIndex = 6;
        else if (randomNumber >= 99 && randomNumber < 100)
            numberIndex = 7;
        else
            numberIndex = 8;
        return numberIndex;
    }
}

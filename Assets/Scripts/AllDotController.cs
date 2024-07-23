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
    }
}

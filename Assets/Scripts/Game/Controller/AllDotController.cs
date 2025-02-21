using System.Collections;
using UnityEngine;

public class AllDotController : MonoBehaviour
{
    #region Variable
    [SerializeField] private int width, height;
    [SerializeField] private GameObject[] dots = new GameObject[9];
    [SerializeField] private Transform parentObj;
    [SerializeField] private GameObject girdPrefab;
    [SerializeField] private GameObject[] allEffects;
    private GameObject[,] allGrids;
    private GameObject[,] allDots;
    private ScoreController scoreController;
    private SaveAllData saveAllData;
    #endregion
    #region Public
    public int Width { get => width; set => width = value; }
    public int Height { get => height; set => height = value; }
    public GameObject[,] AllDots { get => allDots; set => allDots = value; }
    public GameObject[,] AllGrids { get => allGrids; set => allGrids = value; }
    #endregion

    private void Awake()
    {
        this.saveAllData = FindFirstObjectByType<SaveAllData>();
        this.scoreController = FindFirstObjectByType<ScoreController>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        this.allDots = new GameObject[this.width, this.height];
        this.allGrids = new GameObject[this.width, this.height];
        this.GetAllDotToArray();
        StartCoroutine(this.CreateDotAndGrid());
        this.saveAllData.audioSource.volume = SaveGame.Instance.saveData.saveSound[0];
    }

    private void GetAllDotToArray()
    {
        for (var i = 0; i < this.parentObj.childCount; i++)
        {
            this.dots[i] = this.parentObj.GetChild(i).gameObject;
        }
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
                        dotObj.GetComponent<DotInteraction>().Column = i;
                        dotObj.GetComponent<DotInteraction>().Row = j;
                        this.allDots[i, j] = dotObj;
                        break;
                    }
                }

            }
        }
        StartCoroutine(this.ReduceCoRo());
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
        else if (randomNumber >= 80 && randomNumber < 93)
            numberIndex = 4;
        else if (randomNumber >= 93 && randomNumber < 95)
            numberIndex = 5;
        else if (randomNumber >= 95 && randomNumber < 97)
            numberIndex = 6;
        else if (randomNumber >= 97 && randomNumber < 99)
            numberIndex = 7;
        else
            numberIndex = 8;
        return numberIndex;
    }

    #region Spawn Effects

    public int GetEffects(GameObject dot)
    {
        var index = 0;
        switch (dot.tag)
        {
            case "Blood":
                index = 0;
                break;
            case "Gold":
                index = 1;
                break;
            case "Mana":
                index = 2;
                break;
            case "Shield":
                index = 3;
                break;
            case "Sword":
                index = 4;
                break;
            default:
                break;
        }
        return index;
    }
    #endregion
    #region Destroy Matched
    private void DestroyMatchedAt(int column, int row)
    {
        var dot = this.allDots[column, row];
        if (this.allDots[column, row].GetComponent<DotInteraction>().IsMatched)
        {

            this.SpawnDestroyEffects(column, row);
            Destroy(dot);
            this.allDots[column, row] = null;
        }
    }


    public IEnumerator DestroyMatched()
    {
        yield return new WaitForSeconds(0.5f);
        for (var i = 0; i < this.width; i++)
        {
            for (var j = 0; j < this.height; j++)
            {
                if (this.allDots[i, j] != null)
                {
                    this.DestroyMatchedAt(i, j);
                    //this.SpawnDestroyEffects(i, j);
                }
            }
        }
        StartCoroutine(MakeFallingDot());
    }

    public void SpawnDestroyEffects(int column, int row)
    {
        var dot = this.allDots[column, row];
        var effectDot = this.GetEffects(dot);
        var position = dot.transform.position;
        var objEffect = Instantiate(allEffects[effectDot], position, Quaternion.identity);
        Destroy(objEffect, 1f);
    }

    private IEnumerator MakeFallingDot()
    {
        yield return new WaitForSeconds(0.2f);
        var countRow = 0;
        for (var i = 0; i < this.width; i++)
        {
            for (var j = 0; j < this.height; j++)
            {
                if (!this.allDots[i, j]) countRow++;
                else if (countRow > 0)
                {
                    this.allDots[i, j].GetComponent<DotInteraction>().Row -= countRow;
                    this.allDots[i, j] = null;
                }
            }
            countRow = 0;
        }
        //yield return null;
        StartCoroutine(ReduceCoRo());
    }

    private IEnumerator SpawnAgain()
    {
        yield return new WaitForSeconds(0.2f);
        for (var i = 0; i < this.width; i++)
        {
            for (var j = 0; j < this.height; j++)
            {
                if (this.allDots[i, j] == null)
                {
                    var pos = new Vector2(i, j + 1);
                    var dotToUse = DotToUse();
                    var dotObj = Instantiate(this.dots[dotToUse], pos, Quaternion.identity);
                    dotObj.transform.parent = transform;
                    dotObj.SetActive(true);
                    dotObj.name = "(" + i + " , " + j + ")";
                    dotObj.GetComponent<DotInteraction>().Column = i;
                    dotObj.GetComponent<DotInteraction>().Row = j;
                    this.allDots[i, j] = dotObj;
                }
            }
        }
    }

    private bool IsCheckMatched()
    {
        for (var i = 0; i < this.width; i++)
        {
            for (var j = 0; j < this.height; j++)
            {
                if (this.allDots[i, j] != null && this.allDots[i, j].GetComponent<DotInteraction>().IsMatched)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public IEnumerator ReduceCoRo()
    {
        StartCoroutine(this.SpawnAgain());
        yield return new WaitForSeconds(0.3f);
        if (IsCheckMatched())
        {
            SoundManager.Instance.audioSrc.PlayOneShot(SoundManager.Instance.SoundContinuousDestruction);
            StartCoroutine(DestroyMatched());
        }
        else
        {
            if (GameStateController.Instance.CurrentGameState == GameState.None)
            {
                yield return null;
                GameStateController.Instance.CurrentGameState = GameState.Swipe;
            }
            if (GameStateController.Instance.CurrentGameState == GameState.FillingDot)
            {
                this.scoreController.UpdateScore();
                yield return new WaitForSeconds(0.5f);
                if (GameStateController.Instance.CurrentGameState == GameState.FillingDot)
                {
                    GameStateController.Instance.CurrentGameState = GameState.Finish;
                }
            }
        }
    }
    #endregion
}

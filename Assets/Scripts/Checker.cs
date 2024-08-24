using UnityEngine;

public class Checker : MonoBehaviour
{
    [SerializeField] private GameObject objChecker;
    [SerializeField] private GameObject[] checkers = new GameObject[3];
    private AllDotController allDot;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.allDot = FindFirstObjectByType<AllDotController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnChecker(int column, int row)
    {
        var pos = this.allDot.AllDots[column, row].transform.position;
        var obj = Instantiate(objChecker, pos, Quaternion.identity);
        this.checkers[0] = obj;
    }
}

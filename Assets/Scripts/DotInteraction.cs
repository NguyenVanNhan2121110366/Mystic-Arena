using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DotInteraction : MonoBehaviour
{
    #region Variable
    [SerializeField] private int column, row;
    [SerializeField] private Vector2 mouseUp, mouseDown;
    [SerializeField] private GameObject targetDot;
    [SerializeField] private int preColumn, preRow;
    [SerializeField] private bool isMatched;
    [SerializeField] private int targetX, targetY;
    private AllDotController alldots;
    #endregion
    #region Public
    public int Column { get => column; set => column = value; }
    public int Row { get => row; set => row = value; }
    public bool IsMatched { get => isMatched; set => isMatched = value; }

    private void Awake()
    {
        this.alldots = FindFirstObjectByType<AllDotController>();
    }
    #endregion
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isMatched = false;
    }

    // Update is called once per frame
    void Update()
    {
        targetX = column;
        targetY = row;
        this.Find3Matched();
        this.MoveDot();


    }


    public void Find3Matched()
    {
        if (column + 1 < this.alldots.Width && column - 1 >= 0)
        {
            var leftDot = this.alldots.AllDots[column - 1, row];
            var rightDot = this.alldots.AllDots[column + 1, row];
            if (leftDot && rightDot && leftDot.CompareTag(gameObject.tag) && rightDot.CompareTag(gameObject.tag) && leftDot != gameObject && rightDot != gameObject)
            {
                isMatched = true;
                leftDot.GetComponent<DotInteraction>().IsMatched = true;
                rightDot.GetComponent<DotInteraction>().IsMatched = true;
            }
        }

        if (row + 1 < this.alldots.Height && row - 1 >= 0)
        {
            var upDot = this.alldots.AllDots[column, row - 1];
            var downDot = this.alldots.AllDots[column, row + 1];
            if (upDot && downDot && upDot.CompareTag(gameObject.tag) && downDot.CompareTag(gameObject.tag) && upDot != gameObject && downDot != gameObject)
            {
                isMatched = true;
                upDot.GetComponent<DotInteraction>().IsMatched = true;
                downDot.GetComponent<DotInteraction>().IsMatched = true;
            }
        }
    }


    private void MoveDot()
    {
        if (Mathf.Abs(targetX - transform.position.x) > 0.1f)
        {
            var pos = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, pos, 9 * Time.deltaTime);
            if (this.alldots.AllDots[column, row] != gameObject)
            {
                this.alldots.AllDots[column, row] = gameObject;
            }
        }
        else
        {
            var pos = new Vector2(targetX, transform.position.y);
            transform.position = pos;
        }

        if (Mathf.Abs(targetY - transform.position.y) > 0.1f)
        {
            var pos = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, pos, 9 * Time.deltaTime);
            if (this.alldots.AllDots[column, row] != gameObject)
            {
                this.alldots.AllDots[column, row] = gameObject;
            }
        }
        else
        {
            var pos = new Vector2(transform.position.x, targetY);
            transform.position = pos;
        }

    }

    private IEnumerator ChecktargetDot()
    {
        if (!targetDot)
        {
            GameStateController.Instance.CurrentGameState = GameState.Swipe;
        }
        else
        {
            yield return null;
            if (!isMatched && !this.targetDot.GetComponent<DotInteraction>().isMatched)
            {
                yield return new WaitForSeconds(0.5f);
                targetDot.GetComponent<DotInteraction>().column = column;
                targetDot.GetComponent<DotInteraction>().row = row;
                row = preRow;
                column = preColumn;
            }
            else
            {
                GameStateController.Instance.CurrentGameState = GameState.FillingDot;
                StartCoroutine(this.alldots.DestroyMatched());
                Debug.Log("Destroy");
            }
            targetDot = null;

        }
    }
    #region Input
    private string CheckTouch()
    {
        var mouseDirection = "invalid";
        if (Vector2.Distance(mouseUp, mouseDown) < 10) return mouseDirection;
        var posX = mouseUp.x - mouseDown.x;
        var posY = mouseUp.y - mouseDown.y;
        if (Mathf.Abs(posX) > Mathf.Abs(posY))
        {
            if (posX > 0) mouseDirection = "right";
            else mouseDirection = "left";
        }
        else
        {
            if (posY > 0) mouseDirection = "up";
            else mouseDirection = "down";
        }
        return mouseDirection;
    }
    private void GetValueInput(string inputDirection)
    {
        if (inputDirection == "right" && column + 1 < this.alldots.Width)
        {
            targetDot = this.alldots.AllDots[column + 1, row];
            targetDot.GetComponent<DotInteraction>().column -= 1;
            this.SetValue();
            column += 1;
            GameStateController.Instance.CurrentGameState = GameState.CheckingDot;
            Debug.Log("right");
        }
        if (inputDirection == "left" && column - 1 >= 0)
        {
            targetDot = this.alldots.AllDots[column - 1, row];
            targetDot.GetComponent<DotInteraction>().column += 1;
            this.SetValue();
            column -= 1;
            GameStateController.Instance.CurrentGameState = GameState.CheckingDot;
            Debug.Log("left");
        }
        if (inputDirection == "up" && row + 1 < this.alldots.Height)
        {
            targetDot = this.alldots.AllDots[column, row + 1];
            targetDot.GetComponent<DotInteraction>().row -= 1;
            this.SetValue();
            row += 1;
            GameStateController.Instance.CurrentGameState = GameState.CheckingDot;
            Debug.Log("up");
        }
        if (inputDirection == "down" && row - 1 >= 0)
        {
            targetDot = this.alldots.AllDots[column, row - 1];
            targetDot.GetComponent<DotInteraction>().row += 1;
            this.SetValue();
            row -= 1;
            GameStateController.Instance.CurrentGameState = GameState.CheckingDot;
            Debug.Log("down");
        }
        StartCoroutine(ChecktargetDot());
    }

    private Vector2 GetInput()
    {
        var inputMouse = Input.mousePosition;
        return inputMouse;
    }

    private void OnMouseDown()
    {
        mouseDown = GetInput();

    }
    private void OnMouseUp()
    {
        mouseUp = GetInput();
        var inputDirection = CheckTouch();
        this.GetValueInput(inputDirection);
    }
    #endregion

    private void SetValue()
    {
        preColumn = column;
        preRow = row;
    }


}

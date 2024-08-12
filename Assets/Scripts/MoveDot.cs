using UnityEngine;

public class MoveDot
{
    private int originDotCol, originDotRow, targetDotCol, targetDotRow;
    public int OriginDotCol { get => originDotCol; set => originDotCol = value; }
    public int OriginDotRow { get => originDotRow; set => originDotRow = value; }
    public int TargetDotCol { get => targetDotCol; set => targetDotCol = value; }
    public int TargetDotRow { get => targetDotRow; set => targetDotRow = value; }
    public MoveDot()
    {

    }

    public MoveDot(int originDotCol, int originDotRow, int targetDotCol, int targetDotRow)
    {
        this.OriginDotCol = originDotCol;
        this.OriginDotRow = originDotRow;
        this.TargetDotCol = targetDotCol;
        this.TargetDotRow = targetDotRow;
    }
}

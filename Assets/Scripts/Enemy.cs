using UnityEngine;

public class Enemy : Character
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.CurrentScoreHeal = this.MaxScoreHeal;
        this.CurrentScoreMana = 50f;
    }

    // Update is called once per frame
    void Update()
    {
        this.UpdateBar();
    }
}

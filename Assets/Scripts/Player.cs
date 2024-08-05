using UnityEngine;

public class Player : Character
{
    private static Player instance;
    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<Player>();
            }
            return instance;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CurrentScoreHeal = MaxScoreHeal;
        CurrentScoreMana = 50f;
    }

    // Update is called once per frame
    void Update()
    {
        this.UpdateBar();
    }
}

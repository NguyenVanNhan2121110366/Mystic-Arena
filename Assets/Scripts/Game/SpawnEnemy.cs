using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject[] enemys;
    [SerializeField] private Transform parentPrefabEnemy;
    [SerializeField] private int currentLevel;
    public int CurrentLevel { get => currentLevel; set => currentLevel = value; }

    private void Awake()
    {
        this.enemys = new GameObject[3];

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.currentLevel = SaveGame.Instance.saveData.currentLevel[0];
        this.GetPrefabEnemy();
        this.ChooseEnemySpawn();
    }

    private void GetPrefabEnemy()
    {
        for (var i = 0; i < this.parentPrefabEnemy.childCount; i++)
        {
            enemys[i] = this.parentPrefabEnemy.GetChild(i).gameObject;
        }
    }

    public void ChooseEnemySpawn()
    {
        switch (this.currentLevel)
        {
            case 0:
                this.SpawnEnemyFirst(0,new Vector3((float)11.11, (float)0.04, 0));
                break;
            case 1:
                this.SpawnEnemyFirst(1,new Vector3((float)9.35, (float)0.16, 0));
                break;
            case 2:
                this.SpawnEnemyFirst(2,new Vector3((float)11.11, (float)0.04, 0));
                break;
            default:
                break;
        }
    }

    private void SpawnEnemyFirst(int numberEnemy,Vector3 pos)
    {
        var objEnemy = Instantiate(this.enemys[numberEnemy],pos, Quaternion.identity);
        objEnemy.SetActive(true);
    }
}

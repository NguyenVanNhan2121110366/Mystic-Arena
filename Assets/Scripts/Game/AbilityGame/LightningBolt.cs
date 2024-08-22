using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LightningBolt : MonoBehaviour
{
    private List<GameObject> dots = new List<GameObject>();
    private List<GameObject> lightnightBolts = new List<GameObject>();
    private AllDotController allDots;
    private bool isGetRandomDot;
    [SerializeField] private Transform currentDot;
    [SerializeField] private Transform targetDot;
    [SerializeField] private bool isConnected;
    [SerializeField] private int countCurrentDot;
    [SerializeField] private int countRandomDot = 8;
    [SerializeField] private GameObject boltPre;
    [SerializeField] private float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.allDots = FindFirstObjectByType<AllDotController>();
        this.isGetRandomDot = true;
        this.isConnected = false;
        this.timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isGetRandomDot)
        {
            this.dots = GetRandomdot();
            this.isGetRandomDot = false;
        }
        else
        {
            this.LineConnected();
        }
    }

    private List<GameObject> GetRandomdot()
    {
        var dot = new List<GameObject>();

        for (; ; )
        {
            var count = 0;
            var randomCol = Random.Range(0, this.allDots.Width);
            var randomRow = Random.Range(0, this.allDots.Height);
            var randomObj = this.allDots.AllDots[randomCol, randomRow];
            foreach (var obj in dot)
            {
                if (obj == randomObj)
                {
                    count++;
                    break;
                }
            }

            if (count == 0)
                dot.Add(randomObj);
            if (dot.Count > 8)
                break;
        }
        return dot;
    }

    private void LineConnected()
    {
        if (!targetDot && !currentDot && !this.isConnected)
        {
            this.countCurrentDot = 0;
            this.currentDot = this.dots[countCurrentDot].transform;
            this.targetDot = this.dots[countCurrentDot + 1].transform;
        }
        else if (!this.isConnected && this.countCurrentDot < this.countRandomDot)
        {
            timer += Time.deltaTime;
            Debug.Log("test");
            if (timer > 0.3f)
            {
                var boltObj = Instantiate(this.boltPre);
                boltObj.transform.GetChild(0).position = this.dots[this.countCurrentDot].transform.position;
                boltObj.transform.GetChild(1).position = this.dots[this.countCurrentDot + 1].transform.position;
                boltObj.SetActive(true);
                this.lightnightBolts.Add(boltObj);
                this.countCurrentDot++;
                timer = 0;
                Debug.Log("test");
            }
        }
        else if (!this.isConnected && this.countCurrentDot == this.countRandomDot)
        {
            Debug.Log("test");
            this.isConnected = true;
            StartCoroutine(this.ExcuteDestroy());
        }
    }

    private void DestroyDot()
    {
        foreach (var dot in this.dots)
        {
            Destroy(dot);
        }
    }

    private void DestroyBolt()
    {
        foreach (var obj in this.lightnightBolts)
        {
            Destroy(obj);
        }
    }

    private IEnumerator ExcuteDestroy()
    {
        yield return new WaitForSeconds(0.6f);
        this.DestroyDot();
        this.DestroyBolt();
        yield return null;
        GameStateController.Instance.CurrentGameState = GameState.FillingDot;
        StartCoroutine(this.allDots.DestroyMatched());
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}

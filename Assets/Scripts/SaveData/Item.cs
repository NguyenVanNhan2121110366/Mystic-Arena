using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private int price;
    public int Price { get => price; set => price = value; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(price);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

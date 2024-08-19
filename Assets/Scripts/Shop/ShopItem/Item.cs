using TMPro;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private int price;
    [SerializeField] private TextMeshProUGUI txtPrice;
    public int Price { get => price; set => price = value; }

    private void Awake()
    {

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(price);
        this.txtPrice.text = price.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using TMPro;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private int price;
    [SerializeField] private TextMeshProUGUI txtPrice;
    [SerializeField] private GameObject description;
    public int Price { get => price; set => price = value; }

    private void Awake()
    {

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.description.SetActive(false);
        Debug.Log(price);
        this.txtPrice.text = price.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private Vector2 GetInput()
    {
        var mouseInput = Input.mousePosition;
        return mouseInput;
    }

    private void OnMouseDown()
    {

        this.description.SetActive(true);
        GetInput();
        Debug.Log("Touch");

    }


    private void OnMouseUp()
    {
        GetInput();
    }


    private void OnMouseExit()
    {
        this.description.SetActive(false);
        Debug.Log("Touch");
    }
}

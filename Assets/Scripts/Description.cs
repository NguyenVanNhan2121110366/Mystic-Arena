using UnityEngine;

public class Description : MonoBehaviour
{
    [SerializeField] private GameObject objDescription;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.objDescription.SetActive(false);
    }

    private void OnMouseEnter()
    {
        this.objDescription.SetActive(true);
        Debug.Log("Check");
    }

    private void OnMouseExit()
    {
        this.objDescription.SetActive(false);
        Debug.Log("Check 2");
    }
}

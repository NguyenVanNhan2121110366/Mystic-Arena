using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UISettingController : MonoBehaviour
{
    [SerializeField] private Button reset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.reset.onClick.AddListener(this.ClickReset);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ClickReset()
    {
        SceneManager.LoadScene("Menu");
    }
}

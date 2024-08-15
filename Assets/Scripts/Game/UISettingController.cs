using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UISettingController : MonoBehaviour
{
    private SaveAllData save;

    [SerializeField] private Button reset;

    private void Awake()
    {
        this.save = FindFirstObjectByType<SaveAllData>();
    }
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
        this.save.SaveDataGoldPlayer();
    }
}

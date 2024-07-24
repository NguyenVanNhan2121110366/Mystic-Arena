using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIMenu : MonoBehaviour
{
    [SerializeField] private Button bntStart;

    private void Awake()
    {
        this.bntStart.onClick.AddListener(ClickStart);
    }
    private void ClickStart()
    {
        SceneManager.LoadScene("Game");
    }
}

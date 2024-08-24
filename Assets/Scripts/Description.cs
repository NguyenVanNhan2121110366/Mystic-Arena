using System.Collections;
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
        StartCoroutine(Delay());
        this.objDescription.SetActive(true);
        Debug.Log("Check");
    }

    private void OnMouseExit()
    {
        this.objDescription.SetActive(false);
        Debug.Log("Check 2");
    }

    private IEnumerator Delay()
    {
        AudioManager.Instance.audioSrc.PlayOneShot(AudioManager.Instance.SoundClick);
        yield return new WaitForSeconds(0.3f);
    }
}

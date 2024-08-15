using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    [SerializeField] private RawImage rawImage;
    [SerializeField] private float posX, posY;

    private void Start()
    {
        StartCoroutine(this.SwitchX());
        StartCoroutine(this.SwitchY());
    }

    // Update is called once per frame
    void Update()
    {
        rawImage.uvRect = new Rect(rawImage.uvRect.position + new Vector2(posX, posY) * Time.deltaTime * 0.8f, rawImage.uvRect.size);
    }

    private IEnumerator SwitchY()
    {
        while (true)
        {

            if (posY == 0.01f)
            {
                yield return new WaitForSeconds(2.8f);
                posY = -0.01f;
            }

            if (posY == -0.01f)
            {
                yield return new WaitForSeconds(2.8f);
                posY = 0.01f;
            }
        }
    }

    private IEnumerator SwitchX()
    {
        while (true)
        {

            if (posX == 0.01f)
            {
                yield return new WaitForSeconds(6f);
                posX = -0.01f;
            }

            if (posX == -0.01f)
            {
                yield return new WaitForSeconds(6f);
                posX = 0.01f;
            }
        }
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    [SerializeField] private RawImage rawImage;
    [SerializeField] private float posX, posY;
    [SerializeField] private bool isCheck;

    // Update is called once per frame
    void Update()
    {
        rawImage.uvRect = new Rect(rawImage.uvRect.position + new Vector2(posX, posY) * Time.deltaTime*1, rawImage.uvRect.size);
        StartCoroutine(this.Switch());
    }

    private IEnumerator Switch()
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
                yield return new WaitForSeconds(3f);
                posY = 0.01f;
            }
        }
    }
}

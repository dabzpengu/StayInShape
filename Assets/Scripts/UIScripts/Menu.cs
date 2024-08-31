using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Menu: MonoBehaviour
{
    public string menuName;
    public bool isOpened;

    public void Open()
    {
        isOpened = true;
        gameObject.SetActive(true);
        StartCoroutine(OpenAnimation());
    }

    IEnumerator OpenAnimation()
    {
        gameObject.transform.DOMoveX(10, 1).SetEase(Ease.InOutSine).From();
        yield return 0;
    }

    public void Close()
    {
        isOpened = false;
        StartCoroutine(CloseAnimation());
    }

    IEnumerator CloseAnimation()
    {
        gameObject.transform.DOMoveX(-10, 1).SetEase(Ease.InOutSine).OnComplete(() => gameObject.SetActive(false));
        yield return 0;
    }
}

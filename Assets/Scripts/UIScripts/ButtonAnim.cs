using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;
public class ButtonAnim : MonoBehaviour
{
    [SerializeField]
    float buttonPopupTime = 0.1f;
    [SerializeField]
    float buttonShrinkBackTime = 0.4f;
    public UnityEvent onClickEvents;

    private Button button;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => StartCoroutine(ButtonClickAnim()));
    }

    IEnumerator ButtonClickAnim()
    {
       transform.DOScale(1.1f, 0.1f).OnComplete(() => transform.DOScale(1f, 0.5f).SetEase(Ease.InOutSine).OnComplete(() => onClickEvents.Invoke())).SetEase(Ease.InOutSine);
       yield return 0;
    }

}

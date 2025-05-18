using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.Events;

public class AnimButton : MonoBehaviour, IPointerClickHandler
{
     public UnityEvent unityEvent;
     public void OnPointerClick(PointerEventData eventData)
     {
          transform.DOScale(0.9f, 0.1f).SetUpdate(true).OnComplete(() =>
                      {
                           transform.DOScale(1f, 0.1f).SetUpdate(true).OnComplete(() =>
                           {
                                unityEvent.Invoke();
                           });
                      });
     }


}

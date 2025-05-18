using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimPopUp : MonoBehaviour
{

     private void OnEnable()
     {
          transform.DOScale(1f, 1f).SetUpdate(true);
     }
     private void OnDisable()
     {
          transform.DOScale(0.1f, 1f).SetUpdate(true);
     }
}

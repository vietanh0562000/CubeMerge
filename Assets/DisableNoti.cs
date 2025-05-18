using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DisableNoti : MonoBehaviour
{
     private void OnEnable()
     {
          transform.DOMove(Vector3.up, 2f).OnComplete(() =>
          {
               transform.gameObject.SetActive(false);
          }).SetUpdate(true);
     }
     private void OnDisable()
     {
          transform.DORewind();
     }
}

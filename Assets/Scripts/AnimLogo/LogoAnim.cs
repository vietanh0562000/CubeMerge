using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class LogoAnim : MonoBehaviour
{
     // Start is called before the first frame update
     void Start()
     {
          ScaleTillDie();
          StartCoroutine(BlinkBlink());
     }
     private void ScaleTillDie()
     {
          transform.DOScale(1.2f, 1f).OnComplete(() =>
                              {
                                   KeepScaling();
                              });
     }
     private void KeepScaling()
     {
          transform.DOScale(1f, 1f).OnComplete(() =>
                                        {
                                             ScaleTillDie();
                                        });
     }
     // Update is called once per frame
     private IEnumerator BlinkBlink()
     {
          int i = 0;
          while (i < 8)
          {
               if (i == 7) i = 0;
               transform.GetChild(i).gameObject.SetActive(false);
               yield return new WaitForSecondsRealtime(0.1f);
               transform.GetChild(i).gameObject.SetActive(true);
               i++;

          }

     }
}

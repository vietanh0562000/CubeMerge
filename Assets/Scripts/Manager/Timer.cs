using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
     [SerializeField] Image uiFill;
     [SerializeField] TextMeshProUGUI uiText;
     public int _cdTime;
     private int _remainingTime;
     public GameObject _noTks;


     void OnEnable()
     {
          Being(_cdTime);
          _noTks.SetActive(false);
     }

     private void Being(int Second)
     {
          _remainingTime = Second;
          StartCoroutine(UpdateTimer());
     }
     private IEnumerator UpdateTimer()
     {
          while (_remainingTime >= 0)
          {
               if (_remainingTime == 10) _noTks.SetActive(true);
               uiText.text = _remainingTime.ToString();
               uiFill.fillAmount = Mathf.InverseLerp(0, _cdTime, _remainingTime);
               _remainingTime--;
               yield return new WaitForSecondsRealtime(1f);
          }
          OnEnd();
     }
     private void OnEnd()
     {
          UIManager.instance.ResetGameWhenLose();
     }
}

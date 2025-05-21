using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIManager : MonoBehaviour
{

     public TextMeshProUGUI txtScore;
     public Canvas _adsReward;
     string txtTemp;

     Button _start;
     public GameObject _continue;
     public bool _pausing = false;

     public GameObject _loserPopUp;
     public GameObject _loserWatchedPopUp;
     public TextMeshProUGUI _loserScore;

     public int score;
     public static UIManager instance;

     bool _loserArlert = false;

     public GameObject flyingAds;

     public Image _spiningSpining;

     public GameObject _shopUI;
     
     Quaternion _buttonService;
     private void Awake()
     {
          instance = this;
          _buttonService = _spiningSpining.transform.GetChild(0).rotation;
          StartCoroutine(WakeUpAds());

     }
     private void Update()
     {
          _spiningSpining.transform.Rotate(new Vector3(0, 0, 5) * 12 * Time.deltaTime);
          _spiningSpining.transform.GetChild(0).rotation = _buttonService;
     }
     public IEnumerator WakeUpAds()
     {
          yield return new WaitForSeconds(60f);
          
          flyingAds.SetActive(true);
     }
     public void FlyingAdsOnComplete()
     {
          StartCoroutine(FlyingAds());
     }
     public IEnumerator FlyingAds()
     {
          flyingAds.SetActive(false);
          yield return new WaitForSeconds(30f);
          flyingAds.SetActive(true);
     }
     public void addScore(int total)
     {
          txtScore.transform.DOScale(1.6f, .3f).OnComplete(() =>
                    {
                         txtScore.transform.DOScale(1f, .2f);
                    });
          score += total;
          txtScore.text = score.ToString();
     }
     public void ResetGame()
     {
          SceneManager.LoadScene("InGameScene");
          SoundManager.instance.PlaySound(0);
          _pausing = false;
          GameManager.instance.DeleteKey();
          GameManager.instance.DeleteNewBox();

     }
     public void ResetGameWhenLose()
     {
          SoundManager.instance.PlaySound(0);
          GameManager.instance.DeleteKey();
          GameManager.instance.DeleteNewBox();
          SceneManager.LoadScene("ChooseMode");

          _pausing = false;

     }
     public void Backbtn()
     {
          SceneManager.LoadScene("ChooseMode");
     }
     public void ContinueBtn()
     {
          _continue.SetActive(false);
          _pausing = false;
     }
     public void LoserPopUp()
     {
          if (!_loserArlert)
          {
               SoundManager.instance.PlaySound(4);
               _loserArlert = true;
          }

          _loserPopUp.SetActive(true);
          _loserPopUp.GetComponent<Timer>().enabled = true;
          _pausing = true;
     }
     public void LoserContinue()
     {
          // AdmobManager.instance.ShowAdReward(() =>
          // {
          //      _loserPopUp.GetComponent<Timer>().enabled = false;
          //      _loserArlert = false;
          //      UIManager.instance._pausing = false;
          //      Trigger.instace.timeDelayLose = 300f;
          //      _loserPopUp.SetActive(false);
          //      GameManager.instance.ImDead();
          // }, () =>
          // {
          //      MenuManager.instance.Failed.gameObject.SetActive(true);
          // });

     }


     public void Pause()
     {
          if (_pausing)
          {
               Time.timeScale = 0;
          }
          else Time.timeScale = 1;
     }

     public void OpenShop()
     {
          _shopUI.SetActive(true);
     }

     public void CloseShop()
     {
          _shopUI.SetActive(false);
     }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{
     public static MenuManager instance;
     public int _mode;
     public AudioClip _clip;
     AudioSource _effS;
     public int _played = 0;
     public GameObject[] _sellectedMode;
     int _sellected = -1;
     int _countArr;

     public int _watchedSphereAds = 0;
     public int _watchedTriangleAds = 0;
     public int _watchedMixAds = 0;
     public Button _sphere;
     public Button _triangle;
     public Button _mix;
     public TextMeshProUGUI _adstext;
     public Button _sphereMode;
     public Button _triangleMode;
     public Button _mixMode;

     public TextMeshProUGUI Failed;

     private void Awake()
     {
          instance = this;
          _effS = GetComponent<AudioSource>();
          _sellected = PlayerPrefs.GetInt("gameMode");
     }

     private void Update()
     {
          if (SceneManager.GetActiveScene().buildIndex == 1)
          {

               _watchedSphereAds = PlayerPrefs.GetInt("sphere");
               _watchedTriangleAds = PlayerPrefs.GetInt("triangle");
               _watchedMixAds = PlayerPrefs.GetInt("mix");
               if (_watchedSphereAds == 1)
               {
                    _sphere.gameObject.SetActive(false);
                    _sphereMode.gameObject.SetActive(true);

               }
               if (_watchedTriangleAds == 1)
               {
                    _triangle.gameObject.SetActive(false);
                    _triangleMode.gameObject.SetActive(true);
               }
               if (_watchedMixAds == 3)
               {
                    _mix.gameObject.SetActive(false);
                    _mixMode.gameObject.SetActive(true);
               }
               _adstext.text = _watchedMixAds + "/3";
               _effS.clip = _clip;

               if (_sellectedMode.Length != 0)
                    for (int i = 0; i < _sellectedMode.Length; i++)
                    {
                         if (_sellected == i) _sellectedMode[i].SetActive(true);
                         else _sellectedMode[i].SetActive(false);
                    }

          }
     }
     public Button _startGame;
     public void StartGame()
     {
          _effS.PlayOneShot(_effS.clip);
          _startGame.transform.DOScale(0.9f, 0.1f).OnComplete(() =>
          {
               ZoomBack();
          });
     }
     private void ZoomBack()
     {
          _startGame.transform.DOScale(1f, 0.1f).OnComplete(() =>
                    {
                         if (PlayerPrefs.GetInt("Played") == 1)
                         {
                              SceneManager.LoadScene(PlayerPrefs.GetInt("LastScenePlayed"));
                         }
                         else
                              SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    });
     }
     public void Played()
     {
          PlayerPrefs.SetInt("Played", MenuManager.instance._played = 1);
          PlayerPrefs.SetInt("LastScenePlayed", SceneManager.GetActiveScene().buildIndex + 1);
     }
     public void UnlockSphereBtn()
     {
          // AdmobManager.instance.ShowAdReward(() =>
          //      {
          //           _watchedSphereAds++;
          //           PlayerPrefs.SetInt("sphere", _watchedSphereAds++);
          //      }, () =>
          //      {
          //           Failed.gameObject.SetActive(true);
          //      });
     }


     public void UnlockTriangleBtn()
     {
          // AdmobManager.instance.ShowAdReward(() =>
          //      {
          //           _watchedTriangleAds++;
          //           PlayerPrefs.SetInt("triangle", _watchedTriangleAds++);


          //      }, () =>
          //      {
          //           Failed.gameObject.SetActive(true);
          //      });
     }
     public void UnlockMixBtn()
     {
          // AdmobManager.instance.ShowAdReward(() =>
          //      {
          //           _watchedMixAds++;
          //           PlayerPrefs.SetInt("mix", _watchedMixAds++);
          //      }, () =>
          //      {
          //           Failed.gameObject.SetActive(true);
          //      });
     }
     public void Disable()
     {
          Failed.gameObject.SetActive(false);
     }
     public void ChooseMode(int index)
     {
          Played();

          _mode = index;
          _effS.PlayOneShot(_effS.clip);
          PlayerPrefs.SetInt("gameMode", _mode);
          if (_mode == 3)
          {
               SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

          }
          else
          {
               SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

          }
     }
}

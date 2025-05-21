using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
     public static AdsManager instance;
     public Canvas _adsRocket;
     public GameObject RocketReward;
     public ParticleSystem _explode;




     Vector2 _posTemp;
     private void Awake()
     {
          instance = this;
     }

     public void SpawnRocketReward()
     {

          // AdmobManager.instance.ShowAdReward(() =>
          // {
              UIManager.instance.FlyingAdsOnComplete(); 
              RocketReward.transform.gameObject.SetActive(true);
          // }, () =>
          // {
          //      MenuManager.instance.Failed.gameObject.SetActive(true);
          // });
     }



     public void DestroyOnExplode()
     {

          _posTemp = RocketReward.transform.GetChild(0).position;
          RocketReward.transform.gameObject.SetActive(false);

          Instantiate(_explode, _posTemp, Quaternion.identity);
          SoundManager.instance.PlaySound(5);
          StartCoroutine(EXPLOSION());
     }

     public void TestExplosion(){
          StartCoroutine(EXPLOSION());
     }

     public IEnumerator EXPLOSION()
     {
          int i = 0;
          float x = -2.5f;
          float y = -2f;
          while (i < 2)
          {
               yield return new WaitForSeconds(0.1f);
               Instantiate(_explode, _posTemp + new Vector2(x, y), Quaternion.identity);
               SoundManager.instance.PlaySound(5);
               i++;
               x += 5f;

          }
          foreach (GameObject box in GameManager.instance._droppedList)
          {
               UIManager.instance.addScore(box.GetComponent<NumberGen>().number);
               Destroy(box);
          }
     }
}
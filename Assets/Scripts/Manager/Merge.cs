using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Merge : MonoBehaviour
{
     float timeDelay = 0.5f;
     float timeTemp;
     bool _isTouching = false;
     private void Update()
     {
          if (Time.time - timeTemp > timeDelay)
          {
               _isTouching = false;
          }
     }

     private void OnCollisionEnter2D(Collision2D other)
     {
          if (!_isTouching)
          {
               SoundManager.instance.PlaySound(1);
               _isTouching = true;
          }
          timeTemp = Time.time;



          if (other.gameObject.tag == "Finish" || (other.gameObject.tag == "Dropped" && gameObject.tag == "Dropping"))
               gameObject.tag = "Dropped";
          if (gameObject.tag == "Dropped")
          {
               GameManager.instance.SaveGameMix();
               if (!GameManager.instance._droppedList.Contains(gameObject))
                    GameManager.instance._droppedList.Add(gameObject);
          }
          if (other.gameObject.tag == gameObject.tag)
          {
               if (other.gameObject.GetComponent<NumberGen>().number == gameObject.GetComponent<NumberGen>().number)
               {
                    GameManager.instance.SaveGameMix();
                    float _forceImpact = other.relativeVelocity.magnitude;
                    GameManager.instance.merge(transform, gameObject.GetComponent<NumberGen>().number, other.gameObject, gameObject, _forceImpact);
               }
          }
     }
     private void OnCollisionStay2D(Collision2D other)
     {

          {
               if (!GameManager.instance._droppedList.Contains(gameObject))
                    GameManager.instance._droppedList.Add(gameObject);
          }
          if (other.gameObject.tag == gameObject.tag && other.gameObject.name == gameObject.name)
          {
               if (other.gameObject.GetComponent<NumberGen>().number == gameObject.GetComponent<NumberGen>().number)
               {
                    float _forceImpact = other.relativeVelocity.magnitude;

                    GameManager.instance.merge(transform, gameObject.GetComponent<NumberGen>().number, other.gameObject, gameObject, _forceImpact);
               }
          }
     }
}





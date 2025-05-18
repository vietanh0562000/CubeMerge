using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchManager : MonoBehaviour
{
     public static TouchManager instance;
     public GameObject objectToSpawn;
     public Transform dropPosition;
     public Vector2 defaultSpawnPos;
     GameObject _2048;
     public float speed = 0.1f;
     public float xMin;
     public float xMax;
     float timeDelay = 1;
     float timeTemp;
     bool canSpawn = false;
     int rnd;


     public GameObject _tut;

     Vector3 mousePos;
     Vector2 newTarget;
     public int rndspawn;
     private void Awake()
     {
          instance = this;
     }
     private void Start()
     {

          if (GameManager.instance.RandomMode(GameManager.instance._gameMode))
          {
               rndspawn = GameManager.instance.random2048();
               objectToSpawn = GameManager.instance.prefabs[rndspawn];
          }
          else objectToSpawn = GameManager.instance.prefabs[GameManager.instance._gameMode];
          _2048 = Instantiate(objectToSpawn, transform.position, transform.rotation);
          _2048.name = objectToSpawn.name;
          rnd = GameManager.instance.random();
          _2048.GetComponent<NumberGen>().Init(_2048, GameManager.instance.num[rnd], rnd);
          timeTemp = Time.time;

     }
     private bool IsPointerOverUIObject()
     {
          PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
          eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
          List<RaycastResult> results = new List<RaycastResult>();
          EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
          return results.Count > 0;
     }
     private void Update()
     {
          if (!IsPointerOverUIObject())
          {
               if (Input.GetMouseButton(0))
               {
                    _tut.SetActive(false);
                    if (!canSpawn)
                    {
                         Vector3 temp = Input.mousePosition;
                         temp.z = 10;
                         mousePos = Camera.main.ScreenToWorldPoint(temp);


                         newTarget = new Vector2(mousePos.x, transform.position.y);
                         if (GameManager.instance._gameMode == 2 || TouchManager.instance.rndspawn == 2)
                         {
                              if (newTarget.x < xMin + 0.2)
                              {
                                   newTarget.x = xMin + 0.2f;
                              }
                              if (newTarget.x > xMax - 0.2f)
                              {
                                   newTarget.x = xMax - 0.2f;
                              }
                         }
                         else
                         {
                              if (newTarget.x < xMin)
                              {
                                   newTarget.x = xMin;
                              }
                              if (newTarget.x > xMax)
                              {
                                   newTarget.x = xMax;
                              }
                         }

                         _2048.transform.position = Vector3.Lerp(transform.position, newTarget, speed);

                    }
               }
               if (Input.GetMouseButtonUp(0))
               {

                    if (!canSpawn)
                    {

                         SoundManager.instance.PlaySound(1);


                         _2048.tag = "Dropping";
                         if (_2048.tag == "Dropping")
                         {
                              _2048.GetComponent<Rigidbody2D>().bodyType =
                                                  RigidbodyType2D.Dynamic;
                              _2048.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                              _2048.transform.GetChild(1).gameObject.SetActive(false);
                         }
                         canSpawn = true;
                         timeTemp = Time.time;
                    }
                    dropPosition.transform.position = defaultSpawnPos;
               }



               if (canSpawn)
               {
                    if (Time.time - timeTemp > timeDelay)
                    {

                         if (GameManager.instance.RandomMode(GameManager.instance._gameMode))
                         {
                              rndspawn = GameManager.instance.random2048();
                              objectToSpawn = GameManager.instance.prefabs[rndspawn];
                         }

                         _2048 = Instantiate(objectToSpawn, transform.position, transform.rotation);
                         _2048.name = objectToSpawn.name;


                         rnd = GameManager.instance.random();
                         _2048.GetComponent<NumberGen>().Init(_2048, GameManager.instance.num[rnd], rnd);
                         canSpawn = false;
                    }
               }

          }
     }
     public int test;
}


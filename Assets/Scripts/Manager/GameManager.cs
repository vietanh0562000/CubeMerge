using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
     public int _gameMode;
     public List<GameObject> prefabs;
     int index;
     public List<int> num;
     public List<int> _newNum;
     int rnd;
     public float _power;
     public GameObject particleSys;
     public static GameManager instance;
     public TextMeshProUGUI txtNew;
     public GameObject _new;
     float timeDelayMerge = 0.01f;
     public bool canMerge = true;
     float timeTemp;
     float timeTempNew;
     float timeNewDelay = 5f;
     Vector2 newTarget;
     GameObject _2048;
     public List<GameObject> _droppedList;
     public string _name;

     public bool isSphere;
     public int _combo = 0;
     public float timeTempCombo;
     public float timeDelayCombo;

     GameObject _explode;


     float timeDelayAds = 25f;
     public List<GameObject> _womboCombo;
     private void Awake()
     {
          instance = this;
          _gameMode = PlayerPrefs.GetInt("gameMode");
     }
     private void Start()
     {
          LoadNewBox();
          if (_gameMode == 1)
               isSphere = true;
          if (PlayerPrefs.GetInt("Played") == 1)
               for (int i = 0; i < PlayerPrefs.GetInt("listCount" + _gameMode); i++)
               {
                    if (_gameMode != 3)
                    {
                         _name = i + " " + _gameMode;
                         LoadGame(prefabs[_gameMode], _name);
                    }
                    if (_gameMode == 3)
                    {
                         _name = i + "MIX";
                         foreach (GameObject prefab in prefabs)
                         {
                              if (prefab.name == PlayerPrefs.GetString(_name + "prefabname"))
                              {
                                   LoadGame(prefab, _name);
                              }
                         }
                    }
               }
     }
     public bool _dying;
     public float timeTempDying;
     public float timeDyingDelay = 3f;
     private void Update()
     {

          //Test Zone
          for (int i = 0; i < _droppedList.Count; i++)
          {
               if (_droppedList[i] == null) _droppedList.Remove(_droppedList[i]);
          }
          //EndTest

          UIManager.instance.Pause();

          if (Time.time - timeTempDying > timeDyingDelay)
               _dying = false;
          if (Time.time - timeTemp > timeDelayMerge)
          {
               canMerge = true;
          }
          if (Time.time - timeTempNew > timeNewDelay)
          {
               _new.SetActive(false);
               txtNew.text = "";
          }

     }
     public int droppedCount = 0;
     public void merge(Transform posSpawn, int n, GameObject Box, GameObject detroyedBox, float impactForce)
     {

          if (canMerge)
          {
               GameManager.instance._droppedList.Remove(detroyedBox);
               GameManager.instance._droppedList.Remove(Box);

               foreach (GameObject prefab in prefabs)
               {
                    if (prefab.name == Box.name)
                    {
                         _2048 = Instantiate(prefab, new Vector2(Box.transform.position.x, Box.transform.position.y), Box.transform.rotation);
                         _2048.name = prefab.name;
                    }
               }



               _droppedList.Add(_2048);

               Destroy(detroyedBox);
               Destroy(Box);

               n *= 2;
               if (!num.Contains(n))
               {
                    txtNew.text = "New Box: " + n;
                    _new.SetActive(true);
                    if (index > 10) index = 10;
                    SoundManager.instance.PlaySound(3);
                    timeTempNew = Time.time;
                    _newNum.Add(n);
                    num.Add(n);
               }
               SoundManager.instance.PlaySound(2);




               if (Time.time - timeTempCombo > timeDelayCombo)
               {
                    _combo = 0;
               }
               timeTempCombo = Time.time;
               _combo++;
               if (_combo == 2)
               {
                    timeTempCombo = Time.time;
                    Instantiate(_womboCombo[0], new Vector3(2, 3, -2), Quaternion.identity);
               }
               else if (_combo == 3)
               {
                    timeTempCombo = Time.time;
                    Instantiate(_womboCombo[1], new Vector3(-2, 3, -2), Quaternion.identity);
               }
               else if (_combo == 4)
               {
                    timeTempCombo = Time.time;
                    Instantiate(_womboCombo[2], new Vector3(0, 3, -2), Quaternion.identity);
               }
               else if (_combo == 5)
               {
                    timeTempCombo = Time.time;
                    Instantiate(_womboCombo[3], new Vector3(0, 0, -2), Quaternion.identity);
               }
               else if (_combo > 5)
               {
                    timeTempCombo = Time.time;
                    Instantiate(_womboCombo[4], new Vector3(0, 2, -2), Quaternion.identity);
               }

               _2048.transform.GetChild(1).gameObject.SetActive(false);

               _explode = Instantiate(particleSys, new Vector3(_2048.transform.position.x, _2048.transform.position.y, _2048.transform.position.z - 1), Quaternion.identity);

               for (int i = 0; i < num.Count(); i++)
                    if (n == num[i]) index = i;
               _2048.GetComponent<NumberGen>().Init(_2048, n, index);



               _2048.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
               _2048.tag = "Dropped";


               Vector2 direction = Box.transform.position - detroyedBox.transform.position;

               _2048.GetComponent<Rigidbody2D>().AddForce(direction * impactForce * _power, ForceMode2D.Impulse);

               float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
               _2048.transform.Rotate(new Vector3(0, 0, angle) * Time.deltaTime);


               UIManager.instance.addScore(n);

               timeTemp = Time.time;
               canMerge = false;



          }

     }
     public void ImDead()
     {
          for (int i = 0; i < _droppedList.Count; i++)
               for (int j = i + 1; j < _droppedList.Count; j++)
                    if (_droppedList[i].GetComponent<NumberGen>().number == _droppedList[j].GetComponent<NumberGen>().number)
                    {
                         _droppedList[i].transform.position = _droppedList[j].transform.position;
                    }
     }

     public void GameOver()
     {
          DeleteKey();
          DeleteNewBox();
          UIManager.instance.LoserPopUp();
          UIManager.instance._pausing = true;


     }
     public int random()
     {
          float biggestNumber = num[num.Count - 1];

          int rndTest = Random.Range(0, 101);

          int test = (int)Mathf.Log(1f - (rndTest * (biggestNumber - 2f) / (100f * biggestNumber)), 0.5f);

          return test;
     }
     public int random2048()
     {
          rnd = Random.Range(0, 3);

          return rnd;
     }
     public bool RandomMode(int index)
     {
          if (index == 3)
          {
               return true;
          }
          else return false;

     }
     public void SaveNewBox()
     {
          PlayerPrefs.SetInt("Arr.Len" + _gameMode, _newNum.Count);
          for (int i = 0; i < PlayerPrefs.GetInt("Arr.Len" + _gameMode); i++)
          {
               PlayerPrefs.SetInt("num" + i, _newNum[i]);
          }
     }
     public void LoadNewBox()
     {
          if (PlayerPrefs.GetInt("Arr.Len" + _gameMode) != 0)
          {
               for (int i = 0; i < PlayerPrefs.GetInt("Arr.Len" + _gameMode); i++)
               {
                    if (PlayerPrefs.GetInt("num" + i) != 0)
                    {
                         num.Add(PlayerPrefs.GetInt("num" + i));
                         _newNum.Add(PlayerPrefs.GetInt("num" + i));
                    }
               }
          }
     }

     public void SaveGameMix()
     {
          SaveNewBox();
          for (int i = 0; i < _droppedList.Count; i++)
          {
               if (_gameMode != 3 && _droppedList[i] != null)
               {
                    _name = i + " " + _gameMode;
                    SaveGame(_droppedList[i], _name);
               }
               if (_gameMode == 3 && _droppedList[i] != null)
               {
                    foreach (GameObject prefab in prefabs)
                    {
                         _name = i + "MIX";
                         SaveGame(_droppedList[i], _name);
                    }
               }
          }

     }
     public void SaveGame(GameObject _dropped, string _name)
     {
          PlayerPrefs.SetInt("listCount" + _gameMode, _droppedList.Count);
          PlayerPrefs.SetString(_name + "prefabname", _dropped.name);
          PlayerPrefs.SetFloat(_name + "X", _dropped.transform.position.x);
          PlayerPrefs.SetFloat(_name + "Y", _dropped.transform.position.y);
          PlayerPrefs.SetFloat(_name + "Rot", _dropped.transform.eulerAngles.z);

          PlayerPrefs.SetInt("Score" + _gameMode, UIManager.instance.score);
          PlayerPrefs.SetInt(_name + "num", _dropped.GetComponent<NumberGen>().number);

          PlayerPrefs.SetInt(_name + "color", _dropped.GetComponent<NumberGen>().colornum);
     }
     public void LoadGame(GameObject _dropped, string _name)
     {
          if (PlayerPrefs.GetInt(_name + "num") != 0)
          {
               UIManager.instance._continue.SetActive(true);
               UIManager.instance._pausing = true;
               GameObject _saveddropped;

               _saveddropped = Instantiate(_dropped, new Vector2(PlayerPrefs.GetFloat(_name + "X"), PlayerPrefs.GetFloat(_name + "Y")), Quaternion.Euler(0, 0, PlayerPrefs.GetFloat(_name + "Rot")));
               _saveddropped.name = _dropped.name;
               UIManager.instance.score = PlayerPrefs.GetInt("Score" + _gameMode);
               UIManager.instance.txtScore.text = UIManager.instance.score.ToString();
               _saveddropped.tag = "Dropped";
               _saveddropped.GetComponent<NumberGen>().Init(_dropped, PlayerPrefs.GetInt(_name + "num"), PlayerPrefs.GetInt(_name + "color"));

               _saveddropped.transform.GetChild(1).gameObject.SetActive(false);
               _saveddropped.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
               _droppedList.Add(_saveddropped);


          }

     }
     public void DeleteNewBox()
     {
          for (int i = 0; i < PlayerPrefs.GetInt("Arr.Len", 0); i++)
          {
               PlayerPrefs.DeleteKey("num" + i);
          }
          PlayerPrefs.DeleteKey("Arr.Len" + _gameMode);
     }
     public void DeleteKey()
     {
          for (int i = 0; i < _droppedList.Count; i++)
          {
               _name = i + " " + GameManager.instance._gameMode;
               PlayerPrefs.DeleteKey(_name + "X");
               PlayerPrefs.DeleteKey(_name + "Y");
               PlayerPrefs.DeleteKey(_name + "Rot");
               PlayerPrefs.DeleteKey("Score" + _gameMode);
               PlayerPrefs.DeleteKey(_name + "num");
               PlayerPrefs.DeleteKey(_name + "color");
          }
          PlayerPrefs.DeleteKey("listCount"
          + _gameMode);

     }
}


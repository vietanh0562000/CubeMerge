using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Trigger : MonoBehaviour
{
     public bool canLose = false;
     public bool lost = false;
     public static Trigger instace;
     public float _posYToLose;
     float timeTemp;
     public float timeDelayLose = 300f;
     public SpriteRenderer _lineColor;
     GameObject _droppedPos;
     public List<GameObject> _boxOnLine;
     private void Awake()
     {
          instace = this;
     }
     private void Update()
     {
          if (_boxOnLine.Count != 0)
          {
               timeDelayLose--;
               _lineColor.color = Color.red;
               canLose = true;
          }
          else if (_boxOnLine.Count == 0)
          {
               timeDelayLose = 200f;
               _lineColor.color = new Color(1, 1, 1, 0.2f);
               canLose = false;
          }
          if (canLose && timeDelayLose == 0)
          {
               GameManager.instance.GameOver();
          }
          foreach (GameObject box in GameManager.instance._droppedList)
               if (box.transform.position.y >= _posYToLose && box.tag == "Dropped" && box != null)
                    if (!_boxOnLine.Contains(box) && box != null)
                    {
                         _boxOnLine.Add(box);
                    }
          for (int i = 0; i < _boxOnLine.Count; i++)
          {
               if (_boxOnLine[i] == null || _boxOnLine[i].transform.position.y < _posYToLose)
                    _boxOnLine.RemoveAt(i);
          }
     }


}

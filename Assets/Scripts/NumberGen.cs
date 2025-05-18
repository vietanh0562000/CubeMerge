using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberGen : MonoBehaviour
{
     public static NumberGen instance;
     public List<Material> _mate;
     private void Awake()
     {
          instance = this;
     }
     public Color[] _color;
     public TextMeshPro txtNumber;

     public MeshRenderer cubeRenderer;
     public int number;
     public int colornum;
     public void Init(GameObject _prefab, int _number, int _colornum)
     {
          if (_prefab.name == GameManager.instance.prefabs[1].name)
          {
               Material[] newMat = new Material[2];
               newMat[0] = _mate[_colornum];
               newMat[1] = cubeRenderer.materials[1];
               cubeRenderer.materials = newMat;
          }


          if (_colornum > 10) _colornum = 10;
          colornum = _colornum;
          number = _number;
          txtNumber.text = _number.ToString();
          cubeRenderer.materials[1].color = _color[_colornum];
     }


}

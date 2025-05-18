using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
     public static SoundManager instance;
     bool Sound = true;
     public Image soundbtn;
     public AudioSource _effectSound;
     public AudioClip[] _clipArr;
     private void Awake()
     {
          instance = this;
          _effectSound = GetComponent<AudioSource>();
     }
     private void Update()
     {
          if (!Sound)
               _effectSound.Stop();
     }
     public void PlaySound(int _clipnum)
     {
          _effectSound.clip = _clipArr[_clipnum];
          _effectSound.PlayOneShot(_effectSound.clip);
     }
     public void TurnOffSound()
     {
          soundbtn.gameObject.SetActive(false);
          Sound = false;
     }
     public void TurnOnSound()
     {
          Sound = true;
          soundbtn.gameObject.SetActive(true);
     }
}

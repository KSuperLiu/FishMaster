using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public bool isPlay =true;
    public AudioSource audioSource;
    public AudioClip seaWaveClip;
    public AudioClip goldClip;
    public AudioClip rewardClip;  
    public AudioClip FireClip; 
    public AudioClip changeClip;
    public AudioClip clickClip;
    public AudioClip lvUpClip;


    public AudioClip[] fishSpeakClip;
  
    
    void Awake () {
        _instance = this;

         isPlay = PlayerPrefs.GetInt("isPlay",1)==1?true:false;//默认是播放的
        DoChange();
	}

   
    private void DoChange()
    {
        if (isPlay)
        { 
          
            audioSource.Play();
        }
        else
        {
          
            audioSource.Pause();
        }
    }

    public void SwitchBgMusicState(bool isOn)
    { 
        isPlay = isOn;
        DoChange();
    }
	
    public void PlayEffectSound(AudioClip clip)
    {
        if (isPlay)
        {
          //  print(clip.name + "::播放了");
            AudioSource.PlayClipAtPoint(clip,new Vector3(0,0,-5));
        }
    }


    public bool IsMute
    {
        get
        {
            return isPlay;
        }
    }
}

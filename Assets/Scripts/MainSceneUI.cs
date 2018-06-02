using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainSceneUI : MonoBehaviour {
    public GameObject settingPanel;
    public Toggle toggle;

    private void Start()
    {
        //isPlay方法在Awake中  Awake在Start前面
       toggle.isOn= AudioManager.Instance.isPlay;
        
    }
    //是否打开声音
    public void ChoseBg(bool isOn)
    {
        AudioManager.Instance.PlayEffectSound(AudioManager.Instance.clickClip);
        print("isOn：" + isOn);
        AudioManager.Instance.SwitchBgMusicState(isOn);
    }

    public void OnBackDown()
    {
        AudioManager.Instance.PlayEffectSound(AudioManager.Instance.clickClip);
        settingPanel.SetActive(false);
    }

    public void OnSettingDown()
    {
        AudioManager.Instance.PlayEffectSound(AudioManager.Instance.clickClip);
        settingPanel.SetActive(true);
    }

    public void Exit()
    {
        AudioManager.Instance.PlayEffectSound(AudioManager.Instance.clickClip);

        PlayerPrefs.SetInt("gold", GameController.Instance.gold);
        PlayerPrefs.SetInt("lv", GameController.Instance.lv);
        PlayerPrefs.SetFloat("smallTimer", GameController.Instance.smallTimer);
        PlayerPrefs.SetFloat("bigTmer", GameController.Instance.bigTmer);
        PlayerPrefs.SetInt("exp", GameController.Instance.exp);
        int isPlay = (AudioManager.Instance.IsMute == false) ? 0 : 1;
        PlayerPrefs.SetInt("isPlay", isPlay);
        PlayerPrefs.SetInt("costIndex", GameController.Instance.costIndex);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

}

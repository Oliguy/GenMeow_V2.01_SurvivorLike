using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;


[System.Serializable]
public enum UIsfx
{
    Click,
    Open,
    Close,
}


public class MeowSoundBase : MMSingleton<MeowSoundBase>
{
    /// <summary>
    /// 场景音乐通过字典string调用
    /// </summary>
    [Header("Music")]
    public List<AudioClip> musicList;

    /// <summary>
    /// SFX通过字典string调用
    /// </summary>
    [Header("SFX")]
    public List<AudioClip> ChestAppear;
    public List<AudioClip> Fail;

    /// <summary>
    /// UI通过Enum调用
    /// </summary>
    [Header("UI")]
    public List<AudioClip> Open;
    public List<AudioClip> Close;
    public List<AudioClip> Click;

    public Dictionary<string, AudioClip> MusicSoundDic;
    public Dictionary<string, AudioClip> SFXSoundDic;


    protected override void Awake()
    {
        if (Instance != null && Instance != this)
        {
            // 如果已经有一个实例存在并且不是当前实例，那么销毁当前实例
            Destroy(gameObject);
        }
        else
        {
            // 否则，这个实例就是我们的单例，不要在加载新场景时销毁它。
            base.Awake();
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        //加载Music和SFX到字典中
        LoadClipToDic(ChestAppear, SFXSoundDic);
        LoadClipToDic(Fail, SFXSoundDic);
        //LoadClipToDic(musicList, MusicSoundDic);
    }

    public void PlayMusic(string musicName)
    {
        if (MusicSoundDic[musicName] == null)
        {
            Debug.LogError("试图播放字典中不存在的音乐，请检查！");
            return;
        }
        MMSoundManagerSoundPlayEvent.Trigger(MusicSoundDic[musicName], MMSoundManager.MMSoundManagerTracks.Music, this.transform.position);
    }


    public void PlaySFX(string sfxName)
    {
        if (SFXSoundDic[sfxName] == null)
        {
            Debug.LogError("试图播放字典中不存在的音乐，请检查！");
            return;
        }
        MMSoundManagerSoundPlayEvent.Trigger(SFXSoundDic[sfxName], MMSoundManager.MMSoundManagerTracks.Sfx, this.transform.position);
    }

    public void PlayUISound(UIsfx uiEnum)
    {
        switch (uiEnum)
        {
            case UIsfx.Close:
                PlayUISoundAudioClip(Close);
                break;
            case UIsfx.Open:
                PlayUISoundAudioClip(Open);
                break;
            case UIsfx.Click:
                PlayUISoundAudioClip(Click);
                break;
        }
    }

    private void PlayUISoundAudioClip(List<AudioClip> _clips)
    {
        if(_clips.Count == 0)
        {
            Debug.LogError("试图播放空的声音列表，请检查！");
            return;
        }
        AudioClip _clip = _clips[Random.Range(0, _clips.Count)];
        MMSoundManagerSoundPlayEvent.Trigger(_clip, MMSoundManager.MMSoundManagerTracks.UI, this.transform.position);
    }

    public void LoadClipToDic(List<AudioClip> _clips,Dictionary<string,AudioClip> _targetDic)
    {
        foreach(var clip in _clips)
        {
            _targetDic[clip.name] = clip;
        }
    }
}

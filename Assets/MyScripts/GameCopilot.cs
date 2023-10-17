using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCopilot : MMSingleton<GameCopilot>
{
    [Header("Level")]
    public int WaveNow;//当前关卡
    public int WaveMax;//最大关卡


    //[Header("PlayerData")]
    public MeowStatus MeowStatus { get; private set; }

    [Header("Economic")]
    public int havestingInterest;
    public int havestCurrent;

    [Header("Cursor")]
    public Texture2D cursorTexture;


    //幸运值加成属性，供其他脚本调用
    public float LuckAdditionItemChance;



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
        WaveNow = 1; //TODO:修正？

        GenMeowEvent.GameStart += GameStart;

        GenMeowEvent.MainMenu += AutoDestroy;
    }

    public void MeowRegister(MeowStatus _meowStatus)
    {
        this.MeowStatus = _meowStatus;
        GenMeowInventoryManager.Instance._inventory.meowSO = _meowStatus.SO;
    }

    public void MeowQuit()
    {
        this.MeowStatus = null;
    }

    #region GameController
    public void GameStart()
    {
        MMSoundManager.Instance.UnmuteTrack(MMSoundManager.MMSoundManagerTracks.Sfx);
        MMSoundManager.Instance.UnmuteTrack(MMSoundManager.MMSoundManagerTracks.UI);
        MMSoundManager.Instance.UnmuteTrack(MMSoundManager.MMSoundManagerTracks.Music);
    }
    #endregion

    private void OnDisable()
    {
        GenMeowEvent.GameStart -= GameStart;
        GenMeowEvent.MainMenu -= AutoDestroy;
    }

    private void AutoDestroy()
    {
        Destroy(this);
    }
}

using DG.Tweening;
using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathSceneDemo : MonoBehaviour
{
    public Text Timer;

    public CanvasGroup CaiGroup;
    public CanvasGroup InfoGroup;

    public int _countDownNum;
    private void OnEnable()
    {
        FadeIn();
    }

    void FadeIn()
    {
        CaiGroup.alpha = 0;
        InfoGroup.alpha = 0;
        CaiGroup.DOFade(1, 2f).SetUpdate(true).SetEase(Ease.Linear).OnComplete(()=>StartCoroutine(CountDown()));
    }

    IEnumerator CountDown()
    {
        InfoGroup.alpha = 1;
        for (int i = 0; i < _countDownNum; i++)
        {
            Timer.text = (_countDownNum - i).ToString();
            yield return StartCoroutine(WaitForRealSeconds(1));
        }
        MMSceneLoadingManager.LoadScene("GenMeowMainMenu", "LoadingScreen");
        Debug.Log("游戏退出！");
    }

    public static IEnumerator WaitForRealSeconds(float time)
    {
        float start = Time.realtimeSinceStartup;

        while (Time.realtimeSinceStartup < start + time)
        {
            yield return null;
        }
    }
}

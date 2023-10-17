using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeowUISoundTrigger : MonoBehaviour
{
    public UIsfx uiSoundType;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(PlayUISound);
    }

    private void PlayUISound()
    {
        MeowSoundBase.Instance.PlayUISound(uiSoundType);
    }

}

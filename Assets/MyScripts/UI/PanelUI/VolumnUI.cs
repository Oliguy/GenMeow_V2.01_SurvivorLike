using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumnUI : MonoBehaviour
{
    public List<MMSoundManagerTrackVolumeSlider> sounds;

    IEnumerator Start()
    {
        if(MMSoundManager.Instance.settingsSo != null)
        {
            Debug.Log("找到MMSoundManager");
        }
        yield return new WaitForSeconds(3f);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class MolaUI : MonoBehaviour
{
    protected Text text;
    private void OnEnable()
    {
        text = GetComponent<Text>();
        SetMolaNum();
        GenMeowEvent.UpdateMola += SetMolaNum;
    }
    private void OnDisable()
    {
        GenMeowEvent.UpdateMola -= SetMolaNum;
    }

    public void SetMolaNum()
    {
        Debug.Log(GenMeowInventoryManager.Instance.CurrentMola);
        text.text = GenMeowInventoryManager.Instance.CurrentMola.ToString();
    }

}

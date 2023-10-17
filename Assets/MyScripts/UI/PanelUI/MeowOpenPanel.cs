using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeowOpenPanel : MonoBehaviour
{
    public GameObject PanelPrefab;
    public void OpenPanel()
    {
        Instantiate(PanelPrefab);
    }
}

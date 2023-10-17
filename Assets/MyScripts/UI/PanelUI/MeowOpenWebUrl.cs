using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeowOpenWebUrl : MonoBehaviour
{
    public enum UrlEnum
    {
        Ouyang,
        EntireFall,
        Zheng
    }

    public UrlEnum url;

    public void OpenWebUrl()
    {
        string _url = url switch
        {
            UrlEnum.Ouyang => "https://space.bilibili.com/7465986",
            UrlEnum.EntireFall => "https://space.bilibili.com/23360094",
            UrlEnum.Zheng => "https://space.bilibili.com/46384731",
            _ => "https://space.bilibili.com/401742377"//原神B站
        };
        Application.OpenURL(_url);
    }


}

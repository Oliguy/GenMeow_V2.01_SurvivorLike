using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlUIManager : MMSingleton<ControlUIManager>
{
    //TODO MARK: 如果UI之间嵌套，似乎会出问题！
    public PanelUI settingPanelPrefab;

    protected PanelUI _currentPanel;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("调出设置面板");
            if (this._currentPanel == null)
            {
                this._currentPanel = Instantiate(settingPanelPrefab,this.transform);
                Time.timeScale = 0f;
            }
        }
    }


}

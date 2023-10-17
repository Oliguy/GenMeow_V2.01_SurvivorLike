using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanelUI : MonoBehaviour, IPointerClickHandler
{
    public bool clickAnyPosToClose;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePanelBtn();
        }
    }
    public void ClosePanelBtn()
    {
        Destroy(gameObject);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickAnyPosToClose)
            ClosePanelBtn();
    }

    public void QuitGameBtn() {
        //TODO:可能需要保存数据
        Application.Quit();
    }

    public void SaveSettings()
    {
        Debug.Log("测试设置保存，请在实际游戏中完善！");

        ClosePanelBtn();
    }

}

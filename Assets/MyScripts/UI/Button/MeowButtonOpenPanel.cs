using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeowButtonOpenPanel : MonoBehaviour
{
    public GameObject objectToOpen;
    public Canvas canvas;
    public void OpenPanel()
    {
        if(objectToOpen == null)
        {
            Debug.LogError("需打开物体为空，请检查！");
            return;
        }
        if(canvas == null)
            Instantiate(objectToOpen);
        else
            Instantiate(objectToOpen,canvas.transform);

    }

}

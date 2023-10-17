using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_SetActive : MonoBehaviour
{
    public GameObject targetGameObject;
    public void OnClickSetActive()
    {
        targetGameObject.SetActive(!targetGameObject.activeInHierarchy);
    }

}

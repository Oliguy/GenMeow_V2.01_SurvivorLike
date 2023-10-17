using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeowUpdateSprite : MonoBehaviour
{
    private void Start()
    {

        if(CharacterInfoManager.Instance.Character.characterCombatSprite != null)
        {
            GetComponent<SpriteRenderer>().sprite = CharacterInfoManager.Instance.Character.characterCombatSprite;
        }
    }

}

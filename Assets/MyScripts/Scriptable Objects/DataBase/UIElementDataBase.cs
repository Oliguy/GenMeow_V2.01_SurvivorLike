using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UIElementDataBase_DB", menuName = "GenMeow/DataBase/UIElementDataBase")]
public class UIElementDataBase : ScriptableObject
{
    [Header("卡首稀有度颜色")]
    public Color color_CardHeadRarity_gray;
    public Color color_CardHeadRarity_green;
    public Color color_CardHeadRarity_blue;
    public Color color_CardHeadRarity_purple;
    public Color color_CardHeadRarity_gold;

    [Header("卡上部稀有度颜色")]
    public Color color_CardTopRarity_gray;
    public Color color_CardTopRarity_green;
    public Color color_CardTopRarity_blue;
    public Color color_CardTopRarity_purple;
    public Color color_CardTopRarity_gold;

    [Header("道具图标稀有度背景")]
    public Sprite sprite_ItemRarity_gray;
    public Sprite sprite_ItemRarity_green;
    public Sprite sprite_ItemRarity_blue;
    public Sprite sprite_ItemRarity_purple;
    public Sprite sprite_ItemRarity_gold;



}

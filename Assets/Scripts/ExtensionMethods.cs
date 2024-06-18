using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtentionMethods
{

    //I will be honest this one did fly over my head, but i belive this is to trigger other menu groups(other as in not the same as in "UiMenu")
    //I belive this way perhabs for the pause or Win/Lose screen
    public static void HideCanvasGroup(this CanvasGroup myCanvasGroup)
    {

        myCanvasGroup.alpha = 0f;
        myCanvasGroup.interactable = false;
        myCanvasGroup.blocksRaycasts = false;
    }

    public static void ShowCanvasGroup(this CanvasGroup myCanvasGroup)
    {

        myCanvasGroup.alpha = 1f;
        myCanvasGroup.interactable = true;
        myCanvasGroup.blocksRaycasts = true;
    }
}
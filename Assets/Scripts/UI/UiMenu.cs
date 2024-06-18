using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class UiMenu : MonoBehaviour
{

    //this script is incomplelty  because i wasnt able to keep up with the speed of the last lesson, because the last 30 mins were very rushed
    //i was not able to complet this scipt on my own bc every other day i worked on a diffrent deadline, and i somehow lost even more of the script? 
    //I am like a toddler trying to drive a car,,,i dont fully understand what i am doing most of the time but i think i may have accidantly just deleted some stuff and saved it
    //i then just deleted all the other "loose" script i had flying around to only this now, so atleast i know what i am looking at rn
    [SerializeField] private CanvasGroup panelMain;
    [SerializeField] private Button buttonNewGame;
    [SerializeField] private Button buttonLevelSelection;
    [SerializeField] private Button buttonBacktoMain;
    [SerializeField] private Button buttonExitGame;
    [SerializeField] private CanvasGroup panelLevelSelection;

    [SerializeField] private Transform levelsParent;
    [SerializeField] private GameObject prefabLevelButton;


    void Start()
    {
        //this is for the level selector, so they are able to be selected
        int i = 0;
        foreach (SceneReference levels in GameManager.instance.scenesLevel)
        {
            Button button = Instantiate(prefabLevelButton, levelsParent).GetComponent<Button>();
            button.GetComponentInChildren<TextMeshProGUI>().text = levels.Name;
            int currentIndex = i;
            button.onClick.AddListener(call: () =>
                {
                    GameManager.instance.LoadLevel(currentIndex);
                });
            i++;



        }
    }


    //these 2 ware meant for triggering the certain parts of the menu, Main menu and the levle selector, to show and hide when needed
    void ShowLevelSelection()
    {
        panelLevelSelection.HideCanvasGroup();
        panelMain.ShowCanvasGroup();

    }

   void ShowMain()
    {
        panelLevelSelection.HideCanvasGroup();
        panelMain.HideCanvasGroup();
    }
















}

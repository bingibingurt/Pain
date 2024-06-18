using System.Collections;
using System.Collections.Generic;
using Eflatun.SceneReference;
using UnityEngine.SceneManagement;
using UnityEngine;


public class GameManager : MonoBehaviour
{

    [SerializeField] private SceneReference sceneMenu;
    public SceneReference[] scenesLevel;
    public static GameManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this);
        
    }

    public void LoadMenu(int listIndex)
    {
        SceneManager.LoadScene(scenesLevel[listIndex].BuildIndex);
    }








}




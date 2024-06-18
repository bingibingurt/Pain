using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject panelLost;

    [SerializeField] private Button buttonTryAgian;

    //this is the UI menu works, making the cursor show so we can select the buttons and retry the level if we lose it
    void Start()
    {
        panelLost.SetActive(false);
        Time.timeScale = 1f;

        buttonTryAgian.onClick.AddListener(ReloadCurrentLevel);
    }

    public void ShowLostPanel()
    {
        Cursor.lockState = CursorLockMode.None;
        panelLost.SetActive(true);
        Time.timeScale = 0f;
        
    }

    void ReloadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}

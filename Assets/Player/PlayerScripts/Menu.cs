using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool GamePaused = false;
    public GameObject MenuUI;
      public GameObject PLayerUI;
    void Start()
    {
          
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            
            if(GamePaused)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                 PLayerUI.SetActive(false);
                Resume();
            }
            else
            {
                Pause();
            }
        }
        else
        {
           
        }
    }
    public void Resume()
    {
        MenuUI.SetActive(false);
        Time.timeScale=1f;
        GamePaused= false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
         PLayerUI.SetActive(true);
       
    }
    void Pause()
    {
        MenuUI.SetActive(true);
        Time.timeScale=0f;
        GamePaused= true;
         Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void quiteGame()
    {
        Application.Quit();

    }

}
     
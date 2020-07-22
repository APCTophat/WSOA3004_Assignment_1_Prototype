using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//functions for buttons and to quit game //Samantha Thurgood 1827593
public class Navigation : MonoBehaviour
{
    //opens game scene
    public void PlayBtn()
    {
        SceneManager.LoadScene("Play_Scene", LoadSceneMode.Single); //change to whatever the scene gets named
    }

    //return to menu from game scene
    public void ReturnBtn()
    {
        SceneManager.LoadScene("Menu_Scene", LoadSceneMode.Single); //change to whatever the scene gets named
    }

    //quits application
    public void Quit()
    {
        Application.Quit();
    }

    //press ESC to quit
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
    }
}

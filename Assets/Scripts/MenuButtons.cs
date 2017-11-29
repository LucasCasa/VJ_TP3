using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuButtons: MonoBehaviour
{
    void goToStart(){
        SceneManager.LoadScene("Test");
    }

    void goToSettings(){
        //SceneManager.LoadScene("settings");
    }

    void Quit(){
        Application.Quit();
    }
}

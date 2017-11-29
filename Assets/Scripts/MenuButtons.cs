using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuButtons: MonoBehaviour
{
    public void goToStart(){
        SceneManager.LoadScene("Test");
    }

    public void Quit(){
        Application.Quit();
    }
}

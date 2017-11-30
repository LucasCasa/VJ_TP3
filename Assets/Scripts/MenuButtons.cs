using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuButtons: MonoBehaviour
{
    public void goToStart(){
        SceneManager.LoadScene("Game");
    }

    public void Quit(){
        Application.Quit();
    }

	public void LoadGame() {
		SaveLoad.load = true;
		goToStart ();
	}
}

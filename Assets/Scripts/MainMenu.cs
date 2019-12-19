using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
        Debug.Log("start");
    }
  
    public void Menu()
    {
        SceneManager.LoadScene("Start");
    }
    public void Quit()
    {
        Debug.Log("quit!");
        Application.Quit();
    }

}

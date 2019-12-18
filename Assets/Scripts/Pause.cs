using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public bool GamePaused = false;
    public GameObject PauseM;//can

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
            if(GamePaused == false)
            {
                Time.timeScale = 1f;
                Cursor.visible = true;
                GamePaused = true;
                PauseM.SetActive(true);

            }
    }

}

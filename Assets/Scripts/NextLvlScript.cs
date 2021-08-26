using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLvlScript : MonoBehaviour
{
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;

    public int pera;

    private string nextScene = "Level";

    // Start is called before the first frame update
    void Start()
    {
       pera = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (pera == 3)
        {
            Player1Controller.Lvl = Player1Controller.Lvl + 1;
            if (Player1Controller.Lvl == 3) 
            {
                Player1Controller.Lvl = 1;
                SceneManager.LoadScene("MainMenu");
            }
            SceneManager.LoadScene("Level" + Player1Controller.Lvl);
        }

        if (Input.GetKeyDown(KeyCode.Escape) == true)
        {
            Player1Controller.Lvl = 1;
            SceneManager.LoadScene("MainMenu");
        }
        if (Input.GetKeyDown(KeyCode.R) == true) 
        {
            SceneManager.LoadScene("Level" + Player1Controller.Lvl);
        }

    }

    public void DodajPero()
    {
        pera++;
    }

}

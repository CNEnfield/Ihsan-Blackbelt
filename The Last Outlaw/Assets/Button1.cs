using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button1 : MonoBehaviour
{
    [SerializeField] private string newGameLevel = "Main Menu";
    // Start is called before the first frame update

    public void NewGameButton()
    {
        SceneManager.LoadScene(newGameLevel);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

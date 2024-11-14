using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button3 : MonoBehaviour
{
    [SerializeField] private string newGameLevel = "Controlls";

    public void NewGameButton()
    {
        SceneManager.LoadScene(newGameLevel);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

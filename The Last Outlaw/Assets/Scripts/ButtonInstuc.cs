using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonInstuc : MonoBehaviour
{
    [SerializeField] private string newGameLevel = "Instructions";
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

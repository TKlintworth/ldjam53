using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void StartGameButton(){
        //SceneManager.LoadScene("TheShallows");
        // Hide the button
        //gameObject.SetActive(false);
        // Tell the GameManager to start the game
        GameManager.Instance.StartGame();
        Debug.Log("Start Game Button Pressed");
    }
}

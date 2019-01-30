using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = ReInput.players.GetSystemPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetAnyButtonDown())
        {
            SceneManager.LoadScene(0);
        }
    }
}

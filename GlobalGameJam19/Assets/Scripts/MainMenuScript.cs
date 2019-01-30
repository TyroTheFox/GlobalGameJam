using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Rewired;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public GameObject bedroom;
    public GameObject bridge;
    [Space(20)] 
    public StandaloneInputModule standaloneInputModule;
    [Space(20)]
    private bool startGame = false;
    public float totalSeconds;
    private float currentSeconds;
    
    public Transform alertLight;
    private int alertCount;
    public int alertTillMoveCamera;
    
    public Transform bedroomCamera;
    public Transform cameraMoveToPoint;
    public SpriteRenderer blackoutSpot;
    
    public float pushStartTotalSeconds;
    private float pushStartCurrentSeconds;
    public Text pushStartText;
    
    private Player player;

    [Space(20)] public AudioSource alarm;
    
    // Start is called before the first frame update
    void Start()
    {
        player = ReInput.players.GetSystemPlayer();
        currentSeconds = totalSeconds;
        alarm.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetAnyButtonDown())
        {
            startGame = true;
        }

        if (startGame)
        {
            pushStartText.enabled = false;
            currentSeconds -= Time.deltaTime;  
            if (currentSeconds < 0)
            {
                alarm.Stop();
                alertLight.gameObject.SetActive(!alertLight.gameObject.activeSelf);
                currentSeconds = totalSeconds;
                alertCount++;
                if(alertLight.gameObject.activeSelf)
                alarm.Play();
            }

            if (alertCount > alertTillMoveCamera)
            {
                blackoutSpot.color = Color.Lerp(blackoutSpot.color, Color.black, Time.deltaTime * 6.0f);
                bedroomCamera.position = Vector3.Lerp(bedroomCamera.position, cameraMoveToPoint.position, Time.deltaTime * 1.0f);
                if (blackoutSpot.color == Color.black)
                {
                    alarm.Stop();
                    standaloneInputModule.enabled = true;
                    bedroom.SetActive(false);
                    bridge.SetActive(true);
                }
            }
        }
        else
        {
            pushStartCurrentSeconds -= Time.deltaTime;  
            if (pushStartCurrentSeconds < 0)
            {
                pushStartText.enabled = !pushStartText.enabled;
                pushStartCurrentSeconds = pushStartTotalSeconds; 
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Animator OTTO;

    public GameObject Boss;

    private Health bossHealth;
    
    public float totalSeconds;
    private float currentSeconds;
    
    // Start is called before the first frame update
    void Start()
    {
        bossHealth = Boss.GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bossHealth.HP <= 0)
        {
            OTTO.SetBool("Victory", true);
            currentSeconds -= Time.deltaTime;
            if (currentSeconds < 0)
            {
                SceneManager.LoadScene(2);
            }
        }
        else
        {
            OTTO.SetBool("Victory", false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour
{

    public GameObject player;
    public GameObject pickup;
    public GameObject enemies;
    public GameObject posNodes;
    public GameObject zoneNodes;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    /*
    void buildLevel()
    {
        Instantiate(player, new Vector3(-10,0,0), Quaternion.identity);

        Instantiate(enemies, new Vector3(5, 0, 0), Quaternion.identity);

        Instantiate(zoneNodes, new Vector3(5, 0, 0), Quaternion.identity);
    }*/

    void resetLevel()
    {
    }
}

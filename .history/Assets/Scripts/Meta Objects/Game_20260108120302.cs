using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public enum debugMode { ON, OFF }

public enum gameState { PLAYING, PAUSED, OVER }

public class Game : MonoBehaviour
{
    //STATISTICS
    private debugMode debug = debugMode.OFF;
    private gameState state;
    [SerializeField] private Transform playerSpawnTransform;
    public float timer = 0.0f;
    public int hitsTaken = 0;

    //META OBJECTS
    public SceneManager SM;
    public CameraController CC;
    public PlayerHealthManager PHM;
    public TimerRenderer TR;

    //PREFABS AND INSTANCES
    public GameObject playerCharacterPrefab;
    public GameObject playerCharacter;
    public GameObject volleyOrbPrefab;
    public GameObject ceilingProjectilePrefab;
    public GameObject handWavePrefab;
    public GameObject bodyPillarPrefab;
    public GameObject minionPrefab;
    public GameObject specimen9Prefab;
    
    private Player playerScript;
    //private GameObject pauseScreen;

    void Start()
    {
        CC = GameObject.Find("Main_Camera").GetComponent<CameraController>();

        SpawnPlayer();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(debug == debugMode.OFF) 
            { 
                debug = debugMode.ON;
                Debug.Log("Debug Mode ON");
            }
            else if (debug == debugMode.ON)
            { 
                debug = debugMode.OFF; 
                Debug.Log("Debug Mode OFF");
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Instantiate(volleyOrbPrefab, new Vector3(10, 0, -2), Quaternion.identity);
        }

        if(Input.GetKeyDown(KeyCode.W))
        {
            Instantiate(ceilingProjectilePrefab, new Vector3(playerCharacter.transform.position.x, 6, -2), Quaternion.identity);
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(minionPrefab, new Vector3(10, playerCharacter.transform.position.y, -2), Quaternion.identity);
        }

        if(state == gameState.PLAYING)
        {
            timer += Time.deltaTime;
        }
    }

    private void SpawnPlayer()
    {
        playerCharacter = Instantiate(playerCharacterPrefab, playerSpawnTransform.position, Quaternion.identity);
        playerScript = playerCharacter.GetComponent<Player>();
        CC.target = playerCharacter.transform;
    }
}

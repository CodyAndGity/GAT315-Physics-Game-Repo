using CGL.DesignPatterns;
using System.Collections;
using System.Diagnostics;
using Unity.Cinemachine;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] Transform playerRespawnPoint;
    [SerializeField] GameObject playerRespawnPointStart;

    bool playerHasWon= false;
    [SerializeField] GameObject winText;
    [SerializeField] GameObject deathCountText;
    [SerializeField] GameObject startPanel;
    [SerializeField] bool debug = false;

    bool respawnPlayer = false;
    GameObject player;
    public float deathCount = 0;
    public float respawnCount = 0;
    public void FillMissingFields()
    {
        if (playerRespawnPointStart == null)
        {
    
            playerRespawnPointStart = GameObject.FindGameObjectWithTag("CheckPoint");
        }

            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
        }
        if (playerRespawnPoint==null)
        {
            playerRespawnPoint = playerRespawnPointStart.transform;

        }
        if(startPanel == null)
        {
            startPanel = GameObject.FindGameObjectWithTag("StartPanel");
        }
        if(winText == null)
        {
            winText = GameObject.FindGameObjectWithTag("WinText");
        }
        if(deathCountText == null)
        {
            deathCountText = GameObject.FindGameObjectWithTag("DeathCountText");
        }
    }
    public void OnGameStart()
    {
       startPanel.SetActive(false);
        Time.timeScale = 1f;

    }
    public void SetPlayerRespawnPoint(Vector3 newRespawnPoint)
    {
        playerRespawnPoint.position = newRespawnPoint;
    }

    public void TriggerPlayerRespawn()
    {
        deathCount++;
        respawnPlayer = true;
    }
    public void PlayerHasWon()
    {
        //playerHasWon = true;
        OnGameWin();
        //winText.gameObject.SetActive(true);
        // Additional logic for winning the game can be added here (e.g., show win screen, stop player movement, etc.)
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        deathCount = 0;
        respawnCount = 0;
        FillMissingFields();
        Time.timeScale = (debug) ? 1f : 0f;
        playerHasWon = false;
        winText.SetActive(false);

        //player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            UnityEngine.Debug.LogError("Player object not found in the scene. Please ensure there is a GameObject tagged 'Player'.");
        }
        playerRespawnPoint = new GameObject().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHasWon)
        {
            Start();
        }

        if (winText == null)
        {
            winText = GameObject.FindGameObjectWithTag("WinText");
        }
        if(deathCountText == null)
        {
            deathCountText = GameObject.FindGameObjectWithTag("DeathCountText");
        }
        deathCountText.GetComponent<TMPro.TextMeshProUGUI>().text = "Deaths: " + deathCount.ToString();
        /*
        //if(playerHasWon) winText.gameObject.SetActive(false);
        if(winText == null)
        {
            //Find text with tag
            var test = GameObject.FindGameObjectWithTag("Text");
            if (test ==null)
            {
                test=GameObject.Find("WinText");
            }
                winText=test.GetComponent<TMPro.TextMeshProUGUI>();
        }
        */
        if (player == null)
        {
            //Find player with tag
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if(playerRespawnPoint == null)
        {
            //Find with tag
            playerRespawnPoint = playerRespawnPointStart.transform;
        }
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            //if (!playerHasWon)
            //{
                RespawnPlayer();

            //}
            //else
            //{
                // If the player has won, pressing R will reset the game state
                //playerHasWon = false;
                //winText.gameObject.SetActive(false);
                ////SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                //playerRespawnPoint = playerRespawnPointStart.transform;

                //RespawnPlayer();
            //}

        }
        else if (respawnPlayer)
        {
            RespawnPlayer();
        }
    }


    private void RespawnPlayer()
    {
        respawnCount++;
        // reset player position to respawn point
        player.transform.position = playerRespawnPoint.position;
        player.transform.rotation = Quaternion.identity;
        player.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        respawnPlayer = false;
    }

    public void OnGameWin()
    {
        winText.SetActive(true);
        winText.GetComponent<TMPro.TextMeshProUGUI>().text = "You Win!"+$"\n\n You've respawned {respawnCount} time{(respawnCount == 1 ? "" : "s")}.";
        StartCoroutine(ResetGameCR(2f));
    }
    //public void OnGameOver()
    //{
    //    deathText.gameObject.SetActive(true);
    //    StartCoroutine(ResetGameCR(2f));
    //}

    IEnumerator ResetGameCR(float time)
    {
        yield return new WaitForSeconds(time);
        playerHasWon = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

   
}

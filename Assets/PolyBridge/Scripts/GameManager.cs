using CGL.DesignPatterns;
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
    [SerializeField] TMPro.TextMeshProUGUI winText;

    bool respawnPlayer = false;
    GameObject player;
    public void SetPlayerRespawnPoint(Vector3 newRespawnPoint)
    {
        playerRespawnPoint.position = newRespawnPoint;
    }

    public void TriggerPlayerRespawn()
    {
        respawnPlayer = true;
    }
    public void PlayerHasWon()
    {
        playerHasWon = true;
        winText.gameObject.SetActive(true);
        // Additional logic for winning the game can be added here (e.g., show win screen, stop player movement, etc.)
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        winText.gameObject.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player object not found in the scene. Please ensure there is a GameObject tagged 'Player'.");
        }
        playerRespawnPoint = new GameObject().transform;
    }

    // Update is called once per frame
    void Update()
    {
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
        if(player == null)
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
            if (!playerHasWon)
            {
                RespawnPlayer();

            }
            else
            {
                // If the player has won, pressing R will reset the game state
                playerHasWon = false;
                winText.gameObject.SetActive(false);
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                playerRespawnPoint = playerRespawnPointStart.transform;

                RespawnPlayer();
            }

        }
        else if (respawnPlayer)
        {
            RespawnPlayer();
        }
    }


    private void RespawnPlayer()
    {
        // reset player position to respawn point
        player.transform.position = playerRespawnPoint.position;
        player.transform.rotation = Quaternion.identity;
        player.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        respawnPlayer = false;
    }
}

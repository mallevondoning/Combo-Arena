using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    public PlayerStatus PlayerStatus;
    public GameObject Player { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Player = FindPlayer();
    }

    private GameObject FindPlayer()
    {
        PlayerController[] playerList = FindObjectsByType<PlayerController>(FindObjectsSortMode.InstanceID);
        if (playerList.Length > 1)
        {
            Debug.LogError("There are more than one player in this scene");
            return null;
        }
        else if (playerList.Length == 0)
        {
            Debug.LogError("There are no players in this scene");
            return null;
        }
        else return playerList[0].gameObject;
    }
}

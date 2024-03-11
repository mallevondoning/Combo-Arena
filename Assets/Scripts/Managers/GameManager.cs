using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    public PlayerStatus PlayerStatus;

    private void Awake()
    {
        Instance = this;
    }
}

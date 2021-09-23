<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;

    public Transform playerPrefab;
    public Transform spawnPoint;

    //public List<Transform> checkpoints = new List<Transform>();

    public Vector3 spawnPosition; //{get {return checkpoints[checkpoints.Count -1].position; } }
    //public CheckPoint curCheckpoint {get { return checkpoints[checkpoints.Count - 1].GetComponent<CheckPoint>();}}

    public float spawnPointReset = 0;

    void Awake()
    {
        if(gm == null)
        {
            gm = this;
            //checkpoints.Add(spawnPoint);
        }

        spawnPosition = new Vector3 (-6.5f, 21.1f, 0f);
        playerPrefab.position = spawnPosition;
    }
    
    public static void KillPlayer(PlayerState player)
    {
        gm.RespawnPlayer();
    }

    public void RespawnPlayer()
    {
        playerPrefab.transform.position = spawnPosition;
    }
}
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;

    public Transform playerPrefab;
    public Transform spawnPoint;

    public List<Transform> checkpoints = new List<Transform>();

    public Vector3 spawnPosition {get {return checkpoints[checkpoints.Count -1].position; } }
    public CheckPoint curCheckpoint {get { return checkpoints[checkpoints.Count - 1].GetComponent<CheckPoint>();}}

    public float spawnPointReset = 0;

    void Awake()
    {
        if(gm == null)
        {
            gm = this;
            checkpoints.Add(spawnPoint);
        }
    }
    
    public static void KillPlayer(PlayerState player)
    {
        gm.RespawnPlayer();
    }

    public void RespawnPlayer()
    {
        playerPrefab.transform.position = spawnPosition;
    }
}
>>>>>>> main

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour {
    [SerializeField] private Transform players;
    [SerializeField] private List<Transform> playerList;
    
    // Start is called before the first frame update
    void Start() {
        FillPlayerList();
    }

    void FillPlayerList() {
        playerList = new List<Transform>();
        foreach (Transform player in players) {
            playerList.Add(player);
        }
    }
}

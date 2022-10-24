using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragGrid : MonoBehaviour {
    [SerializeField] private Vector2 GridSize = new Vector2(1,1);
    [SerializeField] private int CellSize = 10;

    [SerializeField] private GameObject Cell;
    // Start is called before the first frame update
    void Start() {
        int sizeX = (int) GridSize.x;
        int sizeY = (int) GridSize.y;
        for (int y = 0; y < sizeY; y++) {
            for (int x = 0; x < sizeX; x++) {
                var posX = x * CellSize;
                var posY = y * CellSize;
                Instantiate(Cell, new Vector3(posX, 0, posY),Quaternion.identity, transform);
            }
        }
    }
}

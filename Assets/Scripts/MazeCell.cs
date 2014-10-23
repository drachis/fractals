using UnityEngine;
using System.Collections;

public class MazeCell : MonoBehaviour {

    public int sizeX, sizeZ;
    
    public MazeCell cellPrefab;

    public IntVector2 coordinates;
    
    private MazeCell[,] cells;   
}

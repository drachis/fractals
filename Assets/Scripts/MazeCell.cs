using UnityEngine;
using System.Collections;

public class MazeCell : MonoBehaviour {

    public int sizeX, sizeZ;    
    public MazeCell cellPrefab;
    public IntVector2 coordinates;
    
    private MazeCell[,] cells;
    private MazeCellEdge[] edges = new MazeCellEdge[MazeDirections.Count];

    public MazeCellEdge GetEdge (MazeDirection direction) {
    	return edges[(int)direction];
    }
    public void SetEdge (MazeDirection direction, MazeCellEdge edge) {
    	edges[(int)direction] = edge;
    }
}

﻿using UnityEngine;
using System.Collections;

public class MazeCell : MonoBehaviour {

    public int sizeX, sizeZ;    
    public MazeCell cellPrefab;
    public IntVector2 coordinates;
    
    private MazeCell[,] cells;
    private MazeCellEdge[] edges = new MazeCellEdge[MazeDirections.Count];
    private int initializedEdgeCount;

    public bool IsFullyInitialized {
        get { 
            return initializedEdgeCount == MazeDirections.Count;
        }
    }

    public void SetEdge (MazeDirection direction, MazeCellEdge edge) {
        edges[(int)direction] = edge;
        initializedEdgeCount += 1;
    }
    
    public MazeCellEdge GetEdge (MazeDirection direction) {
    	return edges[(int)direction];
    }

    public MazeDirection RandomUninitializedDirection {
        get {
            int skips = Random.Range(0, MazeDirections.Count - initializedEdgeCount);
            for (int i = 0; i < MazeDirections.Count; i++) {
                if (edges[i] == null) {
                    if (skips == 0) {
                        return (MazeDirection)i;
                    }
                    skips -= 1;
                }
            }
            throw new System.InvalidOperationException(  
                "MazeCell has no uninitialized directions left.");
        }
    }
}

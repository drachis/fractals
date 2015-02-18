using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    
    public Maze mazePrefab;
    
    private Maze mazeInstance;
    

    
	void Start () {
		BeginGame();	
	}
		
	void Update () {
		if (Input.anyKeyDown){//(Input.GetKeyDown(KeyCode.Space)) {
			RestartGame();
		}
	}
    
    private void BeginGame () {
        mazeInstance = Instantiate (mazePrefab) as Maze;
        StartCoroutine(mazeInstance.Generate());
    }
    
    private void RestartGame(){
    	Debug.Log("reset");
    	StopAllCoroutines();
        Destroy (mazeInstance.gameObject);
        BeginGame();
    }
}

using UnityEngine;
using System.Collections;

public class Fractal : MonoBehaviour {
    public Mesh mesh;
    public Material material;
    public int maxDepth;


    public float childScale;

    private int depth;
    private Material[] materials;
    private static Vector3[] childDirections = {
        Vector3.up,
        Vector3.right,
        Vector3.left,
        Vector3.forward,
        Vector3.back
    };
    private static Quaternion[] childOrientations = {
        Quaternion.identity, 
        Quaternion.Euler(0f,  0f, -90f),
        Quaternion.Euler(0f, 0f, 90f),
        Quaternion.Euler(90f, 0f, 0f),
        Quaternion.Euler(-90f, 0f, 0f)
    };

    // Use this for initialization
    void Start () {
        if ( materials == null ) {
            InitializeMaterials();
        }
        gameObject.AddComponent<MeshFilter>().mesh = mesh;
        gameObject.AddComponent<MeshRenderer>().material = materials[depth];
        if (depth < maxDepth) {
            StartCoroutine (CreateChildren());
        }
    }

    private void InitializeMaterials () {
        materials = new Material[maxDepth + 1];
        for (int i = 0; i <= maxDepth; i ++) {
            materials[i] = new Material(material);
            materials[i].color = Color.Lerp(
                Color.yellow,
                Color.magenta,
                Mathf.Pow((float)i/maxDepth,2f)
                );
        }
    }
    
    private IEnumerator CreateChildren () {
        for(int i = 0; i < childOrientations.Length; i++) {
            yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
            new GameObject ("Fractal Child").AddComponent<Fractal> ().Initialize (this, i);
        }
    }

    private void Initialize (Fractal parent, int childIndex) {
        mesh = parent.mesh;
        material = parent.material;
        maxDepth = parent.maxDepth;
        depth = parent.depth + 1;
        childScale = parent.childScale;
        transform.parent = parent.transform;
        transform.localScale = Vector3.one * childScale;
        transform.localPosition = childDirections[childIndex] * (0.5f + 0.5f * childScale);
        transform.localRotation = childOrientations[childIndex];
    }


    // Update is called once per frame
    void Update () {
        
    }
}

using UnityEngine;

public class MinimapArrow : MonoBehaviour
{
    private Transform car;
    private GameObject arrowObj;

    [SerializeField] private Material arrowMaterial; // assign in Inspector!

    void Start()
    {
        // Find car by tag - more reliable than Rigidbody search
        GameObject carObj = GameObject.FindWithTag("Player");
        if (carObj != null)
            car = carObj.transform;
        else
        {
            Rigidbody rb = FindObjectOfType<Rigidbody>();
            if (rb != null) car = rb.transform;
        }

        if (car == null)
            Debug.LogError("No car found for MinimapArrow!");

        arrowObj = new GameObject("ArrowMesh");
        arrowObj.transform.SetParent(transform);

        MeshFilter mf = arrowObj.AddComponent<MeshFilter>();
        MeshRenderer mr = arrowObj.AddComponent<MeshRenderer>();

        // Use assigned material instead of Shader.Find
        if (arrowMaterial != null)
            mr.material = arrowMaterial;
        else
            Debug.LogError("Arrow material not assigned!");

        mr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        mr.receiveShadows = false;

        Mesh mesh = new Mesh();
        mesh.vertices = new Vector3[]
        {
        new Vector3( 0f,   0, -2.5f),   
        new Vector3( 1.5f, 0,  2.0f),  
        new Vector3(-1.5f, 0,  2.0f),   
        };
        mesh.triangles = new int[] { 0, 2, 1 };
        mesh.RecalculateNormals();
        mf.mesh = mesh;

        arrowObj.transform.localPosition = Vector3.zero;
        arrowObj.transform.localScale = Vector3.one * 5f;
        arrowObj.layer = LayerMask.NameToLayer("Minimap");
    }

    void LateUpdate()
    {
        if (car == null || arrowObj == null) return;

        arrowObj.transform.position = new Vector3(
            car.position.x,
            car.position.y + 5f,
            car.position.z
        );
        arrowObj.transform.rotation = Quaternion.Euler(0f, car.eulerAngles.y, 0f);
    }
}

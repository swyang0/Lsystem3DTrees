using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


//[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class BranchMesh
{
    // Start is called before the first frame update
    public int numfaces;
    public Vector3 start;
    public Vector3 end;
    public float startRadius;
    public float endRadius;

    private Mesh mesh = new Mesh();
    private MeshRenderer rend;
    private Vector3[] meshVetices;
    private int[] triangles;

    private void Awake()
    {
        //mesh = GetComponent<MeshFilter>().mesh;
    }

    public Mesh create(int numfaces, Vector3 start, Vector3 end, float startRadius, float endRadius)
    {
        this.numfaces = numfaces;
        this.start = start;
        this.end = end;
        this.startRadius = startRadius;
        this.endRadius = endRadius;
        //mesh = GetComponent<MeshFilter>().mesh;
        //StartGenerate();
        GenerateCylinder();
        return mesh;
        
    }


    public void Start()
    {
        makeMeshData();
        //printMeshData();
        generateMesh();
    }

    public void GenerateCylinder()
    {
        makeMeshData();
        //printMeshData();
        generateMesh();

        //return GetComponent<MeshFilter>();

        // generate meshes
    }

    void makeMeshData()
    {
        float angle = 2 * Mathf.PI / numfaces;


        List<Vector3> vertices = new List<Vector3>();

        // calculate vertices
        for (int i = 0; i < numfaces; i++)
        {
            // caculate base floor vertext coord
            Vector3 v = calculateVertex(start, i * angle, startRadius);
            vertices.Add(v);

            // calculate end floor vertext coord
            v = calculateVertex(end, i * angle, endRadius);
            vertices.Add(v);
        }

        meshVetices = vertices.ToArray();

        List<int> tri = new List<int>();
        int index = 0;
        // set up triangles
        for (int i = 0; i < numfaces;i++)
        {

            List<int> temp = calculateFace(index, numfaces);
            tri.AddRange(temp);
            index += 2;
        }

        triangles = tri.ToArray();
    }

       void generateMesh()
    {
        mesh.vertices = meshVetices;
        mesh.triangles = triangles;
        //mesh.RecalculateNormals();
        
        //GetComponent<MeshRenderer>().material = new Material(Shader.Find("Diffuse")); 

    }

    public List<int> calculateFace(int index, int numfaces)
    {
        numfaces = 2 * numfaces;
        int pos0 = index;
        int pos1 = (index + 1) % numfaces;
        int pos2 = (index + 2) % numfaces;
        int pos3 = (index + 3) % numfaces;
        return new List<int> { pos0, pos1, pos2, pos2, pos1, pos3 };
    }

    //public List<int> calculateTop(int numfaces)

    public Vector3 calculateVertex(Vector3 pivot, float angle, float radius)
    {
        return pivot + new Vector3(radius * Mathf.Cos(angle), pivot.y, radius * Mathf.Sin(angle));
    }

    void printMeshData()
    {
        string str = "Mesh Vertices: \n";
        foreach (var vertice in meshVetices)
        {
            str += $"{vertice}\n";
        }

        str += "triangles: \n";
        foreach (var tri in triangles)
        {
            str += $"{tri}\n";
        }

        //print(str);
    }
}

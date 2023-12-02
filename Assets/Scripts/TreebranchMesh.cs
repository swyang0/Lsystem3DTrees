using System.Collections;
using System.Collections.Generic;
using Unity.Android.Types;
using UnityEngine;

/*
public class TreebranchMesh
{
    // a list used to hold the filters
    private List<MeshFilter> meshFilters;

    // the vertices of the tree branches
    private List<Vertice> nodes;

    private GameObject tree;

    private MeshFilter meshFilter;

    private MeshRenderer meshRenderer;

    private int numfaces;

    private List<Vector3> meshVertices;
    private int[] triangles;

    private float startRadius = 0.5f;
    private float endRadius = 0.5f;

    private List<Mesh> meshList;

    public TreebranchMesh(List<Vertice> inNodes, int inNumfaces)
    {
        nodes = inNodes;
        numfaces = inNumfaces;
        tree = new GameObject();
        meshFilter = tree.AddComponent<MeshFilter>();
        meshRenderer = tree.AddComponent<MeshRenderer>();
        meshRenderer.material = new Material(Shader.Find("Diffuse"));
        meshList = new List<Mesh>();
    }

    public GameObject getTree()
    {
        return tree;
    }

    public GameObject createTreeBranches()
    {
        for (int i = 0; i < nodes.Count; i+=2)
        {
            generateCylinder(nodes[i], nodes[i + 1]);
        }

        CombineInstance[] combine = new CombineInstance[meshList.Count];

        for (int i = 0;i < meshList.Count;i++ )
        {
            combine[i].mesh = meshList[i];
        }

        Mesh mesh = new Mesh();
        mesh.CombineMeshes(combine);
        meshFilter.sharedMesh = mesh;

        return getTree();
    }

    public void generateCylinder(Vertice start, Vertice end) {

        Mesh mesh = new Mesh();
        float angle = 2 * Mathf.PI / numfaces;

        // set up mesh vertices
        List<Vector3> vertices = new List<Vector3>();

        // calculate vertices
        for (int i = 0; i < numfaces; i++)
        {
            // caculate base floor vertext coord
            Vector3 v = calculateVertex(start.pos, i * angle, startRadius);
            vertices.Add(v);

            // calculate end floor vertext coord
            v = calculateVertex(end.pos, i * angle, endRadius);
            vertices.Add(v);
        }

        mesh.vertices = vertices.ToArray();

        // set up triangels
        List<int> tri = new List<int>();
        int index = 0;
        // set up triangles
        for (int i = 0; i < numfaces; i++)
        {

            List<int> temp = calculateFace(index, numfaces);
            tri.AddRange(temp);
            index += 2;
        }
        mesh.triangles = tri.ToArray();
        mesh.RecalculateNormals();
        meshList.Add(mesh);
    }

    public Vector3 calculateVertex(Vector3 pivot, float angle, float radius)
    {
        return pivot + new Vector3(radius * Mathf.Cos(angle), pivot.y, radius * Mathf.Sin(angle));
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



}
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreebranchMesh
{
    // a list used to hold the filters
    private List<MeshFilter> meshFilters;

    // the vertices of the tree branches
    private List<Vertice> nodes;

    private GameObject tree;

    private MeshFilter meshFilter;

    private MeshRenderer meshRenderer;

    public TreebranchMesh(List<Vertice> inNodes) {
        nodes = inNodes;
        tree = new GameObject();
        meshFilter = tree.AddComponent<MeshFilter>();
        meshRenderer = tree.AddComponent<MeshRenderer>();
        meshRenderer.material = new Material(Shader.Find("Diffuse"));
    }

    public GameObject getTree()
    {
        return tree;
    }

    public GameObject createTreeBranches()
    {
        for (int i = 0; i < nodes.Count; i+=2)
        {
            generateCylinder(nodes[i].pos, nodes[i + 1].pos);
        }
        return getTree();
    }

    public void generateCylinder(Vector3 start, Vector3 end) {
        
    }



}

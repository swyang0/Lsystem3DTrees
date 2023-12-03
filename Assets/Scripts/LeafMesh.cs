using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafMesh
{
    Vertice leaf;
    float scale;


    public LeafMesh(Vertice inLeaf, float inScale)
    {
        leaf = inLeaf;
        scale = inScale;
    }

    public Mesh createLeaf()
    {
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[3];
        // front 
        vertices[0] = leaf.pos + new Vector3(0, 0, 0) * scale;
        vertices[1] = leaf.pos + new Vector3(0, 0, 1) * scale;
        vertices[2] = leaf.pos + new Vector3(1, 0, 1) * scale;

        /*
        vertices[3] = leaf.pos + new Vector3(0, 0, 0) * scale;
        vertices[4] = leaf.pos + new Vector3(0, 0, 1) * scale;
        vertices[5] = leaf.pos + new Vector3(1, 0, 1) * scale;
        */
        mesh.vertices = vertices;
        mesh.triangles = new int[]
        {
            0,1,2,
            0,2,1,
        };
        //back
        /*
        vertices[3] = leaf.pos + new Vector3(0, 0, 0) * scale;
        vertices[4] = leaf.pos + new Vector3(0, 0.1f, 1) * scale;
        vertices[5] = leaf.pos + new Vector3(1, 0.1f, 1) * scale;
        mesh.vertices = vertices;

        mesh.triangles = new int[] { 
            0,3,2,2,3,5,
            1,4,0,0,4,3,
            2,5,1,1,5,4,
            3,4,5,
            0,2,1
        };
        */

        return mesh;
    }
}




using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Vertice
{
    Vector3 pos;
    Vector3 dir;

    public Vertice() {
        pos = new Vector3(0, 0, 0);
        dir = new Vector3(0, 0.3f, 0);

    } 

    public void setVert(Vector3 inPos, Vector3 inDir)
    {
        pos = inPos;
        dir = inDir;

    }

    
    public Vector3 getPos()
    {
        return pos;
    }

    public Vector3 getDir()
    {
         return dir;
    }

    public Vertice Clone()
    {
        return (Vertice)this.MemberwiseClone();
    }

    public void toString()
    {
        Debug.Log($"pos: {pos.ToString()}\n dir:{dir.ToString()}");
    }
}

using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Vertice
{
    public Vector3 pos;
    public Vector3 dir;


    public Vertice() {
        pos = new Vector3(0, 0, 0);
        dir = new Vector3(0, 0, 0);
    } 

    public Vertice(Vector3 pos, Vector3 dir)
    {
        this.pos = pos;
        this.dir = dir;
    }

    public void setVert(Vector3 inPos, Vector3 inDir)
    {
        pos = inPos;
        dir = inDir;

    }

    public void setPos(Vector3 inPos)
    {
        pos = inPos;
    }

    public void setDir(Vector3 inDir)
    {
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

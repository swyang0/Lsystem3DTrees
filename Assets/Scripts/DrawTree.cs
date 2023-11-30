using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class DrawTree : MonoBehaviour
{

    [SerializeField]
    private Lsystem lsystem;

    [SerializeField]
    [Range(-100, 100)]
    private float angle;

    [SerializeField]
    private List<Rule> rules;

    [SerializeField]
    private char axiom;

    private LineRenderer lr;


    private void Awake()
    {
    }

    public void generate()
    {
        //printRule();
        
        lsystem = new Lsystem(rules, axiom);
        lr = GetComponent<LineRenderer>();
        growTree();
        
    }

    public void growTree()
    {
        lsystem.generate();
        clearVerts();
        addVerts(lsystem.getCurrentStr());
        
    }

    public void addVerts(string str)
    {
        print($"-----------{lsystem.iterations}---- ----");
        Stack<Vertice> stk = new Stack<Vertice>();

        List<Vector3> vertices = new List<Vector3>();
        Vertice vert = new Vertice();

        Vector3 curpos = vert.getPos();
        Vector3 curdir = vert.getDir();


        print(str);

        foreach (char c in str)
        {

            /*
            string s = "";
            s += c;
            s += "\n";
            */
            Matrix4x4 mat;
            switch (c)
            {
                case 'F':
                    vertices.Add(curpos);
                    //s += $"start{curpos},";
                    curpos += curdir;
                    //print("currentpos: " + curpos);
                    vertices.Add(curpos);
                    //s += $"end{curpos}\n";
                    //printVerts();
                    break;

                case '+': // clockwise rotate around x axis
                    mat = getRollMat(angle);
                    curdir = Vector3.Normalize(mat * curdir);
                    //Debug.Log("+ :" + curdir);
                    break;

                case '-': // ccw rotate around x axis
                    mat = getRollMat(-angle);
                    curdir = Vector3.Normalize(mat * curdir);
                    //Debug.Log("- :" + curdir);
                    break;

                case '\\': //clockwise rotate around y axis
                    mat = getYawMat(angle);
                    curdir = Vector3.Normalize(mat * curdir);
                    //Debug.Log("- :" + curdir);
                    break;

                case '/': // ccw rotate around y axis
                    mat = getYawMat(-angle);
                    curdir = Vector3.Normalize(mat * curdir);
                    //Debug.Log("- :" + curdir);
                    break;

                case '{': //clockwise rotate around z axis
                    mat = getPitchMat(angle);
                    curdir = Vector3.Normalize(mat * curdir);
                    //Debug.Log("- :" + curdir);
                    break;

                case '}': // ccw rotate around z axis
                    mat = getPitchMat(-angle);
                    curdir = Vector3.Normalize(mat * curdir);
                    //Debug.Log("- :" + curdir);
                    break;

                case '[':
                    vert.setVert(curpos, curdir);
                    stk.Push(vert.Clone());
                    //s += $"push vert:{vert.getPos()}\n";
                    //printvert(vert);
                    break;

                 case ']':
                    vert = stk.Pop();
                    curpos = vert.getPos(); 
                    curdir = vert.getDir();
                    //s += $"pop vert:{curpos}\n";
                    //printvert(vert);
                    break;
                

            }

        }


        //printVerts();
        drawTree(vertices);

    }

    public void printcurrentpos(Vector3 curpos, Vector3 curdir)
    {
        string strstr = "";
        strstr += $"pos:{curpos}";
        strstr += '\n';
        strstr += $"dir:{curdir}";
        print(strstr);
    }

    public void drawTree(List<Vector3> vertices) {
        for (int i = 0; i < vertices.Count; i+=2)
        {
            drawLine(vertices[i], vertices[i + 1]);
        }

    } 

    public void drawLine(Vector3 start, Vector3 end)
    {
        GameObject obj = new GameObject();
        obj.transform.parent = transform;
        LineRenderer temp = obj.AddComponent<LineRenderer>();
        Vector3[] arr = new[] {start, end};
        temp.material = lr.sharedMaterial;
        temp.startWidth = temp.endWidth = 0.05f;
        temp.positionCount = 2;
        temp.startColor = lr.startColor;
        temp.endColor = lr.endColor;
        //printVerts(vertices);
        temp.SetPositions(arr);
    }

    public void printRule()
    {
        foreach (var rule in rules)
        {
            print("ch: " + rule.variable);
            print("rule: " + rule.rule);
        }
    }

    public void printVerts(List<Vector3> vertices)
    {
        string str = "";
        foreach (var v in vertices)
        {
            str += v.ToString();
                str += "\n";
        }

        print(str);
    }



    public void printvert(Vertice vert)
    {
        string str = "";
        str += vert.getPos().ToString();
        str += "\n";
        str += vert.getDir().ToString();
        print(str);
    }


    public void clearVerts()
    {
        //vertices.Clear();
        //lr.positionCount = 0;
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }

    public void dispose()
    {
        clearVerts();
        lsystem.clear();
    }

    public Matrix4x4 getRollMat(float angle)
    {
        float rad = Mathf.PI * angle / 180;
        Matrix4x4 mat = new Matrix4x4(new Vector4(1, 0, 0, 0),
                                            new Vector4(0, Mathf.Cos(rad), Mathf.Sin(rad), 0),
                                            new Vector4(0, -Mathf.Sin(rad), Mathf.Cos(rad), 0),
                                            new Vector4(0, 0, 0, 1));
        return mat;
    }

    public Matrix4x4 getYawMat(float angle)
    {
        float rad = Mathf.PI * angle / 180;
        Matrix4x4 mat = new Matrix4x4(new Vector4(Mathf.Cos(rad), 0, -Mathf.Sin(rad), 0),
                                            new Vector4(0, 1, 0, 0),
                                            new Vector4(Mathf.Sin(rad), 0, Mathf.Cos(rad), 0),
                                            new Vector4(0, 0, 0, 1));
        return mat;
    }

    public Matrix4x4 getPitchMat(float angle)
    {
        float rad = Mathf.PI * angle / 180;
        Matrix4x4 mat = new Matrix4x4(new Vector4(Mathf.Cos(rad), Mathf.Sin(rad), 0, 0),
                                            new Vector4(-Mathf.Sin(rad), Mathf.Cos(rad), 0, 0),
                                            new Vector4(0, 0, 1, 0),
                                            new Vector4(0, 0, 0, 1));
        return mat;
    }

}

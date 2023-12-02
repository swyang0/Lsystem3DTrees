using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class DrawTree : MonoBehaviour
{

    [SerializeField]
    private Lsystem lsystem;

    [SerializeField]
    [Range(0, 100)]
    private float angle;


    [SerializeField]
    [Range(0, 1)]
    private float scale = 0.1f;

    [SerializeField]
    [Range(0, 1)]
    private float windIntensity = 0;

    [SerializeField]
    WindDirection windDirection = new WindDirection();


    [SerializeField]
    [Range(3, 10)]
    private int branchShape = 5;

    [SerializeField]
    private List<Rule> rules;

    [SerializeField]
    private char axiom;


    public GameObject objholder;
    //private GameObject lineholder;
    public GameObject treeholder; 
    public GameObject leafPrefab;


    private List<Vector3> existPos;

    //private LineRenderer lr;

    private Vector3 windOffset;

    private List<GameObject> treeParts = new List<GameObject>();


    private void Awake()
    {
    }

    public void generate()
    {
        //printRule();
        
        lsystem = new Lsystem(rules, axiom);
        //lr = GetComponent<LineRenderer>();
        existPos = new List<Vector3>();

        // set up tree holder
        treeholder = new GameObject("tree");
        treeholder.AddComponent<MeshFilter>(); 
        treeholder.AddComponent<MeshRenderer>().material = new Material(Shader.Find("Diffuse"));


        // set up object holder
        objholder = new GameObject("obj");

        setWindOffset();
        growTree();
        
    }

    public void clearVerts()
    {
        //lr.positionCount = 0;
        clearHolders(objholder);
        //clearHolders(lineholder);
        clearHolders(treeholder);

        existPos.Clear();
        treeParts.Clear();
    }

    public void dispose()
    {
        clearVerts();
        lsystem.clear();
        DestroyImmediate(treeholder);
        DestroyImmediate(objholder);
        //treeholder.GetComponent<MeshFilter>().mesh = null;
    }

    public void setWindOffset()
    {
        switch (windDirection)
        {
            case WindDirection.NoWind:
                windOffset = Vector3.zero; 
                break;
            case WindDirection.PositiveZ:
                windOffset = Vector3.forward;
                break;
            case WindDirection.NegativeZ:
                windOffset = Vector3.back;
                break;
            case WindDirection.PositiveX:
                windOffset = Vector3.right;
                break;
            case WindDirection.NegativeX: 
                windOffset = Vector3.left;
                break;

        }

        windOffset = windOffset * windIntensity;
        print($"offset: {windOffset}");
    }

    public void growTree()
    {
        lsystem.generate();
        clearVerts();
        addVerts(lsystem.getCurrentStr());
        
    }

    public void addVerts(string str)
    {
        //print($"-----------{lsystem.iterations}---- ----");
        Stack<Vertice> stk = new Stack<Vertice>();

        //List<Vector3> vertices = new List<Vector3>();
        List<Vertice> vertices = new List<Vertice>();
        List<Vertice> leafPos = new List<Vertice>();

        Vertice vert = new Vertice(new Vector3(), new Vector3(0, 1, 0));
        
        //vert.setVert(curpos, curdir);

        float affect = 0;

        //print(str);

        foreach (char c in str)
        {

            
            string s = "";
            s += c;
            s += "\n";
            
            Matrix4x4 mat;
            Vector3 newpos = new Vector3();


            switch (c)
            {
                case 'F':      
                    if (lsystem.iterations == 1)
                    {
                        newpos = vert.pos + vert.dir * scale;
                    } else
                    {
                        newpos = vert.pos + Vector3.Normalize(vert.dir + windOffset * affect) * scale;
                    }
                    
                    
                    if (existPos.Contains(newpos))
                    {
                        break;
                    }
                    
                    s += $"start{vert.pos}, newvert{newpos}";
                    vertices.Add(vert.Clone());
                    existPos.Add(vert.pos);

                    Vector4 old = vert.pos;
                    vert.pos = newpos;

                    vertices.Add(vert.Clone());
                    existPos.Add(vert.pos);

                    s += $"end{vert.pos}\n";

                    if (affect <= 0.3)
                    {
                        affect += 0.01f;
                    }

                    print($"effect: {affect}");

                    createBranch(old, newpos);

                    break;

                case 'L':
                    leafPos.Add(vert.Clone());
                    break;

                case '+': // clockwise rotate around x axis
                    mat = getRollMat(angle);
                    vert.dir = Vector3.Normalize(mat * vert.dir) * scale;
                    s += $"dir: {vert.dir}";
                    break;

                case '-': // ccw rotate around x axis
                    mat = getRollMat(-angle);
                    vert.dir = Vector3.Normalize(mat * vert.dir) * scale;
                    s += $"dir: {vert.dir}";
                    break;

                case '$': //clockwise rotate around y axis
                    mat = getYawMat(angle);
                    vert.dir = Vector3.Normalize(mat * vert.dir) * scale;
                    break;

                case '%': // ccw rotate around y axis
                    mat = getYawMat(-angle);
                    vert.dir = Vector3.Normalize(mat * vert.dir) * scale;
                    break;

                case '^': //clockwise rotate around z axis
                    mat = getPitchMat(angle);
                    vert.dir = Vector3.Normalize(mat * vert.dir) * scale;
                    break;

                case '&': // ccw rotate around z axis
                    mat = getPitchMat(-angle);
                    vert.dir = Vector3.Normalize(mat * vert.dir) * scale;
                    break;

                case '[':
                    stk.Push(vert.Clone());
                    s += $"push vert:{vert.getPos()}\n";
                    //printvert(vert);
                    break;

                 case ']':
                    vert = stk.Pop();
                    s += $"pop vert:{vert.getPos()}\n";
                    //printvert(vert);
                    break;
                

            }
            //print(s);
        }



        drawLeaves(leafPos);
        combineMeshes();


    }


    /*
    public void drawLines(List<Vertice> vertices) {
        for (int i = 0; i < vertices.Count; i+=2)
        {
            Vector3 start = vertices[i].pos;
            Vector3 end = vertices[i+1].pos;
            //drawLine(vertices[i], vertices[i + 1]);        
            GameObject line = new GameObject();
            line.transform.parent = lineholder.transform;
            LineRenderer temp = line.AddComponent<LineRenderer>();
            Vector3[] arr = new[] {start, end};
            temp.material = lr.sharedMaterial;
            temp.startWidth = temp.endWidth = 0.05f;
            temp.positionCount = 2;
            temp.startColor = lr.startColor;
            temp.endColor = lr.endColor;
            //printVerts(vertices);
            temp.SetPositions(arr);
        }

    } 
    */
    public void createBranch(Vector3 start, Vector3 end)
    {
        GameObject obj = new GameObject();
        obj.transform.parent = objholder.transform;

        BranchMesh cy = new BranchMesh();
        //BranchMesh cy = new BranchMesh();
        Mesh mesh = cy.create(branchShape, start, end, 0.1f, 0.1f);

        obj.AddComponent<MeshFilter>().mesh = mesh;
        obj.AddComponent<MeshRenderer>();

        treeParts.Add(obj);
        

        //obj.transform.LookAt(end);
    }

    public void combineMeshes()
    {
        MeshFilter[] meshFilters = objholder.GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        
        for (int i = 0; i < meshFilters.Length; i++)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            //meshFilters[i].gameObject.De
        }

        Mesh mesh = new Mesh();
        mesh.CombineMeshes(combine);
        treeholder.GetComponent<MeshFilter>().sharedMesh = mesh;
        treeholder.SetActive(true);
        mesh.RecalculateNormals();
        clearHolders(objholder);

    }

    public void drawLeaves(List<Vertice> leafPos)
    {
        foreach (var leaf in leafPos)
        {
            GameObject obj = Instantiate(leafPrefab);
            obj.transform.parent = objholder.transform;
            obj.transform.position = leaf.getPos();
            obj.transform.rotation = Quaternion.Euler(leaf.getDir());
        }
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

    public void printcurrentpos(Vector3 curpos, Vector3 curdir)
    {
        string strstr = "";
        strstr += $"pos:{curpos}";
        strstr += '\n';
        strstr += $"dir:{curdir}";
        print(strstr);
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

    public void clearHolders(GameObject obj)
    {
        while (obj.transform.childCount > 0)
        {
            DestroyImmediate(obj.transform.GetChild(0).gameObject);
        }
    }

    private List<Rule> tree1()
    {
        return new List<Rule>()
        {
            new Rule('F', "F[+{\\F]F[/}-F]F", 0.6),
            new Rule('F', "F[+F+F]F", 0.7),
            new Rule('F', "F[-F]F", 0.5),
        };
    }

    private List<Rule> tree2()
    {
        return new List<Rule>()
        {
            new Rule('F', "FF", 0.8),
            new Rule('F', "F", 0.2),
            new Rule('X', "F[+/A][}\\A][-\\A][{/A]", 0.5),
            new Rule('X', "F[-\\A][{/A][+/A][}\\A]", 0.5),
            new Rule('A', "F[-\\A][{+/A]", 0.3),
            new Rule('A', "F[{\\A]F[{/A]", 0.3),
            new Rule('A', "F[-{A]F[+}A]", 0.3)
        };
    }

}

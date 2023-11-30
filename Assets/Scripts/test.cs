using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    Stack<int> stack = new Stack<int>();
    void Start()
    {
        stack.Push(0);
        
        stack.Push(1);

        stack.Push(2);

        var test = stack.Pop();
        print(test);

        test = stack.Pop();
        print(test);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

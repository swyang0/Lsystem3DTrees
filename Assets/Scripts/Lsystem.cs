using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

public class Lsystem
{

    public int iterations;

    // rules to apply
    private List<Rule> rules;

    // old str
    private string str = "";

    // map
    private Dictionary<char, string> map;

    // 

    public Lsystem(List<Rule> inRules, char inAxiom) {
        iterations = 0;
        map = new Dictionary<char, string>();
        rules = inRules;
        str = inAxiom.ToString();
        generateRuleMap();
        printMap();
    }


    public string generate()
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < str.Length; i++)
        {
            char c = str[i];

            if (map.ContainsKey(c))
            {
                sb.Append(map[c]);
            } else
            {
                sb.Append(c);
            }

        }

        iterations++;
        str = sb.ToString();

        //Debug.Log($"Iterations: {iterations}\nGenerate str: {str}");

        return str;
    }

    public string getCurrentStr()
    {
        return str;
    }

    private void generateRuleMap()
    {
        rules.ForEach(r =>
        {
            if (!map.ContainsKey(r.variable))
            {
                map.Add(r.variable, r.rule);
            }
        });
    }

    public void clear()
    {
        iterations = 0;
        map.Clear();
        str = "";

    }



    private void printMap()
    {
        foreach (var element in map) 
        {
            Debug.Log($"key : {element.Key}, value: {element.Value}");
        }
    }

}

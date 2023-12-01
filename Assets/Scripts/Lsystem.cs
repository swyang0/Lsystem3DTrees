using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
    private Dictionary<char, List<Rule>> map;
    
    public Lsystem(List<Rule> inRules, char inAxiom) {
        iterations = 0;
        map = new Dictionary<char, List<Rule>>();
        rules = inRules;
        str = inAxiom.ToString();
        generateRuleMap();
        setWeight();
    }


    public string generate()
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < str.Length; i++)
        {
            char c = str[i];
            // c is not a letter, add c and break
            if (!map.ContainsKey(c))
            {
                sb.Append(c);
                continue;
            }

            // if c is a letter
            // traverse the list to find the correct rule
            int rand = randomGenerate();
            Debug.Log($"rand num: {rand}");
            double crl = 0.0;
            for (int j = 0; j < map[c].Count; j++)
            {
                crl += map[c][j].weight;
                if (crl >= rand)
                {
                    sb.Append(map[c][j].rule);
                    Debug.Log($"RULE: {map[c][j].rule}\n");
                    break;
                }
            }

        }

        iterations++;
        str = sb.ToString();
        Debug.Log($"Iterations: {iterations}\nGenerate str: {str}");

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
            char key = r.variable;


            if (map.ContainsKey(key))
            {
                map[key].Add(r.Clone());
            } else
            {
                map.Add(key, new List<Rule> { r.Clone() });
            }
        });

        printMap();
    }

    public void clear()
    {
        iterations = 0;
        map.Clear();
        str = "";

    }



    private void printMap()
    {
        string str = "";
        foreach (var element in map) 
        {
            str += $"key : {element.Key}\n ";
            foreach (var rule in element.Value)
            {
                str += rule.ToString();
                str += ";";
            }
        }
    }

    public int randomGenerate()
    {
        //var seed = System.DateTime.Now.Minute + System.DateTime.Now.Second;
        var rand = Random.Range(0, 99);
        return rand;

    }

    public void setWeight()
    {
        foreach(var variable in map)
        {
            double tot = 0;
            // find the total weight
            foreach(var rule in variable.Value)
            {
                tot += rule.weight;
            }

            // set the weight
            foreach(var rule in variable.Value)
            {
                rule.weight = (int)(rule.weight/tot * 100);
            }

        }
    }

}

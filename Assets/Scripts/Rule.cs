using UnityEngine;

[System.Serializable]
public class Rule
{
    [SerializeField]
    public char variable;

    [SerializeField]
    public string rule;

    [SerializeField]
    public double weight;

    public Rule(char inVar, string inRule, double inWeight) 
    {
        variable = inVar;
        rule = inRule;
        weight = inWeight;
    
    }


    public Rule Clone()
    {
        return (Rule)this.MemberwiseClone();
    }

    public override string ToString()
    {
        string str = $"var: {this.variable}; rule: {this.rule}; weight: {this.weight}";
        return str;
    }

}

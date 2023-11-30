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
}

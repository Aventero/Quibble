using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Explanation", menuName = "Tutorial/Explanation", order = 1)]
public class Explanation : ScriptableObject
{
    [TextArea]
    public List<string> parts;
}

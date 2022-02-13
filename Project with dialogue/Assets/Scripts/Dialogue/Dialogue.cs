using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Dialogue
{
    public string name;

    [TextArea(2, 7)]
    public string[] sentences;
}

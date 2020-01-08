using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Border
{
    public int start;
    public int end;

    public override string ToString()
    {
        return $"({start},{end})";
    }
}

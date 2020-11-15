using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// That class defines which spots should be connected with list of objects typeof Pipe by Stream.
/// 
/// Object of that class can be the source of the Stream and the end of the Stream
/// </summary>
public class PipeSource : PipesObject
{
    public bool streamSource; // That helps to invert stream's direction

    protected override void Update()
    {
        base.Update();
    }
}

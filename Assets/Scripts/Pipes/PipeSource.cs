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
    public bool streamSourse; // That helps to invert stream's direction

    public List<PipeSource> pipeSpots; // One or more spots, where pipe ends

    private void Update()
    {
        if (pipeSpots.Count == 0)
            Debug.LogWarning(this.name+" Not connected");
    }

    public void AddSpot(PipeSource spot)
    {
        pipeSpots.Add(spot);
    }
}

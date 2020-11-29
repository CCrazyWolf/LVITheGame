using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LVITHeGame.Pipes
{
    public class PipesStream : MonoBehaviour
{
    public List<PipeSource> sources;
    public List<PipeSource> drains;

    public void AddSource(PipeSource source)
    {
        sources.Add(source);
    }
    public void AddDrain(PipeSource drain)
    {
        drains.Add(drain);
    }
    public void RemoveSource(PipeSource source)
    {
        sources.Remove(source);
    }
    public void RemoveDrain(PipeSource drain)
    {
        drains.Remove(drain);
    }

    private void OnEnable()
    {
        for (int i=0; i<sources.Count; i++)
        {
            
        }
    }

    public bool FindPath()
    {


        return false;
    }
}
}

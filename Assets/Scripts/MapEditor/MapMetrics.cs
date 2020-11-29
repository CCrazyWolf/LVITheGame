using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MapMetrics
{
    public static float cellLength = 2f;

    public static Vector3[] vertices =
    {
        new Vector3(-0.5f * cellLength, 0f, -0.5f * cellLength),
        new Vector3(-0.5f * cellLength, 0f, 0.5f * cellLength),
        new Vector3(0.5f * cellLength, 0f, 0.5f * cellLength),
        new Vector3(0.5f * cellLength, 0f, -0.5f * cellLength),
        new Vector3(-0.5f * cellLength, 0f, -0.5f * cellLength)
    };
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LVITHeGame.Pipes
{
    public enum PipesDirection
    {
        W, N, E, S
    }

    public static class PipesDirectionExtensions
    {
        public static PipesDirection Next(this PipesDirection direction)
        {
            return direction != PipesDirection.S ? direction + 1 : PipesDirection.W;
        }

        public static PipesDirection Prev(this PipesDirection direction)
        {
            return direction != PipesDirection.W ? direction - 1 : PipesDirection.S;
        }

        public static PipesDirection Opposite(this PipesDirection direction)
        {
            return (int)direction > 1 ? direction - 2 : direction + 2;
        }
    }
}
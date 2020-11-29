using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LVITHeGame.MapEditor
{
    public enum QubeDirections
    {
        W, N, E, S
    }

    public static class PipesDirectionExtensions
    {
        public static QubeDirections Next(this QubeDirections direction)
        {
            return direction != QubeDirections.S ? direction + 1 : QubeDirections.W;
        }

        public static QubeDirections Prev(this QubeDirections direction)
        {
            return direction != QubeDirections.W ? direction - 1 : QubeDirections.S;
        }

        public static QubeDirections Opposite(this QubeDirections direction)
        {
            return (int)direction > 1 ? direction - 2 : direction + 2;
        }
    }
}
using System;
using UnityEngine;

namespace P2_Flyweight
{
    [Serializable]
    public class TreeSeasonColors
    {
        [SerializeField] private ColorInfo[] colors;
    
        public Color GetSeasonColor(int index)
        {
            return new Color(colors[index].r, colors[index].g, colors[index].b);
        }
    }
}

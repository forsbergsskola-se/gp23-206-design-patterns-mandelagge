using UnityEngine;

namespace P2_Flyweight
{
    public class TreeData
    {
        private TreeSeasonColors _treeColors;
        
        
        public void LoadColorInfos()
        {
            var fileContents = Resources.Load<TextAsset>("treeColors").text;
            this._treeColors = JsonUtility.FromJson<TreeSeasonColors>(fileContents);
        }
        
        
        public Color GetSeasonColor()
        {

            this._treeColors.MoveNext();
            Debug.Log(this._treeColors.CurrentColor);
            return this._treeColors.CurrentColor;
        }
    }
}
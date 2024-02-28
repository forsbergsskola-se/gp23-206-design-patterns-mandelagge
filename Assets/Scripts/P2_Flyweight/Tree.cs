using UnityEngine;

namespace P2_Flyweight
{
    public class Tree : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private TreeSeasonColors _treeData;
        private int _tick;
        
        private int _colorIndex;

    
        void Start()
        {
            this._spriteRenderer = GetComponent<SpriteRenderer>();
            _treeData = FindObjectOfType<TreeSpawner>().GetSeasonColors();
            this._spriteRenderer.color = _treeData.GetSeasonColor(_colorIndex);
        }
    
        void Update()
        {
            _colorIndex++;
            this._spriteRenderer.color = _treeData.GetSeasonColor(_colorIndex);
        }
    }
}

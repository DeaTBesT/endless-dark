using System.Collections.Generic;
using UnityEngine;

namespace MapGeneration
{
    [CreateAssetMenu(menuName = "Map/New settings", fileName = "New map settings")]
    public class MapSettings : ScriptableObject
    {
        [Header("Map tiles")] 
        [SerializeField] private bool _isAutoFillID;
        [SerializeField] private List<SerializedDictionary> _tileset = new();

        [Header("Size")] 
        [SerializeField] private Vector2Int _size = new Vector2Int(160, 90);

        [Header("Noise")] 
        [SerializeField] private float _magnitudeMin = 4f;
        [SerializeField] private float _magnitudeMax = 20f; // recommend 4 to 20
        [SerializeField] private Vector2Int _offsetMin = Vector2Int.zero,
            _offsetMax = Vector2Int.one;

        [System.Serializable]
        public struct SerializedDictionary
        {
            public int tileId;
            public GameObject tilePrefab;
        }

        #region [PublicVars]

        public bool GetIsAutoFill => _isAutoFillID;
        public List<SerializedDictionary> GetTileset => _tileset;

        public Vector2Int GetSize => _size;

        //Return like x - min, y - max magnitude
        public Vector2 GetMagnitude => 
            new Vector2(_magnitudeMin, _magnitudeMax);

        public Vector2Int GetOffsetMin => _offsetMin;

        public Vector2Int GetOffsetMax => _offsetMax;

        #endregion
    }   
}
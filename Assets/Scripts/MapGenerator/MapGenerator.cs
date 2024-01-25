using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.Tilemaps;
using static MapGeneration.MapSettings;

namespace MapGeneration
{
    public class MapGenerator : NetworkBehaviour
    {
        [SerializeField] private MapSettings _mapSettings;

        [Header("Bot map")] 
        [SerializeField] private Tile _botTile;
        [SerializeField] private Tilemap _tilemap;
        
        private List<SerializedDictionary> _tileset = new();
        
        private Dictionary<int, GameObject> _tileGroups;

        private List<List<int>> _noiseGrid = new();
        private List<List<GameObject>> _tileGrid = new();
        
        private int _mapWidth = 160;
        private int _mapHeight = 90;
        
        // recommend 4 to 20
        private float _magnitude;
        
        private int _offsetX;
        private int _offsetY;

        private string _seed = "default";
        
        public void Initialize()
        {
            SetMapSettings();
            FillBotmap();
            
            if (_tileset.Count <= 0)
            {
                Debug.LogError("Error map generate: tileset is contains 0 elements");
                return;
            }

            if (!isServer)
            {
                return;
            }
            
            CreateTileset();
            CreateTileGroups();
            GenerateMap();
        }

        private void SetMapSettings()
        {
            Random.InitState(_seed.GetHashCode());
            
            _tileset = _mapSettings.GetTileset;

            _mapWidth = _mapSettings.GetSize.x;
            _mapHeight = _mapSettings.GetSize.y;
            
            _magnitude = Random.Range(_mapSettings.GetMagnitude.x, _mapSettings.GetMagnitude.y);
            
            _offsetX = Random.Range(_mapSettings.GetOffsetMin.x, _mapSettings.GetOffsetMax.x);
            _offsetX = Random.Range(_mapSettings.GetOffsetMin.y, _mapSettings.GetOffsetMax.y);
            
            _tilemap.ClearAllTiles();
        }

        private void FillBotmap()
        {
            for (int x = 0; x < _mapWidth; x++)
            {
                for (int y = 0; y < _mapHeight; y++)
                {
                    _tilemap.SetTile(new Vector3Int(-x + _mapWidth / 2, -y + _mapHeight / 2, 0), _botTile);
                }
            }
        }
        
        private void CreateTileset()
        {
            if (!_mapSettings.GetIsAutoFill)
            {
                return;
            }

            for (int i = 0; i < _tileset.Count; i++)
            {
                var tile = _tileset[i];
                tile.tileId = i;
            }
        }

        private void CreateTileGroups()
        {
            _tileGroups = new Dictionary<int, GameObject>();

            foreach (SerializedDictionary tile in _tileset)
            {
                GameObject tileGroup = new GameObject(tile.tilePrefab.name);
                tileGroup.transform.parent = gameObject.transform;
                tileGroup.transform.localPosition = new Vector3(0, 0, 0);
                _tileGroups.Add(tile.tileId, tileGroup);
            }
        }

        private void GenerateMap()
        {
            for (int x = 0; x < _mapWidth; x++)
            {
                _noiseGrid.Add(new List<int>());
                _tileGrid.Add(new List<GameObject>());

                for (int y = 0; y < _mapHeight; y++)
                {
                    int tileID = GetIdUsingPerlin(x, y);
                    _noiseGrid[x].Add(tileID);
                    CreateTile(tileID, x, y);
                }
            }
        }

        private int GetIdUsingPerlin(int x, int y)
        {
            float rawPerlin = Mathf.PerlinNoise(
                (x - _offsetX) / _magnitude,
                (y - _offsetY) / _magnitude
            );
            float clampPerlin =
                Mathf.Clamp01(rawPerlin);
            float scaledPerlin = clampPerlin * _tileset.Count;

            scaledPerlin = Mathf.Clamp(scaledPerlin, 0, _tileset.Count - 1);

            return Mathf.FloorToInt(scaledPerlin);
        }

        private void CreateTile(int tileID, int x, int y)
        {
            GameObject tile = Instantiate(_tileset[tileID].tilePrefab, _tileGroups[tileID].transform);

            tile.name = string.Format("tile_x{0}_y{1}", x, y);
            tile.transform.localPosition = new Vector3(-x + _mapWidth / 2 + 1, -y + _mapHeight / 2 + 1, 0);

            _tileGrid[x].Add(tile);
            
            NetworkServer.Spawn(tile);
        }
    }
}
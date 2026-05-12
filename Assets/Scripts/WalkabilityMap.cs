using UnityEngine;

public class WalkabilityMap
{
    [SerializeField] private float mapWidth = 20;
    [SerializeField] private float mapHeight = 20;
    [SerializeField] private float _cellSize = 2;

    public float CellSize => _cellSize;
    private bool[,] walkable;

    public WalkabilityMap()
    {
        Create();
    }

    private void Create()
    {
        int gridWidth = Mathf.CeilToInt(mapWidth / _cellSize);
        int gridHeight = Mathf.CeilToInt(mapHeight / _cellSize);
        walkable = new bool[gridWidth, gridHeight];

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 worldPos = new(
                    x * _cellSize + _cellSize * 0.5f,
                    0,
                    y * _cellSize + _cellSize * 0.5f
                );

                walkable[x, y] = IsCellWalkable(worldPos);
            }
        }
    }

    public Vector2Int WorldToGrid(Vector3 point)
    {
        int gridX = (int)((point.x + mapWidth/2) / _cellSize);
        int gridY = (int)((point.z + mapHeight/2) / _cellSize);
        return new Vector2Int(gridX, gridY);
    }
    
    public Vector3 GridToWorld(Vector2Int vector)
    {
        int x = (vector.x - Mathf.CeilToInt(mapWidth / _cellSize)/2) * 2 + (int)(_cellSize/2);
        int y = (vector.y - Mathf.CeilToInt(mapHeight / _cellSize)/2) * 2 + (int)(_cellSize/2);
        return new Vector3(x, 0, y);
    }

    private bool IsCellWalkable(Vector3 worldPos)
    {
        return true;
    }

    public bool IsWalkable(int x, int y)
    {
        if(x < 0 || y < 0 || x >= Mathf.CeilToInt(mapWidth / _cellSize) || y >= Mathf.CeilToInt(mapHeight / _cellSize))
        {
            return false;
        }
        
        return walkable[x, y];
    }
}

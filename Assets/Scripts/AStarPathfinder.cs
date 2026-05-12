using UnityEngine;
using System.Collections.Generic;

public class AStarPathfinder
{
    private readonly WalkabilityMap walkabilityMap;
    private readonly List<Vector2Int> neighbors;

    // A* 노드 정보
    private class Node
    {
        public Vector2Int position;
        public float g;  // 시작점으로부터의 비용
        public float h;  // 휴리스틱 (목표까지의 추정 비용)
        public float f => g + h;  // 전체 비용
        public Vector2Int parent;  // 경로 추적용
        public bool isParentSet;
    }

    private const float ORTHOGONAL_COST = 1f;  // 상하좌우 비용
    private const float DIAGONAL_COST = 1.414213562f;  // 대각선 비용 sqrt(2)

    public AStarPathfinder(WalkabilityMap map)
    {
        walkabilityMap = map;
        neighbors = new();
    }

    /// <summary>
    /// 시작점에서 목표점까지의 경로를 찾습니다
    /// </summary>
    public List<Vector3> FindPath(Vector3 startPos, Vector3 destination)
    {
        Vector2Int startIndex = walkabilityMap.WorldToGrid(startPos);
        Vector2Int goalIndex = walkabilityMap.WorldToGrid(destination);

        // 열린 목록과 닫힌 목록
        var openSet = new List<Vector2Int> { startIndex };
        var closedSet = new HashSet<Vector2Int>();
        var nodes = new Dictionary<Vector2Int, Node>();

        // 시작 노드 초기화
        nodes[startIndex] = new Node
        {
            position = startIndex,
            g = 0,
            h = CalculateHeuristic(startIndex, goalIndex),
            isParentSet = false
        };

        while (openSet.Count > 0)
        {
            // 열린 목록에서 f값이 가장 낮은 노드 찾기
            int currentIndex = 0;
            for (int i = 1; i < openSet.Count; i++)
            {
                if (nodes[openSet[i]].f < nodes[openSet[currentIndex]].f)
                {
                    currentIndex = i;
                }
            }

            Vector2Int current = openSet[currentIndex];

            // 목표 도달
            if (current == goalIndex)
            {
                return ReconstructPath(nodes, current, startIndex);
            }

            openSet.RemoveAt(currentIndex);
            closedSet.Add(current);

            // 이웃 노드 검사
            List<Vector2Int> neighborList = GetNeighbors(current);
            foreach (Vector2Int neighbor in neighborList)
            {
                if (closedSet.Contains(neighbor))
                    continue;

                // 이동 비용 계산
                float moveCost = GetMoveCost(current, neighbor);
                float newG = nodes[current].g + moveCost;

                // 새로운 경로가 더 좋은 경우
                if (!nodes.ContainsKey(neighbor) || newG < nodes[neighbor].g)
                {
                    if (!nodes.ContainsKey(neighbor))
                    {
                        nodes[neighbor] = new Node
                        {
                            position = neighbor,
                            h = CalculateHeuristic(neighbor, goalIndex)
                        };
                    }

                    nodes[neighbor].g = newG;
                    nodes[neighbor].parent = current;
                    nodes[neighbor].isParentSet = true;

                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                }
            }
        }

        // 경로를 찾을 수 없음
        return new List<Vector3>();
    }

    /// <summary>
    /// 두 그리드 사이의 이동 비용을 계산합니다 (8방향)
    /// </summary>
    private float GetMoveCost(Vector2Int from, Vector2Int to)
    {
        int dx = Mathf.Abs(to.x - from.x);
        int dy = Mathf.Abs(to.y - from.y);

        // 대각선 이동
        if (dx != 0 && dy != 0)
            return DIAGONAL_COST;
        // 상하좌우 이동
        else
            return ORTHOGONAL_COST;
    }

    /// <summary>
    /// 맨해튼 거리 기반 휴리스틱 (대각선 이동 고려)
    /// </summary>
    private float CalculateHeuristic(Vector2Int from, Vector2Int to)
    {
        int dx = Mathf.Abs(to.x - from.x);
        int dy = Mathf.Abs(to.y - from.y);
        
        // 대각선으로 이동 가능한 최대 거리 + 남은 수평/수직 거리
        return (Mathf.Min(dx, dy) * DIAGONAL_COST) + (Mathf.Abs(dx - dy) * ORTHOGONAL_COST);
    }

    /// <summary>
    /// 목표점부터 시작점까지 역추적하여 경로를 재구성합니다
    /// </summary>
    private List<Vector3> ReconstructPath(Dictionary<Vector2Int, Node> nodes, Vector2Int current, Vector2Int start)
    {
        var path = new List<Vector2Int>();
        
        while (current != start)
        {
            path.Add(current);
            if (!nodes[current].isParentSet)
                break;
            current = nodes[current].parent;
        }
        path.Add(start);
        path.Reverse();

        // 그리드 좌표를 월드 좌표로 변환
        var worldPath = new List<Vector3>();
        foreach (var gridPos in path)
        {
            worldPath.Add(walkabilityMap.GridToWorld(gridPos));
        }

        return worldPath;
    }

    // 인접한 노드 찾기
    private List<Vector2Int> GetNeighbors(Vector2Int current)
    {
        neighbors.Clear();

        // 상하좌우 + 대각선 (8방향)
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                if (dx == 0 && dy == 0) continue;  // 자신은 제외

                int newX = current.x + dx;
                int newY = current.y + dy;

                // 워크어빌리티 맵으로 확인
                if (walkabilityMap.IsWalkable(newX, newY))
                {
                    neighbors.Add(new Vector2Int(newX, newY));
                }
            }
        }

        return neighbors;
    }
}

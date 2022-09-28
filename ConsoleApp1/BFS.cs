using Microsoft.VisualBasic.FileIO;

namespace ConsoleApp1;

public class BFS
{
    private int[][] grid;
    Dictionary<int, Node> nodes = new Dictionary<int, Node>();
    Queue<int> queue = new Queue<int>();
    HashSet<int> visited = new HashSet<int>();
    
    public BFS()
    {
        grid = File.ReadAllLines(Path.GetFullPath(@"..\..\..\input.txt")).Select(x =>
            x.ToCharArray().Select(ch =>
                (int)Char.GetNumericValue(ch)
                ).ToArray()
            ).ToArray();
        
        for (int y = 0; y < grid.Length; y++)
        {
            for (int x = 0; x < grid[y].Length; x++)
            {
                if (grid[y][x] != 1)
                {
                    Node nd = new Node();
                    nd.id = x + y * grid[y].Length;
                    int[][] neighbours =
                    {
                        new[] { x, y + 1 },
                        new[] { x, y - 1 },
                        new[] { x + 1, y },
                        new[] { x - 1, y },
                    };
                    foreach (var neighbour in neighbours)
                    {
                        if (neighbour[1] >= 0 && neighbour[1] < grid.Length && neighbour[0] >= 0 && neighbour[0] < grid[neighbour[1]].Length && grid[neighbour[1]][neighbour[0]] != 1)
                        {
                            nd.neighbours.Add(neighbour[0] + neighbour[1] * grid[neighbour[1]].Length);
                        }
                    }
                    nodes.Add(nd.id,nd);
                }
            }
        }
        queue.Enqueue(0);
        visited.Add(0);
        while (queue.Count != 0)
        {
            Node cur = nodes[queue.Dequeue()];
            visited.Add(cur.id);
            foreach (var neighbour in cur.neighbours)
            {
                if (!visited.Contains(nodes[neighbour].id))
                {
                    nodes[neighbour].parent = cur.id;
                    queue.Enqueue(nodes[neighbour].id);
                }
            }
        }

        var ndId = 63;
        while (true)
        {
            Console.Write(ndId + " -> ");
            if(ndId == 0) break;
            ndId = nodes[ndId].parent;
        }
    }
}

public class Node
{
    public int id;
    public int parent;
    public List<int> neighbours;

    public Node()
    {
        id = -1;
        parent = -1;
        neighbours = new List<int>();
    }
}
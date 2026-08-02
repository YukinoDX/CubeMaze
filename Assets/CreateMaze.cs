using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMaze : MonoBehaviour
{
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject path;
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject goal;
    public static int size;
    private int[,,] maze;
    public GameObject[,,] mazeObject;
    private int[,,] dists;
    private const int WALL = 1;
    private const int PATH = 0;

    // Start is called before the first frame update
    void Start()
    {
        maze = new int[size, size, size];
        mazeObject = new GameObject[size, size, size];
        dists = new int[size, size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                for (int k = 0; k < size; k++)
                {
                    maze[i, j, k] = WALL;
                    mazeObject[i, j, k] = Instantiate(wall, new Vector3(-size / 2.0f + i + 0.5f, -size / 2.0f + j + 0.5f, k + 0.5f), Quaternion.identity, transform);
                    dists[i, j, k] = -1;
                }
            }
        }

        int sx = Random.Range(0, size / 2 - 1) * 2 + 1;
        int sy = Random.Range(0, size / 2 - 1) * 2 + 1;
        int sz = Random.Range(0, size / 2 - 1) * 2 + 1;
        Debug.Assert(0 <= sx && sx < size && 0 <= sy && sy < size && 0 <= sz && sz < size);
        maze[sx, sy, sz] = PATH;
        Destroy(mazeObject[sx, sy, sz]);
        mazeObject[sx, sy, sz] = Instantiate(path, new Vector3(-size / 2.0f + sx + 0.5f, -size / 2.0f + sy + 0.5f, sz + 0.5f), Quaternion.identity, transform);
        dists[sx, sy, sz] = 0;

        DFS(sx, sy, sz, 0);
        (int gx, int gy, int gz) = (0, 0, 0);
        int distMax = 0;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                for (int k = 0; k < size; k++)
                {
                    if (distMax < dists[i, j, k])
                    {
                        distMax = dists[i, j, k];
                        (gx, gy, gz) = (i, j, k);
                    }

                    if (i == 0 || i == size - 1 || j == 0 || j == size - 1 || k == 0 || k == size - 1)
                    {
                        Debug.Assert(maze[i, j, k] == WALL && mazeObject[i, j, k] != null);
                        Debug.Assert(maze[i, j, k] != PATH || dists[i, j, k] != -1);
                        Debug.Assert(maze[i, j, k] != WALL || dists[i, j, k] == -1);
                    }
                }
            }
        }
        Instantiate(ball, new Vector3(-size / 2.0f + sx + 0.5f, -size / 2.0f + sy + 0.5f, sz + 0.5f), Quaternion.identity);
        Instantiate(goal, new Vector3(-size / 2.0f + gx + 0.5f, -size / 2.0f + gy + 0.5f, gz + 0.5f), Quaternion.identity, transform);
    }

    private void DFS(int cx, int cy, int cz, int dist)
    {
        (int, int, int)[] dirs = { (1, 0, 0), (-1, 0, 0), (0, 1, 0), (0, -1, 0), (0, 0, 1), (0, 0, -1) };
        Shuffle(dirs);
        foreach (var (dx, dy, dz) in dirs)
        {
            (int nx, int ny, int nz) = (cx + dx, cy + dy, cz + dz);
            (int nx2, int ny2, int nz2) = (cx + 2 * dx, cy + 2 * dy, cz + 2 * dz);
            if (0 <= nx2 && nx2 < size && 0 <= ny2 && ny2 < size && 0 <= nz2 && nz2 < size && maze[nx, ny, nz] == WALL && maze[nx2, ny2, nz2] == WALL)
            {
                maze[nx, ny, nz] = PATH;
                maze[nx2, ny2, nz2] = PATH;
                Destroy(mazeObject[nx, ny, nz]);
                Destroy(mazeObject[nx2, ny2, nz2]);
                mazeObject[nx, ny, nz] = Instantiate(path, new Vector3(-size / 2.0f + nx + 0.5f, -size / 2.0f + ny + 0.5f, nz + 0.5f), Quaternion.identity, transform);
                mazeObject[nx2, ny2, nz2] = Instantiate(path, new Vector3(-size / 2.0f + nx2 + 0.5f, -size / 2.0f + ny2 + 0.5f, nz2 + 0.5f), Quaternion.identity, transform);
                dists[nx, ny, nz] = dist + 1;
                dists[nx2, ny2, nz2] = dist + 2;

                DFS(nx2, ny2, nz2, dist + 2);
            }
        }
    }

    void Shuffle((int, int, int)[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            (int, int, int) temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}

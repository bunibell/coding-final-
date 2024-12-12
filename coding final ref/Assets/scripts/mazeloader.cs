using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

public class MazeLoader : MonoBehaviour
{
    public GameObject wallPrefab;
    public GameObject playerPrefab;

    private void Start()
    {
        LoadMaze("Assets/Maze.txt");
    }

    void LoadMaze(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);

        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                char currentChar = lines[y][x];
                Vector2 position = new Vector2(x, -y);

                if (currentChar == '#') // Wall
                {
                    Instantiate(wallPrefab, position, Quaternion.identity);
                }
                else if (currentChar == 'P') // Player
                {
                    Instantiate(playerPrefab, position, Quaternion.identity);
                }
            }
        }
    }
}
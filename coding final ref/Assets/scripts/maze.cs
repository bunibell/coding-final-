// Maze Escape Game
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGame : MonoBehaviour
{
    public TextAsset mazeFile; // ASCII file for maze
    public GameObject playerPrefab;

    private char[,] maze;
    private int rows;
    private int cols;
    private GameObject player;

    void Start()
    {
        LoadMaze();
        CreateMaze();
        SpawnPlayer();
    }

    void LoadMaze()
    {
        string[] lines = mazeFile.text.Split('\n');
        rows = lines.Length;
        cols = lines[0].Length;
        maze = new char[rows, cols];

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                maze[r, c] = lines[r][c];
            }
        }
    }

    void CreateMaze()
    {
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                if (maze[r, c] == '#')
                {
                    GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    wall.transform.position = new Vector3(c, -r, 0);
                }
            }
        }
    }

    void SpawnPlayer()
    {
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                if (maze[r, c] == 'P')
                {
                    player = Instantiate(playerPrefab, new Vector3(c, -r, 0), Quaternion.identity);
                }
            }
        }
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 move = Vector3.zero;
            if (Input.GetKeyDown(KeyCode.W)) move = Vector3.up;
            if (Input.GetKeyDown(KeyCode.S)) move = Vector3.down;
            if (Input.GetKeyDown(KeyCode.A)) move = Vector3.left;
            if (Input.GetKeyDown(KeyCode.D)) move = Vector3.right;

            Vector3 newPos = player.transform.position + move;
            int newRow = -(int)newPos.y;
            int newCol = (int)newPos.x;

            if (maze[newRow, newCol] != '#')
            {
                player.transform.position = newPos;

                if (maze[newRow, newCol] == 'E')
                {
                    Debug.Log("You win!");
                    Destroy(player);
                }
            }
        }
    }
}

// Falling Objects Dodge Game
public class DodgeGame : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject fallingObjectPrefab;
    public float spawnInterval = 1f;
    public float objectSpeed = 2f;

    private GameObject player;

    void Start()
    {
        player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-8f, 8f), 5f, 0);
            GameObject fallingObject = Instantiate(fallingObjectPrefab, spawnPosition, Quaternion.identity);

            Rigidbody2D rb = fallingObject.AddComponent<Rigidbody2D>();
            rb.velocity = new Vector2(0, -objectSpeed);

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal") * 5f * Time.deltaTime;
        player.transform.Translate(moveX, 0, 0);

        if (player.transform.position.x < -8f) player.transform.position = new Vector3(-8f, player.transform.position.y, 0);
        if (player.transform.position.x > 8f) player.transform.position = new Vector3(8f, player.transform.position.y, 0);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("FallingObject"))
        {
            Debug.Log("Game Over!");
            Destroy(player);
        }
    }
}

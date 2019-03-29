using System.Collections;
using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class TornadoBehaviour : MonoBehaviour
{
    /*private float timer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        System.UnityEngine.Random rand = new System.UnityEngine.Random();
        float nextX = (float) rand.Next(-100, 100) / 200f, nextY = (float)rand.Next(-100, 100) / 200f; 
        Vector3 newPos = new Vector3(nextX, nextY, 0);
        Debug.Log(newPos);
        transform.position += newPos;
    }*/
    public float velocityMax;

    public float xMax;
    public float yMax;
    public float xMin;
    public float yMin;
    public float rotationAngle;

    public Grid obj;
    public Sprite testSprite, blankSprite;
    private List<Transform> mapLevels = new List<Transform>();

    private float x;
    private float y;
    private float tempo;
    private float angle;

    void Start()
    {
        Transform trans = obj.transform;
        foreach (Transform child in trans)
        {
            mapLevels.Add(child);
        }
        x = UnityEngine.Random.Range(-velocityMax, velocityMax);
        y = UnityEngine.Random.Range(-velocityMax, velocityMax);
        angle = Mathf.Atan2(x, y) * (180 / 3.141592f) + rotationAngle;
        transform.localRotation = Quaternion.Euler(0, angle, 0);
    }

    void Update()
    {
        placeTile();
        tempo += Time.deltaTime;

        if (transform.localPosition.x > xMax)
        {
            x = UnityEngine.Random.Range(-velocityMax, 0.0f);
            angle = Mathf.Atan2(x, y) * (180 / 3.141592f) + rotationAngle;
            transform.localRotation = Quaternion.Euler(0, angle, 0);
            tempo = 0.0f;
        }
        if (transform.localPosition.x < xMin)
        {
            x = UnityEngine.Random.Range(0.0f, velocityMax);
            angle = Mathf.Atan2(x, y) * (180 / 3.141592f) + rotationAngle;
            transform.localRotation = Quaternion.Euler(0, angle, 0);
            tempo = 0.0f;
        }
        if (transform.localPosition.y > yMax)
        {
            y = UnityEngine.Random.Range(-velocityMax, 0.0f);
            angle = Mathf.Atan2(x, y) * (180 / 3.141592f) + rotationAngle;
            transform.localRotation = Quaternion.Euler(0, angle, 0);
            tempo = 0.0f;
        }
        if (transform.localPosition.y < yMin)
        {
            y = UnityEngine.Random.Range(0.0f, velocityMax);
            angle = Mathf.Atan2(x, y) * (180 / 3.141592f) + rotationAngle;
            transform.localRotation = Quaternion.Euler(0, angle, 0);
            tempo = 0.0f;
        }


        if (tempo > 1.0f)
        {
            x = UnityEngine.Random.Range(-velocityMax, velocityMax);
            y = UnityEngine.Random.Range(-velocityMax, velocityMax);
            angle = Mathf.Atan2(x, y) * (180 / 3.141592f) + rotationAngle;
            transform.localRotation = Quaternion.Euler(0, angle, 0);
            tempo = 0.0f;
        }

        transform.localPosition = new Vector3(transform.localPosition.x + x, transform.localPosition.y + y);
    }

    void placeTile()
    {
        int x1, y1;
        Vector3Int gridPos = obj.WorldToCell(transform.position);
        x1 = gridPos.x; y1 = gridPos.y;
        if (x1 < 0 || y1 < 0 || x1 > Global.mapWidth-1 || y1 > Global.mapHeight-1)
            { return; }
        if (Global.FeatureMap[x1, y1] == -1 || Global.FeatureMap[x1, y1] == 1 || Global.FeatureMap[x1, y1] == 2)
            return;
        //Debug.Log(x1 + " " + y1);
        Tile hoverTile = new Tile();
        foreach (Transform child in mapLevels)
        {
            Tilemap locmap = child.gameObject.GetComponent<Tilemap>();
            TilemapRenderer tmRenderer = child.gameObject.GetComponent<TilemapRenderer>();
            if (tmRenderer.sortingOrder == 1)
            { 
                hoverTile.sprite = blankSprite;
                locmap.SetTile(new Vector3Int(x1, y1, 0), hoverTile);
            }
        }
    }
}

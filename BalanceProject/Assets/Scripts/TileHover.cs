///Tile placement system
///Author: Cucu Stefan
///-------------------

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileHover : MonoBehaviour
{
    public Grid grid;
    public Vector3 offset;
    public Sprite hoverSprite;
    public Texture2D buildCursor;
    public GameObject warningBox;
    public TextMeshProUGUI warningText;
    public int type = -1;
    private List<Transform> mapLevels = new List<Transform>();
    public Building[] models;
    public TimeManager tm;

    void Start()
    {
        TilemapDisplay tmDisplay = FindObjectOfType<TilemapDisplay>();
        mapLevels = tm.mapLevels;
    }

    void Update()
    {
        //if (type == -1)
            //{ Cursor.SetCursor(null, Vector3.zero, CursorMode.Auto); return; }
        //Cursor.SetCursor(buildCursor, Vector3.zero, CursorMode.Auto);
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPos = grid.WorldToCell(mousePos);
            gridPos.z = 0;
            Tilemap tilemap = new Tilemap();
            Tile hoverTile = new Tile();
            hoverTile.sprite = hoverSprite;
            foreach (Transform child in mapLevels)
            {
                Tilemap locmap = child.gameObject.GetComponent<Tilemap>();
                TilemapRenderer tmRenderer = child.gameObject.GetComponent<TilemapRenderer>();
                if (tmRenderer.sortingOrder == 0) continue;
                tilemap = locmap;
            }
            build(gridPos, tilemap, type);
        }
    }

    public void build(Vector3Int pos, Tilemap tilemap, int type) // Places the selected tile
    {
        Debug.Log(type + " " + models[type].Name);
        if (!Validate(type, pos))
            return;
        if (1 <= type && type <= 16)
            FindObjectOfType<AudioManager>().Play("DrillSound", 0f, 4.57f);

        if (type > 16 && type <= 21)
            FindObjectOfType<AudioManager>().Play("BuildersDrilling", 0f, 5f);

        if (type > 21)
            FindObjectOfType<AudioManager>().Play("BuzzSaw", 0f, 4.8f);

        if (type == 0 && Global.FeatureMap[pos.x, pos.y] == 0)
            Global.wood += 10;
        Tile tile = new Tile();
        tile.sprite = models[type].sprite;
        tilemap.SetTile(pos, tile);

        if (Global.FeatureMap[pos.x, pos.y] != -1 && type == 0)
            Global.buildings[(int)Global.FeatureMap[pos.x, pos.y]]--;
        if (type != 0) { Global.FeatureMap[pos.x, pos.y] = type + 2; Global.buildings[type + 2]++; }
        else Global.FeatureMap[pos.x, pos.y] = -1;
        Global.wood -= models[type].wood;
        Global.stone -= models[type].stone;
        Global.bricks -= models[type].brick;
        Global.metal -= models[type].metal;
        Global.glass -= models[type].glass;
    }

    #region Validate
    private bool Validate(int type, Vector3Int pos)
    {
        if (type != 0 && (Global.wood < models[type].wood
            || Global.stone < models[type].stone
            || Global.bricks < models[type].brick
            || Global.metal < models[type].metal
            || Global.glass < models[type].glass))
            { Warn("You require more materials"); return false; } // Player must have the necessary materials
        if (pos.x < 0 || pos.x > Global.mapWidth || pos.y < 0 || pos.y > Global.mapHeight)
            { return false; }
        if (type == 0 && (Global.FeatureMap[pos.x, pos.y] == 1 || Global.FeatureMap[pos.x, pos.y] == 2)) // Can't destroy ore or oil deposit
            { Warn("Can't build on ore / oil deposits"); return false; }
        if (!checkRoad(pos)) //Buildings must be connected to a road
            { Warn("Building not connected to road"); return false; }
        if ((type == 16 || type == 22) && !checkWater(pos)) //Fishery and tidal power plant must be placed near water
            { Warn("Building must be placed on coast"); return false; }
        if (type != 0 && type != 6 && type != 7 && type != 8 && type != 9 && type != 10 && Global.FeatureMap[pos.x, pos.y] != -1) // Buildings must be placed on empty space
            { return false; }
        if ((type == 6 || type == 8 || type == 9 || type == 10) && Global.FeatureMap[pos.x, pos.y] != 1) // Mines must be placed on ore
            { Warn("Mines must be placed on ore!"); return false; }
        if (type == 7) // Petrol pump
        {
            if (Global.FeatureMap[pos.x, pos.y] != 2) //Must be placed on oil deposit
                { Warn("Must be placed on an oil deposit"); return false; }
        }
        if (Global.HeightMap[pos.x, pos.y] > 0.4) // Buildings can't be placed on water
            { return false; }
        return true;
    }

    bool checkRoad(Vector3Int pos)
    {
        if (type == 0 || type == 1 || type == 5 || type == 6 || type == 7 || type == 8 || type == 9 || type == 10 || type == 17 || type == 18)
            return true;
        if (Global.FeatureMap[pos.x, pos.y - 1] == 3
            || Global.FeatureMap[pos.x, pos.y + 1] == 3
            || Global.FeatureMap[pos.x - 1, pos.y] == 3
            || Global.FeatureMap[pos.x + 1, pos.y] == 3)
            return true;
        else { return false; }
    }

    bool checkWater(Vector3Int pos)
    {
        if (Global.HeightMap[pos.x, pos.y - 1] > 0.4
            || Global.HeightMap[pos.x, pos.y + 1] > 0.4
            || Global.HeightMap[pos.x - 1, pos.y] > 0.4
            || Global.HeightMap[pos.x + 1, pos.y] > 0.4)
            return true;
        return false;
    }
    #endregion

    public void Warn(string warning)
    {
        warningBox.SetActive(true);
        warningText.text = "You cannot place that! \n " + warning;
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        warningBox.SetActive(false);
    }
    #region Structs
    [System.Serializable]
    public struct Building
    {
        public string Name;
        public Sprite sprite;
        public int wood;
        public int stone;
        public int brick;
        public int metal;
        public int glass;
    }
    #endregion
}

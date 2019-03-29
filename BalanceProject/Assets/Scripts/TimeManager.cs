using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class TimeManager : MonoBehaviour
{
    public static int days = 0;
    public int speed = 2, nrTilesToFlood = 200;
    private int last = -1;

    public GameObject obj, tornadoPrefab;
    public buildingIncomes[] incomes;
    public TextMeshProUGUI popText, energyText, daysText, woodText, stoneText, brickText, glassText, metalText, oilText, coalText, biofuelText, uraniumText, sourcesText, totalPanelText, foodWaterText, islandNameText;
    public Sprite waterSprite, blankSprite;
    public TilemapDisplay tmDisp;
    public bool ok = true;
    public List<Transform> mapLevels = new List<Transform>();
    private SortedList pollutionSources = new SortedList();

    public GameObject slider;

    void Start()
    {
        MapSave.Load();
        tmDisp.buildTilemap();
        islandNameText.text = Global.name;
        Transform transform = obj.transform;
        foreach (Transform child in transform)
        {
            mapLevels.Add(child);
        }
    }

    void Update()
    {
        int time = (int) Time.time + 1;
        int availableSpace = Global.buildings[4] * 5 + Global.buildings[5] * 15 + Global.buildings[6] * 30;

        slider.GetComponent<Slider>().value = 100 - Global.pollution;

        if (time % speed == 0 && time != last && ok)
        {
            days++;
            last = time;
            popText.text = "Population: " + Global.population.ToString() + " / " + availableSpace;
            if (availableSpace < Global.population) popText.color = Color.red;
            else popText.color = Color.white;
            foodWaterText.text = "Food: " + Global.food + " Water: " + Global.water; 
            energyText.text = "Electricity: " + Global.electricity.ToString();
            daysText.text = "Days past: " + days.ToString();
            woodText.text = "Wood \n" + Global.wood.ToString();
            stoneText.text = "Stone \n" + Global.stone.ToString();
            brickText.text = "Brick \n" + Global.bricks.ToString();
            glassText.text = "Glass \n" + Global.glass.ToString();
            metalText.text = "Metal \n" + Global.metal.ToString();
            oilText.text = "Oil \n" + Global.petrol.ToString();
            coalText.text = "Coal \n" + Global.coal.ToString();
            biofuelText.text = "Biofuel \n" + Global.biofuel.ToString();
            uraniumText.text = "Uranium \n" + Global.uranium.ToString();
            ///Debug.Log(days);
            for (int type = 0; type <= 25; type++) {
                Global.petrol += incomes[type].petrol * Global.buildings[type];
                Global.gas += incomes[type].gas * Global.buildings[type];
                Global.coal += incomes[type].coal * Global.buildings[type];
                Global.biofuel += incomes[type].biofuel * Global.buildings[type];
                Global.uranium += incomes[type].uranium * Global.buildings[type];
                Global.wood += incomes[type].wood * Global.buildings[type];
                Global.stone += incomes[type].stone * Global.buildings[type];
                Global.bricks += incomes[type].bricks * Global.buildings[type];
                Global.metal += incomes[type].metal * Global.buildings[type];
                Global.water += incomes[type].water * Global.buildings[type];
                Global.glass += incomes[type].glass * Global.buildings[type];
                Global.food += incomes[type].food * Global.buildings[type];
                Global.electricity += incomes[type].electricity * Global.buildings[type];
            }

            if (time % (speed * 10) == 0)
            {
                Global.water -= Global.population / 5;
                Global.food -= Global.population / 5;
                Global.population += (int)(Global.population / 5) + 2;
            }
            if (time % (speed * 15) == 0)
            {
                for (int type = 0; type < 26; type++){
                    Global.pollution += incomes[type].pollution * Global.buildings[type];
                    if(incomes[type].pollution > 0)
                        try
                        {
                            pollutionSources.Add(incomes[type].pollution * Global.buildings[type], incomes[type].name);
                        }
                        catch
                        {
                            Debug.Log("An element with Key = " + incomes[type].name + " already exists.");
                        }
                }
                if (Global.population > availableSpace)
                    Global.pollution += (Global.population - availableSpace) / 15;
                if (Global.water < 0)
                    Global.population += (Global.water / 10) - 1;
                if (Global.food < 0)
                    Global.population += (Global.food / 10) - 1;
                System.Random rnd = new System.Random();
                int nrRand = rnd.Next(1, 100);
                Debug.Log(nrRand + " " + Global.pollution / 4);
                if (nrRand < Global.pollution / 2)
                {
                    FindObjectOfType<AudioManager>().Play("EvacuationSound", 0f, 11.5f);
                    causeTornado();
                }
                nrRand = rnd.Next(1, 100);
                if (nrRand < Global.pollution)
                {
                    FindObjectOfType<AudioManager>().Play("EvacuationSound", 0f, 11.5f);
                    causeFloods();
                }

                sourcesText.text = "";
                int nrSources = pollutionSources.Count;
                if (nrSources > 5) nrSources = 5;
                for (int i = pollutionSources.Count - 1; i > pollutionSources.Count - nrSources; i--)
                {
                    sourcesText.text += i + ") " + pollutionSources.GetKey(i) + " " + pollutionSources[pollutionSources.GetKey(i)] + "\n";
                }
                totalPanelText.text = "Overall pollution level: " + Global.pollution.ToString();

            }
        }

        //Debug.Log(Time.time);
    }

    public void causeFloods()
    {
        Debug.Log(nrTilesToFlood);

        for (int i = 0; i < nrTilesToFlood; i++)
        {
            if (i > Global.tileToSink.Count) break;
            float pos = Global.tileHeights[Global.tileToSink.Count - i - 1] ;
            pair tileToFlood = Global.tileToSink[pos];
            Global.tileToSink.Remove(pos);
            Global.tileHeights.Remove(pos);
            int x = tileToFlood.x, y = tileToFlood.y;
            Global.HeightMap[x, y] = 1;
            Global.FeatureMap[x, y] = -1;
            Tile hoverTile = new Tile();

            foreach (Transform child in mapLevels)
            {
                Tilemap locmap = child.gameObject.GetComponent<Tilemap>();
                TilemapRenderer tmRenderer = child.gameObject.GetComponent<TilemapRenderer>();
                if(tmRenderer.sortingOrder == 0)
                {
                    hoverTile.sprite = waterSprite;
                    locmap.SetTile(new Vector3Int(x, y, 0), hoverTile);
                }
                else
                {
                    hoverTile.sprite = blankSprite;
                    locmap.SetTile(new Vector3Int(x, y, 0), hoverTile);
                }
            }
        }
    }

    public void causeTornado()
    {
        System.Random rnd = new System.Random();
        int spawnX = rnd.Next(1, Global.mapWidth/2), spawnY = rnd.Next(1, Global.mapHeight/2);
        GameObject tornado = Instantiate(tornadoPrefab);
        tornado.GetComponent<TornadoBehaviour>().obj = obj.GetComponent<Grid>();
        StartCoroutine(Wait(tornado));
    }

    IEnumerator Wait(GameObject tornado)
    {
        yield return new WaitForSeconds(5);
        Destroy(tornado);
    }

    [System.Serializable]
    public struct buildingIncomes
    {
        public string name;
        public int petrol;
        public int gas;
        public int coal;
        public int biofuel;
        public int uranium;
        public int wood;
        public int stone;
        public int bricks;
        public int metal;
        public int glass;
        public int water;
        public int food;
        public int electricity;
        public float pollution;
    }
}

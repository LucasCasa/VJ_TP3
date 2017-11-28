using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour {
	public Texture2D txt;
	public GameObject tile;
    public static int sizex = 200;
    static int sizey = 200 / 2;
    public int[,] map = new int[sizex, sizey];
    private GameObject[,] terrain = new GameObject[sizex, sizey];
    public Sprite floor;
    public Sprite empty;
    public Sprite ladder;
    Sprite[] sprites;
	public GameObject enemyPrefab;
    List<GameObject> enemies = new List<GameObject>();
    private bool alreadyGenerated = false;
	// Use this for initialization
	enum Tiles{BORDER1, BORDER2, BOTTOM, BOX, CORNER, TOP, TOP_WITH_FLAG, TOP_WITHOUT_FLAG, FLOOR};

    void Start() {
        sprites = Resources.LoadAll<Sprite>(txt.name);
    }

    void Update() {
    }

    public void LoadConnectedMap(int rooms, int min, int variance, int enemies) {
        KillRemainingEnemies();
        ClearMap();
        List<Vector2> seeds = new List<Vector2> {
            new Vector2(1, 1)
        };
        List<Vector2> floor = new List<Vector2>();
        for (int n = 0; n < rooms; n++) {
            int pos = (int)Random.Range(0, seeds.Count);
            Vector2 newAreaCenter = seeds[pos];
            seeds.RemoveAt(pos);
            int width = Random.Range(min, min + variance * 2);
            int height = Random.Range(min, min + variance);
            int startX = Mathf.Max(1, (int) newAreaCenter.x);
            int startY = Mathf.Max(1, (int) newAreaCenter.y);
            Debug.Log("Placing new Room");
            PlaceNewRoom(startX, startY, width, height, seeds, floor, n == 0);
        }
        FindPlaceForStair(floor, rooms);
        FindPlaceForEnemies(floor, enemies);
    }

    private void ClearMap() {
        if (!alreadyGenerated)
            return;

        for(int i = 0; i<sizex; i++) {
            for(int j = 0; j< sizey; j++) {
                map[i, j] = 0;
            }
        }
    }

    private void KillRemainingEnemies() {
        for (int i = 0; i< enemies.Count; i++) {
            Destroy(enemies[i]);
        }
        enemies.Clear();
    }

    /**
*  The rooms always goes to the right of the startX, but can go down or up, depending on a random
*/
    private void PlaceNewRoom(int startX, int startY, int width, int height, List<Vector2> seeds, List<Vector2> floor, bool forceDown) {
        bool toTop = Random.value > 0.5f;
        int increment = 1;
        if (toTop && !forceDown) {
            height *= -1;
            increment = -1;
        }
        Debug.Log("Placing new Room1");
        for (int i = startX - 1; i < startX + width && i < sizex; i++) {
            Debug.Log("Placing new Room2");
            for (int j = startY - increment; condition(j, startY, height); j+= increment) {
                Debug.Log("Placing new Room3");
                if (i == startX - 1 || j == startY - increment || j == startY + height - increment || i == startX + width - 1 || i == 0 || j == 0 || i == sizex - 1 || j == sizey - 1) {
                    if (map[i, j] != 1) {
                        map[i, j] = 2;
                        if (i != startX - 1 && j != startY - 1)
                            seeds.Add(new Vector2(i, j));
                    }
                } else if (j > 0 && j < sizey && i > 0 && i < sizex) {
                    if (map[i, j] == 0)
                        floor.Add(new Vector2(i, j));
                    map[i, j] = 1;
                    seeds.Remove(new Vector2(i, j));

                }
            }
        }
        Debug.Log("Floor Size:" + floor.Count);
    }

    private bool condition(int j, int startY, int height) {
        return j < sizey && j >= 0 && ((height < 0) ? j > startY + height : j < startY + height);
    }
    private void FindPlaceForStair(List<Vector2> floor, int rooms) {
        Debug.Log(floor.Count);
        Vector2 pos;
        int rand = 0;
        int max = 0;
        int maxPos = -1;
        int dist = 0;
        int iters = 0; 
        do {
            iters++;
            rand = Random.Range(0, floor.Count);
            pos = floor[rand];
            dist = (int)(pos.x * pos.x + pos.y * pos.y);
            if (dist > max) {
                max = dist;
                maxPos = rand;
            }
        } while (dist < 10 + rooms * 5 && iters < floor.Count*2);
        if(iters == floor.Count * 2) {
            pos = floor[maxPos];
            map[(int)pos.x, (int)pos.y] = 3;
            floor.RemoveAt(rand);
        } else {
            map[(int)pos.x, (int)pos.y] = 3;
            floor.RemoveAt(rand);
        }
        
    }

    private void FindPlaceForEnemies(List<Vector2> floor, int enemies) {
        Vector2 pos;
        int rand = 0;
        for (int i = 0; i < enemies && floor.Count > 0; i++) {
            rand = Random.Range(0, floor.Count);
            pos = floor[rand];
            bool couldPlace = PlaceEnemy(new Vector2Int((int)pos.x, (int)pos.y));
            floor.RemoveAt(rand);
            if (!couldPlace) {
                i--;
            }
        }
    }

    bool PlaceEnemy(Vector2Int pos){
		Vector2 truePosition = new Vector2(0,0);
		bool valid = false;
		if (map [pos.x, pos.y] == 1) {
			truePosition = new Vector2 (pos.x, -1.5f * pos.y);
			valid = true;
		} else {
			if (map [pos.x + 1, pos.y + 1] == 1) {
				valid = true;
				truePosition = new Vector2 (pos.x + 1, -1.5f * (pos.y + 1));
			} else if (map [pos.x - 1, pos.y - 1] == 1) {
				valid = true;
				truePosition = new Vector2 (pos.x - 1, -1.5f * (pos.y - 1));
			} else if (map [pos.x + 1, pos.y - 1] == 1) {
				valid = true;
				truePosition = new Vector2 (pos.x + 1, -1.5f * (pos.y - 1));
			} else if (map [pos.x - 1, pos.y + 1] == 1) {
				valid = true;
				truePosition = new Vector2 (pos.x - 1, -1.5f * (pos.y + 1));
			}
		}
		if (valid) {
			GameObject go = GameObject.Instantiate (enemyPrefab) as GameObject;
			go.transform.position = truePosition;
            enemies.Add(go);
		}
		return valid;
	}
	void LoadRandomMap (int rooms, int min, int variance) {
		for (int n = 0; n < rooms; n++) {
			int startX = (int)(Random.value * 180 + 1);
			int startY = (int)(Random.value * 180 + 1);
			int sizeX = (int)(Random.value * variance + min);
			int sizeY = (int)(Random.value * variance + min);
			for (int i = startX; i < startX + sizeX; i++) {
				for (int j = startY; j < startY + sizeY; j++) {
					if (i == startX || j == startY || j == startY + sizeY - 1 || i == startX + sizeX - 1) {
						if (map [i, j] != 1) {
							map [i, j] = 2;
						}
					} else {
						map [i, j] = 1;
					}
				}
			}
		}
	}
    
	public void FillWorld () {
		for (int i = 0; i < sizex; i++) {
			for (int j = 0; j < sizey; j++) {
                switch(map[i, j]) {
                    case 0:
                        CreateEmpty(i, j);
                        break;
                    case 1:
                        CreateFloor(i, j);
                        break;
                    case 2:
                        CalculateTypeOfWall(i, j);
                        break;
                    case 3:
                        CreateStair(i, j);
                        break;
                }
			}
		}
        alreadyGenerated = true;
	}

    private void CreateStair(int i, int j) {
        LoadTerrain(i, j, ladder);
    }

    void CreateEmpty (int i, int j) {
        //LoadTerrain(i, j, sprites[(int)Tiles.BORDER2]);
        LoadTerrain(i, j, empty);
    }

	void CreateFloor (int i, int j) {
        //LoadTerrain(i, j, sprites[(int)Tiles.FLOOR]);
        LoadTerrain(i, j, floor);
	}

	void CalculateTypeOfWall (int i, int j) { // Incredible complicated, don't touch
        if (j - 1 < 0) {
            if (i - 1 < 0 || i + 1 >= sizex || map[i, j + 1] == 2) {
                CreateBorder(i, j);
            } else {
                CreateTop(i, j);
            }
        } else if(j+1 >= sizey) {
            CreateBottom(i, j);
        } else if(i + 1 >= sizex || i - 1 < 0) {
            if (map[i, j + 1] == 0) {
                CreateBottom(i, j);
            } else {
                CreateBorder(i, j);
            }
        } else if (map[i, j - 1] == 0 || map[i, j - 1] == 2) {
            if (map[i, j + 1] == 2) {
                CreateCorner(i, j);
            } else {
                CreateTop(i, j);
            }
        } else if (map[i, j + 1] == 0) {
            CreateBottom(i, j);
        } else if (map[i, j + 1] == 2) {
            CreateBorder(i, j);

        } else if (map[i, j - 1] == 1) {
            CreateTop(i, j);
        } else if (map[i + 1, j] == 0 || map[i - 1, j] == 0 || map[i, j + 1] == 1) {
            CreateBorder(i, j);
        }
	}

    private void CreateCorner(int i, int j) {
        LoadTerrain(i, j, sprites[(int)Tiles.CORNER]);
    }

    private void CreateTop(int i, int j) {
        Sprite s;
        if (Random.value > 0.3f) {
            if(Random.value > 0.6f) {
                s = sprites[(int)Tiles.TOP];
            } else {
                s = sprites[(int)Tiles.TOP_WITH_FLAG];
            }
        } else {
            s = sprites[(int)Tiles.TOP_WITHOUT_FLAG];
        }
        LoadTerrain(i, j, s);
    }

    private void CreateBorder(int i, int j) {
        Sprite s;
        if(Random.value > 0.5f) {
            s = sprites[(int)Tiles.BORDER1];
        } else {
            s = sprites[(int)Tiles.BORDER2];
        }
        LoadTerrain(i, j, s);
    }

    private void CreateBottom(int i, int j) {
        LoadTerrain(i, j, sprites[(int)Tiles.BOTTOM]);
    }

    private Vector2 matrixToWorld(int i, int j) {
        return new Vector2(i, -j * 1.5f);
    }
    private void LoadTerrain(int i, int j, Sprite s) {
        GameObject o;
        if (alreadyGenerated) {
            o = terrain[i, j];
        } else {
            Vector2 pos = matrixToWorld(i, j);
            o = GameObject.Instantiate(tile, new Vector3(pos.x, pos.y), new Quaternion(0, 0, 0, 0)) as GameObject;
        }
        if (map[i, j] == 2) {
            o.GetComponent<BoxCollider2D>().enabled = true;
            o.layer = 9;
        } else if(map[i, j] < 2){
            o.layer = 8;
        } else {
            o.GetComponent<BoxCollider2D>().enabled = true;
            o.tag = "exit";
        }
        o.GetComponent<SpriteRenderer>().sprite = s;
        terrain[i, j] = o;
    }
    void DebugBoard() {
        for (int i = 0; i < sizex; i++) {
            string line = "";
            for (int j = 0; j < sizey; j++) {
                line = line + " " + map[i, j];
            }
            Debug.Log(line);
        }
    }

}

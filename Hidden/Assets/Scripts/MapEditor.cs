using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class MapEditor : MonoBehaviour
{
    [SerializeField]
    GameObject Char1;
    [SerializeField]
    GameObject Char2;
    private string _sLevelName = "Levels/map.xml";

    WorldManager worldManage;
    private SavableLevel toLoad;

    void Awake()
    {
        worldManage = gameObject.GetComponent<WorldManager>();
    }

    public void LoadFile()
    {

        //read the file
        XmlSerializer levelDeserializer = new XmlSerializer(typeof(SavableLevel));
        FileStream levelReader = new FileStream(Path.Combine(Application.dataPath, _sLevelName), FileMode.Open); // TODO(Julian): Varying filenames
        XmlReader xmlReader = XmlReader.Create(levelReader);
        toLoad = (SavableLevel)levelDeserializer.Deserialize(xmlReader);
        levelReader.Close();
/*
        // TODO(Julian): Destroy the characters!
        SavableEditorBlock[] loadableBlocks = toLoad.levelBlocks;
        toLoad.worldDims = new Vector2(_width, _height);
        for (int i = 0; i < blocks.Length; i++) {
            SavableEditorBlock currBlock = loadableBlocks[i];
            for (int j = 0; j < _spawnablePrefabs.Length; j++) {
                IEditorBlock blockRef = _spawnablePrefabs[j].GetInterface<IEditorBlock>();
                if (blockRef.TypeName == currBlock.type) {
                    GameObject createdObject = (Instantiate(_spawnablePrefabs[j], currBlock.position, Quaternion.Euler(currBlock.rotation)) as GameObject);
                    createdObject.name = currBlock.name;
                    // TODO(Julian): Use the scale!
                    // TODO(Julian): Use the editor pivot!
                    break;
                }
            }
        }*/
    }
    public IntVector GetDim()
    {
        return toLoad.vDim;
    }
    public void SetCharacters()
    {
        Char1.GetComponent<WorldEntity>().Location = toLoad.vChar1StartPos;
        Char2.GetComponent<WorldEntity>().Location = toLoad.vChar2StartPos;
    }
    public void SetEntities()
    {

    }
    public void SetMap()
    {
        Tile[] tMap = toLoad.tMap;
        for (int i = 0; i < tMap.Length; i++)
        {
            switch (toLoad.tMap[i].iTileType)
            {
                case 0:
                    tMap[i].tileType = TileType.Empty;
                    break;
                case 1:
                    tMap[i].tileType = TileType.Floor;
                    break;
                case 2:
                    tMap[i].tileType = TileType.Wall;
                    break;

                default:
                    tMap[i].tileType = TileType.Empty;
                    break;
            }
        }
        worldManage.GenerateBasicMap(tMap);
    }

}

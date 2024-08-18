namespace MapStructure
{
    public class XYPosition
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
    public class MapStruc
    {
        public int LocationID { get; set;}
        public XYPosition Position { get; set;}        
        public int[] EnemiesHere { get; set;}
        public int[] NpcsHere { get; set;}
        public int StoreHere { get; set;}
        public bool PlayerHere { get; set; }
        public MapStruc(int LocationID, XYPosition Position, int[] EnemiesHere,int[] NpcsHere, int StoreHere, bool PlayerHere)
        {
            this.LocationID = LocationID;
            this.Position = Position;
            this.EnemiesHere = EnemiesHere;
            this.NpcsHere = NpcsHere;
            this.StoreHere = StoreHere;
            this.PlayerHere = PlayerHere;
        }
        public override string ToString()
        {
            return $"LocationID: {LocationID}, Enemies: {string.Join(",", EnemiesHere)}, NPCs: {string.Join(",", NpcsHere)}, Store: {StoreHere}";
        }
    }
}
/*
[0][1][][][][]
[][][][][][]
[][][][][][]
[][][][][][]

*/
namespace MapData
{
    using MapStructure;
    using PlayerData;

    public class MapCreation
    {
        public static MapStruc[,] MAPDATA = new MapStruc[10, 10];
        static NPCStructures.NPCStruc[] NPCIDS = NPCData.NPCDefinition.NPCS;

        public static void GENERATEMAPDATA()
        {
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    MAPDATA[x, y] = new MapStruc(
                        LocationID: x * 10 + y,
                        Position: new XYPosition { X = x, Y = y },
                        EnemiesHere: new int[] { },
                        NpcsHere: new int[] { },
                        StoreHere: 0,
                        PlayerHere: false
                    );
                }
            }

            Random random = new Random();

            List<int> npcIndices = new List<int>();
            for (int i = 0; i < NPCIDS.Length; i++)
            {
                npcIndices.Add(i);
            }

            while (npcIndices.Count > 0)
            {
                int RandomIndexX = random.Next(10);
                int RandomIndexY = random.Next(10);
                int Chance = random.Next(100);

                if (Chance > 85)
                {
                    int npcIndex = npcIndices[npcIndices.Count - 1];
                    MapStruc mapLocation = MAPDATA[RandomIndexX, RandomIndexY];

                    List<int> enemiesList = new List<int>(mapLocation.EnemiesHere) { NPCIDS[npcIndex].ID };
                    mapLocation.EnemiesHere = enemiesList.ToArray();

                    npcIndices.RemoveAt(npcIndices.Count - 1);
                }
            }
        }
        public static void REVEALMAP()
        {
            for (int i = 0; i < MapCreation.MAPDATA.GetLength(0); i++)
            {
                string rowLog = "";
                for (int j = 0; j < MapCreation.MAPDATA.GetLength(1); j++)
                {
                    var mapStruc = MapCreation.MAPDATA[i, j];
                    if (PlayerData.playerCharacter.Player.PlayerPosition.PlayerX == j && PlayerData.playerCharacter.Player.PlayerPosition.PlayerY == i)
                    {
                        rowLog += "[PL]";
                    } 
                    else if (mapStruc != null && mapStruc.EnemiesHere.Length > 0)
                    {
                        rowLog += $"[{string.Join(",", mapStruc.EnemiesHere)}] ";
                    }
                    else
                    {
                        rowLog += "[ ] ";
                    }
                }
                Console.WriteLine(rowLog);
            }
        }
        public static MapStruc CHECKLOCATION()
        {
            return MAPDATA[playerCharacter.Player.PlayerPosition.PlayerY, playerCharacter.Player.PlayerPosition.PlayerX];
        }
    }
}

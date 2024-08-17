using Data;
using NPCData;


namespace MapStructure
{
    public class MapStruc
    {
        public int Location { get; set;}
        public int[] Position { get; set;}        
        public int[] EnemiesHere { get; set;}
        public int[] NpcsHere { get; set;}
        public int StoreHere { get; set;}
    }
}
namespace MapData
{
    public class MapCreation
    {
        static MapStruc[] MAPDATA = new MapStruc[100];
        static NPCStructures.NPCStruc[] NPCIDS = NPCData.NPCDefinition.NPCS;
        public void GENERATEMAPDATA(NPCStructures.NPCStruc[] NPCIDS)
        {
            while (NPCIDS.Length > 0)
            {
                Random random = new Random();
                int RandomIndex = random.Next(100);
                if (RandomIndex > 75){

                }
            
            }
        }        
    }

}
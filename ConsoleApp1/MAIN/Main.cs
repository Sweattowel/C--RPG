namespace ConsoleRPG
{
    using MapData;
    using System;
    using PlayerData;
    using ItemStructures;
    using NPCStructures;
    using PlayerStructures;

    public class Mainloop
    {
        public static void Main()
        {
            MapCreation.GENERATEMAPDATA();
            MapCreation.REVEALMAP();
            Console.WriteLine("You find yourself in strange lands");
            EXPLORE();
        }
        public static void REST()
        {
            Console.WriteLine("You sit down to rest, Type 1 to move, type 2 to check items");
            string choice = Console.ReadLine()!;
            switch (choice.ToLower())
            {
                case"1":
                MapCreation.REVEALMAP();
                EXPLORE();
                break;
                case"2":
                for (int i = 0; i < playerCharacter.Player.Inventory.Length; i++)
                {
                    ItemStruct currItem = ItemData.GameItemArray.GameItems.First((item) => item.ItemID == playerCharacter.Player.Inventory[i].ItemID);
                    Console.WriteLine($"NAME: {currItem.ItemName} DESC: {currItem.ItemDesc} VALUE: {currItem.GoldValue} TYPE: {currItem.ItemType} EFFECT: {currItem.Effect} EFFECT POWER: {currItem.EffectValue}");
                }
                REST();
                break;
                default:
                REST();
                break;
            }
        }
        public static void EXPLORE()
        {
            Console.WriteLine("Type the corresponding direction to travel: UP DOWN LEFT RIGHT");
            string choice = Console.ReadLine()!;
            PlayerPosition currPos =  new(playerCharacter.Player.PlayerPosition.PlayerX, playerCharacter.Player.PlayerPosition.PlayerY);
            
            switch (choice.ToUpper()){
                // TODO Generate new map when going up and add to preexisting map if possible
                case "UP":
                    currPos.PlayerY -= 1;
                    playerCharacter.Player.PlayerPosition = MapMove.HandleExplore(currPos);
                break;
                case "LEFT":
                    currPos.PlayerX -= 1;
                    playerCharacter.Player.PlayerPosition = MapMove.HandleExplore(currPos);
                break;
                case "RIGHT":
                    currPos.PlayerX += 1;
                    playerCharacter.Player.PlayerPosition = MapMove.HandleExplore(currPos);
                break;
                case "DOWN":
                    currPos.PlayerY += 1;
                    playerCharacter.Player.PlayerPosition = MapMove.HandleExplore(currPos);

                break;
                default:
                    REST();
                break;
            }
            MapCreation.REVEALMAP();
            MapStructure.MapStruc CurrentLocation = MapCreation.CHECKLOCATION();
            if (CurrentLocation.EnemiesHere.Length > 0){
                var npcHere = NPCData.NPCDefinition.NPCS.FirstOrDefault(n => n.ID == CurrentLocation.EnemiesHere[0]);
                
                NPCStruc.NpcResult result = new()
                {
                    GoldWon = 0,
                    ExperienceWon = 0,
                    ResultDialogue  = ""
                };
                
                switch (npcHere!.Disposition)
                {
                    case > 3:
                        result = NPCStruc.BeginTalk(npcHere.ID);
                    break;
                    default:
                        result = NPCStruc.BeginFight(npcHere.ID);
                        break;
                }
                if (result.ExperienceWon > 0){
                    EXPLORE();
                } else {
                    REST();
                }
            } else {
                EXPLORE();
            }
        }
    }
}

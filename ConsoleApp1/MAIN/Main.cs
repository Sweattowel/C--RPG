namespace ConsoleRPG
{
    using MapData;
    using System;
    using PlayerData;
    using ItemStructures;
    using NPCStructures;

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
            switch (choice.ToUpper()){
                case "UP":
                    playerCharacter.Player.PlayerPosition.PlayerY -= 1;
                break;
                case "LEFT":
                    playerCharacter.Player.PlayerPosition.PlayerX -= 1;
                break;
                case "RIGHT":
                    playerCharacter.Player.PlayerPosition.PlayerX += 1;
                break;
                case "DOWN":
                    playerCharacter.Player.PlayerPosition.PlayerY += 1;

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

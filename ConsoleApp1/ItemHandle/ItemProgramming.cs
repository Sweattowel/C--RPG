namespace ItemStructures
{
    public class ItemStruct
    {
        public int ItemID;
        public string ItemName;
        public string ItemDesc;
        public int GoldValue;
        public string ItemType;
        public string Effect;
        public int EffectValue;
        public ItemStruct(int ItemID, string ItemName, string ItemDesc, int GoldValue, string ItemType, string Effect, int EffectValue)
        {
            this.ItemID = ItemID;
            this.ItemName = ItemName;
            this.ItemDesc = ItemDesc;
            this.GoldValue = GoldValue;
            this.ItemType = ItemType;
            this.Effect = Effect;
            this.EffectValue = EffectValue;
        }
    }
}
namespace ItemData
{
    using ItemStructures;
    public class GameItems
    {
        public static ItemStruct[] GameItem = new ItemStruct[]
        {
            new ItemStruct(
                ItemID:0,
                ItemName:"Trash",
                ItemDesc:"Useless...",
                GoldValue:1,
                ItemType:"MISC",
                Effect:"NONE",
                EffectValue:0
            ),
            new ItemStruct(
                ItemID:1,
                ItemName:"Healing potion",
                ItemDesc:$"A Potion, Use it to recover... 8 HP, Drink it quickly before the bitterness kicks in",
                GoldValue:252,
                ItemType:"POTION",
                Effect:"HEAL",
                EffectValue:8
            ),
            new ItemStruct(
                ItemID:2,
                ItemName:"Jewel",
                ItemDesc:"A shiny jewel, does no damage and is too fragile to be used in anything, sell it to a collector for 50 GOLD",
                GoldValue:50,
                ItemType:"VALU",
                Effect:"NONE",
                EffectValue:0
            ),
            new ItemStruct(
                ItemID:3,
                ItemName:"Scrap",
                ItemDesc:"Appears uselss, but scrap of this quality is easily recycled, sell for a high value of 100 GOLD",
                GoldValue:100,
                ItemType:"MISC",
                Effect:"NONE",
                EffectValue:0
            ),
        };
    }
}
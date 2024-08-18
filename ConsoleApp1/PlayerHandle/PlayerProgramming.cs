namespace PlayerStructures
{
    public class AttackStruc 
    {
        public int AttackID { get; set; }
        public int AttackDamage { get; set; }
        public string AttackName { get; set; }
        public int CurrentCoolDown { get; set; }
        public int AttackCooldown { get; set; }
        public string AttackActionDialogue { get; set; }
        public AttackStruc(int AttackID,int AttackDamage, string AttackName, int CurrentCoolDown, int AttackCooldown, string AttackActionDialogue){
            this.AttackID = AttackID;
            this.AttackDamage = AttackDamage;
            this.AttackName = AttackName;
            this.CurrentCoolDown = CurrentCoolDown;
            this.AttackCooldown = AttackCooldown;
            this.AttackActionDialogue = AttackActionDialogue;
        }
    }
    public class PlayerInventoryItem
    {
        public int ItemID { get; set; }
        public int ItemCount { get; set;}
        public PlayerInventoryItem(int ItemID, int ItemCount)
        {
            this.ItemID = ItemID;
            this.ItemCount = ItemCount;
        }
    }
    public class PlayerPosition
    {
        public int PlayerX { get; set; }
        public int PlayerY { get; set; }
        public PlayerPosition(int PlayerX, int PlayerY)
        {
            this.PlayerX = PlayerX;
            this.PlayerY = PlayerY;

        }
    }
    public class PlayerStruc
    {
        public int PlayerHealth { get; set; }
        public int Gold { get; set; }
        public int Experience { get; set; }
        public int Level { get; set; }
        public int Reputation { get; set; }
        public int Speed { get; set; }
        public PlayerPosition PlayerPosition { get; set; }
        public PlayerInventoryItem[] Inventory { get; set; }
        public string PlayerName { get; set; }
        public AttackStruc[] PlayerAttacks { get; set; }
        public PlayerStruc(int PlayerHealth, int Gold, int Experience, int Level, int Reputation, int Speed, PlayerPosition PlayerPosition, PlayerInventoryItem[] Inventory, string PlayerName, AttackStruc[] PlayerAttacks)
        {
            this.PlayerHealth = PlayerHealth;
            this.Gold = Gold;
            this.Experience = Experience;
            this.Level = Level;
            this.Reputation = Reputation;
            this.Speed = Speed;
            this.PlayerPosition = PlayerPosition;
            this.Inventory = Inventory;
            this.PlayerName = PlayerName;
            this.PlayerAttacks = PlayerAttacks;
        }
    }
}
namespace PlayerData
{
    using PlayerStructures;

    public class playerCharacter
    {
        public static PlayerStruc Player = new PlayerStruc(
            PlayerHealth: 15,
            Gold: 0,
            Experience: 0,
            Level: 0,
            Reputation: 0,
            Speed: 10,
            PlayerPosition: new PlayerPosition
            (
                PlayerX: 0,
                PlayerY: 0
            ),
            Inventory: new PlayerInventoryItem[]
            {
                new PlayerInventoryItem(ItemID:0, ItemCount: 2)
            },
            PlayerName: "Thoams",
            PlayerAttacks: new AttackStruc[]
            {
                new AttackStruc(AttackID: 1, AttackDamage: 1, AttackName: "Bite", CurrentCoolDown: 0, AttackCooldown: 1, AttackActionDialogue: "ORY yum!"),
                new AttackStruc(AttackID: 2, AttackDamage: 3, AttackName: "Slash", CurrentCoolDown: 0, AttackCooldown: 2, AttackActionDialogue: "ORY yum!"),
                new AttackStruc(AttackID: 1, AttackDamage: 2, AttackName: "Roar", CurrentCoolDown: 0, AttackCooldown: 3, AttackActionDialogue: "ORY yum!"),
                new AttackStruc(AttackID: 4, AttackDamage: 5, AttackName: "Kick", CurrentCoolDown: 0, AttackCooldown: 5, AttackActionDialogue: "ORY yum!")
            }
        );
    }
}

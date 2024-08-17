namespace PlayerStructures
{
    public class AttackStruc 
    {
        public int AttackID { get; set; }
        public string AttackName { get; set; }
        public int CurrentCoolDown { get; set; }
        public int AttackCooldown { get; set; }
        public string AttackActionDialogue { get; set; }
        public AttackStruc(int AttackID, string AttackName, int CurrentCoolDown, int AttackCooldown, string AttackActionDialogue)
        {
            this.AttackID = AttackID;
            this.AttackName = AttackName;
            this.CurrentCoolDown = CurrentCoolDown;
            this.AttackCooldown = AttackCooldown;
            this.AttackActionDialogue = AttackActionDialogue;
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
        public string PlayerName { get; set; }
        public AttackStruc[] PlayerAttacks { get; set; }
        public PlayerStruc(int PlayerHealth, int Gold, int Experience, int Level, int Reputation, int Speed, string PlayerName, AttackStruc[] PlayerAttacks)
        {
            this.PlayerHealth = PlayerHealth;
            this.Gold = Gold;
            this.Experience = Experience;
            this.Level = Level;
            this.Reputation = Reputation;
            this.Speed = Speed;
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
            PlayerName: "Thoams",
            PlayerAttacks: new AttackStruc[]
            {
                new AttackStruc(AttackID: 0, AttackName: "Bite", CurrentCoolDown: 0, AttackCooldown: 2, AttackActionDialogue: "ORY yum!")
            }
        );
    }
}

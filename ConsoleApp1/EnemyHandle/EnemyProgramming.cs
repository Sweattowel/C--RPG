namespace NpcHandle
{
    public class AttackStruc
    {
        public int AttackID { get; set; }
        public string AttackName { get; set; }
        public int AttackDamage { get; set; }
        public int CoolDown { get; set; }
        public int CurrentCoolDown { get; set; }

        public AttackStruc(int attackID, string attackName, int attackDamage, int coolDown, int currentCoolDown)
        {
            this.AttackID = attackID;
            this.AttackName = attackName;
            this.AttackDamage = attackDamage;
            this.CoolDown = coolDown;
            this.CurrentCoolDown = currentCoolDown;
        }
    }
    public class DialogueChoices 
    {
        public string failTalk;
        public string succTalk; 
        public string[] talk;
    }

    public class NpcStruc
    {
        public int NpcID { get; set; }
        public int NpcHealth { get; set; }
        public string NpcName { get; set; }
        public int gold { get; set; }
        public string Disposition { get; set; }
        public AttackStruc[] Attacks { get; set; }
        public int[] position { get; set; }
        public int Initiative { get; set; }
        public string[] AggressionStatement { get; set; }
        public DialogueChoices NpcDialogue { get; set;}
        public bool Defeated { get; set;}
        public NpcStruc(int NpcID, int NpcHealth, string NpcName, int gold, string disposition, AttackStruc[] attacks, int[] position, int initiative, string[] AggressionStatement, DialogueChoices NpcDialogue,bool Defeated)
        {
            this.NpcID = NpcID;
            this.NpcHealth = NpcHealth;
            this.NpcName = NpcName;
            this.gold = gold;
            this.Disposition = disposition;
            this.Attacks = attacks;
            this.position = position;
            this.Initiative = initiative;
            this.AggressionStatement = AggressionStatement;
            this.Defeated = Defeated;
            this.NpcDialogue = NpcDialogue;
        }
    }

}
namespace NpcData
{
    
}
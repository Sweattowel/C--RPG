namespace EnemyHandle
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

    public class EnemyStruc
    {
        public int EnemyID { get; set; }
        public int EnemyHealth { get; set; }
        public string EnemyName { get; set; }
        public int gold { get; set; }
        public string Disposition { get; set; }
        public AttackStruc[] Attacks { get; set; }
        public int[] position { get; set; }
        public int Initiative { get; set; }
        public string[] AggressionStatement { get; set; }
        public DialogueChoices EnemyDialogue { get; set;}
        public bool Defeated { get; set;}
        public EnemyStruc(int enemyID, int enemyHealth, string enemyName, int gold, string disposition, AttackStruc[] attacks, int[] position, int initiative, string[] AggressionStatement, DialogueChoices EnemyDialogue,bool Defeated)
        {
            this.EnemyID = enemyID;
            this.EnemyHealth = enemyHealth;
            this.EnemyName = enemyName;
            this.gold = gold;
            this.Disposition = disposition;
            this.Attacks = attacks;
            this.position = position;
            this.Initiative = initiative;
            this.AggressionStatement = AggressionStatement;
            this.Defeated = Defeated;
            this.EnemyDialogue = EnemyDialogue;
        }
    }

}
namespace EnemyData
{
    
}
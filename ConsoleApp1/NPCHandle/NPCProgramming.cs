using NPCStructures;

namespace NPCStructures
{
    public class AttackStruc 
    {
        public int AttackID { get; set; }
        public string AttackName { get; set; }
        public int CurrentCoolDown { get; set; }
        public int AttackCooldown { get; set; }
        public string AttackActionDialogue { get; set; }
        public AttackStruc(int AttackID, string AttackName, int CurrentCoolDown, int AttackCooldown, string AttackActionDialogue){
            this.AttackID = AttackID;
            this.AttackName = AttackName;
            this.CurrentCoolDown = CurrentCoolDown;
            this.AttackCooldown = AttackCooldown;
            this.AttackActionDialogue = AttackActionDialogue;
        }
    }
    public class BaseNPCResponseStruc
    {
        public string[] Hate { get; set;}
        public string[] Neutral { get; set;}
        public string[] Like { get; set;}
        public BaseNPCResponseStruc(string[] Hate, string[] Neutral, string[] Like){
            this.Hate = Hate;
            this.Neutral = Neutral;
            this.Like = Like;
        }
    }
    public class DialogueTree
    {
        public int DialogueID { get; set; }
        public string DialogueText { get; set; }
        public string[] Responses { get; set; }
        public DialogueTree(int DialogueID, string DialogueText, string[] Responses){
            this.DialogueID = DialogueID;
            this.DialogueText = DialogueText;
            this.Responses = Responses;
        }
    }
    public class NPCStruc
    {
        public int ID { get; set;}
        public string Name { get; set; }
        public int Disposition { get; set; }
        public int Health { get; set; }
        public int Speed { get; set; }
        public int Gold { get; set; }
        public int Experience { get; set; }
        public BaseNPCResponseStruc BaseResponse { get; set; }
        public DialogueTree[] DialogueTrees { get; set; }
        public AttackStruc[] Attacks { get; set; }
        public NPCStruc(int ID, string Name, int Disposition, BaseNPCResponseStruc BaseResponse, AttackStruc[] Attacks, DialogueTree[] dialogueTrees, int Health, int Speed, int Gold, int Experience){
            this.ID = ID;
            this.Name = Name;
            this.Disposition = Disposition;
            this.BaseResponse = BaseResponse;
            this.Attacks = Attacks;
            this.DialogueTrees = DialogueTrees;
            this.Health = Health;
            this.Speed = Speed;
            this.Gold = Gold;
            this.Experience = Experience;
        }
        public class BattleResult
        {
            public int GoldWon { get; set; }
            public int ExperienceWon { get; set; }
            public String ResultDialogue { get; set; }
        }
        public static BattleResult BeginFight(PlayerStructures.PlayerStruc Player, NPCStruc Enemy){
            Boolean playerTurn = false;
            Boolean Win = false;
            if (Player.Speed > Enemy.Speed){
                playerTurn = true;
            }
            while (Player.PlayerHealth > 0 && Enemy.Health > 0)
            {
                if (playerTurn)
                {

                }
                if (!playerTurn)
                {

                }
            }
            if (Win){
                return new BattleResult() 
                {
                    GoldWon = Enemy.Gold, 
                    ExperienceWon = Enemy.Experience, 
                    ResultDialogue = Enemy.BaseResponse.Hate[0]
                };
            } else {
                return new BattleResult()
                {
                    GoldWon = 0,
                    ExperienceWon = 0,
                    ResultDialogue = "You have been defeated..."
                };
            }
        }
        public static void BeginTalk(){

        }        
    }
}
namespace NPCData
{
    public class NPCDefinition
    {
        public static NPCStructures.NPCStruc[] NPCS  = new NPCStructures.NPCStruc[] 
        {
            new NPCStructures.NPCStruc(
                ID: 0,
                Name: "Jeeves",
                Disposition: 10, 
                Health: 40,
                Speed: 25,
                Gold: 450,
                Experience: 8,
                BaseResponse: new NPCStructures.BaseNPCResponseStruc(
                    Hate: ["test H"],
                    Neutral: ["test N"],
                    Like: ["test L"]
                ),
                Attacks: new AttackStruc[]
                {
                    new AttackStruc(AttackID: 0, AttackName: "Bite", CurrentCoolDown: 0, AttackCooldown: 1, AttackActionDialogue: "Bites down HARD"),
                    new AttackStruc(AttackID: 1, AttackName: "Slash", CurrentCoolDown: 0, AttackCooldown: 2, AttackActionDialogue: "Swings wildly with a sharp claw"),
                    new AttackStruc(AttackID: 2, AttackName: "Roar", CurrentCoolDown: 0, AttackCooldown: 3, AttackActionDialogue: "Lets out a terrifying roar"),
                    new AttackStruc(AttackID: 3, AttackName: "Sting", CurrentCoolDown: 0, AttackCooldown: 1, AttackActionDialogue: "Stings with a venomous barb"),
                    new AttackStruc(AttackID: 4, AttackName: "Charge", CurrentCoolDown: 0, AttackCooldown: 4, AttackActionDialogue: "Charges forward with great force")
                },
                dialogueTrees: new NPCStructures.DialogueTree[]
                {
                    new NPCStructures.DialogueTree(DialogueID: 0, DialogueText: "Hey Boyo", Responses: ["Hey Jeevs How are you", "Fuck off", "Bye"]),
                }
            ),
            new NPCStructures.NPCStruc(
                ID: 1,
                Name: "Jason",
                Disposition: 5,  
                Health: 8,
                Speed: 8,
                Gold: 50,
                Experience: 8,     
                BaseResponse: new NPCStructures.BaseNPCResponseStruc(
                    Hate: ["test H"],
                    Neutral: ["test N"],
                    Like: ["test L"]
                ),
                Attacks: new AttackStruc[]
                {
                    new AttackStruc(AttackID: 0, AttackName: "Bite", CurrentCoolDown: 0, AttackCooldown: 1, AttackActionDialogue: "Bites down HARD"),
                    new AttackStruc(AttackID: 1, AttackName: "Slash", CurrentCoolDown: 0, AttackCooldown: 2, AttackActionDialogue: "Swings wildly with a sharp claw"),
                    new AttackStruc(AttackID: 2, AttackName: "Roar", CurrentCoolDown: 0, AttackCooldown: 3, AttackActionDialogue: "Lets out a terrifying roar"),
                    new AttackStruc(AttackID: 3, AttackName: "Sting", CurrentCoolDown: 0, AttackCooldown: 1, AttackActionDialogue: "Stings with a venomous barb"),
                    new AttackStruc(AttackID: 4, AttackName: "Charge", CurrentCoolDown: 0, AttackCooldown: 4, AttackActionDialogue: "Charges forward with great force")
                },
                dialogueTrees: new NPCStructures.DialogueTree[]
                {
                    new NPCStructures.DialogueTree(DialogueID: 0, DialogueText: "sup...", Responses: ["Hey...", "Fight?", "Bye"]),
                }
            ),
            new NPCStructures.NPCStruc(
                ID: 2,
                Name: "Jona",
                Disposition: 0, 
                Health: 120,
                Speed: 4,
                Gold: 100,
                Experience: 8,
                BaseResponse: new NPCStructures.BaseNPCResponseStruc(
                    Hate: ["test H"],
                    Neutral: ["test N"],
                    Like: ["test L"]
                ),
                Attacks: new AttackStruc[]
                {
                    new AttackStruc(AttackID: 0, AttackName: "Bite", CurrentCoolDown: 0, AttackCooldown: 1, AttackActionDialogue: "Bites down HARD"),
                    new AttackStruc(AttackID: 1, AttackName: "Slash", CurrentCoolDown: 0, AttackCooldown: 2, AttackActionDialogue: "Swings wildly with a sharp claw"),
                    new AttackStruc(AttackID: 2, AttackName: "Roar", CurrentCoolDown: 0, AttackCooldown: 3, AttackActionDialogue: "Lets out a terrifying roar"),
                    new AttackStruc(AttackID: 3, AttackName: "Sting", CurrentCoolDown: 0, AttackCooldown: 1, AttackActionDialogue: "Stings with a venomous barb"),
                    new AttackStruc(AttackID: 4, AttackName: "Charge", CurrentCoolDown: 0, AttackCooldown: 4, AttackActionDialogue: "Charges forward with great force")
                },
                dialogueTrees: new NPCStructures.DialogueTree[]
                {
                    new NPCStructures.DialogueTree(DialogueID: 0, DialogueText: "Go away?", Responses: ["Rude...", "Ill kick your ass", "Bye"]),
                }
            ),
        };
    }
    
}
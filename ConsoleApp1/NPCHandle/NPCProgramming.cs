using ItemData;
using ItemStructures;

namespace NPCStructures
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
    public class ResponseOption
    {
        public string ResponseText { get; set; }
        public int NextDialogueID { get; set; }

        public ResponseOption(string responseText, int nextDialogueID)
        {
            this.ResponseText = responseText;
            this.NextDialogueID = nextDialogueID;
        }
    }
    public class DialogueTree
    {
        public int DialogueID { get; set; }
        public string DialogueText { get; set; }        
        public ResponseOption[] Responses { get; set; }

        public DialogueTree(int DialogueID, string DialogueText, ResponseOption[] Responses)
        {
            this.DialogueID = DialogueID;
            this.DialogueText = DialogueText;
            this.Responses = Responses;
        }
    }
    public class NPCInventoryItem
    {
        public int ItemID { get; set; }
        public int ItemCount { get; set;}
        public NPCInventoryItem(int ItemID, int ItemCount)
        {
            this.ItemID = ItemID;
            this.ItemCount = ItemCount;
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
        public NPCInventoryItem[] Inventory { get; set; }
        public BaseNPCResponseStruc BaseResponse { get; set; }
        public DialogueTree[] DialogueTrees { get; set; }
        public AttackStruc[] Attacks { get; set; }
        public NPCStruc(int ID, string Name, int Disposition, BaseNPCResponseStruc BaseResponse, AttackStruc[] Attacks, DialogueTree[] DialogueTrees, int Health, int Speed, int Gold, int Experience, NPCInventoryItem[] Inventory){
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
            this.Inventory = Inventory;
        }
        public class BattleResult
        {
            public int GoldWon { get; set; }
            public int ExperienceWon { get; set; }
            public required string ResultDialogue { get; set; }
        }
        public static BattleResult BeginFight(PlayerStructures.PlayerStruc Player, NPCStruc Enemy){
            int EnemyMaxHealth = Enemy.Health;
            bool playerTurn = false;
            bool Win = false;
            Console.WriteLine($"You encounter an enemy {Enemy.Name}, ");
            if (Player.Speed > Enemy.Speed){
                playerTurn = true;
                Console.WriteLine("You are faster, first turn is yours");    
            } else {
                Console.WriteLine("You're too SLOW, Watch out!'");
            }
            while (Player.PlayerHealth > 0 && Enemy.Health > 0)
            {
                if (playerTurn)
                {   
                    Console.WriteLine("PLAYER TURN");
                    Console.WriteLine("Type 1 to choose Attack, Type 2 to check and use Items, Type 3 to talk, Type 4 to attempt to flee");
                    string response = Console.ReadLine()!;
                    switch (response)
                    {
                        case "1":
                            break;
                        default:
                            break;
                    }
                }
                if (!playerTurn)
                {
                    Console.WriteLine($"{Enemy.Name}'S TURN");
                    if (Enemy.Health <= EnemyMaxHealth / 2 && Enemy.Inventory.Length > 0){
                        for (int i = 0; i < Enemy.Inventory.Length ; i++)
                        {
                            ItemStruct Heal = GameItemArray.GameItems.First((item) => (Enemy.Inventory[i].ItemID == item.ItemID) && (item.Effect == "HEAL"));
                            if (Heal != null)
                            {
                                Enemy.Health = Math.Min(EnemyMaxHealth, Enemy.Health += Heal.EffectValue);
                                Enemy.Inventory[i].ItemCount -= 1;
                                if (Enemy.Inventory[i].ItemCount <= 0) {
                                    Enemy.Inventory = Enemy.Inventory.Where(item => item.ItemCount > 0).ToArray();
                                }
                                Console.WriteLine($"{Enemy.Name} Has healed {Heal.EffectValue} to {Enemy.Health}");
                                break;
                            }
                        }
                    }
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
    using NPCStructures;
    using PlayerData;
    public class NPCDefinition
    {
        public static NPCStruc[] NPCS  = new NPCStruc[] 
        {
            new NPCStruc(
                ID: 0,
                Name: "Jeeves",
                Disposition: 10, 
                Health: 40,
                Speed: 25,
                Gold: 450,
                Experience: 8,
                Inventory: new NPCInventoryItem[] {
                    new NPCInventoryItem(ItemID: 1, ItemCount: 4),
                }, 
                BaseResponse: new BaseNPCResponseStruc(
                    Hate: ["I'm dissapointed"],
                    Neutral: ["..."],
                    Like: ["Good Day"]
                ),
                Attacks: new AttackStruc[]
                {
                    new AttackStruc(AttackID: 0, AttackDamage: 2, AttackName: "Artful bite", CurrentCoolDown: 0, AttackCooldown: 1, AttackActionDialogue: "Bites with poise and class"),
                    new AttackStruc(AttackID: 1, AttackDamage: 3, AttackName: "High class High Slash", CurrentCoolDown: 0, AttackCooldown: 2, AttackActionDialogue: "Slashes with a rehearsed swing"),
                    new AttackStruc(AttackID: 2, AttackDamage: 1, AttackName: "Stinging Words", CurrentCoolDown: 0, AttackCooldown: 3, AttackActionDialogue: "Condemns Your actions from a higher caste"),
                    new AttackStruc(AttackID: 3, AttackDamage: 1, AttackName: "Roulette Kick", CurrentCoolDown: 0, AttackCooldown: 1, AttackActionDialogue: "Roulette Kick"),
                    new AttackStruc(AttackID: 4, AttackDamage: 1, AttackName: "Accident", CurrentCoolDown: 0, AttackCooldown: 4, AttackActionDialogue: "Gets into his rolls royce and Runs you over")
                },
                DialogueTrees: new DialogueTree[]
                {
                    new DialogueTree
                    (
                        DialogueID: 0, 
                        DialogueText: $"Evening {playerCharacter.Player.PlayerName}", 
                        Responses: new ResponseOption[] 
                        { 
                            new ResponseOption("Goodbye", 1),
                            new ResponseOption("Hey Jeeves, how are you?", 2),
                            new ResponseOption("Leave me alone.", 3)
                        }
                    ),
                    new DialogueTree
                    (
                        DialogueID: 1, 
                        DialogueText: "Goodbye!", 
                        Responses: new ResponseOption[] { }
                    ),
                    new DialogueTree
                    (
                        DialogueID: 2, 
                        DialogueText: "I'm well, thank you.", 
                        Responses: new ResponseOption[]
                        {
                            new ResponseOption("Great!", 3),
                            new ResponseOption("Whatever.", 3)
                        }
                    ),
                    new DialogueTree
                    (
                        DialogueID: 3, 
                        DialogueText: "That's rude!", 
                        Responses: new ResponseOption[]
                        {
                            new ResponseOption("Sorry.", 3),
                            new ResponseOption("I don't care.", 3)
                        }
                    ),
                }
            ),
            new NPCStruc(
                ID: 1,
                Name: "Jason",
                Disposition: 7,  
                Health: 8,
                Speed: 8,
                Gold: 0,
                Experience: 14,
                Inventory: new NPCInventoryItem[] {
                    new NPCInventoryItem(ItemID: 3, ItemCount: 1),
                },    
                BaseResponse: new BaseNPCResponseStruc(
                    Hate: ["Not cool, hope your head starts hurting"],
                    Neutral: ["I hope my head doesnt start hurting"],
                    Like: ["Hope your head doesnt start hurting"]
                ),
                Attacks: new AttackStruc[]
                {
                    new AttackStruc(AttackID: 0, AttackDamage: 1, AttackName: "Artful bite", CurrentCoolDown: 0, AttackCooldown: 1, AttackActionDialogue: "Bites your head"),
                    new AttackStruc(AttackID: 1, AttackDamage: 3, AttackName: "Slash", CurrentCoolDown: 0, AttackCooldown: 2, AttackActionDialogue: "Swings at your head"),
                    new AttackStruc(AttackID: 2, AttackDamage: 1, AttackName: "Roar", CurrentCoolDown: 0, AttackCooldown: 2, AttackActionDialogue: "Yells in your ear thats connected to your head"),
                    new AttackStruc(AttackID: 3, AttackDamage: 2, AttackName: "Sting", CurrentCoolDown: 0, AttackCooldown: 1, AttackActionDialogue: "Stings your head with a stinging front fist"),
                    new AttackStruc(AttackID: 4, AttackDamage: 4, AttackName: "Hit", CurrentCoolDown: 0, AttackCooldown: 4, AttackActionDialogue: "Hits you, you most likely know where")
                },
                DialogueTrees: new DialogueTree[]
                {
                    new DialogueTree
                    (
                        DialogueID: 0, 
                        DialogueText: "Whats up brah", 
                        Responses: new ResponseOption[]
                        {
                            new ResponseOption("Goodbye.", 1),
                            new ResponseOption("Got hit in the head recently", 2)
                        }
                    ),
                    new DialogueTree
                    (
                        DialogueID: 1, 
                        DialogueText: "Catch ya", 
                        Responses: new ResponseOption[]
                        {

                        }
                    ),
                    new DialogueTree
                    (
                        DialogueID: 2, 
                        DialogueText: "Ouch.", 
                        Responses: new ResponseOption[]
                        {
                            new ResponseOption("Goodbye.", 1),
                            new ResponseOption("I got hit in the head ya know?", 3)
                        }
                    ),
                    new DialogueTree
                    (
                        DialogueID: 3, 
                        DialogueText: "Sucks man", 
                        Responses: new ResponseOption[]
                        {
                            new ResponseOption("Goodbye.", 1),
                            new ResponseOption("Head hurts", 4)
                        }
                    ),
                    new DialogueTree
                    (
                        DialogueID: 4, 
                        DialogueText: "Damn what happened dude?", 
                        Responses: new ResponseOption[]
                        {
                            new ResponseOption("Goodbye.", 1),
                            new ResponseOption("i got hit in the head or something like that", 2)
                        }
                    ),
                }
            ),
            new NPCStruc(
                ID: 2,
                Name: "Jona",
                Disposition: 0, 
                Health: 120,
                Speed: 4,
                Gold: 100,
                Experience: 8,
                Inventory: new NPCInventoryItem[] {
                    new NPCInventoryItem(ItemID: 1, ItemCount: 1),
                    new NPCInventoryItem(ItemID: 2, ItemCount: 2),
                }, 
                BaseResponse: new BaseNPCResponseStruc(
                    Hate: ["..."],
                    Neutral: ["I neutrally dont like you"],
                    Like: ["... cya"]
                ),
                Attacks: new AttackStruc[]
                {
                    new AttackStruc(AttackID: 0, AttackDamage: 2, AttackName: "Punch", CurrentCoolDown: 0, AttackCooldown: 2, AttackActionDialogue: "Punches you in the teeth"),
                    new AttackStruc(AttackID: 1, AttackDamage: 2, AttackName: "Cut", CurrentCoolDown: 0, AttackCooldown: 2, AttackActionDialogue: "Cuts deep with his blade"),
                    new AttackStruc(AttackID: 2, AttackDamage: 5, AttackName: "Kick", CurrentCoolDown: 0, AttackCooldown: 3, AttackActionDialogue: "Hits you with a kick to the solar plexus"),
                },
                DialogueTrees: new DialogueTree[]
                {
                    new DialogueTree
                    (
                        DialogueID: 0, 
                        DialogueText: "Go away", 
                        Responses: new ResponseOption[]
                        {
                            new ResponseOption("Goodbye.", 1),
                            new ResponseOption("ermm i..", 2)
                        }
                    ),
                    new DialogueTree
                    (
                        DialogueID: 1, 
                        DialogueText: "...", 
                        Responses: new ResponseOption[]
                        {

                        }
                    ),
                    new DialogueTree
                    (
                        DialogueID: 2, 
                        DialogueText: "Dont pick stupid dialogue options that have clear cutoffs", 
                        Responses: new ResponseOption[]
                        {

                        }
                    ),
                }
            ),
        };
    }
    
}
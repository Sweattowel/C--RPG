using ItemData;
using ItemStructures;
using PlayerStructures;

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

        public string Action {get; set;}
        public ResponseOption(string responseText, int nextDialogueID, string Action)
        {
            this.ResponseText = responseText;
            this.NextDialogueID = nextDialogueID;
            this.Action = Action;
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
        public static BattleResult BeginFight(int EnemyID){
            int PlayerMaxHealth = PlayerData.playerCharacter.Player.PlayerHealth;
            int EnemyMaxHealth = NPCData.NPCDefinition.NPCS.First((npc) => npc.ID == EnemyID).Health;
            bool playerTurn = false;
            bool Win = false;
            bool fightCancel = false;

            Console.WriteLine($"You encounter an enemy {NPCData.NPCDefinition.NPCS.First((npc) => npc.ID == EnemyID).Name}, ");
            if (PlayerData.playerCharacter.Player.Speed > NPCData.NPCDefinition.NPCS.First((npc) => npc.ID == EnemyID).Speed){
                playerTurn = true;
                Console.WriteLine("You are faster, first turn is yours");    
            } else {
                Console.WriteLine("You're too SLOW, Watch out!'");
            }
            while (PlayerData.playerCharacter.Player.PlayerHealth > 0 && NPCData.NPCDefinition.NPCS.First((npc) => npc.ID == EnemyID).Health > 0 && fightCancel == false)
            {
                while (playerTurn && !fightCancel)
                {   
                    Console.WriteLine("PLAYER TURN");
                    Console.WriteLine("Type 1 to choose Attack, Type 2 to check and use Items, Type 3 to talk, Type 4 to attempt to flee");
                    string response = Console.ReadLine()!;
                    switch (response)
                    {
                        case "1":
                            Console.WriteLine($"Type Attack name to use attack");
                            for (int i = 0; i < PlayerData.playerCharacter.Player.PlayerAttacks.Length; i++)
                            {
                                PlayerStructures.AttackStruc CurrAttack = PlayerData.playerCharacter.Player.PlayerAttacks[i];
                                string coolDownDialogue = "";
                                if (CurrAttack.CurrentCoolDown > 0){
                                    coolDownDialogue = $"Cooling down {CurrAttack.CurrentCoolDown} turns left";
                                } else {
                                    coolDownDialogue = $"CD: {CurrAttack.AttackCooldown}";
                                }
                                Console.WriteLine($"Attack: {CurrAttack.AttackName} Damage: {CurrAttack.AttackDamage} {coolDownDialogue}");
                            }
                            string AttackChoice = Console.ReadLine()!;
                            if (AttackChoice != null)
                            {
                                PlayerStructures.AttackStruc SelectedAttack = PlayerData.playerCharacter.Player.PlayerAttacks.First((attack) => attack.AttackName.ToLower() == AttackChoice.ToLower());

                                if ( SelectedAttack != null && SelectedAttack.CurrentCoolDown == 0)
                                {
                                    Console.WriteLine($"You use {SelectedAttack.AttackName} Against {NPCData.NPCDefinition.NPCS.First((npc) => npc.ID == EnemyID).Name} Dealing {SelectedAttack.AttackDamage} Damage reducing enemy health from {NPCData.NPCDefinition.NPCS.First((npc) => npc.ID == EnemyID).Health} to {NPCData.NPCDefinition.NPCS.First((npc) => npc.ID == EnemyID).Health - SelectedAttack.AttackDamage}");
                                    NPCData.NPCDefinition.NPCS.First((npc) => npc.ID == EnemyID).Health -= SelectedAttack.AttackDamage;
                                    SelectedAttack.CurrentCoolDown = SelectedAttack.AttackCooldown;
                                    playerTurn = false;
                                }
                                else 
                                {
                                    Console.WriteLine("Blunder, Turn lost");
                                    playerTurn = false;
                                }
                            } else {
                                Console.WriteLine("You trip and break your nose");
                                playerTurn = false;
                            }
                            break;
                        case "2":
                            Console.WriteLine("Type itemName to use");
                            for (int i = 0; i < PlayerData.playerCharacter.Player.Inventory.Length; i++)
                            {
                                ItemStruct CurrItem = GameItemArray.GameItems.First((item) => item.ItemID == PlayerData.playerCharacter.Player.Inventory[i].ItemID);
                                Console.WriteLine($"ITEM {CurrItem.ItemName} EFFECT {CurrItem.Effect} EFFECT POWER {CurrItem.EffectValue} VALUE {CurrItem.GoldValue}");
                            }
                            string itemChoice = Console.ReadLine()!;
                            if (itemChoice != null)
                            {
                                ItemStruct CurrItem = GameItemArray.GameItems.First((item) => item.ItemName.ToLower() == itemChoice.ToLower());
                                switch (CurrItem.Effect)
                                {
                                    case "HEAL":
                                        PlayerData.playerCharacter.Player.PlayerHealth = Math.Min(PlayerData.playerCharacter.Player.PlayerHealth + CurrItem.EffectValue, PlayerMaxHealth);
                                        Console.WriteLine($"You heal for ${CurrItem.EffectValue} HP Current health is {PlayerData.playerCharacter.Player.PlayerHealth}");
                                        PlayerInventoryItem HealItemToDecrement = PlayerData.playerCharacter.Player.Inventory.First((item) => item.ItemID == CurrItem.ItemID);
                                            HealItemToDecrement.ItemCount -= 1;
                                        break;
                                    case "MISC":
                                        Console.WriteLine($"You are confused and throw whatever you hold, {CurrItem.ItemName} is thrown dealing {CurrItem.EffectValue} Damage, wasting {CurrItem.GoldValue} Gold");
                                        PlayerInventoryItem MiscItemToDecrement = PlayerData.playerCharacter.Player.Inventory.First((item) => item.ItemID == CurrItem.ItemID);
                                            MiscItemToDecrement.ItemCount -= 1;
                                        break;
                                    default:
                                        Console.WriteLine("You trip opening your inventory, turn lost");
                                        break;
                                }
                            }
                            break;
                        case "3":
                            switch (NPCData.NPCDefinition.NPCS.First((npc) => npc.ID == EnemyID).Disposition)
                            {
                                case >=7:
                                    Console.WriteLine($"{NPCData.NPCDefinition.NPCS.First((npc) => npc.ID == EnemyID).BaseResponse.Like}");
                                    break;
                                case >= 3:
                                    Console.WriteLine($"{NPCData.NPCDefinition.NPCS.First((npc) => npc.ID == EnemyID).BaseResponse.Hate}");
                                    break;
                                default:
                                    Console.WriteLine($"{NPCData.NPCDefinition.NPCS.First((npc) => npc.ID == EnemyID).BaseResponse.Neutral}");
                                    break;
                            }
                            break;
                        case "4":
                            fightCancel = true;
                            break;
                        default:
                            Console.WriteLine("You trip and fall over");
                            break;
                    }
                    playerTurn = false;
                }
                while (!playerTurn && !fightCancel)
                {
                    Console.WriteLine($"{NPCData.NPCDefinition.NPCS.First((npc) => npc.ID == EnemyID).Name}'S TURN HP: {NPCData.NPCDefinition.NPCS.First((npc) => npc.ID == EnemyID).Health}");
                    Console.WriteLine($"{NPCData.NPCDefinition.NPCS.First((npc) => npc.ID == EnemyID).BaseResponse.Hate}");
                    if (NPCData.NPCDefinition.NPCS.First((npc) => npc.ID == EnemyID).Health <= EnemyMaxHealth / 2 && NPCData.NPCDefinition.NPCS.First((npc) => npc.ID == EnemyID).Inventory.Length > 0){
                        for (int i = 0; i < NPCData.NPCDefinition.NPCS.First((npc) => npc.ID == EnemyID).Inventory.Length ; i++)
                        {
                            ItemStruct Heal = GameItemArray.GameItems.First((item) => (NPCData.NPCDefinition.NPCS.First((npc) => npc.ID == EnemyID).Inventory[i].ItemID == item.ItemID) && (item.Effect == "HEAL"));
                            if (Heal != null)
                            {
                                NPCData.NPCDefinition.NPCS.First((npc) => npc.ID == EnemyID).Health = Math.Min(EnemyMaxHealth, NPCData.NPCDefinition.NPCS.First((npc) => npc.ID == EnemyID).Health += Heal.EffectValue);
                                NPCData.NPCDefinition.NPCS.First((npc) => npc.ID == EnemyID).Inventory[i].ItemCount -= 1;
                                if (NPCData.NPCDefinition.NPCS.First((npc) => npc.ID == EnemyID).Inventory[i].ItemCount <= 0) {
                                    NPCData.NPCDefinition.NPCS.First((npc) => npc.ID == EnemyID).Inventory = NPCData.NPCDefinition.NPCS.First((npc) => npc.ID == EnemyID).Inventory.Where(item => item.ItemCount > 0).ToArray();
                                }
                                Console.WriteLine($"{NPCData.NPCDefinition.NPCS.First((npc) => npc.ID == EnemyID).Name} Has healed {Heal.EffectValue} to {NPCData.NPCDefinition.NPCS.First((npc) => npc.ID == EnemyID).Health}");
                                playerTurn = true;
                                break;
                            }
                        }
                    }
                    for (int i = 0; i < NPCData.NPCDefinition.NPCS.First((npc) => npc.ID == EnemyID).Attacks.Length; i++)
                    {
                        AttackStruc Attack = NPCData.NPCDefinition.NPCS.First((npc) => npc.ID == EnemyID).Attacks[i];
                        if (Attack != null && Attack.CurrentCoolDown == 0)
                        {
                            PlayerData.playerCharacter.Player.PlayerHealth -= Attack.AttackDamage;
                            Console.WriteLine($"{NPCData.NPCDefinition.NPCS.First((npc) => npc.ID == EnemyID).Name} {Attack.AttackActionDialogue}, You received {Attack.AttackDamage}, your health is now {PlayerData.playerCharacter.Player.PlayerHealth}");
                            playerTurn = true;
                            break;
                        }
                    }
                    Console.WriteLine($"{NPCData.NPCDefinition.NPCS.First((npc) => npc.ID == EnemyID).Name} Is recovering, Take your strike");
                    playerTurn = true;
                }
            }
            if (PlayerData.playerCharacter.Player.PlayerHealth > NPCData.NPCDefinition.NPCS.First((npc) => npc.ID == EnemyID).Health && PlayerData.playerCharacter.Player.PlayerHealth > 0){
                Win = true;
            } 
            if (fightCancel)
            {
                return new BattleResult() 
                {
                    GoldWon = 0, 
                    ExperienceWon = 1, 
                    ResultDialogue = "You ran"
                };  
            } else if (Win){
                return new BattleResult() 
                {
                    GoldWon = NPCData.NPCDefinition.NPCS.First((npc) => npc.ID == EnemyID).Gold, 
                    ExperienceWon = NPCData.NPCDefinition.NPCS.First((npc) => npc.ID == EnemyID).Experience, 
                    ResultDialogue = NPCData.NPCDefinition.NPCS.First((npc) => npc.ID == EnemyID).BaseResponse.Hate[0]
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
        public static void BeginTalk(int EnemyID){
            Console.WriteLine($"{NPCData.NPCDefinition.NPCS.First((npc) => npc.ID == EnemyID).Name} Stands before you");
            Console.WriteLine($"Write the appropriate key to select dialogue");
            bool Conversation = true;
            int CurrentID = 0;
            while (Conversation)
            {
                DialogueTree CurrentDialogue = NPCData.NPCDefinition.NPCS.First((npc) => npc.ID == EnemyID).DialogueTrees.First((dialogue) => dialogue.DialogueID == CurrentID);
                Console.WriteLine($"{CurrentDialogue.DialogueText}");
                for (int i = 0; i < CurrentDialogue.Responses.Length; i++)
                {       
                    Console.WriteLine($"{CurrentDialogue.Responses[i].NextDialogueID}: {CurrentDialogue.Responses[i].ResponseText}");
                }
                string Choice = Console.ReadLine()!;
                if (Choice != null)
                {
                    int parsedChoice = Int32.Parse(Choice);
                    
                    ResponseOption chosenResponse = CurrentDialogue.Responses.First((response) => response.NextDialogueID == parsedChoice);

                    switch (chosenResponse.Action)
                    {
                        case "FIGHT":
                            BattleResult Battle = BeginFight(EnemyID);
                            Conversation = false;
                            break;
                        case "LEAVE":
                            Conversation = false;
                            break;    
                        default:
                            DialogueTree NextDialogue = NPCData.NPCDefinition.NPCS.First((npc) => npc.ID == EnemyID).DialogueTrees.First((dialogue) => dialogue.DialogueID == parsedChoice);
                            
                            if (NextDialogue != null){
                                CurrentID = NextDialogue.DialogueID;
                            }                        
                        break;
                    }

                }
            }
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
                            new ResponseOption(responseText: "Goodbye",nextDialogueID: 1, Action: "TALK"),
                            new ResponseOption(responseText: "Hey Jeeves, how are you?", nextDialogueID: 2, Action: "TALK"),
                            new ResponseOption(responseText: "Leave me alone.", nextDialogueID: 3, Action: "LEAVE")
                        }
                    ),
                    new DialogueTree
                    (
                        DialogueID: 1, 
                        DialogueText: "Goodbye!", 
                        Responses: new ResponseOption[] {
                            new ResponseOption(responseText: "", nextDialogueID: -1, Action: "LEAVE")
                         }
                    ),
                    new DialogueTree
                    (
                        DialogueID: 2, 
                        DialogueText: "I'm well, thank you.", 
                        Responses: new ResponseOption[]
                        {
                            new ResponseOption("Great!", 3, Action: "TALK"),
                            new ResponseOption("Whatever.", 3, "TALK")
                        }
                    ),
                    new DialogueTree
                    (
                        DialogueID: 3, 
                        DialogueText: "That's rude!", 
                        Responses: new ResponseOption[]
                        {
                            new ResponseOption("Sorry.", 3, "TALK"),
                            new ResponseOption("I don't care.", 3, "TALK")
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
                    new AttackStruc(AttackID: 0, AttackDamage: 1, AttackName: "Bite", CurrentCoolDown: 0, AttackCooldown: 1, AttackActionDialogue: "Bites your head"),
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
                            new ResponseOption("Goodbye.", 1, "LEAVE"),
                            new ResponseOption("Got hit in the head recently", 2, "TALK")
                        }
                    ),
                    new DialogueTree
                    (
                        DialogueID: 1, 
                        DialogueText: "Catch ya", 
                        Responses: new ResponseOption[]
                        {
                            new ResponseOption("", 1, "LEAVE"),
                        }
                    ),
                    new DialogueTree
                    (
                        DialogueID: 2, 
                        DialogueText: "Ouch.", 
                        Responses: new ResponseOption[]
                        {
                            new ResponseOption("Goodbye.", 1, "LEAVE"),
                            new ResponseOption("I got hit in the head ya know?", 3, "TALK")
                        }
                    ),
                    new DialogueTree
                    (
                        DialogueID: 3, 
                        DialogueText: "Sucks man", 
                        Responses: new ResponseOption[]
                        {
                            new ResponseOption("Goodbye.", 1, "LEAVE"),
                            new ResponseOption("Head hurts", 4, "TALK")
                        }
                    ),
                    new DialogueTree
                    (
                        DialogueID: 4, 
                        DialogueText: "Damn what happened dude?", 
                        Responses: new ResponseOption[]
                        {
                            new ResponseOption("Goodbye.", 1, "LEAVE"),
                            new ResponseOption("i got hit in the head or something like that", 2, "TALK")
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
                            new ResponseOption("Goodbye.", 1, "LEAVE"),
                            new ResponseOption("ermm i..", 2, "TALK")
                        }
                    ),
                    new DialogueTree
                    (
                        DialogueID: 1, 
                        DialogueText: "...", 
                        Responses: new ResponseOption[]
                        {
                            new ResponseOption("", 1, "LEAVE"),
                        }
                    ),
                    new DialogueTree
                    (
                        DialogueID: 2, 
                        DialogueText: "Dont pick stupid dialogue options that have clear cutoffs", 
                        Responses: new ResponseOption[]
                        {
                            new ResponseOption("", 1, "LEAVE"),
                        }
                    ),
                }
            ),
        };
    }
    
}
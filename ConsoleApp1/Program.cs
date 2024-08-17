using System;
using System.Linq;

namespace Data
{
    public class UserData
    {
        public int userID = 1;
        public int gold = 150;
        public int currX = 0;
        public int currY = 0;
        public int initiative = 2;
        public int playerHealth = 15;
        public AttackStruc[] playerAttacks =
        [
            new AttackStruc(attackID: 1, attackName: "Bite", attackDamage: 1, coolDown: 3, currentCoolDown: 0),
            new AttackStruc(attackID: 2, attackName: "Scratch", attackDamage: 1, coolDown: 1, currentCoolDown: 0),
            new AttackStruc(attackID: 3, attackName: "Slash", attackDamage: 2, coolDown: 3, currentCoolDown: 0),
            new AttackStruc(attackID: 4, attackName: "Bash", attackDamage: 1, coolDown: 1, currentCoolDown: 0),
        ];
        public ItemStruc[] userItems;

        public UserData()
        {
            userItems = new ItemStruc[]
            {
                new ItemStruc { itemID = 4, itemName = "Stick", itemPrice = 10 },
                new ItemStruc { itemID = 5, itemName = "Jewel", itemPrice = 200 },
                new ItemStruc { itemID = 6, itemName = "Potion", itemPrice = 50 }
            };
        }

        public void AddItem(ItemStruc item)
        {
            var itemList = userItems.ToList();
            itemList.Add(item);
            userItems = itemList.ToArray();
        }

        public void SellItem(StoreData store, string itemToSell)
        {
            var item = userItems.FirstOrDefault(i => i.itemName.Equals(itemToSell, StringComparison.OrdinalIgnoreCase));

            if (item != null && store.gold >= item.itemPrice)
            {
                store.gold -= item.itemPrice;
                gold += item.itemPrice;
                store.AddItem(item);
                userItems = userItems.Where(i => i.itemID != item.itemID).ToArray();
                Console.WriteLine($"You have successfully sold {item.itemName}");
            }
            else
            {
                Console.WriteLine("I can't afford this.");
                RPG.RPG.AFK();
            }
        }
    }

    public class ItemStruc
    {
        public int itemID;
        public string itemName;
        public int itemPrice;
    }

    public class MapStruc
    {
        public int x;
        public int y;
        public bool store;
        public int storeID;
    }

    public class DialogueStruc(int dialogueID, string[] dialogueTree)
    {
        public required int dialogueID = dialogueID;
        public  string[] dialogueTree = dialogueTree;
    }        
    public class AttackStruc
    {
        public int AttackID { get; set; }
        public string AttackName { get; set; }
        public int AttackDamage { get; set; }
        public int CoolDown { get; set; }
        public int CurrentCoolDown { get; set; }

        public AttackStruc(int attackID, string attackName, int attackDamage, int coolDown, int currentCoolDown, string AttackName, int CurrentCoolDown, int AttackCooldown, string AttackActionDialogue)
        {
            this.AttackID = attackID;
            this.AttackName = attackName;
            this.AttackDamage = attackDamage;
            this.CoolDown = coolDown;
            this.CurrentCoolDown = currentCoolDown;
        }
    }
    public class DialogueChoices {
        public string failTalk;
        public string succTalk; 
        public string[] talk;
        public DialogueChoices(string failTalk, string succTalk, string[] talk){
            this.failTalk = failTalk;
            this.succTalk = succTalk;
            this.talk = talk;
        }
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
    public class StoreData(int storeID, int gold, ItemStruc[] items)
    {
        public int storeID = storeID;
        public int gold = gold;
        public string userPerception = "Neutral";
        public ItemStruc[] storeItems = items;

        public void AddItem(ItemStruc item)
        {
            var itemList = storeItems.ToList();
            itemList.Add(item);
            storeItems = itemList.ToArray();
        }

        public void BuyItem(UserData user, string itemToBuy)
        {
            var item = storeItems.FirstOrDefault(i => i.itemName.Equals(itemToBuy, StringComparison.OrdinalIgnoreCase));

            if (item != null && user.gold >= item.itemPrice)
            {
                user.gold -= item.itemPrice;
                gold += item.itemPrice;
                user.AddItem(item);
                storeItems = storeItems.Where(i => i.itemID != item.itemID).ToArray();
                Console.WriteLine($"You have successfully bought {item.itemName}");
            }
            else
            {
                Console.WriteLine("Either the item doesn't exist or you don't have enough gold.");
                RPG.RPG.AFK();
            }
        }

        public void StoreTalk(UserData user)
        {
            Console.WriteLine($"Good, let's continue. Now let's see how much gold you have: {user.gold}");
            if (user.gold > gold)
            {
                Console.WriteLine("Wowee, that's a lot of gold, you're quite rich now huh");
                userPerception = "Rich";
            }
            else
            {
                Console.WriteLine("Broke ass, I now see you as poor");
                userPerception = "Poor";
            }
            Console.WriteLine("Buyin or sell'n ?");
            string choice = Console.ReadLine();
            if (choice != "Buy" && choice != "Sell")
            {
                Console.WriteLine("Wise guy ey? Get out");
                RPG.RPG.AFK();
            }
            else if (choice == "Buy")
            {
                Console.WriteLine($"Now let's see what you want to {choice}, here are my wares:");
                for (int i = 0; i < storeItems.Length; i++)
                {
                    Console.WriteLine($"ITEM: {storeItems[i].itemName} PRICE: {storeItems[i].itemPrice} gold");
                }
            }
            else
            {
                Console.WriteLine($"Now let's see what you're sellin");
                for (int i = 0; i < user.userItems.Length; i++)
                {
                    Console.WriteLine($"ITEM: {user.userItems[i].itemName} PRICE: {user.userItems[i].itemPrice} gold");
                }
            }

            Console.WriteLine($"Now {(userPerception == "Good" ? "sir" : "Brokie")} What would you like to {choice}? please type its full name or if you want to leave press 1");
            string wantedItem = Console.ReadLine();
            if (wantedItem == "1" || wantedItem == "")
            {
                RPG.RPG.AFK();
            }
            else
            {
                if (choice == "Buy")
                {
                    BuyItem(user, wantedItem);
                }
                else
                {
                    user.SellItem(this, wantedItem);
                }

                StoreTalk(user);
            }
        }
    }
}

namespace RPG
{
    using Data;

    class RPG
    {
        static UserData userData = new();
        static StoreData[] stores =
        [
            new StoreData(
                storeID: 1, 
                gold: 250, 
                items:
                [
                    new ItemStruc { itemID = 1, itemName = "Sword", itemPrice = 100 },
                    new ItemStruc { itemID = 2, itemName = "Shield", itemPrice = 150 },
                    new ItemStruc { itemID = 3, itemName = "Potion", itemPrice = 50 }
                ]
            ),
            new StoreData(
                storeID: 2,
                gold: 300,
                items:
                [
                    new ItemStruc { itemID = 7, itemName = "Axe", itemPrice = 120 },
                    new ItemStruc { itemID = 8, itemName = "Helmet", itemPrice = 80 },
                    new ItemStruc { itemID = 9, itemName = "Boots", itemPrice = 60 }
                ]
            ),
            new StoreData(
                storeID: 3,
                gold: 500,
                items:
                [
                    new ItemStruc { itemID = 10, itemName = "Bow", itemPrice = 200 },
                    new ItemStruc { itemID = 11, itemName = "Arrow", itemPrice = 10 },
                    new ItemStruc { itemID = 12, itemName = "Quiver", itemPrice = 50 }
                ]
            ),
            new StoreData(
                storeID: 4,
                gold: 150,
                items:
                [
                    new ItemStruc { itemID = 13, itemName = "Dagger", itemPrice = 90 },
                    new ItemStruc { itemID = 14, itemName = "Cloak", itemPrice = 70 },
                    new ItemStruc { itemID = 15, itemName = "Ring", itemPrice = 40 }
                ]
            )
        ];
        static EnemyStruc[] enemies =
        [
            new EnemyStruc(
                enemyID: 1,
                enemyHealth: 2,
                enemyName: "Rat",
                gold: 15,
                disposition: "Hate",
                attacks: new[]
                {
                    new AttackStruc(attackID: 1, attackName: "Bite", attackDamage: 1, coolDown: 3, currentCoolDown: 0),
                    new AttackStruc(attackID: 2, attackName: "Scratch", attackDamage: 1, coolDown: 1, currentCoolDown: 0),
                },
                position: new[] {5, 5},
                initiative: 0,
                AggressionStatement:
                [
                    "Screech",
                    "Critch Critch",
                    "Eungh"
                ],         
                Defeated: false,   
                EnemyDialogue: new DialogueChoices(
                    failTalk: "Im a fucking rat bro?",
                    succTalk: "I have found god and will leave",
                    talk : new[]
                    {
                        "Sceech?",
                        "ritch ritch",
                        "*it grooms its head*",
                    }                    
                )        
            ),
            new EnemyStruc(
                enemyID: 2,
                enemyHealth: 5,
                enemyName: "Skeleton",
                gold: 25,
                disposition: "Hate",
            attacks: new[]
            {
                new AttackStruc(attackID: 3, attackName: "Slash", attackDamage: 2, coolDown: 3, currentCoolDown: 0),
                new AttackStruc(attackID: 4, attackName: "Bash", attackDamage: 1, coolDown: 1, currentCoolDown: 0),
            },
                position: new[] {10, 10},
                initiative: 4,
                AggressionStatement:
                [
                    "Rattle Rattle",
                    "Skelibi Rizz",
                    "Crackle Crunch"
                ],
                Defeated: false,  
                EnemyDialogue: new DialogueChoices(
                    failTalk: "Die human",
                    succTalk: "I never wanted this...",
                    talk : new[]
                    {
                        "Torment...",
                        "These bones never heal",
                        "*it attempts to sob*",
                    }
                )                             
            ),
        ];
        

        static MapStruc[] Locations =
        [
            new MapStruc { x = 1, y = 0, store = true, storeID = 1 },
            new MapStruc { x = 0, y = 5, store = false, storeID = -1 },
            new MapStruc { x = 8, y = 0, store = true, storeID = 2 },
            new MapStruc { x = 0, y = 10, store = false, storeID = -1 },
            new MapStruc { x = 0, y = 15, store = true, storeID = 3 },
            new MapStruc { x = 0, y = 20, store = false, storeID = -1 },
            new MapStruc { x = 4, y = 2, store = true, storeID = 4 },
            new MapStruc { x = 0, y = 1, store = false, storeID = -1 },
        ];
        static void Main(string[] args)
        {
            ConversationStart();
        }

        public static void AFK()
        {
            Console.WriteLine("Waiting... 1 to wake up, 2 to see items");
            string wakeUp = Console.ReadLine();
            if (wakeUp == "1")
            {
                ConversationStart();
            }
            else if (wakeUp == "2")
            {
                for (int i = 0; i < userData.userItems.Length; i++)
                {
                    Console.WriteLine($"ITEM: {userData.userItems[i].itemName}, PRICE: {userData.userItems[i].itemPrice}");
                }
                Console.WriteLine($"Remaining gold: {userData.gold}");
                AFK();
            }
            else
            {
                AFK();
            }
        }

        static void ConversationStart()
        {
            Console.WriteLine("Welcome Traveler, This test application is used to learn the basics of C#. Please press 1 to continue");
            string response = Console.ReadLine();
            if (response != "1")
            {
                Console.WriteLine("Bad move buddy, listen to me");
                ConversationStart();
            }
            else
            {
                Explore();
            }
        }

        static void Explore()
        {


            Console.WriteLine($"You find yourself at x:{userData.currX} and y:{userData.currY},\n to go up type 1\n to go left type 4\n to go right type 2\n to go down type 3, Type Rest to Rest");
            string direction = Console.ReadLine();
            switch (direction)
            {
                case "1":
                    userData.currY += 1;
                    break;
                case "2":
                    userData.currX += 1;
                    break;
                case "3":
                    userData.currY -= 1;
                    break;
                case "4":
                    userData.currX -= 1;
                    break;
                case "Rest":
                    AFK();
                    break;
                default:
                    Console.WriteLine("Invalid direction");
                    break;
            }

            var location = Locations.FirstOrDefault(loc => loc.x == userData.currX && loc.y == userData.currY);
            var enemy = enemies.FirstOrDefault(enemy => enemy.position[0] == userData.currX && enemy.position[1] == userData.currY);
            if (enemy != null && enemy.Defeated == false)
            {
                if (engageFight(enemy))
                {
                    Console.WriteLine($"{enemy.EnemyName} Defeated! {enemy.gold} Gold Acquired!");
                    Explore();
                } 
                else 
                {
                    Console.WriteLine($"Clearly the {enemy.EnemyName} Was too strong");
                    AFK();
                }
            }
            if (location != null && location.store)
            {
                Console.WriteLine($"There is a store here, Want to go inside? the sign says its the {location.store}sth store");
                string goInStore = Console.ReadLine();
                if (goInStore == "Yes")
                {
                    var store = stores.FirstOrDefault(s => s.storeID == location.storeID);
                    store?.StoreTalk(userData);                    
                } else {
                    Explore();
                }

            }
            else
            {
                Console.WriteLine($"You find yourself at x:{userData.currX} and y:{userData.currY}. There is nothing here.");
                Explore();
            }
        }
        public static bool engageFight(EnemyStruc enemy){
            bool playerTurn = enemy.Initiative > userData.initiative ? false : true ;
            int playerHealth = userData.playerHealth;
            Console.WriteLine($"You encounter a enemy {enemy.EnemyName} it {enemy.Disposition}s you!");
            Console.WriteLine($"{(playerTurn ? "Looks like your faster, First turn is yours" : "Its Quick!")}");
            // battle logic
            while (playerHealth > 0 && enemy.EnemyHealth > 0) {
                if (playerTurn){
                    Console.WriteLine("Ready to attack, Type the attack name to attack, Type talk to attempt diplomacy, Type Run to attempts to flee");
                    for (int i = 0; i < userData.playerAttacks.Length; i++){
                        Console.WriteLine($"\n Attack: {userData.playerAttacks[i].AttackName} Damage: {userData.playerAttacks[i].AttackDamage} " +
                        $"{(userData.playerAttacks[i].CurrentCoolDown == 0 ? "Ready" : $"On cooldown: {userData.playerAttacks[i].CurrentCoolDown}")}");
                    }
                    string choice = Console.ReadLine();
                    if (choice == "Run")
                    {
                        Explore();
                    }
                    else if (choice == "talk")
                    {
                        if (enemy.Disposition == "Hate"){
                            Console.WriteLine(enemy.EnemyDialogue.failTalk);
                            enemy.Disposition = "neutral";
                        }
                        else if (enemy.Disposition == "neutral")
                        {   
                            Random random = new Random();
                            int randomIndex = random.Next(enemy.EnemyDialogue.talk.Length);
                            Console.WriteLine(enemy.EnemyDialogue.talk[randomIndex]);
                        } 
                        else 
                        {
                            Console.WriteLine(enemy.AggressionStatement[0]);
                        };
                    }
                    else
                    {
                        var attack = userData.playerAttacks.FirstOrDefault((attack) => attack.AttackName.ToLower() == choice.ToLower());
                        if (attack.CurrentCoolDown > 0){
                            Console.WriteLine("Attack is still cooling down! Dont get careless now LOOK OUT!");
                            playerTurn = false;
                        } else {
                            enemy.EnemyHealth -= attack.AttackDamage;
                            attack.CurrentCoolDown = attack.CoolDown + 1;
                            Console.WriteLine($"You hit for {attack.AttackDamage}! {enemy.EnemyName}'s Health is reduced {attack.AttackDamage} to {enemy.EnemyHealth} HP");
                        }
                    };
                    for (int i = 0; i < userData.playerAttacks.Length; i++){
                        userData.playerAttacks[i].CurrentCoolDown = Math.Max(0, userData.playerAttacks[i].CurrentCoolDown - 1);
                    }
                    playerTurn = false;
                }
                if (!playerTurn){
                    Console.WriteLine("Enemy turn!");
                    Random random = new Random();
                    int randomIndex = random.Next(enemy.AggressionStatement.Length);
                    bool hasAttacked = false;
                    var Attacks = enemy.Attacks;
                    for (int i = 0; i < Attacks.Length; i++){
                        if (Attacks[i].CurrentCoolDown == 0){
                            Console.WriteLine($"{enemy.AggressionStatement[randomIndex]}");
                            playerHealth -= Attacks[i].AttackDamage;
                            Attacks[i].CurrentCoolDown = Attacks[i].CoolDown;
                            Console.WriteLine($"You are hit with a {Attacks[i].AttackName} for {Attacks[i].AttackDamage}! current health is {playerHealth}");
                            hasAttacked = true;
                            break;
                        }
                    }
                    Console.WriteLine($"{enemy.AggressionStatement[randomIndex]}");
                    if (!hasAttacked){
                        Console.WriteLine($"{enemy.EnemyName} Is recovering their attacks!");
                    }
                    
                    playerTurn = true;
                }
            }
            if (playerHealth > 0 && playerHealth > enemy.EnemyHealth){
                userData.gold += enemy.gold;
                enemy.Defeated = true;
                return true;
            }
            return false;
        }
    }
}

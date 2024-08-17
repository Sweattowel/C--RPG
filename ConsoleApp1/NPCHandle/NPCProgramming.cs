using Data;

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
    public class NPCStruc
    {
        public int ID { get; set;}
        public string Name { get; set; }
        public int Disposition { get; set; }
        public BaseNPCResponseStruc BaseResponse { get; set; }
        public AttackStruc[] Attacks { get; set; }
        public NPCStruc(int ID, string Name, int Disposition, BaseNPCResponseStruc BaseResponse, AttackStruc[] Attacks){
            this.ID = ID;
            this.Name = Name;
            this.Disposition = Disposition;
            this.BaseResponse = BaseResponse;
            this.Attacks = Attacks;
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
                }
            ),
            new NPCStructures.NPCStruc(
                ID: 1,
                Name: "Jason",
                Disposition: 5,       
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
                }
            ),
            new NPCStructures.NPCStruc(
                ID: 2,
                Name: "Jona",
                Disposition: 0, 
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
                }
            ),
        };
    }
    
}
// Læs automatisk fra fil (eller lav default hvis den ikke findes)
using GameWorldLibrary.Creatures;
using GameWorldLibrary.DesignPattern;
using GameWorldLibrary.Interface;
using GameWorldLibrary.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Xml;


#region Creating

//Laver en verden:
World world = new World();
//Lav to væsener
Zombie zombie = new Zombie("Zombie", 20);
Human human = new Human("Human",10);
//Lav to AttackItem
AttackItem Sword = new AttackItem("Sword", 10, 30);
AttackItem Axe = new AttackItem("Axe", 20, 20);
//Laver to DefenceItem
DefenceItem Leggnings = new DefenceItem("Leggnings", 20);
DefenceItem Helmet = new DefenceItem("Helmet", 6);
//Laver tre worldObject
WorldObject lootBox = new WorldObject("LootBox", true, false);
WorldObject lootBox2 = new WorldObject("LootBox", true, false);
WorldObject FalseBox = new WorldObject("FalseBox", false, false);
#endregion

#region AddToLootBox
lootBox.AttackList.AddRange(new[] { Sword, Axe });
lootBox2.AttackList.Add(Sword);
lootBox2.DefenseList.Add(Helmet);
FalseBox.DefenseList.Add(Leggnings);
#endregion

#region CreatureLooting
zombie.Loot(FalseBox);
human.Loot(lootBox2);
zombie.Loot(lootBox);
#endregion

#region Notify

//zombie.PropertyChanged += MyObserber;
#region Method
//void MyObserber(object sender, PropertyChangedEventArgs e)
//{
//    Console.WriteLine("Named Method: " + e.PropertyName);
//}
#endregion

#region Other Way
////lambda er anyomr som der lytter på der sker noget
//zombie.PropertyChanged += (senderObj, args) =>
//{
//    Console.WriteLine("Anoyous Method:" + args.PropertyName);
//};

#endregion
#endregion

#region Fight
int zombieDMG = zombie.Hit(zombie.HitPoint);
human.ReceiveHit(zombieDMG);

int humanDMG = human.Hit(human.HitPoint);
zombie.ReceiveHit(humanDMG);
#endregion




#region SOLID
//S: En klasse skal kun have ét ansvar
//AttackItem håndterer kun våben-data og skade
//Creature håndterer karakterdata + kampadfærd
//BoostAttackDecorator håndterer kun ekstra skade

//O: Klasser skal være åbne for udvidelse, men lukkede for ændring
// AttackItem kan udvides med decorators (boost, weaken) uden ændringer
// Nye IAttackStrategy-klasser (fx CriticalStrikeStrategy) kan tilføjes uden at ændre i Creature

//L: Subklasser skal kunne erstatte deres baseklasse uden at bryde funktionalitet
//BoostAttackDecorator og WeakenAttackDecorator erstatter IAttackItem uden fejl
//Zombie og Human kan bruges som Creature uden at ændre resten af koden

//I: Klienter skal ikke tvinges til at implementere unødvendige metoder
//Der er små, fokuserede interfaces:
//IAttackItem kun med Attack()
//IAttackStrategy kun med CalculateHit()

//D: Klasser skal afhænge af abstraktioner (interfaces), ikke konkrete klasser
//Creature afhænger af:public IAttackStrategy AttackStrategy { get; set; }
//Og AttackList er en liste af IAttackItem, ikke konkrete våben.
//Du bruger interfaces, ikke afhængighed af konkrete klasser.

#endregion

#region Delete

//XmlDocument configDoc = new XmlDocument();
//configDoc.Load("gameconfig.xml");


//// MaxX
//XmlNode maxXNode = configDoc.DocumentElement.SelectSingleNode("//GameSettings/WorldSize/MaxX");
//if (maxXNode != null)
//{
//    Console.WriteLine("MaxX: " + maxXNode.InnerText.Trim());
//}

//// MaxY
//XmlNode maxYNode = configDoc.DocumentElement.SelectSingleNode("//GameSettings/WorldSize/MaxY");
//if (maxYNode != null)
//{
//    Console.WriteLine("MaxY: " + maxYNode.InnerText.Trim());
//}

//// GameLevel
//XmlNode levelNode = configDoc.DocumentElement.SelectSingleNode("//GameSettings/GameLevel");
//if (levelNode != null)
//{
//    Console.WriteLine("GameLevel: " + levelNode.InnerText.Trim());
//}
#endregion
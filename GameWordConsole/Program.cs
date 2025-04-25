// Læs automatisk fra fil (eller lav default hvis den ikke findes)
using GameWorldLibrary.Creatures;
using GameWorldLibrary.DesignPattern;
using GameWorldLibrary.DesignPattern.Composite;
using GameWorldLibrary.DesignPattern.Decorator;
using GameWorldLibrary.DesignPattern.Strategy;
using GameWorldLibrary.Interface;
using GameWorldLibrary.Logger;
using GameWorldLibrary.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Xml;


#region Creating

// Sæt op hvor logbeskeder skal gå hen:
MyLogger logger = MyLogger.GetInstance();
logger.AddListener(new TextWriterTraceListener("GameLogger.txt"));
logger.AddListener(new TextWriterTraceListener("Game.txt"));
logger.AddListener(new TextWriterTraceListener("G.txt"));
//logger.SetFormatter((msg) =>
//    $"[{DateTime.Now:HH:mm:ss}] : {msg}");
logger.SetFormatter((msg, level) =>
    $" {level} {DateTime.Now:yyyy-MM-dd HH:mm:ss} [GameWorld] : {msg}");

//Laver en verden:
World world = new World();
//Lav fire væsener
Zombie zombie = new Zombie("Zombie", 20);
Human human = new Human("Human",10);
Human HumanJole = new Human("Jole", 100);
Zombie ZombieX = new Zombie("X", 200);
#region Check om Id går op
////Viser i console at den tæller op
//Console.WriteLine("Human Id:" + human.Id);
//Console.WriteLine("Zombie Id:" + zombie.Id);
//Console.WriteLine("Human Id 2: " + HumanJole.Id);
//Console.WriteLine("Zombie Id 2:" + ZombieX.Id);
#endregion
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

#region Looting
Console.WriteLine();
Console.WriteLine("Looting:");
Console.WriteLine();
zombie.Loot(FalseBox);
human.Loot(lootBox2);
zombie.Loot(lootBox);
#endregion

#region Notify

//zombie.PropertyChanged += MyObserber;
//#region Method
//void MyObserber(object sender, PropertyChangedEventArgs e)
//{
//    Console.WriteLine("Named Method: " + e.PropertyName);
//}
#region Other Way
////lambda er anyomr som der lytter på der sker noget
//zombie.PropertyChanged += (senderObj, args) =>
//{
//    Console.WriteLine("Anoyous Method:" + args.PropertyName);
//};

#endregion

#endregion

#region Fight
Console.WriteLine();
Console.WriteLine("Fight:");
Console.WriteLine();
int zombieDMG = zombie.Hit();  
human.ReceiveHit(zombieDMG);

int humanDMG = human.Hit();   
zombie.ReceiveHit(humanDMG);

//zombie.PropertyChanged -= MyObserber;
#endregion

#region Change Logging format
Console.WriteLine();
Console.WriteLine("Change Logging Format");
Console.WriteLine();
logger.SetFormatter((msg, level) =>
    $"[LOG - {level}] >>> {msg} <<< @ {DateTime.Now:HH:mm}");

#endregion

#region Stategy (CriticalStrikeStrategy)
Console.WriteLine();
Console.WriteLine("Strategy (CriticalStrikeStrategy):");
Console.WriteLine();
ZombieX.AttackList.Add(new AttackItem("Spear", 10, 1));
ZombieX.AttackStrategy = new CriticalStrikeStrategy(new BasicAttackStrategy());
int damageCriticalStrikeStrategy = ZombieX.Hit();
HumanJole.ReceiveHit(damageCriticalStrikeStrategy);

#endregion

#region Decorator

#region Boost Attack Decorator
Console.WriteLine();
Console.WriteLine("Boost Attack Decorator:");
Console.WriteLine();

Zombie TestBoost = new Zombie("TestBoost", 150);
Human TestBoostHuman = new Human("HumanTest",13);
WorldObject worldObjectTest = new WorldObject("Sand", true, true);

worldObjectTest.AttackList.Add(new BoostAttackDecorator(Sword));

TestBoostHuman.Loot(worldObjectTest);


int TestBoostHumanDMG = TestBoostHuman.Hit();
TestBoost.ReceiveHit(TestBoostHumanDMG);

#endregion

#region Weaken Attack Decorator
Console.WriteLine();
Console.WriteLine("Weaken Attack Decorator:");
Console.WriteLine();

Console.WriteLine();
Console.WriteLine(Axe.Hit);
WeakenAttackDecorator weakenAttackDecorator = new WeakenAttackDecorator(Axe);
WorldObject worldObjectweakenAttackDecorator = new WorldObject("weakenAttackDecorator", true, true);

worldObjectweakenAttackDecorator.AttackList.Add(weakenAttackDecorator);
Human NewLooter = new Human("Nwe", 12);
weakenAttackDecorator.SetCreature(NewLooter);
NewLooter.Loot(worldObjectweakenAttackDecorator);


int NewLooterDMG = NewLooter.Hit();
TestBoost.ReceiveHit(NewLooterDMG);


#endregion

#endregion

#region Composite
Console.WriteLine();
Console.WriteLine("Composite:");
Console.WriteLine();
var swordCombo = new AttackItem("Sword", 10, 10);
var axeCombo = new AttackItem("Axe", 15, 15);

var combo = new AttackItemComposite();
combo.Add(swordCombo);
combo.Add(axeCombo);

// Her: combo implementerer IAttackItem
Human creatureCombo = new Human("TestHuman", 100);
creatureCombo.AttackList.Add(combo);

int creatureComboDamage = creatureCombo.Hit(); // Hit bruger alle AttackList items og kalder Attack()
Zombie Z = new Zombie("Composite", 100);
Z.ReceiveHit(creatureComboDamage);

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
//fokuserede interfaces med en ting:
//IAttackItem kun med Attack()
//IAttackStrategy kun med CalculateHit()

//D: Klasser skal afhænge af abstraktioner (interfaces), ikke konkrete klasser
//Creature afhænger af:public IAttackStrategy AttackStrategy { get; set; }
//Og AttackList er en liste af IAttackItem, ikke konkrete våben.

#endregion

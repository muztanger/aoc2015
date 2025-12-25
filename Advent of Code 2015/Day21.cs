using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advent_of_Code_2015
{
    /// <summary>
    /// Day 21: RPG Simulator 20XX
    /// You have 100 HP. Boss stats are in input.
    /// Must buy exactly 1 weapon, 0-1 armor, 0-2 rings.
    /// Player always goes first. Damage = attacker_damage - defender_armor (min 1).
    /// Find minimum gold to win (Part 1) and maximum gold to lose (Part 2).
    /// </summary>
    [TestClass]
    public class Day21
    {
        private static readonly string inputString = InputLoader.ReadAllText("day21.txt");

        static string exampleInput = @"Hit Points: 12
Damage: 7
Armor: 2";

        record Item(string Name, int Cost, int Damage, int Armor);
        record Character(int HitPoints, int Damage, int Armor);

        static readonly List<Item> Weapons = new()
        {
            new("Dagger", 8, 4, 0),
            new("Shortsword", 10, 5, 0),
            new("Warhammer", 25, 6, 0),
            new("Longsword", 40, 7, 0),
            new("Greataxe", 74, 8, 0)
        };

        static readonly List<Item> Armors = new()
        {
            new("None", 0, 0, 0),  // Armor is optional
            new("Leather", 13, 0, 1),
            new("Chainmail", 31, 0, 2),
            new("Splintmail", 53, 0, 3),
            new("Bandedmail", 75, 0, 4),
            new("Platemail", 102, 0, 5)
        };

        static readonly List<Item> Rings = new()
        {
            new("None1", 0, 0, 0),  // Can buy 0-2 rings
            new("None2", 0, 0, 0),
            new("Damage +1", 25, 1, 0),
            new("Damage +2", 50, 2, 0),
            new("Damage +3", 100, 3, 0),
            new("Defense +1", 20, 0, 1),
            new("Defense +2", 40, 0, 2),
            new("Defense +3", 80, 0, 3)
        };

        static Character ParseBoss(string input)
        {
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            int hp = int.Parse(lines[0].Split(':')[1].Trim());
            int damage = int.Parse(lines[1].Split(':')[1].Trim());
            int armor = int.Parse(lines[2].Split(':')[1].Trim());
            return new Character(hp, damage, armor);
        }

        static bool PlayerWins(Character player, Character boss)
        {
            int playerHp = player.HitPoints;
            int bossHp = boss.HitPoints;

            while (true)
            {
                // Player attacks
                int playerDamage = Math.Max(1, player.Damage - boss.Armor);
                bossHp -= playerDamage;
                if (bossHp <= 0) return true;

                // Boss attacks
                int bossDamage = Math.Max(1, boss.Damage - player.Armor);
                playerHp -= bossDamage;
                if (playerHp <= 0) return false;
            }
        }

        static IEnumerable<(int cost, Character player)> GetAllLoadouts()
        {
            // Must buy exactly 1 weapon
            foreach (var weapon in Weapons)
            {
                // Can buy 0-1 armor
                foreach (var armor in Armors)
                {
                    // Can buy 0-2 rings (need to avoid duplicates)
                    for (int i = 0; i < Rings.Count; i++)
                    {
                        for (int j = i + 1; j < Rings.Count; j++)
                        {
                            var ring1 = Rings[i];
                            var ring2 = Rings[j];

                            int totalCost = weapon.Cost + armor.Cost + ring1.Cost + ring2.Cost;
                            int totalDamage = weapon.Damage + armor.Damage + ring1.Damage + ring2.Damage;
                            int totalArmor = weapon.Armor + armor.Armor + ring1.Armor + ring2.Armor;

                            var player = new Character(100, totalDamage, totalArmor);
                            yield return (totalCost, player);
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void Part1Examples()
        {
            // Example: player (8 HP, 5 damage, 5 armor) vs boss (12 HP, 7 damage, 2 armor)
            var player = new Character(8, 5, 5);
            var boss = new Character(12, 7, 2);
            
            Assert.IsTrue(PlayerWins(player, boss));
        }

        [TestMethod]
        public void Part1()
        {
            var boss = ParseBoss(inputString);
            
            int minCost = int.MaxValue;
            foreach (var (cost, player) in GetAllLoadouts())
            {
                if (PlayerWins(player, boss))
                {
                    minCost = Math.Min(minCost, cost);
                }
            }
            
            Console.WriteLine($"Part 1: Minimum gold to win = {minCost}");
        }

        [TestMethod]
        public void Part2Examples()
        {
            // Part 2: Find maximum gold you can spend and still lose
            // No specific example given
        }

        [TestMethod]
        public void Part2()
        {
            var boss = ParseBoss(inputString);
            
            int maxCost = 0;
            foreach (var (cost, player) in GetAllLoadouts())
            {
                if (!PlayerWins(player, boss))
                {
                    maxCost = Math.Max(maxCost, cost);
                }
            }
            
            Console.WriteLine($"Part 2: Maximum gold to lose = {maxCost}");
        }
    }
}
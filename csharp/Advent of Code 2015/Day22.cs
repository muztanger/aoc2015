using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advent_of_Code_2015
{
    /// <summary>
    /// Day 22: Wizard Simulator 20XX
    /// Turn-based RPG combat as a wizard with spells.
    /// Player has 50 HP, 500 mana. Boss stats from input.
    /// Find minimum mana to win (Part 1) and minimum mana on Hard mode (Part 2).
    /// </summary>
    [TestClass]
    public class Day22
    {
        private static readonly string inputString = InputLoader.ReadAllText("day22.txt");

        record Spell(string Name, int Cost, int Damage, int Heal, int Armor, int Mana, int Duration);
        record State(int PlayerHP, int PlayerMana, int BossHP, int ShieldTimer, int PoisonTimer, int RechargeTimer, int ManaSpent, bool PlayerTurn);

        static readonly List<Spell> Spells = new()
        {
            new("Magic Missile", 53, 4, 0, 0, 0, 0),
            new("Drain", 73, 2, 2, 0, 0, 0),
            new("Shield", 113, 0, 0, 7, 0, 6),
            new("Poison", 173, 0, 0, 0, 0, 6),
            new("Recharge", 229, 0, 0, 0, 101, 5)
        };

        static (int hp, int damage) ParseBoss(string input)
        {
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            int hp = int.Parse(lines[0].Split(':')[1].Trim());
            int damage = int.Parse(lines[1].Split(':')[1].Trim());
            return (hp, damage);
        }

        static int FindMinMana(int bossHP, int bossDamage, bool hardMode)
        {
            var initial = new State(50, 500, bossHP, 0, 0, 0, 0, true);
            var queue = new PriorityQueue<State, int>();
            queue.Enqueue(initial, 0);
            var visited = new HashSet<(int, int, int, int, int, int, bool)>();
            int minMana = int.MaxValue;

            while (queue.Count > 0)
            {
                var state = queue.Dequeue();

                if (state.ManaSpent >= minMana) continue;

                var key = (state.PlayerHP, state.PlayerMana, state.BossHP, state.ShieldTimer, state.PoisonTimer, state.RechargeTimer, state.PlayerTurn);
                if (visited.Contains(key)) continue;
                visited.Add(key);

                var (hp, mana, bossHp, shield, poison, recharge, spent, playerTurn) = state;

                // Hard mode: lose 1 HP at start of player turn
                if (hardMode && playerTurn)
                {
                    hp--;
                    if (hp <= 0) continue;
                }

                // Apply effects
                int armor = 0;
                if (shield > 0)
                {
                    armor = 7;
                    shield--;
                }
                if (poison > 0)
                {
                    bossHp -= 3;
                    poison--;
                }
                if (recharge > 0)
                {
                    mana += 101;
                    recharge--;
                }

                // Check if boss is dead
                if (bossHp <= 0)
                {
                    minMana = Math.Min(minMana, spent);
                    continue;
                }

                if (playerTurn)
                {
                    // Try each spell
                    foreach (var spell in Spells)
                    {
                        if (mana < spell.Cost) continue;

                        // Check if effect is already active
                        if (spell.Name == "Shield" && shield > 0) continue;
                        if (spell.Name == "Poison" && poison > 0) continue;
                        if (spell.Name == "Recharge" && recharge > 0) continue;

                        int newHP = hp + spell.Heal;
                        int newMana = mana - spell.Cost;
                        int newBossHP = bossHp - spell.Damage;
                        int newShield = shield + (spell.Name == "Shield" ? spell.Duration : 0);
                        int newPoison = poison + (spell.Name == "Poison" ? spell.Duration : 0);
                        int newRecharge = recharge + (spell.Name == "Recharge" ? spell.Duration : 0);
                        int newSpent = spent + spell.Cost;

                        var newState = new State(newHP, newMana, newBossHP, newShield, newPoison, newRecharge, newSpent, false);
                        queue.Enqueue(newState, newSpent);
                    }
                }
                else
                {
                    // Boss turn
                    int damage = Math.Max(1, bossDamage - armor);
                    int newHP = hp - damage;
                    if (newHP > 0)
                    {
                        var newState = new State(newHP, mana, bossHp, shield, poison, recharge, spent, true);
                        queue.Enqueue(newState, spent);
                    }
                }
            }

            return minMana;
        }

        [TestMethod]
        public void Part1Examples()
        {
            // Example 1: Player has 10 HP, 250 mana, Boss has 13 HP, 8 damage
            int result1 = FindMinMana(13, 8, false);
            Console.WriteLine($"Example 1: {result1} mana");
            // Expected: Can win with Poison (173) then Magic Missile (53) or other combos

            // Example 2: Player has 10 HP, 250 mana, Boss has 14 HP, 8 damage
            int result2 = FindMinMana(14, 8, false);
            Console.WriteLine($"Example 2: {result2} mana");
        }

        [TestMethod]
        public void Part1()
        {
            var (bossHP, bossDamage) = ParseBoss(inputString);
            int minMana = FindMinMana(bossHP, bossDamage, false);
            Assert.AreEqual(900, minMana);
            Console.WriteLine($"Part 1: Minimum mana to win = {minMana}");
        }

        [TestMethod]
        public void Part2Examples()
        {
            // Hard mode: lose 1 HP at start of each player turn
            var (bossHP, bossDamage) = ParseBoss(inputString);
            int minMana = FindMinMana(bossHP, bossDamage, true);
            Console.WriteLine($"Part 2 test: {minMana}");
        }

        [TestMethod]
        public void Part2()
        {
            var (bossHP, bossDamage) = ParseBoss(inputString);
            int minMana = FindMinMana(bossHP, bossDamage, true);
            Assert.AreEqual(1216, minMana);
            Console.WriteLine($"Part 2: Minimum mana on Hard mode = {minMana}");
        }
    }
}
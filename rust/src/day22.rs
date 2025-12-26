use std::cmp::Reverse;
use std::collections::{BinaryHeap, HashSet};

#[derive(Clone, Copy, Debug, Eq, PartialEq, Hash, Ord, PartialOrd)]
struct State {
    player_hp: i32,
    player_mana: i32,
    boss_hp: i32,
    boss_damage: i32,
    shield: i32,
    poison: i32,
    recharge: i32,
    spent: i32,
    player_turn: bool,
}

#[derive(Clone, Copy)]
struct Spell {
    cost: i32,
    damage: i32,
    heal: i32,
    armor: i32,
    mana: i32,
    duration: i32,
    kind: SpellKind,
}

#[derive(Clone, Copy, PartialEq, Eq)]
enum SpellKind {
    Instant,
    Shield,
    Poison,
    Recharge,
}

const SPELLS: [Spell; 5] = [
    Spell { cost: 53, damage: 4, heal: 0, armor: 0, mana: 0, duration: 0, kind: SpellKind::Instant },
    Spell { cost: 73, damage: 2, heal: 2, armor: 0, mana: 0, duration: 0, kind: SpellKind::Instant },
    Spell { cost: 113, damage: 0, heal: 0, armor: 7, mana: 0, duration: 6, kind: SpellKind::Shield },
    Spell { cost: 173, damage: 0, heal: 0, armor: 0, mana: 0, duration: 6, kind: SpellKind::Poison },
    Spell { cost: 229, damage: 0, heal: 0, armor: 0, mana: 101, duration: 5, kind: SpellKind::Recharge },
];

fn parse_boss(input: &str) -> (i32, i32) {
    let mut hp = 0;
    let mut damage = 0;
    for line in input.lines() {
        let mut parts = line.split(':');
        let key = parts.next().unwrap().trim();
        let val: i32 = parts.next().unwrap().trim().parse().unwrap();
        match key {
            "Hit Points" => hp = val,
            "Damage" => damage = val,
            _ => {}
        }
    }
    (hp, damage)
}

fn apply_effects(state: &mut State) {
    if state.shield > 0 {
        state.shield -= 1;
    }
    if state.poison > 0 {
        state.boss_hp -= 3;
        state.poison -= 1;
    }
    if state.recharge > 0 {
        state.player_mana += 101;
        state.recharge -= 1;
    }
}

fn min_mana_to_win(boss_hp: i32, boss_damage: i32, hard: bool) -> i32 {
    let start = State {
        player_hp: 50,
        player_mana: 500,
        boss_hp,
        boss_damage,
        shield: 0,
        poison: 0,
        recharge: 0,
        spent: 0,
        player_turn: true,
    };

    let mut heap = BinaryHeap::new();
    heap.push((Reverse(0), start));
    let mut seen: HashSet<State> = HashSet::new();
    let mut best = i32::MAX;

    while let Some((Reverse(cost), mut s)) = heap.pop() {
        if cost >= best {
            continue;
        }
        if !seen.insert(s) {
            continue;
        }

        if hard && s.player_turn {
            s.player_hp -= 1;
            if s.player_hp <= 0 {
                continue;
            }
        }

        apply_effects(&mut s);
        if s.boss_hp <= 0 {
            best = best.min(cost);
            continue;
        }

        if s.player_turn {
            for spell in SPELLS.iter() {
                if s.player_mana < spell.cost {
                    continue;
                }
                // prevent re-casting active effects
                if spell.kind == SpellKind::Shield && s.shield > 0 {
                    continue;
                }
                if spell.kind == SpellKind::Poison && s.poison > 0 {
                    continue;
                }
                if spell.kind == SpellKind::Recharge && s.recharge > 0 {
                    continue;
                }

                let mut ns = s;
                ns.player_turn = false;
                ns.player_mana -= spell.cost;
                ns.spent += spell.cost;
                ns.boss_hp -= spell.damage;
                ns.player_hp += spell.heal;
                match spell.kind {
                    SpellKind::Shield => ns.shield = spell.duration,
                    SpellKind::Poison => ns.poison = spell.duration,
                    SpellKind::Recharge => ns.recharge = spell.duration,
                    SpellKind::Instant => {}
                }

                if ns.boss_hp <= 0 {
                    best = best.min(ns.spent);
                } else {
                    heap.push((Reverse(ns.spent), ns));
                }
            }
        } else {
            let armor = if s.shield > 0 { 7 } else { 0 };
            let dmg = (s.boss_damage - armor).max(1);
            s.player_hp -= dmg;
            if s.player_hp > 0 {
                s.player_turn = true;
                heap.push((Reverse(s.spent), s));
            }
        }
    }
    best
}

pub fn part1(input: &str) -> i32 {
    let (hp, dmg) = parse_boss(input);
    min_mana_to_win(hp, dmg, false)
}

pub fn part2(input: &str) -> i32 {
    let (hp, dmg) = parse_boss(input);
    min_mana_to_win(hp, dmg, true)
}

#[cfg(test)]
mod tests {
    use super::*;
    use crate::read_input;

    #[test]
    fn answers() {
        let input = read_input(22).expect("input");
        assert_eq!(900, part1(&input));
        assert_eq!(1216, part2(&input));
    }
}

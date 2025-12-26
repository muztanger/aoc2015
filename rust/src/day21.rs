use std::cmp::{max, min};

#[derive(Clone, Copy)]
struct Item {
    cost: i32,
    dmg: i32,
    arm: i32,
}

#[derive(Clone, Copy)]
struct Stats {
    hp: i32,
    dmg: i32,
    arm: i32,
}

fn parse_boss(input: &str) -> Stats {
    let mut hp = 0;
    let mut dmg = 0;
    let mut arm = 0;
    for line in input.lines() {
        let mut parts = line.split(':');
        let key = parts.next().unwrap().trim();
        let val: i32 = parts.next().unwrap().trim().parse().unwrap();
        match key {
            "Hit Points" => hp = val,
            "Damage" => dmg = val,
            "Armor" => arm = val,
            _ => {}
        }
    }
    Stats { hp, dmg, arm }
}

fn player_wins(mut player: Stats, mut boss: Stats) -> bool {
    while player.hp > 0 && boss.hp > 0 {
        let dmg_to_boss = max(1, player.dmg - boss.arm);
        boss.hp -= dmg_to_boss;
        if boss.hp <= 0 {
            break;
        }
        let dmg_to_player = max(1, boss.dmg - player.arm);
        player.hp -= dmg_to_player;
    }
    player.hp > 0
}

pub fn solve(input: &str) -> (i32, i32) {
    let boss = parse_boss(input);
    let weapons = vec![
        Item { cost: 8, dmg: 4, arm: 0 },
        Item { cost: 10, dmg: 5, arm: 0 },
        Item { cost: 25, dmg: 6, arm: 0 },
        Item { cost: 40, dmg: 7, arm: 0 },
        Item { cost: 74, dmg: 8, arm: 0 },
    ];
    let armors = vec![
        Item { cost: 0, dmg: 0, arm: 0 },
        Item { cost: 13, dmg: 0, arm: 1 },
        Item { cost: 31, dmg: 0, arm: 2 },
        Item { cost: 53, dmg: 0, arm: 3 },
        Item { cost: 75, dmg: 0, arm: 4 },
        Item { cost: 102, dmg: 0, arm: 5 },
    ];
    let rings = vec![
        Item { cost: 0, dmg: 0, arm: 0 },
        Item { cost: 0, dmg: 0, arm: 0 },
        Item { cost: 25, dmg: 1, arm: 0 },
        Item { cost: 50, dmg: 2, arm: 0 },
        Item { cost: 100, dmg: 3, arm: 0 },
        Item { cost: 20, dmg: 0, arm: 1 },
        Item { cost: 40, dmg: 0, arm: 2 },
        Item { cost: 80, dmg: 0, arm: 3 },
    ];

    let mut min_cost_win = i32::MAX;
    let mut max_cost_lose = 0;

    for w in &weapons {
        for a in &armors {
            for i in 0..rings.len() {
                for j in i + 1..rings.len() {
                    let r1 = rings[i];
                    let r2 = rings[j];
                    let cost = w.cost + a.cost + r1.cost + r2.cost;
                    let dmg = w.dmg + a.dmg + r1.dmg + r2.dmg;
                    let arm = w.arm + a.arm + r1.arm + r2.arm;
                    let player = Stats { hp: 100, dmg, arm };
                    if player_wins(player, boss) {
                        min_cost_win = min(min_cost_win, cost);
                    } else {
                        max_cost_lose = max(max_cost_lose, cost);
                    }
                }
            }
        }
    }

    (min_cost_win, max_cost_lose)
}

pub fn part1(input: &str) -> i32 {
    solve(input).0
}

pub fn part2(input: &str) -> i32 {
    solve(input).1
}

#[cfg(test)]
mod tests {
    use super::*;
    use crate::read_input;

    #[test]
    fn answers() {
        let input = read_input(21).expect("input");
        let (p1, p2) = solve(&input);
        println!("Day 21 part1: {}, part2: {}", p1, p2);
    }
}

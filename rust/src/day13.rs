use std::collections::{HashMap, HashSet};
use itertools::Itertools;

type Happy = HashMap<(String, String), i32>;

fn parse(input: &str, add_self: bool) -> (HashSet<String>, Happy) {
    let mut guests = HashSet::new();
    let mut map = Happy::new();
    for line in input.lines().filter(|l| !l.trim().is_empty()) {
        let parts: Vec<&str> = line.split_whitespace().collect();
        let who = parts[0].to_string();
        let sign = if parts[2] == "gain" { 1 } else { -1 };
        let points: i32 = parts[3].parse().unwrap();
        let next = parts[10].trim_end_matches('.').to_string();
        guests.insert(who.clone());
        guests.insert(next.clone());
        map.insert((who, next), sign * points);
    }
    if add_self {
        for g in guests.clone() {
            map.insert(("you".into(), g.clone()), 0);
            map.insert((g.clone(), "you".into()), 0);
        }
        guests.insert("you".into());
    }
    (guests, map)
}

fn score(order: &[String], map: &Happy) -> i32 {
    let n = order.len();
    let mut sum = 0;
    for i in 0..n {
        let left = &order[i];
        let right = &order[(i + 1) % n];
        let prev = &order[(i + n - 1) % n];
        sum += map[&(left.clone(), right.clone())];
        sum += map[&(left.clone(), prev.clone())];
    }
    sum
}

pub fn part1(input: &str) -> i32 {
    let (guests, map) = parse(input, false);
    guests
        .iter()
        .permutations(guests.len())
        .map(|p| score(&p.iter().cloned().cloned().collect::<Vec<_>>(), &map))
        .max()
        .unwrap()
}

pub fn part2(input: &str) -> i32 {
    let (guests, map) = parse(input, true);
    guests
        .iter()
        .permutations(guests.len())
        .map(|p| score(&p.iter().cloned().cloned().collect::<Vec<_>>(), &map))
        .max()
        .unwrap()
}

#[cfg(test)]
mod tests {
    use super::*;
    use crate::read_input;

    #[test]
    fn part1_answer() {
        let input = read_input(13).expect("input");
        assert_eq!(664, part1(&input));
    }

    #[test]
    fn part2_answer() {
        let input = read_input(13).expect("input");
        assert_eq!(640, part2(&input));
    }
}

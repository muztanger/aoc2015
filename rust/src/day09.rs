use std::collections::{HashMap, HashSet};
use itertools::Itertools;

type DistMap = HashMap<(String, String), i32>;

fn parse(input: &str) -> (DistMap, Vec<String>) {
    let mut map = HashMap::new();
    let mut cities: HashSet<String> = HashSet::new();
    for line in input.lines().filter(|l| !l.trim().is_empty()) {
        let parts: Vec<&str> = line.split_whitespace().collect();
        let from = parts[0].to_string();
        let to = parts[2].to_string();
        let dist: i32 = parts[4].parse().unwrap();
        map.insert((from.clone(), to.clone()), dist);
        map.insert((to.clone(), from.clone()), dist);
        cities.insert(from);
        cities.insert(to);
    }
    (map, cities.into_iter().collect())
}

fn path_length(order: &[String], map: &DistMap) -> i32 {
    order
        .windows(2)
        .map(|w| map[&(w[0].clone(), w[1].clone())])
        .sum()
}

pub fn part1(input: &str) -> i32 {
    let (map, cities) = parse(input);
    cities
        .iter()
        .permutations(cities.len())
        .map(|p| path_length(&p.iter().cloned().cloned().collect::<Vec<_>>(), &map))
        .min()
        .unwrap()
}

pub fn part2(input: &str) -> i32 {
    let (map, cities) = parse(input);
    cities
        .iter()
        .permutations(cities.len())
        .map(|p| path_length(&p.iter().cloned().cloned().collect::<Vec<_>>(), &map))
        .max()
        .unwrap()
}

#[cfg(test)]
mod tests {
    use super::*;
    use crate::read_input;

    #[test]
    fn part1_answer() {
        let input = read_input(9).expect("input");
        assert_eq!(251, part1(&input));
    }

    #[test]
    fn part2_answer() {
        let input = read_input(9).expect("input");
        assert_eq!(898, part2(&input));
    }
}

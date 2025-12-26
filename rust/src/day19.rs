use regex::Regex;
use std::collections::{HashMap, HashSet};

fn parse(input: &str) -> (HashMap<String, Vec<String>>, String) {
    let mut rules: HashMap<String, Vec<String>> = HashMap::new();
    let mut molecule = String::new();
    for line in input.lines() {
        let line = line.trim();
        if line.is_empty() {
            continue;
        }
        if let Some((lhs, rhs)) = line.split_once(" => ") {
            rules.entry(lhs.to_string()).or_default().push(rhs.to_string());
        } else {
            molecule = line.to_string();
        }
    }
    (rules, molecule)
}

pub fn part1(input: &str) -> usize {
    let (rules, molecule) = parse(input);
    let mut results = HashSet::new();
    for (lhs, rhs_list) in &rules {
        for (idx, _) in molecule.match_indices(lhs) {
            for rhs in rhs_list {
                let mut new_mol = String::new();
                new_mol.push_str(&molecule[..idx]);
                new_mol.push_str(rhs);
                new_mol.push_str(&molecule[idx + lhs.len()..]);
                results.insert(new_mol);
            }
        }
    }
    results.len()
}

fn tokenize(molecule: &str) -> Vec<String> {
    let re = Regex::new(r"[A-Z][a-z]?").unwrap();
    re.find_iter(molecule).map(|m| m.as_str().to_string()).collect()
}

pub fn part2(input: &str) -> i32 {
    let (_, molecule) = parse(input);
    let tokens = tokenize(&molecule);
    let num_tokens = tokens.len() as i32;
    let num_rn = tokens.iter().filter(|t| t.as_str() == "Rn").count() as i32;
    let num_ar = tokens.iter().filter(|t| t.as_str() == "Ar").count() as i32;
    let num_y = tokens.iter().filter(|t| t.as_str() == "Y").count() as i32;
    num_tokens - num_rn - num_ar - 2 * num_y - 1
}

#[cfg(test)]
mod tests {
    use super::*;
    use crate::read_input;

    #[test]
    fn part1_answer() {
        let input = read_input(19).expect("input");
        assert_eq!(576, part1(&input));
    }

    #[test]
    fn part2_answer() {
        let input = read_input(19).expect("input");
        assert_eq!(207, part2(&input));
    }
}

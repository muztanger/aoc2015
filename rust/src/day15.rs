use regex::Regex;

fn parse(input: &str) -> Vec<[i32; 5]> {
    let re = Regex::new(r"-?\d+").unwrap();
    input
        .lines()
        .filter(|l| !l.trim().is_empty())
        .map(|line| {
            let nums: Vec<i32> = re
                .find_iter(line)
                .map(|m| m.as_str().parse::<i32>().unwrap())
                .collect();
            [nums[0], nums[1], nums[2], nums[3], nums[4]]
        })
        .collect()
}

fn score(ingredients: &[[i32; 5]], amounts: &[i32], require_calories: bool) -> i32 {
    let mut totals = [0; 5];
    for (amt, ingr) in amounts.iter().zip(ingredients) {
        for i in 0..5 {
            totals[i] += amt * ingr[i];
        }
    }
    if require_calories && totals[4] != 500 {
        return 0;
    }
    let mut prod = 1;
    for i in 0..4 {
        if totals[i] <= 0 {
            return 0;
        }
        prod *= totals[i];
    }
    prod
}

fn search(input: &str, require_calories: bool) -> i32 {
    let ingredients = parse(input);
    let n = ingredients.len();
    assert_eq!(n, 4);
    let mut best = 0;
    for a in 0..=100 {
        for b in 0..=100 - a {
            for c in 0..=100 - a - b {
                let d = 100 - a - b - c;
                let amounts = [a as i32, b as i32, c as i32, d as i32];
                best = best.max(score(&ingredients, &amounts, require_calories));
            }
        }
    }
    best
}

pub fn part1(input: &str) -> i32 {
    search(input, false)
}

pub fn part2(input: &str) -> i32 {
    search(input, true)
}

#[cfg(test)]
mod tests {
    use super::*;
    use crate::read_input;

    #[test]
    fn part1_answer() {
        let input = read_input(15).expect("input");
        // C# doesn't assert, only prints
        let result = part1(&input);
        println!("Day 15 part1: {}", result);
    }

    #[test]
    fn part2_answer() {
        let input = read_input(15).expect("input");
        // C# doesn't assert, only prints
        let result = part2(&input);
        println!("Day 15 part2: {}", result);
    }
}

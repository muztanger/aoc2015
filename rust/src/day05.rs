use std::collections::HashMap;

fn is_nice(s: &str) -> bool {
    let mut vowels = 0;
    let mut has_double = false;
    let mut prev = '\0';
    for c in s.chars() {
        if matches!(c, 'a' | 'e' | 'i' | 'o' | 'u') {
            vowels += 1;
        }
        if c == prev {
            has_double = true;
        }
        if matches!((prev, c), ('a', 'b') | ('c', 'd') | ('p', 'q') | ('x', 'y')) {
            return false;
        }
        prev = c;
    }
    has_double && vowels >= 3
}

fn is_nice2(s: &str) -> bool {
    let mut pair_pos: HashMap<(char, char), usize> = HashMap::new();
    let mut has_pair = false;
    let mut has_repeat = false;
    let chars: Vec<char> = s.chars().collect();
    for i in 0..chars.len() {
        if i + 1 < chars.len() {
            let pair = (chars[i], chars[i + 1]);
            if let Some(&prev) = pair_pos.get(&pair) {
                if i - prev >= 2 {
                    has_pair = true;
                }
            } else {
                pair_pos.insert(pair, i);
            }
        }
        if i + 2 < chars.len() && chars[i] == chars[i + 2] {
            has_repeat = true;
        }
        if has_pair && has_repeat {
            return true;
        }
    }
    has_pair && has_repeat
}

pub fn part1(input: &str) -> usize {
    input.lines().filter(|l| is_nice(l.trim())).count()
}

pub fn part2(input: &str) -> usize {
    input.lines().filter(|l| is_nice2(l.trim())).count()
}

#[cfg(test)]
mod tests {
    use super::*;
    use crate::read_input;

    #[test]
    fn examples_part1() {
        assert!(is_nice("ugknbfddgicrmopn"));
        assert!(is_nice("aaa"));
        assert!(!is_nice("jchzalrnumimnmhp"));
        assert!(!is_nice("haegwjzuvuyypxyu"));
        assert!(!is_nice("dvszwmarrgswjxmb"));
    }

    #[test]
    fn part1_answer() {
        let input = read_input(5).expect("input");
        assert_eq!(238, part1(&input));
    }

    #[test]
    fn examples_part2() {
        assert!(is_nice2("qjhvhtzxzqqjkmpb"));
        assert!(is_nice2("xxyxx"));
        assert!(!is_nice2("uurcxstgmygtbstg"));
        assert!(!is_nice2("ieodomkazucvgmuy"));
    }

    #[test]
    fn part2_answer() {
        let input = read_input(5).expect("input");
        assert_eq!(69, part2(&input));
    }
}

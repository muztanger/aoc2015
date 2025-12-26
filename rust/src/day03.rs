use std::collections::HashSet;

#[derive(Debug, Clone, Copy, PartialEq, Eq, Hash)]
struct Pos {
    x: i32,
    y: i32,
}

fn step(pos: Pos, c: char) -> Pos {
    match c {
        '>' => Pos { x: pos.x + 1, y: pos.y },
        'v' => Pos { x: pos.x, y: pos.y + 1 },
        '<' => Pos { x: pos.x - 1, y: pos.y },
        '^' => Pos { x: pos.x, y: pos.y - 1 },
        _ => pos,
    }
}

pub fn part1(input: &str) -> usize {
    let mut visited: HashSet<Pos> = HashSet::new();
    let mut pos = Pos { x: 0, y: 0 };
    visited.insert(pos);
    for c in input.chars() {
        pos = step(pos, c);
        visited.insert(pos);
    }
    visited.len()
}

pub fn part2(input: &str) -> usize {
    let mut visited: HashSet<Pos> = HashSet::new();
    let mut santa = Pos { x: 0, y: 0 };
    let mut robo = Pos { x: 0, y: 0 };
    visited.insert(santa);

    for (idx, c) in input.chars().enumerate() {
        if idx % 2 == 0 {
            santa = step(santa, c);
            visited.insert(santa);
        } else {
            robo = step(robo, c);
            visited.insert(robo);
        }
    }

    visited.len()
}

#[cfg(test)]
mod tests {
    use super::*;
    use crate::read_input;

    #[test]
    fn examples_part1() {
        assert_eq!(2, part1(">"));
        assert_eq!(4, part1("^>v<"));
        assert_eq!(2, part1("^v^v^v^v^v"));
    }

    #[test]
    fn part1_answer() {
        let input = read_input(3).expect("input");
        assert_eq!(2572, part1(&input));
    }

    #[test]
    fn examples_part2() {
        assert_eq!(3, part2("^v"));
        assert_eq!(3, part2("^>v<"));
        assert_eq!(11, part2("^v^v^v^v^v"));
    }

    #[test]
    fn part2_answer() {
        let input = read_input(3).expect("input");
        assert_eq!(2631, part2(&input));
    }
}

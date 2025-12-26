#[derive(Clone, Copy)]
enum Op {
    On,
    Off,
    Toggle,
}

fn parse_line(line: &str) -> Option<(Op, (usize, usize), (usize, usize))> {
    let line = line.trim();
    if line.is_empty() {
        return None;
    }
    let (op, rest) = if let Some(rem) = line.strip_prefix("turn on ") {
        (Op::On, rem)
    } else if let Some(rem) = line.strip_prefix("turn off ") {
        (Op::Off, rem)
    } else if let Some(rem) = line.strip_prefix("toggle ") {
        (Op::Toggle, rem)
    } else {
        return None;
    };

    let mut parts = rest.split(" through ");
    let p1 = parts.next()?;
    let p2 = parts.next()?;
    let parse_pair = |s: &str| {
        let mut nums = s.split(',');
        Some((nums.next()?.parse::<usize>().ok()?, nums.next()?.parse::<usize>().ok()?))
    };
    Some((op, parse_pair(p1)?, parse_pair(p2)?))
}

pub fn part1(input: &str) -> usize {
    let mut grid = vec![false; 1_000_000];
    for line in input.lines() {
        if let Some((op, (x1, y1), (x2, y2))) = parse_line(line) {
            for x in x1..=x2 {
                let row = x * 1000;
                for y in y1..=y2 {
                    let idx = row + y;
                    match op {
                        Op::On => grid[idx] = true,
                        Op::Off => grid[idx] = false,
                        Op::Toggle => grid[idx] = !grid[idx],
                    }
                }
            }
        }
    }
    grid.into_iter().filter(|b| *b).count()
}

pub fn part2(input: &str) -> usize {
    let mut grid = vec![0i32; 1_000_000];
    for line in input.lines() {
        if let Some((op, (x1, y1), (x2, y2))) = parse_line(line) {
            for x in x1..=x2 {
                let row = x * 1000;
                for y in y1..=y2 {
                    let idx = row + y;
                    match op {
                        Op::On => grid[idx] += 1,
                        Op::Off => grid[idx] = (grid[idx] - 1).max(0),
                        Op::Toggle => grid[idx] += 2,
                    }
                }
            }
        }
    }
    grid.into_iter().map(|v| v as usize).sum()
}

#[cfg(test)]
mod tests {
    use super::*;
    use crate::read_input;

    #[test]
    fn part1_answer() {
        let input = read_input(6).expect("input");
        assert_eq!(569_999, part1(&input));
    }

    #[test]
    fn part2_answer() {
        let input = read_input(6).expect("input");
        assert_eq!(17_836_115, part2(&input));
    }
}

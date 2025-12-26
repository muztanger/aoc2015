fn unescaped_len(s: &str) -> usize {
    let mut chars = s.chars();
    let mut len = 0;
    // skip leading and trailing quotes
    chars.next();
    while let Some(c) = chars.next() {
        if c == '"' {
            break;
        }
        if c == '\\' {
            match chars.next() {
                Some('x') => {
                    // skip two hex digits
                    chars.next();
                    chars.next();
                    len += 1;
                }
                Some(_) => len += 1,
                None => {}
            }
        } else {
            len += 1;
        }
    }
    len
}

fn escaped_len(s: &str) -> usize {
    let mut len = 2; // surrounding quotes
    for c in s.chars() {
        match c {
            '"' | '\\' => len += 2,
            _ => len += 1,
        }
    }
    len
}

pub fn part1(input: &str) -> isize {
    input
        .lines()
        .map(|line| line.len() as isize - unescaped_len(line) as isize)
        .sum()
}

pub fn part2(input: &str) -> isize {
    input
        .lines()
        .map(|line| escaped_len(line) as isize - line.len() as isize)
        .sum()
}

#[cfg(test)]
mod tests {
    use super::*;
    use crate::read_input;

    #[test]
    fn part1_answer() {
        let input = read_input(8).expect("input");
        assert_eq!(1333, part1(&input));
    }

    #[test]
    fn part2_answer() {
        let input = read_input(8).expect("input");
        assert_eq!(2046, part2(&input));
    }
}

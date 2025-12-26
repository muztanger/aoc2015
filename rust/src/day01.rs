pub fn part1(input: &str) -> i32 {
    input.chars().fold(0, |acc, c| match c {
        '(' => acc + 1,
        ')' => acc - 1,
        _ => acc,
    })
}

pub fn part2(input: &str) -> usize {
    let mut floor = 0;
    for (idx, c) in input.chars().enumerate() {
        match c {
            '(' => floor += 1,
            ')' => floor -= 1,
            _ => {}
        }
        if floor == -1 {
            return idx + 1;
        }
    }
    panic!("basement not reached");
}

#[cfg(test)]
mod tests {
    use super::*;
    use crate::read_input;

    #[test]
    fn examples_part1() {
        assert_eq!(0, part1("(())"));
        assert_eq!(0, part1("()()"));
        assert_eq!(3, part1("((("));
        assert_eq!(3, part1("(()(()("));
        assert_eq!(3, part1("))((((("));
        assert_eq!(-1, part1("())"));
        assert_eq!(-1, part1("))("));
        assert_eq!(-3, part1(")))"));
        assert_eq!(-3, part1(")())())"));
    }

    #[test]
    fn part1_answer() {
        let input = read_input(1).expect("input");
        assert_eq!(232, part1(&input));
    }

    #[test]
    fn examples_part2() {
        assert_eq!(1, part2(")"));
        assert_eq!(5, part2("()())"));
    }

    #[test]
    fn part2_answer() {
        let input = read_input(1).expect("input");
        assert_eq!(1783, part2(&input));
    }
}

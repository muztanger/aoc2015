use regex::Regex;

fn parse(input: &str) -> (i64, i64) {
    let re = Regex::new(r"row (\d+), column (\d+)").unwrap();
    let caps = re.captures(input).unwrap();
    let row: i64 = caps[1].parse().unwrap();
    let col: i64 = caps[2].parse().unwrap();
    (row, col)
}

fn code_at(row: i64, col: i64) -> i64 {
    let diag = row + col - 1;
    let pos = diag * (diag - 1) / 2 + col;
    let mut code: i64 = 20151125;
    for _ in 1..pos {
        code = (code * 252533) % 33554393;
    }
    code
}

pub fn part1(input: &str) -> i64 {
    let (row, col) = parse(input.trim());
    code_at(row, col)
}

#[cfg(test)]
mod tests {
    use super::*;
    use crate::read_input;

    #[test]
    fn part1_answer() {
        let input = read_input(25).expect("input");
        assert_eq!(2_650_453, part1(&input));
    }
}

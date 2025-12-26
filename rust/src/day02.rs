fn parse_dims(line: &str) -> Option<[i32; 3]> {
    let mut parts = line.split('x');
    let a = parts.next()?.parse().ok()?;
    let b = parts.next()?.parse().ok()?;
    let c = parts.next()?.parse().ok()?;
    Some([a, b, c])
}

fn paper(l: i32, w: i32, h: i32) -> i32 {
    let a1 = l * w;
    let a2 = w * h;
    let a3 = h * l;
    let extra = *[a1, a2, a3].iter().min().unwrap();
    2 * (a1 + a2 + a3) + extra
}

fn ribbon(l: i32, w: i32, h: i32) -> i32 {
    let mut dims = [l, w, h];
    dims.sort_unstable();
    2 * (dims[0] + dims[1]) + dims[0] * dims[1] * dims[2]
}

pub fn part1(input: &str) -> i32 {
    input
        .lines()
        .filter_map(parse_dims)
        .map(|[l, w, h]| paper(l, w, h))
        .sum()
}

pub fn part2(input: &str) -> i32 {
    input
        .lines()
        .filter_map(parse_dims)
        .map(|[l, w, h]| ribbon(l, w, h))
        .sum()
}

#[cfg(test)]
mod tests {
    use super::*;
    use crate::read_input;

    #[test]
    fn examples_part1() {
        assert_eq!(58, paper(2, 3, 4));
        assert_eq!(43, paper(1, 1, 10));
    }

    #[test]
    fn part1_answer() {
        let input = read_input(2).expect("input");
        assert_eq!(1_588_178, part1(&input));
    }

    #[test]
    fn examples_part2() {
        assert_eq!(34, ribbon(2, 3, 4));
        assert_eq!(14, ribbon(1, 1, 10));
    }

    #[test]
    fn part2_answer() {
        let input = read_input(2).expect("input");
        assert_eq!(3_783_758, part2(&input));
    }
}

fn find_hash(key: &str, prefix: &str) -> u32 {
    let mut i: u32 = 0;
    loop {
        let hash = format!("{:x}", md5::compute(format!("{key}{i}")));
        if hash.starts_with(prefix) {
            return i;
        }
        i = i.wrapping_add(1);
    }
}

pub fn part1(input: &str) -> u32 {
    let key = input.trim();
    find_hash(key, "00000")
}

pub fn part2(input: &str) -> u32 {
    let key = input.trim();
    find_hash(key, "000000")
}

#[cfg(test)]
mod tests {
    use super::*;
    use crate::read_input;

    #[test]
    fn examples_part1() {
        assert_eq!(609_043, find_hash("abcdef", "00000"));
        assert_eq!(1_048_970, find_hash("pqrstuv", "00000"));
    }

    #[test]
    fn part1_answer() {
        let input = read_input(4).expect("input");
        assert_eq!(282_749, part1(&input));
    }

    #[test]
    fn part2_answer() {
        let input = read_input(4).expect("input");
        assert_eq!(9_962_624, part2(&input));
    }
}

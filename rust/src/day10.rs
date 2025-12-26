pub fn look_and_say(s: &str) -> String {
    let mut result = String::new();
    let mut chars = s.chars().peekable();
    while let Some(c) = chars.next() {
        let mut count = 1;
        while let Some(&next) = chars.peek() {
            if next == c {
                count += 1;
                chars.next();
            } else {
                break;
            }
        }
        result.push_str(&format!("{}{}", count, c));
    }
    result
}

pub fn part1(input: &str) -> usize {
    let mut s = input.trim().to_string();
    for _ in 0..40 {
        s = look_and_say(&s);
    }
    s.len()
}

pub fn part2(input: &str) -> usize {
    let mut s = input.trim().to_string();
    for _ in 0..50 {
        s = look_and_say(&s);
    }
    s.len()
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn part1_answer() {
        assert_eq!(360_154, part1("1113122113"));
    }

    #[test]
    fn part2_answer() {
        assert_eq!(5_103_798, part2("1113122113"));
    }
}

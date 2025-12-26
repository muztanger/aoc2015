fn increment(pwd: &str) -> String {
    let mut bytes: Vec<u8> = pwd.bytes().collect();
    let mut i = bytes.len();
    while i > 0 {
        i -= 1;
        bytes[i] += 1;
        if bytes[i] > b'z' {
            bytes[i] = b'a';
        } else {
            break;
        }
    }
    String::from_utf8(bytes).unwrap()
}

fn is_valid(pwd: &str) -> bool {
    if pwd.bytes().any(|b| matches!(b, b'i' | b'o' | b'l')) {
        return false;
    }
    let bytes = pwd.as_bytes();
    let mut has_straight = false;
    for w in bytes.windows(3) {
        if w[0] + 1 == w[1] && w[1] + 1 == w[2] {
            has_straight = true;
            break;
        }
    }
    if !has_straight {
        return false;
    }
    let mut pairs = 0;
    let mut i = 0;
    while i + 1 < bytes.len() {
        if bytes[i] == bytes[i + 1] {
            pairs += 1;
            i += 2;
            if pairs >= 2 {
                return true;
            }
        } else {
            i += 1;
        }
    }
    false
}

fn next_password(mut pwd: String) -> String {
    loop {
        pwd = increment(&pwd);
        if is_valid(&pwd) {
            return pwd;
        }
    }
}

pub fn part1(input: &str) -> String {
    next_password(input.trim().to_string())
}

pub fn part2(input: &str) -> String {
    let first = next_password(input.trim().to_string());
    next_password(first)
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn part1_answer() {
        assert_eq!("cqjxxyzz", part1("cqjxjnds"));
    }

    #[test]
    fn part2_answer() {
        assert_eq!("cqkaabcc", part2("cqjxjnds"));
    }
}

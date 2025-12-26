use serde_json::Value;

fn sum_numbers(v: &Value) -> i64 {
    match v {
        Value::Number(n) => n.as_i64().unwrap(),
        Value::Array(arr) => arr.iter().map(sum_numbers).sum(),
        Value::Object(map) => map.values().map(sum_numbers).sum(),
        _ => 0,
    }
}

fn sum_without_red(v: &Value) -> i64 {
    match v {
        Value::Number(n) => n.as_i64().unwrap(),
        Value::Array(arr) => arr.iter().map(sum_without_red).sum(),
        Value::Object(map) => {
            if map.values().any(|val| matches!(val, Value::String(s) if s == "red")) {
                0
            } else {
                map.values().map(sum_without_red).sum()
            }
        }
        _ => 0,
    }
}

pub fn part1(input: &str) -> i64 {
    let v: Value = serde_json::from_str(input).unwrap();
    sum_numbers(&v)
}

pub fn part2(input: &str) -> i64 {
    let v: Value = serde_json::from_str(input).unwrap();
    sum_without_red(&v)
}

#[cfg(test)]
mod tests {
    use super::*;
    use crate::read_input;

    #[test]
    fn part1_answer() {
        let input = read_input(12).expect("input");
        assert_eq!(191_164, part1(&input));
    }

    #[test]
    fn part2_answer() {
        let input = read_input(12).expect("input");
        assert_eq!(87_842, part2(&input));
    }
}

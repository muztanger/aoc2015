use std::collections::HashMap;

#[derive(Clone, Debug)]
enum Expr {
    Val(u16),
    Wire(String),
    Not(Box<Expr>),
    And(Box<Expr>, Box<Expr>),
    Or(Box<Expr>, Box<Expr>),
    LShift(Box<Expr>, u16),
    RShift(Box<Expr>, u16),
}

fn parse_token(tok: &str) -> Expr {
    if let Ok(v) = tok.parse::<u16>() {
        Expr::Val(v)
    } else {
        Expr::Wire(tok.to_string())
    }
}

fn parse_line(line: &str) -> Option<(String, Expr)> {
    let parts: Vec<&str> = line.split_whitespace().collect();
    if parts.is_empty() {
        return None;
    }
    let target = parts.last()?.to_string();
    let expr = match parts.as_slice() {
        [lhs, "->", _dst] => parse_token(lhs),
        ["NOT", rhs, "->", _dst] => Expr::Not(Box::new(parse_token(rhs))),
        [a, "AND", b, "->", _dst] => Expr::And(Box::new(parse_token(a)), Box::new(parse_token(b))),
        [a, "OR", b, "->", _dst] => Expr::Or(Box::new(parse_token(a)), Box::new(parse_token(b))),
        [a, "LSHIFT", n, "->", _dst] => Expr::LShift(Box::new(parse_token(a)), n.parse().unwrap()),
        [a, "RSHIFT", n, "->", _dst] => Expr::RShift(Box::new(parse_token(a)), n.parse().unwrap()),
        _ => return None,
    };
    Some((target, expr))
}

fn eval(name: &str, map: &mut HashMap<String, Expr>, memo: &mut HashMap<String, u16>) -> u16 {
    if let Some(&v) = memo.get(name) {
        return v;
    }
    let expr = map.get(name).cloned().expect("wire missing");
    let val = match expr {
        Expr::Val(v) => v,
        Expr::Wire(w) => eval(&w, map, memo),
        Expr::Not(a) => !eval_expr(*a, map, memo) & 0xFFFF,
        Expr::And(a, b) => eval_expr(*a, map, memo) & eval_expr(*b, map, memo),
        Expr::Or(a, b) => eval_expr(*a, map, memo) | eval_expr(*b, map, memo),
        Expr::LShift(a, n) => eval_expr(*a, map, memo) << n,
        Expr::RShift(a, n) => eval_expr(*a, map, memo) >> n,
    };
    memo.insert(name.to_string(), val);
    val
}

fn eval_expr(expr: Expr, map: &mut HashMap<String, Expr>, memo: &mut HashMap<String, u16>) -> u16 {
    match expr {
        Expr::Val(v) => v,
        Expr::Wire(w) => eval(&w, map, memo),
        Expr::Not(a) => !eval_expr(*a, map, memo) & 0xFFFF,
        Expr::And(a, b) => eval_expr(*a, map, memo) & eval_expr(*b, map, memo),
        Expr::Or(a, b) => eval_expr(*a, map, memo) | eval_expr(*b, map, memo),
        Expr::LShift(a, n) => eval_expr(*a, map, memo) << n,
        Expr::RShift(a, n) => eval_expr(*a, map, memo) >> n,
    }
}

fn build_map(input: &str) -> HashMap<String, Expr> {
    let mut map = HashMap::new();
    for line in input.lines() {
        if let Some((dst, expr)) = parse_line(line.trim()) {
            map.insert(dst, expr);
        }
    }
    map
}

pub fn part1(input: &str) -> u16 {
    let mut map = build_map(input);
    let mut memo = HashMap::new();
    eval("a", &mut map, &mut memo)
}

pub fn part2(input: &str) -> u16 {
    let mut map = build_map(input);
    let a_val = {
        let mut memo = HashMap::new();
        eval("a", &mut map.clone(), &mut memo)
    };
    map.insert("b".to_string(), Expr::Val(a_val));
    let mut memo = HashMap::new();
    eval("a", &mut map, &mut memo)
}

#[cfg(test)]
mod tests {
    use super::*;
    use crate::read_input;

    #[test]
    fn examples_part1() {
        let example = r#"123 -> x
456 -> y
x AND y -> d
x OR y -> e
x LSHIFT 2 -> f
y RSHIFT 2 -> g
NOT x -> h
NOT y -> i"#;
        let mut map = build_map(example);
        let mut memo = HashMap::new();
        assert_eq!(72, eval("d", &mut map, &mut memo));
        memo.clear();
        assert_eq!(507, eval("e", &mut map, &mut memo));
    }

    #[test]
    fn part1_answer() {
        let input = read_input(7).expect("input");
        assert_eq!(956, part1(&input));
    }

    #[test]
    fn part2_answer() {
        let input = read_input(7).expect("input");
        assert_eq!(40149, part2(&input));
    }
}

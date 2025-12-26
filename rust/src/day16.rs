use regex::Regex;
use std::collections::HashMap;

fn parse(input: &str) -> Vec<(u32, HashMap<String, i32>)> {
    let re = Regex::new(r"Sue (\d+): (.*)").unwrap();
    input
        .lines()
        .filter_map(|line| {
            let caps = re.captures(line.trim())?;
            let id: u32 = caps[1].parse().ok()?;
            let rest = &caps[2];
            let mut map = HashMap::new();
            for part in rest.split(',') {
                let mut kv = part.trim().split(':');
                let k = kv.next()?.trim().to_string();
                let v: i32 = kv.next()?.trim().parse().ok()?;
                map.insert(k, v);
            }
            Some((id, map))
        })
        .collect()
}

fn matches_part1(props: &HashMap<String, i32>, target: &HashMap<&str, i32>) -> bool {
    props
        .iter()
        .all(|(k, v)| target.get(k.as_str()).map_or(false, |t| t == v))
}

fn matches_part2(props: &HashMap<String, i32>, target: &HashMap<&str, i32>) -> bool {
    props.iter().all(|(k, v)| match k.as_str() {
        "cats" | "trees" => target.get(k.as_str()).map_or(false, |t| v > t),
        "pomeranians" | "goldfish" => target.get(k.as_str()).map_or(false, |t| v < t),
        _ => target.get(k.as_str()).map_or(false, |t| v == t),
    })
}

pub fn part1(input: &str) -> u32 {
    let target: HashMap<&str, i32> = vec![
        ("children", 3),
        ("cats", 7),
        ("samoyeds", 2),
        ("pomeranians", 3),
        ("akitas", 0),
        ("vizslas", 0),
        ("goldfish", 5),
        ("trees", 3),
        ("cars", 2),
        ("perfumes", 1),
    ]
    .into_iter()
    .collect();

    let sues = parse(input);
    sues
        .into_iter()
        .find(|(_, props)| matches_part1(props, &target))
        .map(|(id, _)| id)
        .unwrap()
}

pub fn part2(input: &str) -> u32 {
    let target: HashMap<&str, i32> = vec![
        ("children", 3),
        ("cats", 7),
        ("samoyeds", 2),
        ("pomeranians", 3),
        ("akitas", 0),
        ("vizslas", 0),
        ("goldfish", 5),
        ("trees", 3),
        ("cars", 2),
        ("perfumes", 1),
    ]
    .into_iter()
    .collect();

    let sues = parse(input);
    sues
        .into_iter()
        .find(|(_, props)| matches_part2(props, &target))
        .map(|(id, _)| id)
        .unwrap()
}

#[cfg(test)]
mod tests {
    use super::*;
    use crate::read_input;

    #[test]
    fn part1_answer() {
        let input = read_input(16).expect("input");
        assert_eq!(373, part1(&input));
    }

    #[test]
    fn part2_answer() {
        let input = read_input(16).expect("input");
        assert_eq!(260, part2(&input));
    }
}

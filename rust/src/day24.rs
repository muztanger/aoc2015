use itertools::Itertools;

fn parse(input: &str) -> Vec<u64> {
    let mut v: Vec<u64> = input
        .lines()
        .filter(|l| !l.trim().is_empty())
        .map(|l| l.trim().parse().unwrap())
        .collect();
    v.sort_by(|a, b| b.cmp(a));
    v
}

fn min_qe(packages: &[u64], groups: u64) -> u64 {
    let total: u64 = packages.iter().sum();
    let target = total / groups;
    for size in 1..=packages.len() {
        let mut best: Option<u64> = None;
        for combo in packages.iter().combinations(size) {
            let sum: u64 = combo.iter().copied().sum();
            if sum == target {
                let qe = combo.iter().copied().product();
                best = Some(best.map_or(qe, |b| b.min(qe)));
            }
        }
        if let Some(qe) = best {
            return qe;
        }
    }
    unreachable!()
}

pub fn part1(input: &str) -> u64 {
    let packages = parse(input);
    min_qe(&packages, 3)
}

pub fn part2(input: &str) -> u64 {
    let packages = parse(input);
    min_qe(&packages, 4)
}

#[cfg(test)]
mod tests {
    use super::*;
    use crate::read_input;

    #[test]
    fn answers() {
        let input = read_input(24).expect("input");
        assert_eq!(11_846_773_891, part1(&input));
        assert_eq!(80_393_059, part2(&input));
    }
}

fn sum_divisors(n: usize) -> usize {
    let mut sum = 0;
    let limit = (n as f64).sqrt() as usize;
    for d in 1..=limit {
        if n % d == 0 {
            sum += d;
            let other = n / d;
            if other != d {
                sum += other;
            }
        }
    }
    sum
}

pub fn part1(target: usize) -> usize {
    let mut n = 1;
    loop {
        let presents = sum_divisors(n) * 10;
        if presents >= target {
            return n;
        }
        n += 1;
    }
}

pub fn part2(target: usize) -> usize {
    let max = target / 10; // upper bound
    let mut houses = vec![0usize; max];
    for elf in 1..max {
        let mut delivered = 0;
        let mut house = elf;
        while house < max && delivered < 50 {
            houses[house] += elf * 11;
            house += elf;
            delivered += 1;
        }
    }
    houses
        .iter()
        .position(|&p| p >= target)
        .unwrap()
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn part1_answer() {
        assert_eq!(786_240, part1(34_000_000));
    }

    #[test]
    fn part2_answer() {
        assert_eq!(831_600, part2(34_000_000));
    }
}

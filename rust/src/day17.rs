fn containers() -> Vec<i32> {
    vec![
        50, 44, 11, 49, 42, 46, 18, 32, 26, 40, 21, 7, 18, 43, 10, 47, 36, 24, 22, 40,
    ]
}

fn count_ways(target: i32) -> (i32, i32) {
    let cont = containers();
    let n = cont.len();
    let mut total = 0;
    let mut min_cont = i32::MAX;
    let mut min_count = 0;
    for mask in 1..(1u32 << n) {
        let mut sum = 0;
        let mut used = 0;
        for i in 0..n {
            if mask & (1 << i) != 0 {
                sum += cont[i];
                used += 1;
            }
        }
        if sum == target {
            total += 1;
            if (used as i32) < min_cont {
                min_cont = used as i32;
                min_count = 1;
            } else if used as i32 == min_cont {
                min_count += 1;
            }
        }
    }
    (total, min_count)
}

pub fn part1() -> i32 {
    count_ways(150).0
}

pub fn part2() -> i32 {
    count_ways(150).1
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn part1_answer() {
        assert_eq!(654, part1());
    }

    #[test]
    fn part2_answer() {
        assert_eq!(57, part2());
    }
}

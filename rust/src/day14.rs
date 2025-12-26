#[derive(Clone, Debug)]
struct Deer {
    speed: i32,
    fly: i32,
    rest: i32,
    dist: i32,
    time_in_state: i32,
    flying: bool,
    points: i32,
}

fn parse(input: &str) -> Vec<Deer> {
    let mut v = Vec::new();
    for line in input.lines().filter(|l| !l.trim().is_empty()) {
        let parts: Vec<&str> = line.split_whitespace().collect();
        let speed: i32 = parts[3].parse().unwrap();
        let fly: i32 = parts[6].parse().unwrap();
        let rest: i32 = parts[13].parse().unwrap();
        v.push(Deer {
            speed,
            fly,
            rest,
            dist: 0,
            time_in_state: 0,
            flying: true,
            points: 0,
        });
    }
    v
}

fn tick(deer: &mut Deer) {
    if deer.flying {
        deer.dist += deer.speed;
        deer.time_in_state += 1;
        if deer.time_in_state == deer.fly {
            deer.flying = false;
            deer.time_in_state = 0;
        }
    } else {
        deer.time_in_state += 1;
        if deer.time_in_state == deer.rest {
            deer.flying = true;
            deer.time_in_state = 0;
        }
    }
}

pub fn part1(input: &str) -> i32 {
    let mut deer = parse(input);
    for _ in 0..2503 {
        for d in deer.iter_mut() {
            tick(d);
        }
    }
    deer.iter().map(|d| d.dist).max().unwrap()
}

pub fn part2(input: &str) -> i32 {
    let mut deer = parse(input);
    for _ in 0..2503 {
        for d in deer.iter_mut() {
            tick(d);
        }
        let best = deer.iter().map(|d| d.dist).max().unwrap();
        for d in deer.iter_mut() {
            if d.dist == best {
                d.points += 1;
            }
        }
    }
    deer.iter().map(|d| d.points).max().unwrap()
}

#[cfg(test)]
mod tests {
    use super::*;
    use crate::read_input;

    #[test]
    fn part1_answer() {
        let input = read_input(14).expect("input");
        // C# code prints but doesn't assert; AoC answer known from output
        assert_eq!(2696, part1(&input));
    }

    #[test]
    fn part2_answer() {
        let input = read_input(14).expect("input");
        assert_eq!(1084, part2(&input));
    }
}

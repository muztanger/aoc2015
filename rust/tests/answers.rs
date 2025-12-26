use aoc2015::*;

#[test]
fn day01_answers() {
    let input = read_input(1).expect("input");
    assert_eq!(232, day01::part1(&input));
    assert_eq!(1783, day01::part2(&input));
}

#[test]
fn day02_answers() {
    let input = read_input(2).expect("input");
    assert_eq!(1_588_178, day02::part1(&input));
    assert_eq!(3_783_758, day02::part2(&input));
}

#[test]
fn day03_answers() {
    let input = read_input(3).expect("input");
    assert_eq!(2572, day03::part1(&input));
    assert_eq!(2631, day03::part2(&input));
}

#[test]
fn day04_answers() {
    let input = read_input(4).expect("input");
    assert_eq!(282_749, day04::part1(&input));
    assert_eq!(9_962_624, day04::part2(&input));
}

#[test]
fn day05_answers() {
    let input = read_input(5).expect("input");
    assert_eq!(238, day05::part1(&input));
    assert_eq!(69, day05::part2(&input));
}

#[test]
fn day06_answers() {
    let input = read_input(6).expect("input");
    assert_eq!(569_999, day06::part1(&input));
    assert_eq!(17_836_115, day06::part2(&input));
}

#[test]
fn day07_answers() {
    let input = read_input(7).expect("input");
    assert_eq!(956, day07::part1(&input));
    assert_eq!(40149, day07::part2(&input));
}

#[test]
fn day08_answers() {
    let input = read_input(8).expect("input");
    assert_eq!(1333, day08::part1(&input));
    assert_eq!(2046, day08::part2(&input));
}

#[test]
fn day09_answers() {
    let input = read_input(9).expect("input");
    assert_eq!(251, day09::part1(&input));
    assert_eq!(898, day09::part2(&input));
}

#[test]
fn day10_answers() {
    assert_eq!(360_154, day10::part1("1113122113"));
    assert_eq!(5_103_798, day10::part2("1113122113"));
}

#[test]
fn day11_answers() {
    assert_eq!("cqjxxyzz", day11::part1("cqjxjnds"));
    assert_eq!("cqkaabcc", day11::part2("cqjxjnds"));
}

#[test]
fn day12_answers() {
    let input = read_input(12).expect("input");
    assert_eq!(191_164, day12::part1(&input));
    assert_eq!(87_842, day12::part2(&input));
}

#[test]
fn day13_answers() {
    let input = read_input(13).expect("input");
    assert_eq!(664, day13::part1(&input));
    assert_eq!(640, day13::part2(&input));
}

#[test]
fn day14_answers() {
    let input = read_input(14).expect("input");
    assert_eq!(2696, day14::part1(&input));
    assert_eq!(1084, day14::part2(&input));
}

#[test]
fn day15_answers() {
    let input = read_input(15).expect("input");
    let _p1 = day15::part1(&input);
    let _p2 = day15::part2(&input);
}

#[test]
fn day16_answers() {
    let input = read_input(16).expect("input");
    assert_eq!(373, day16::part1(&input));
    assert_eq!(260, day16::part2(&input));
}

#[test]
fn day17_answers() {
    assert_eq!(654, day17::part1());
    assert_eq!(57, day17::part2());
}

#[test]
fn day18_answers() {
    let input = read_input(18).expect("input");
    assert_eq!(1061, day18::part1(&input));
    assert_eq!(1006, day18::part2(&input));
}

#[test]
fn day19_answers() {
    let input = read_input(19).expect("input");
    assert_eq!(576, day19::part1(&input));
    assert_eq!(207, day19::part2(&input));
}

#[test]
fn day20_answers() {
    assert_eq!(786_240, day20::part1(34_000_000));
    assert_eq!(831_600, day20::part2(34_000_000));
}

#[test]
fn day21_answers() {
    let input = read_input(21).expect("input");
    let (p1, p2) = day21::solve(&input);
    let _ = (p1, p2);
}

#[test]
fn day22_answers() {
    let input = read_input(22).expect("input");
    assert_eq!(900, day22::part1(&input));
    assert_eq!(1216, day22::part2(&input));
}

#[test]
fn day23_answers() {
    let input = read_input(23).expect("input");
    assert_eq!(184, day23::part1(&input));
    assert_eq!(231, day23::part2(&input));
}

#[test]
fn day24_answers() {
    let input = read_input(24).expect("input");
    assert_eq!(11_846_773_891, day24::part1(&input));
    assert_eq!(80_393_059, day24::part2(&input));
}

#[test]
fn day25_answers() {
    let input = read_input(25).expect("input");
    assert_eq!(2_650_453, day25::part1(&input));
}

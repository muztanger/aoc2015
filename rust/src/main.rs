use aoc2015::*;

fn main() -> Result<(), Box<dyn std::error::Error>> {
    let input1 = read_input(1)?;
    println!("Day 1 -> part1: {}, part2: {}", day01::part1(&input1), day01::part2(&input1));

    let input2 = read_input(2)?;
    println!("Day 2 -> part1: {}, part2: {}", day02::part1(&input2), day02::part2(&input2));

    let input3 = read_input(3)?;
    println!("Day 3 -> part1: {}, part2: {}", day03::part1(&input3), day03::part2(&input3));

    let input4 = read_input(4)?;
    println!("Day 4 -> part1: {}, part2: {}", day04::part1(&input4), day04::part2(&input4));

    let input5 = read_input(5)?;
    println!("Day 5 -> part1: {}, part2: {}", day05::part1(&input5), day05::part2(&input5));

    let input6 = read_input(6)?;
    println!("Day 6 -> part1: {}, part2: {}", day06::part1(&input6), day06::part2(&input6));

    let input7 = read_input(7)?;
    println!("Day 7 -> part1: {}, part2: {}", day07::part1(&input7), day07::part2(&input7));

    let input8 = read_input(8)?;
    println!("Day 8 -> part1: {}, part2: {}", day08::part1(&input8), day08::part2(&input8));

    let input9 = read_input(9)?;
    println!("Day 9 -> part1: {}, part2: {}", day09::part1(&input9), day09::part2(&input9));

    println!("Day 10 -> part1: {}, part2: {}", day10::part1("1113122113"), day10::part2("1113122113"));

    println!("Day 11 -> part1: {}, part2: {}", day11::part1("cqjxjnds"), day11::part2("cqjxjnds"));

    let input12 = read_input(12)?;
    println!("Day 12 -> part1: {}, part2: {}", day12::part1(&input12), day12::part2(&input12));

    let input13 = read_input(13)?;
    println!("Day 13 -> part1: {}, part2: {}", day13::part1(&input13), day13::part2(&input13));

    let input14 = read_input(14)?;
    println!("Day 14 -> part1: {}, part2: {}", day14::part1(&input14), day14::part2(&input14));

    let input15 = read_input(15)?;
    println!("Day 15 -> part1: {}, part2: {}", day15::part1(&input15), day15::part2(&input15));

    let input16 = read_input(16)?;
    println!("Day 16 -> part1: {}, part2: {}", day16::part1(&input16), day16::part2(&input16));

    println!("Day 17 -> part1: {}, part2: {}", day17::part1(), day17::part2());

    let input18 = read_input(18)?;
    println!("Day 18 -> part1: {}, part2: {}", day18::part1(&input18), day18::part2(&input18));

    let input19 = read_input(19)?;
    println!("Day 19 -> part1: {}, part2: {}", day19::part1(&input19), day19::part2(&input19));

    println!("Day 20 -> part1: {}, part2: {}", day20::part1(34_000_000), day20::part2(34_000_000));

    let input21 = read_input(21)?;
    let (p1, p2) = day21::solve(&input21);
    println!("Day 21 -> part1: {}, part2: {}", p1, p2);

    let input22 = read_input(22)?;
    println!("Day 22 -> part1: {}, part2: {}", day22::part1(&input22), day22::part2(&input22));

    let input23 = read_input(23)?;
    println!("Day 23 -> part1: {}, part2: {}", day23::part1(&input23), day23::part2(&input23));

    let input24 = read_input(24)?;
    println!("Day 24 -> part1: {}, part2: {}", day24::part1(&input24), day24::part2(&input24));

    let input25 = read_input(25)?;
    println!("Day 25 -> part1: {}", day25::part1(&input25));

    Ok(())
}

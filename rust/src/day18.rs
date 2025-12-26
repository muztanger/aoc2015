type Grid = Vec<Vec<bool>>;

fn parse(input: &str) -> Grid {
    input
        .lines()
        .filter(|l| !l.trim().is_empty())
        .map(|line| line.chars().map(|c| c == '#').collect())
        .collect()
}

fn step(grid: &Grid) -> Grid {
    let h = grid.len();
    let w = grid[0].len();
    let mut next = vec![vec![false; w]; h];
    for y in 0..h {
        for x in 0..w {
            let mut cnt = 0;
            for dy in -1..=1 {
                for dx in -1..=1 {
                    if dx == 0 && dy == 0 {
                        continue;
                    }
                    let ny = y as isize + dy;
                    let nx = x as isize + dx;
                    if ny >= 0 && ny < h as isize && nx >= 0 && nx < w as isize {
                        if grid[ny as usize][nx as usize] {
                            cnt += 1;
                        }
                    }
                }
            }
            next[y][x] = if grid[y][x] {
                cnt == 2 || cnt == 3
            } else {
                cnt == 3
            };
        }
    }
    next
}

fn count_on(grid: &Grid) -> usize {
    grid.iter().map(|row| row.iter().filter(|&&b| b).count()).sum()
}

pub fn part1(input: &str) -> usize {
    let mut grid = parse(input);
    for _ in 0..100 {
        grid = step(&grid);
    }
    count_on(&grid)
}

pub fn part2(input: &str) -> usize {
    let mut grid = parse(input);
    let h = grid.len();
    let w = grid[0].len();
    let set_corners = |g: &mut Grid| {
        g[0][0] = true;
        g[0][w - 1] = true;
        g[h - 1][0] = true;
        g[h - 1][w - 1] = true;
    };
    set_corners(&mut grid);
    for _ in 0..100 {
        grid = step(&grid);
        set_corners(&mut grid);
    }
    count_on(&grid)
}

#[cfg(test)]
mod tests {
    use super::*;
    use crate::read_input;

    #[test]
    fn part1_answer() {
        let input = read_input(18).expect("input");
        assert_eq!(1061, part1(&input));
    }

    #[test]
    fn part2_answer() {
        let input = read_input(18).expect("input");
        assert_eq!(1006, part2(&input));
    }
}

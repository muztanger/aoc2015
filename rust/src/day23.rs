struct Computer {
    a: i64,
    b: i64,
    pc: i64,
    prog: Vec<Vec<String>>,
}

impl Computer {
    fn new(program: &str, a: i64) -> Self {
        let prog = program
            .lines()
            .filter(|l| !l.trim().is_empty())
            .map(|line| line.replace(',', "").split_whitespace().map(|s| s.to_string()).collect())
            .collect();
        Self { a, b: 0, pc: 0, prog }
    }

    fn step(&mut self) -> bool {
        if self.pc < 0 || self.pc as usize >= self.prog.len() {
            return false;
        }
        let inst = &self.prog[self.pc as usize];
        match inst[0].as_str() {
            "hlf" => {
                if inst[1] == "a" {
                    self.a /= 2;
                } else {
                    self.b /= 2;
                }
                self.pc += 1;
            }
            "tpl" => {
                if inst[1] == "a" {
                    self.a *= 3;
                } else {
                    self.b *= 3;
                }
                self.pc += 1;
            }
            "inc" => {
                if inst[1] == "a" {
                    self.a += 1;
                } else {
                    self.b += 1;
                }
                self.pc += 1;
            }
            "jmp" => {
                let off: i64 = inst[1].parse().unwrap();
                self.pc += off;
            }
            "jie" => {
                let reg = if inst[1] == "a" { self.a } else { self.b };
                let off: i64 = inst[2].parse().unwrap();
                if reg % 2 == 0 {
                    self.pc += off;
                } else {
                    self.pc += 1;
                }
            }
            "jio" => {
                let reg = if inst[1] == "a" { self.a } else { self.b };
                let off: i64 = inst[2].parse().unwrap();
                if reg == 1 {
                    self.pc += off;
                } else {
                    self.pc += 1;
                }
            }
            _ => unreachable!(),
        }
        true
    }

    fn run(&mut self) {
        while self.step() {}
    }
}

pub fn part1(input: &str) -> i64 {
    let mut c = Computer::new(input, 0);
    c.run();
    c.b
}

pub fn part2(input: &str) -> i64 {
    let mut c = Computer::new(input, 1);
    c.run();
    c.b
}

#[cfg(test)]
mod tests {
    use super::*;
    use crate::read_input;

    #[test]
    fn answers() {
        let input = read_input(23).expect("input");
        assert_eq!(184, part1(&input));
        assert_eq!(231, part2(&input));
    }
}

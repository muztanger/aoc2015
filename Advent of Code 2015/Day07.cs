using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Advent_of_Code_2015
{
    [TestClass]
    public class Day07
    {
        abstract class Signal
        {
            public abstract override string ToString();

            public abstract uint Eval();
        }

        class Value : Signal
        {
            public uint value;

            public override uint Eval()
            {
                return value;
            }

            public override string ToString()
            {
                return $"Value({value})";
            }
        }

        class Wire : Signal
        {
            public string identifier;
            public Signal source;

            public override uint Eval()
            {
                uint result = source.Eval();
                if (source.GetType() != typeof(Value))
                {
                    source = new Value() { value = result };
                }
                return result;
            }

            public override string ToString()
            {
                return $"Wire(id={identifier}, source={source})";
            }
        }

        class Gate : Signal
        {
            public Func<Signal, Signal, uint> oper;
            public bool isUnary = false;
            public Signal left = null;
            public Signal right = null;

            public override uint Eval()
            {
                return isProvider() ? oper(left, right) : 0;
            }

            public override string ToString()
            {
                var result = "Gate(";
                if (!isUnary)
                {
                    result += $"left={left}, ";
                }
                result += $"right={right}, oper={oper.Method})";
                return result;
            }

            bool isProvider()
            {
                return (left != null || isUnary) && right != null;
            }
        }

        uint And(Signal left, Signal right)
        {
            return left.Eval() & right.Eval();
        }

        uint Lshift(Signal left, Signal right)
        {
            return left.Eval() << (int) right.Eval();
        }

        uint Rshift(Signal left, Signal right)
        {
            return left.Eval() >> (int)right.Eval();
        }

        uint Or(Signal left, Signal right)
        {
            return left.Eval() | right.Eval();
        }

        uint Not(Signal left, Signal right)
        {
            return ~right.Eval() & 0xFFFFu;
        }

        Dictionary<string, Func<Signal, Signal, uint>> mFunctions;
        public Day07()
        {
            mFunctions = new Dictionary<string, Func<Signal, Signal, uint>>()
            {
                {"AND", And},
                {"LSHIFT", Lshift },
                {"RSHIFT", Rshift },
                {"OR",  Or},
                {"NOT", Not }
            };
        }

        string example = @"123 -> x
456 -> y
x AND y -> d
x OR y -> e
x LSHIFT 2 -> f
y RSHIFT 2 -> g
NOT x -> h
NOT y -> i";

        [TestMethod]
        public void Part1Examples()
        {
            using (StringReader reader = new StringReader(example))
            {
                Dictionary<string, Wire> wires = parseInstructions(reader);
                var wireNames = wires.Keys.ToArray<string>();
                Array.Sort(wireNames);

                foreach (var wire in wireNames)
                {
                    Console.WriteLine($"{wire} -> {wires[wire].Eval()}");
                }
            }
        }

        private Dictionary<string, Wire> parseInstructions(StringReader reader)
        {
            var wires = new Dictionary<string, Wire>();

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                //Console.WriteLine(line);
                string[] parts = line.Split();
                if (parts.Length == 3)  // Assignment
                {
                    // 456 -> y
                    Wire rightSide;
                    if (wires.ContainsKey(parts[2]))
                    {
                        rightSide = wires[parts[2]];
                    }
                    else
                    {
                        rightSide = new Wire() { identifier = parts[2] };
                        wires[parts[2]] = rightSide;
                    }

                    Signal leftSide;
                    if (int.TryParse(parts[0], out int value))
                    {
                        leftSide = new Value() { value = (uint)value };
                    }
                    else
                    {
                        if (wires.ContainsKey(parts[0]))
                        {
                            leftSide = wires[parts[0]];
                        }
                        else
                        {
                            leftSide = new Wire { identifier = parts[0] };
                            wires[parts[0]] = (Wire)leftSide;
                        }
                    }

                    rightSide.source = leftSide;
                }
                else if (parts.Length == 5)  // Grid
                {
                    // x AND y -> d
                    // 0     2    4
                    string wireId = parts[4];
                    Wire result;
                    if (wires.ContainsKey(wireId))
                    {
                        result = wires[wireId];
                    }
                    else
                    {
                        result = new Wire() { identifier = wireId };
                        wires[wireId] = result;
                    }

                    Signal leftSide;
                    string leftStr = parts[0];
                    if (int.TryParse(leftStr, out int leftValue))
                    {
                        leftSide = new Value() { value = (uint)leftValue };
                    }
                    else
                    {
                        if (wires.ContainsKey(leftStr))
                        {
                            leftSide = wires[leftStr];
                        }
                        else
                        {
                            leftSide = new Wire { identifier = leftStr };
                            wires[leftStr] = (Wire)leftSide;
                        }
                    }

                    //operator
                    var oper = mFunctions[parts[1]];

                    Signal rightSide;
                    string rightStr = parts[2];
                    if (int.TryParse(rightStr, out int rightValue))
                    {
                        rightSide = new Value() { value = (uint)rightValue };
                    }
                    else
                    {
                        if (wires.ContainsKey(rightStr))
                        {
                            rightSide = wires[rightStr];
                        }
                        else
                        {
                            rightSide = new Wire { identifier = rightStr };
                            wires[rightStr] = (Wire)rightSide;
                        }
                    }

                    result.source = new Gate() { isUnary = false, left = leftSide, right = rightSide, oper = oper };
                }
                else if (parts.Length == 4)  // Grid
                {
                    // NOT x -> h
                    //     1    3

                    string wireId = parts[3];
                    Wire result;
                    if (wires.ContainsKey(wireId))
                    {
                        result = wires[wireId];
                    }
                    else
                    {
                        result = new Wire() { identifier = wireId };
                        wires[wireId] = result;
                    }

                    //operator
                    var oper = mFunctions[parts[0]];

                    Signal rightSide;
                    string rightStr = parts[1];
                    if (int.TryParse(rightStr, out int rightValue))
                    {
                        rightSide = new Value() { value = (uint)rightValue };
                    }
                    else
                    {
                        if (wires.ContainsKey(rightStr))
                        {
                            rightSide = wires[rightStr];
                        }
                        else
                        {
                            rightSide = new Wire { identifier = rightStr };
                            wires[rightStr] = (Wire)rightSide;
                        }
                    }

                    result.source = new Gate() { isUnary = true, left = null, right = rightSide, oper = oper };

                }
                else
                {
                    throw new NotImplementedException();
                }
            }

            return wires;
        }

        [TestMethod]
        public void Part1()
        {
            using (StringReader reader = new StringReader(inputString))
            {

                Dictionary<string, Wire> wires = parseInstructions(reader);
                Console.WriteLine($"a -> {wires["a"].Eval()}");
                
            }
  
            //Console.WriteLine(sum);
        }

       
        [TestMethod]
        public void Part2()
        {
            using (StringReader reader = new StringReader(inputStringPart2))
            {
                Dictionary<string, Wire> wires = parseInstructions(reader);
                Console.WriteLine($"a -> {wires["a"].Eval()}");

            }

            //Console.WriteLine(sum);
        }

        private static readonly string inputString = InputLoader.ReadAllText("day07.txt");
        static string inputStringPart2 = @"af AND ah -> ai
NOT lk -> ll
hz RSHIFT 1 -> is
NOT go -> gp
du OR dt -> dv
x RSHIFT 5 -> aa
at OR az -> ba
eo LSHIFT 15 -> es
ci OR ct -> cu
b RSHIFT 5 -> f
fm OR fn -> fo
NOT ag -> ah
v OR w -> x
g AND i -> j
an LSHIFT 15 -> ar
1 AND cx -> cy
jq AND jw -> jy
iu RSHIFT 5 -> ix
gl AND gm -> go
NOT bw -> bx
jp RSHIFT 3 -> jr
hg AND hh -> hj
bv AND bx -> by
er OR es -> et
kl OR kr -> ks
et RSHIFT 1 -> fm
e AND f -> h
u LSHIFT 1 -> ao
he RSHIFT 1 -> hx
eg AND ei -> ej
bo AND bu -> bw
dz OR ef -> eg
dy RSHIFT 3 -> ea
gl OR gm -> gn
da LSHIFT 1 -> du
au OR av -> aw
gj OR gu -> gv
eu OR fa -> fb
lg OR lm -> ln
e OR f -> g
NOT dm -> dn
NOT l -> m
aq OR ar -> as
gj RSHIFT 5 -> gm
hm AND ho -> hp
ge LSHIFT 15 -> gi
jp RSHIFT 1 -> ki
hg OR hh -> hi
lc LSHIFT 1 -> lw
km OR kn -> ko
eq LSHIFT 1 -> fk
1 AND am -> an
gj RSHIFT 1 -> hc
aj AND al -> am
gj AND gu -> gw
ko AND kq -> kr
ha OR gz -> hb
bn OR by -> bz
iv OR jb -> jc
NOT ac -> ad
bo OR bu -> bv
d AND j -> l
bk LSHIFT 1 -> ce
de OR dk -> dl
dd RSHIFT 1 -> dw
hz AND ik -> im
NOT jd -> je
fo RSHIFT 2 -> fp
hb LSHIFT 1 -> hv
lf RSHIFT 2 -> lg
gj RSHIFT 3 -> gl
ki OR kj -> kk
NOT ak -> al
ld OR le -> lf
ci RSHIFT 3 -> ck
1 AND cc -> cd
NOT kx -> ky
fp OR fv -> fw
ev AND ew -> ey
dt LSHIFT 15 -> dx
NOT ax -> ay
bp AND bq -> bs
NOT ii -> ij
ci AND ct -> cv
iq OR ip -> ir
x RSHIFT 2 -> y
fq OR fr -> fs
bn RSHIFT 5 -> bq
0 -> c
956 -> b
d OR j -> k
z OR aa -> ab
gf OR ge -> gg
df OR dg -> dh
NOT hj -> hk
NOT di -> dj
fj LSHIFT 15 -> fn
lf RSHIFT 1 -> ly
b AND n -> p
jq OR jw -> jx
gn AND gp -> gq
x RSHIFT 1 -> aq
ex AND ez -> fa
NOT fc -> fd
bj OR bi -> bk
as RSHIFT 5 -> av
hu LSHIFT 15 -> hy
NOT gs -> gt
fs AND fu -> fv
dh AND dj -> dk
bz AND cb -> cc
dy RSHIFT 1 -> er
hc OR hd -> he
fo OR fz -> ga
t OR s -> u
b RSHIFT 2 -> d
NOT jy -> jz
hz RSHIFT 2 -> ia
kk AND kv -> kx
ga AND gc -> gd
fl LSHIFT 1 -> gf
bn AND by -> ca
NOT hr -> hs
NOT bs -> bt
lf RSHIFT 3 -> lh
au AND av -> ax
1 AND gd -> ge
jr OR js -> jt
fw AND fy -> fz
NOT iz -> ja
c LSHIFT 1 -> t
dy RSHIFT 5 -> eb
bp OR bq -> br
NOT h -> i
1 AND ds -> dt
ab AND ad -> ae
ap LSHIFT 1 -> bj
br AND bt -> bu
NOT ca -> cb
NOT el -> em
s LSHIFT 15 -> w
gk OR gq -> gr
ff AND fh -> fi
kf LSHIFT 15 -> kj
fp AND fv -> fx
lh OR li -> lj
bn RSHIFT 3 -> bp
jp OR ka -> kb
lw OR lv -> lx
iy AND ja -> jb
dy OR ej -> ek
1 AND bh -> bi
NOT kt -> ku
ao OR an -> ap
ia AND ig -> ii
NOT ey -> ez
bn RSHIFT 1 -> cg
fk OR fj -> fl
ce OR cd -> cf
eu AND fa -> fc
kg OR kf -> kh
jr AND js -> ju
iu RSHIFT 3 -> iw
df AND dg -> di
dl AND dn -> do
la LSHIFT 15 -> le
fo RSHIFT 1 -> gh
NOT gw -> gx
NOT gb -> gc
ir LSHIFT 1 -> jl
x AND ai -> ak
he RSHIFT 5 -> hh
1 AND lu -> lv
NOT ft -> fu
gh OR gi -> gj
lf RSHIFT 5 -> li
x RSHIFT 3 -> z
b RSHIFT 3 -> e
he RSHIFT 2 -> hf
NOT fx -> fy
jt AND jv -> jw
hx OR hy -> hz
jp AND ka -> kc
fb AND fd -> fe
hz OR ik -> il
ci RSHIFT 1 -> db
fo AND fz -> gb
fq AND fr -> ft
gj RSHIFT 2 -> gk
cg OR ch -> ci
cd LSHIFT 15 -> ch
jm LSHIFT 1 -> kg
ih AND ij -> ik
fo RSHIFT 3 -> fq
fo RSHIFT 5 -> fr
1 AND fi -> fj
1 AND kz -> la
iu AND jf -> jh
cq AND cs -> ct
dv LSHIFT 1 -> ep
hf OR hl -> hm
km AND kn -> kp
de AND dk -> dm
dd RSHIFT 5 -> dg
NOT lo -> lp
NOT ju -> jv
NOT fg -> fh
cm AND co -> cp
ea AND eb -> ed
dd RSHIFT 3 -> df
gr AND gt -> gu
ep OR eo -> eq
cj AND cp -> cr
lf OR lq -> lr
gg LSHIFT 1 -> ha
et RSHIFT 2 -> eu
NOT jh -> ji
ek AND em -> en
jk LSHIFT 15 -> jo
ia OR ig -> ih
gv AND gx -> gy
et AND fe -> fg
lh AND li -> lk
1 AND io -> ip
kb AND kd -> ke
kk RSHIFT 5 -> kn
id AND if -> ig
NOT ls -> lt
dw OR dx -> dy
dd AND do -> dq
lf AND lq -> ls
NOT kc -> kd
dy AND ej -> el
1 AND ke -> kf
et OR fe -> ff
hz RSHIFT 5 -> ic
dd OR do -> dp
cj OR cp -> cq
NOT dq -> dr
kk RSHIFT 1 -> ld
jg AND ji -> jj
he OR hp -> hq
hi AND hk -> hl
dp AND dr -> ds
dz AND ef -> eh
hz RSHIFT 3 -> ib
db OR dc -> dd
hw LSHIFT 1 -> iq
he AND hp -> hr
NOT cr -> cs
lg AND lm -> lo
hv OR hu -> hw
il AND in -> io
NOT eh -> ei
gz LSHIFT 15 -> hd
gk AND gq -> gs
1 AND en -> eo
NOT kp -> kq
et RSHIFT 5 -> ew
lj AND ll -> lm
he RSHIFT 3 -> hg
et RSHIFT 3 -> ev
as AND bd -> bf
cu AND cw -> cx
jx AND jz -> ka
b OR n -> o
be AND bg -> bh
1 AND ht -> hu
1 AND gy -> gz
NOT hn -> ho
ck OR cl -> cm
ec AND ee -> ef
lv LSHIFT 15 -> lz
ks AND ku -> kv
NOT ie -> if
hf AND hl -> hn
1 AND r -> s
ib AND ic -> ie
hq AND hs -> ht
y AND ae -> ag
NOT ed -> ee
bi LSHIFT 15 -> bm
dy RSHIFT 2 -> dz
ci RSHIFT 2 -> cj
NOT bf -> bg
NOT im -> in
ev OR ew -> ex
ib OR ic -> id
bn RSHIFT 2 -> bo
dd RSHIFT 2 -> de
bl OR bm -> bn
as RSHIFT 1 -> bl
ea OR eb -> ec
ln AND lp -> lq
kk RSHIFT 3 -> km
is OR it -> iu
iu RSHIFT 2 -> iv
as OR bd -> be
ip LSHIFT 15 -> it
iw OR ix -> iy
kk RSHIFT 2 -> kl
NOT bb -> bc
ci RSHIFT 5 -> cl
ly OR lz -> ma
z AND aa -> ac
iu RSHIFT 1 -> jn
cy LSHIFT 15 -> dc
cf LSHIFT 1 -> cz
as RSHIFT 3 -> au
cz OR cy -> da
kw AND ky -> kz
lx -> a
iw AND ix -> iz
lr AND lt -> lu
jp RSHIFT 5 -> js
aw AND ay -> az
jc AND je -> jf
lb OR la -> lc
NOT cn -> co
kh LSHIFT 1 -> lb
1 AND jj -> jk
y OR ae -> af
ck AND cl -> cn
kk OR kv -> kw
NOT cv -> cw
kl AND kr -> kt
iu OR jf -> jg
at AND az -> bb
jp RSHIFT 2 -> jq
iv AND jb -> jd
jn OR jo -> jp
x OR ai -> aj
ba AND bc -> bd
jl OR jk -> jm
b RSHIFT 1 -> v
o AND q -> r
NOT p -> q
k AND m -> n
as RSHIFT 2 -> at
";

    }
}

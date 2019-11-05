﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Advent_of_Code_2015
{
    [TestClass]
    public class Day5
    {

        private HashSet<char> mVowels = new HashSet<char>("aeiou".ToCharArray());
        private HashSet<string> mTabu = new HashSet<string>() { "ab", "cd", "pq", "xy" };

        [TestMethod]
        public void Part1Examples()
        {
            Assert.AreEqual(true, isNice("ugknbfddgicrmopn"));
            Assert.AreEqual(true, isNice("aaa"));
            Assert.AreEqual(false, isNice("jchzalrnumimnmhp"));
            Assert.AreEqual(false, isNice("haegwjzuvuyypxyu"));
            Assert.AreEqual(false, isNice("dvszwmarrgswjxmb"));
        }

        private bool isNice(string v)
        {
            //It contains at least three vowels(aeiou only), like aei, xazegov, or aeiouaeiouaeiou.
            //It contains at least one letter that appears twice in a row, like xx, abcdde(dd), or aabbccdd(aa, bb, cc, or dd).
            //It does not contain the strings ab, cd, pq, or xy, even if they are part of one of the other requirements.

            char last = '\b';
            bool twice = false;
            int vowels = 0;
            foreach (char c in v)
            {
                if (c == last) twice = true;
                if (mVowels.Contains(c)) vowels++;
                if (last == 'a' && c == 'b') return false;
                if (last == 'c' && c == 'd') return false;
                if (last == 'p' && c == 'q') return false;
                if (last == 'x' && c == 'y') return false;
                last = c;
            }

            return twice && vowels >= 3;
        }

        [TestMethod]
        public void Part1()
        {
            int sum = 0;
            using (StringReader reader = new StringReader(inputString))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (isNice(line)) sum++;
                }
            }
            Console.WriteLine(sum);
        }



        [TestMethod]
        public void Part2Examples()
        {
            Assert.IsTrue(isNice2("qjhvhtzxzqqjkmpb"));
            Assert.IsTrue(isNice2("xxyxx"));
            Assert.IsFalse(isNice2("uurcxstgmygtbstg"));
            Assert.IsFalse(isNice2("ieodomkazucvgmuy"));
        }

        [TestMethod]
        public void Part2()
        {
            int sum = 0;
            using (StringReader reader = new StringReader(inputString))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (isNice2(line)) sum++;
                }
            }
            Console.WriteLine(sum);
        }

        private bool isNice2(string v)
        {
            // Now, a nice string is one with all of the following properties:
            // It contains a pair of any two letters that appears at least twice in the string without overlapping, like xyxy (xy) or aabcdefgaa (aa), but not like aaa (aa, but it overlaps).
            // It contains at least one letter which repeats with exactly one letter between them, like xyx, abcdefeghi (efe), or even aaa.
            var pairs = new Dictionary<string, List<int>>();
            var stack = new Stack<char>();
            bool isRepeated = false;
            int i = 0;
            foreach (char c in v)
            {
                switch (stack.Count)
                {
                    case 0:
                        stack.Push(c);
                        break;
                    case 1:
                        { 
                            var l1 = stack.Peek();
                            var key = string.Concat<char>(new char[] { l1, c });
                            if (!pairs.ContainsKey(key))
                            {
                                pairs[key] = new List<int>();
                            }
                            pairs[key].Add(i);
                            stack.Push(c);
                        }
                        break;
                    default:
                        {
                            var l1 = stack.Pop();
                            var key = string.Concat<char>(new char[] { l1, c });
                            if (!pairs.ContainsKey(key))
                            {
                                pairs[key] = new List<int>();
                            }
                            pairs[key].Add(i);
                            if (c == stack.Peek()) isRepeated = true;
                            stack.Push(l1);
                            stack.Push(c);
                        }
                        break;
                }
                i++;
            }
            if (!isRepeated) return false;
            foreach (var p in pairs)
            {
                if (p.Value.Count >= 2)
                {
                    if (p.Value.Max() - p.Value.Min() >= 2) return true;
                }
            }


            return false;
        }

        static string inputString = @"zgsnvdmlfuplrubt
vlhagaovgqjmgvwq
ffumlmqwfcsyqpss
zztdcqzqddaazdjp
eavfzjajkjesnlsb
urrvucyrzzzooxhx
xdwduffwgcptfwad
orbryxwrmvkrsxsr
jzfeybjlgqikjcow
mayoqiswqqryvqdi
iiyrkoujhgpgkcvx
egcgupjkqwfiwsjl
zbgtglaqqolttgng
eytquncjituzzhsx
dtfkgggvqadhqbwb
zettygjpcoedwyio
rwgwbwzebsnjmtln
esbplxhvzzgawctn
vnvshqgmbotvoine
wflxwmvbhflkqxvo
twdjikcgtpvlctte
minfkyocskvgubvm
sfxhhdhaopajbzof
sofkjdtalvhgwpql
uqfpeauqzumccnrc
tdflsbtiiepijanf
dhfespzrhecigzqb
xobfthcuuzhvhzpn
olgjglxaotocvrhw
jhkzpfcskutwlwge
zurkakkkpchzxjhq
hekxiofhalvmmkdl
azvxuwwfmjdpjskj
arsvmfznblsqngvb
ldhkzhejofreaucc
adrphwlkehqkrdmo
wmveqrezfkaivvaw
iyphmphgntinfezg
blomkvgslfnvspem
cgpaqjvzhbumckwo
ydhqjcuotkeyurpx
sbtzboxypnmdaefr
vxrkhvglynljgqrg
ttgrkjjrxnxherxd
hinyfrjdiwytetkw
sufltffwqbugmozk
tohmqlzxxqzinwxr
jbqkhxfokaljgrlg
fvjeprbxyjemyvuq
gmlondgqmlselwah
ubpwixgxdloqnvjp
lxjfhihcsajxtomj
qouairhvrgpjorgh
nloszcwcxgullvxb
myhsndsttanohnjn
zjvivcgtjwenyilz
qaqlyoyouotsmamm
tadsdceadifqthag
mafgrbmdhpnlbnks
aohjxahenxaermrq
ovvqestjhbuhrwlr
lnakerdnvequfnqb
agwpwsgjrtcjjikz
lhlysrshsmzryzes
xopwzoaqtlukwwdu
xsmfrfteyddrqufn
ohnxbykuvvlbbxpf
bbdlivmchvzfuhoc
vtacidimfcfyobhf
tinyzzddgcnmiabd
tcjzxftqcqrivqhn
vgnduqyfpokbmzim
revkvaxnsxospyow
ydpgwxxoxlywxcgi
wzuxupbzlpzmikel
nscghlafavnsycjh
xorwbquzmgmcapon
asmtiycegeobfxrn
eqjzvgkxgtlyuxok
mmjrskloposgjoqu
gceqosugbkvytfto
khivvoxkvhrgwzjl
qtmejuxbafroifjt
ttmukbmpoagthtfl
bxqkvuzdbehtduwv
gvblrpzjylanoggj
cltewhyjxdbmbtqj
fbkgedqvomdipklj
uxvuplhenqawfcjt
fkdjmayiawdkycva
gnloqfgbnibzyidh
kyzorvtopjiyyyqg
drckpekhpgrioblt
tvhrkmbnpmkkrtki
khaldwntissbijiz
aoojqakosnaxosom
xfptccznbgnpfyqw
moqdwobwhjxhtrow
chfwivedutskovri
gprkyalfnpljcrmi
pwyshpwjndasykst
xuejivogihttzimd
bugepxgpgahtsttl
zufmkmuujavcskpq
urybkdyvsrosrfro
isjxqmlxwtqmulbg
pxctldxgqjqhulgz
hclsekryiwhqqhir
hbuihpalwuidjpcq
ejyqcxmfczqfhbxa
xljdvbucuxnnaysv
irqceqtqwemostbb
anfziqtpqzqdttnz
cgfklbljeneeqfub
zudyqkuqqtdcpmuo
iuvhylvznmhbkbgg
mpgppmgfdzihulnd
argwmgcvqqkxkrdi
pdhrfvdldkfihlou
cbvqnjrvrsnqzfob
lkvovtsqanohzcmm
vxoxjdyoylqcnyzt
kurdpaqiaagiwjle
gwklwnazaxfkuekn
rbaamufphjsjhbdl
tzbrvaqvizhsisbd
pbcqlbfjvlideiub
hiwoetbfywaeddtx
fjirczxtuupfywyf
omeoegeyyospreem
ozbbpupqpsskvrjh
pzvcxkvjdiyeyhxa
odclumkenabcsfzr
npdyqezqdjqaszvm
yodkwzmrhtexfrqa
rjcmmggjtactfrxz
mioxfingsfoimual
aqskaxjjborspfaa
wientdsttkevjtkf
tdaswkzckmxnfnct
voucjhzvkkhuwoqk
boaaruhalgaamqmh
iufzxutxymorltvb
pfbyvbayvnrpijpo
obztirulgyfthgcg
ntrenvhwxypgtjwy
ephlkipjfnjfjrns
pkjhurzbmobhszpx
gqbnjvienzqfbzvj
wjelolsrbginwnno
votanpqpccxqricj
bxyuyiglnmbtvehi
qyophcjfknbcbjrb
anoqkkbcdropskhj
tcnyqaczcfffkrtl
rsvqimuqbuddozrf
meppxdrenexxksdt
tyfhfiynzwadcord
wayrnykevdmywycf
mhowloqnppswyzbu
tserychksuwrgkxz
xycjvvsuaxsbrqal
fkrdsgaoqdcqwlpn
vrabcmlhuktigecp
xgxtdsvpaymzhurx
ciabcqymnchhsxkc
eqxadalcxzocsgtr
tsligrgsjtrnzrex
qeqgmwipbspkbbfq
vzkzsjujltnqwliw
ldrohvodgbxokjxz
jkoricsxhipcibrq
qzquxawqmupeujrr
mizpuwqyzkdbahvk
suupfxbtoojqvdca
ywfmuogvicpywpwm
uevmznxmsxozhobl
vjbyhsemwfwdxfxk
iyouatgejvecmtin
tcchwpuouypllcxe
lgnacnphdiobdsef
uoxjfzmdrmpojgbf
lqbxsxbqqhpjhfxj
knpwpcnnimyjlsyz
fezotpoicsrshfnh
dkiwkgpmhudghyhk
yzptxekgldksridv
pckmzqzyiyzdbcts
oqshafncvftvwvsi
yynihvdywxupqmbt
iwmbeunfiuhjaaic
pkpkrqjvgocvaxjs
ieqspassuvquvlyz
xshhahjaxjoqsjtl
fxrrnaxlqezdcdvd
pksrohfwlaqzpkdd
ravytrdnbxvnnoyy
atkwaifeobgztbgo
inkcabgfdobyeeom
ywpfwectajohqizp
amcgorhxjcybbisv
mbbwmnznhafsofvr
wofcubucymnhuhrv
mrsamnwvftzqcgta
tlfyqoxmsiyzyvgv
ydceguvgotylwtea
btyvcjqhsygunvle
usquiquspcdppqeq
kifnymikhhehgote
ybvkayvtdpgxfpyn
oulxagvbavzmewnx
tvvpekhnbhjskzpj
azzxtstaevxurboa
nfmwtfgrggmqyhdf
ynyzypdmysfwyxgr
iaobtgubrcyqrgmk
uyxcauvpyzabbzgv
fbasfnwiguasoedc
mgmjoalkbvtljilq
szgkxiqkufdvtksb
xgfzborpavdmhiuj
hmuiwnsonvfgcrva
zolcffdtobfntifb
mvzgcsortkugvqjr
pbbpgraaldqvzwhs
zvsxegchksgnhpuv
kdpdboaxsuxfswhx
jdfggigejfupabth
tpeddioybqemyvqz
mxsntwuesonybjby
tzltdsiojfvocige
ubtdrneozoejiqrv
fusyucnhncoxqzql
nlifgomoftdvkpby
pyikzbxoapffbqjw
hzballplvzcsgjug
ymjyigsfehmdsvgz
vpqgyxknniunksko
ffkmaqsjxgzclsnq
jcuxthbedplxhslk
ymlevgofmharicfs
nyhbejkndhqcoisy
rjntxasfjhnlizgm
oqlnuxtzhyiwzeto
tntthdowhewszitu
rmxyoceuwhsvfcua
qpgsjzwenzbxyfgw
sumguxpdkocyagpu
ymfrbxwrawejkduu
hetgrtmojolbmsuf
qzqizpiyfasgttex
qnmoemcpuckzsshx
ddyqiihagcmnxccu
oirwxyfxxyktgheo
phpaoozbdogbushy
uctjdavsimsrnvjn
aurbbphvjtzipnuh
hpbtrubopljmltep
pyyvkthqfsxqhrxg
jdxaiqzkepxbfejk
ukgnwbnysrzvqzlw
lfkatkvcssnlpthd
ucsyecgshklhqmsc
rwdcbdchuahkvmga
rxkgqakawgpwokum
hbuyxeylddfgorgu
tbllspqozaqzglkz
rqfwizjlbwngdvvi
xuxduyzscovachew
kouiuxckkvmetvdy
ycyejrpwxyrweppd
trctlytzwiisjamx
vtvpjceydunjdbez
gmtlejdsrbfofgqy
jgfbgtkzavcjlffj
tyudxlpgraxzchdk
gyecxacqitgozzgd
rxaocylfabmmjcvt
tornfzkzhjyofzqa
kocjcrqcsvagmfqv
zfrswnskuupivzxb
cunkuvhbepztpdug
pmpfnmklqhcmrtmf
tfebzovjwxzumxap
xpsxgaswavnzkzye
lmwijdothmxclqbr
upqxhmctbltxkarl
axspehytmyicthmq
xdwrhwtuooikehbk
tpggalqsytvmwerj
jodysbwnymloeqjf
rxbazvwuvudqlydn
ibizqysweiezhlqa
uexgmotsqjfauhzp
ldymyvumyhyamopg
vbxvlvthgzgnkxnf
pyvbrwlnatxigbrp
azxynqididtrwokb
lwafybyhpfvoawto
ogqoivurfcgspytw
cinrzzradwymqcgu
sgruxdvrewgpmypu
snfnsbywuczrshtd
xfzbyqtyxuxdutpw
fmpvjwbulmncykbo
ljnwoslktrrnffwo
ceaouqquvvienszn
yjomrunrxjyljyge
xpmjsapbnsdnbkdi
uetoytptktkmewre
eixsvzegkadkfbua
afaefrwhcosurprw
bwzmmvkuaxiymzwc
gejyqhhzqgsrybni
gjriqsfrhyguoiiw
gtfyomppzsruhuac
ogemfvmsdqqkfymr
jgzbipsygirsnydh
zghvlhpjnvqmocgr
ngvssuwrbtoxtrka
ietahyupkbuisekn
gqxqwjizescbufvl
eiprekzrygkncxzl
igxfnxtwpyaamkxf
soqjdkxcupevbren
fspypobyzdwstxak
qstcgawvqwtyyidf
gsccjacboqvezxvd
bfsblokjvrqzphmc
srezeptvjmncqkec
opmopgyabjjjoygt
msvbufqexfrtecbf
uiaqweyjiulplelu
pbkwhjsibtwjvswi
xwwzstmozqarurrq
nytptwddwivtbgyq
ejxvsufbzwhzpabr
jouozvzuwlfqzdgh
gfgugjihbklbenrk
lwmnnhiuxqsfvthv
bzvwbknfmaeahzhi
cgyqswikclozyvnu
udmkpvrljsjiagzi
zzuhqokgmisguyna
ekwcdnjzuctsdoua
eueqkdrnzqcaecyd
lnibwxmokbxhlris
fdrbftgjljpzwhea
iabvuhhjsxmqfwld
qgogzkynrgejakta
mfcqftytemgnpupp
klvhlhuqhosvjuqk
gdokmxcgoqvzvaup
juududyojcazzgvr
fyszciheodgmnotg
yfpngnofceqfvtfs
cahndkfehjumwavc
dxsvscqukljxcqyi
cqukcjtucxwrusji
vevmmqlehvgebmid
ahswsogfrumzdofy
ftasbklvdquaxhxb
tsdeumygukferuif
ybfgbwxaaitpwryg
djyaoycbymezglio
trzrgxdjqnmlnzpn
rumwchfihhihpqui
ffrvnsgrnzemksif
oizlksxineqknwzd
cirqcprftpjzrxhk
zrhemeqegmzrpufd
kqgatudhxgzlgkey
syjugymeajlzffhq
nlildhmgnwlopohp
flcszztfbesqhnyz
ohzicmqsajyqptrw
ebyszucgozsjbelq
enxbgvvcuqeloxud
ubwnvecbsmhkxwuk
noifliyxvlkqphbo
hazlqpetgugxxsiz
ihdzoerqwqhgajzb
ivrdwdquxzhdrzar
synwycdvrupablib
mqkdjkntblnmtvxj
qmmvoylxymyovrnq
pjtuxskkowutltlq
gchrqtloggkrjciz
namzqovvsdipazae
yfokqhkmakyjzmys
iapxlbuoiwqfnozm
fbcmlcekgfdurqxe
ednzgtczbplwxjlq
gdvsltzpywffelsp
oaitrrmpqdvduqej
gseupzwowmuuibjo
dfzsffsqpaqoixhh
tclhzqpcvbshxmgx
cfqkptjrulxiabgo
iraiysmwcpmtklhf
znwjlzodhktjqwlm
lcietjndlbgxzjht
gdkcluwjhtaaprfo
vbksxrfznjzwvmmt
vpfftxjfkeltcojl
thrmzmeplpdespnh
yafopikiqswafsit
xxbqgeblfruklnhs
qiufjijzbcpfdgig
ikksmllfyvhyydmi
sknufchjdvccccta
wpdcrramajdoisxr
grnqkjfxofpwjmji
lkffhxonjskyccoh
npnzshnoaqayhpmb
fqpvaamqbrnatjia
oljkoldhfggkfnfc
ihpralzpqfrijynm
gvaxadkuyzgbjpod
onchdguuhrhhspen
uefjmufwlioenaus
thifdypigyihgnzo
ugqblsonqaxycvkg
yevmbiyrqdqrmlbw
bvpvwrhoyneorcmm
gbyjqzcsheaxnyib
knhsmdjssycvuoqf
nizjxiwdakpfttyh
nwrkbhorhfqqoliz
ynsqwvwuwzqpzzwp
yitscrgexjfclwwh
dhajwxqdbtrfltzz
bmrfylxhthiaozpv
frvatcvgknjhcndw
xlvtdmpvkpcnmhya
pxpemuzuqzjlmtoc
dijdacfteteypkoq
knrcdkrvywagglnf
fviuajtspnvnptia
xvlqzukmwbcjgwho
bazlsjdsjoeuvgoz
nslzmlhosrjarndj
menvuwiuymknunwm
uavfnvyrjeiwqmuu
yrfowuvasupngckz
taevqhlrcohlnwye
skcudnogbncusorn
omtnmkqnqedsajfv
yqmgsqdgsuysqcts
odsnbtyimikkbmdd
vuryaohxdvjllieb
dhaxldeywwsfamlo
opobvtchezqnxpak
pzfnegouvsrfgvro
rzkcgpxdslzrdktu
ksztdtqzxvhuryam
ctnqnhkcooqipgkh
pyqbbvrzdittqbgm
koennvmolejeftij
rvzlreqikqlgyczj
xrnujfoyhonzkdgd
mmsmhkxaiqupfjil
ypjwoemqizddvyfd
qgugcxnbhvgahykj
cviodlsrtimbkgmy
xbfbbechhmrjxhnw
psuipaoucfczfxkp
hdhwcpeuptgqqvim
gsxlruhjeaareilr
vgyqonnljuznyrhk
eewezahlumervpyu
iiolebrxfadtnigy
tdadlrodykrdfscn
ocvdtzjxrhtjurpo
gidljbuvuovkhhrf
qwfcpilbjwzboohd
xzohxonlezuiupbg
vslpbkkqgvgbcbix
pivzqrzfxosbstzn
fyqcfboevcqmbhhs
yqsrneacnlxswojx
heicqpxxyrwcbsjz
yzynmnnoumkmlbeh
bncadbjdvvmczylw
hlnjskgfzbgmigfn
fphpszymugpcykka
zbifcktanxpmufvy
saklpkhoyfeqbguy
nqtqfcfxmpivnjyo
locygrwerxlsvzqm
qqflecydqvlogjme
njklmixvgkzpgppf
ugzkpjwjflaswyma
lriousvkbeftslcy
nsvsauxzfbbotgmh
tblcpuhjyybrlica
hqwshxcilwtmxrsf
xojwroydfeoqupup
tikuzsrogpnohpib
layenyqgxdfggloc
nqsvjvbrpuxkqvmq
ivchgxkdlfjdzxmk
uoghiuosiiwiwdws
twsgsfzyszsfinlc
waixcmadmhtqvcmd
zkgitozgrqehtjkw
xbkmyxkzqyktmpfi
qlyapfmlybmatwxn
ntawlvcpuaebuypf
clhebxqdkcyndyof
nrcxuceywiklpemc
lmurgiminxpapzmq
obalwqlkykzflxou
huvcudpiryefbcye
zlxbddpnyuyapach
gqfwzfislmwzyegy
jhynkjtxedmemlob
hmrnvjodnsfiukex
pstmikjykzyavfef
wuwpnscrwzsyalyt
hksvadripgdgwynm
tvpfthzjleqfxwkh
xpmrxxepkrosnrco
qjkqecsnevlhqsly
jjnrfsxzzwkhnwdm
pehmzrzsjngccale
bsnansnfxduritrr
ejzxkefwmzmbxhlb
pceatehnizeujfrs
jtidrtgxopyeslzl
sytaoidnamfwtqcr
iabjnikomkgmyirr
eitavndozoezojsi
wtsbhaftgrbqfsmm
vvusvrivsmhtfild
qifbtzszfyzsjzyx
ifhhjpaqatpbxzau
etjqdimpyjxiuhty
fvllmbdbsjozxrip
tjtgkadqkdtdlkpi
xnydmjleowezrecn
vhcbhxqalroaryfn
scgvfqsangfbhtay
lbufpduxwvdkwhmb
tshipehzspkhmdoi
gtszsebsulyajcfl
dlrzswhxajcivlgg
kgjruggcikrfrkrw
xxupctxtmryersbn
hljjqfjrubzozxts
giaxjhcwazrenjzs
tyffxtpufpxylpye
jfugdxxyfwkzqmgv
kbgufbosjghahacw
xpbhhssgegmthwxb
npefofiharjypyzk
velxsseyxuhrpycy
sglslryxsiwwqzfw
susohnlpelojhklv
lfnpqfvptqhogdmk
vtcrzetlekguqyle
jlyggqdtamcjiuxn
olxxqfgizjmvigvl
cyypypveppxxxfuq
hewmxtlzfqoqznwd
jzgxxybfeqfyzsmp
xzvvndrhuejnzesx
esiripjpvtqqwjkv
xnhrwhjtactofwrd
knuzpuogbzplofqx
tihycsdwqggxntqk
xkfywvvugkdalehs
cztwdivxagtqjjel
dsaslcagopsbfioy
gmowqtkgrlqjimbl
ctcomvdbiatdvbsd
gujyrnpsssxmqjhz
nygeovliqjfauhjf
mmgmcvnuppkbnonz
bhipnkoxhzcotwel
wkwpgedgxvpltqid
mliajvpdocyzcbot
kqjhsipuibyjuref
zqdczykothbgxwsy
koirtljkuqzxioaz
audpjvhmqzvhzqas
cxyhxlhntyidldfx
iasgocejboxjgtkx
abehujmqotwcufxp
fmlrzqmazajxeedl
knswpkekbacuxfby
yvyalnvrxgstqhxm
sjnrljfrfuyqfwuw
ssaqruwarlvxrqzm
iaxbpeqqzlcwfqjz
uwyxshjutkanvvsc
uxwrlwbblcianvnb
nodtifgrxdojhneh
mloxjfusriktxrms
lkfzrwulbctupggc
gcrjljatfhitcgfj
tkdfxeanwskaivqs
ypyjxqtmitwubbgt
ssxbygzbjsltedjj
zdrsnoorwqfalnha
xlgmissaiqmowppd
azhbwhiopwpguiuo
fydlahgxtekbweet
qtaveuqpifprdoiy
kpubqyepxqleucem
wlqrgqmnupwiuory
rwyocktuqkuhdwxz
abzjfsdevoygctqv
zsofhaqqghncmzuw
lqbjwjqxqbfgdckc
bkhyxjkrqbbunido
yepxfjnnhldidsjb
builayfduxbppafc
wedllowzeuswkuez
gverfowxwtnvgrmo
tpxycfumxdqgntwf
lqzokaoglwnfcolw
yqsksyheyspmcdqt
vufvchcjjcltwddl
saeatqmuvnoacddt
dxjngeydvsjbobjs
ucrcxoakevhsgcep
cajgwjsfxkasbayt
hknzmteafsfemwuv
xxwhxwiinchqqudr
usfenmavvuevevgr
kxcobcwhsgyizjok
vhqnydeboeunnvyk
bgxbwbxypnxvaacw
bwjzdypacwgervgk
rrioqjluawwwnjcr
fiaeyggmgijnasot
xizotjsoqmkvhbzm
uzphtrpxwfnaiidz
kihppzgvgyoncptg
hfbkfrxwejdeuwbz
zgqthtuaqyrxicdy
zitqdjnnwhznftze
jnzlplsrwovxlqsn
bmwrobuhwnwivpca
uuwsvcdnoyovxuhn
nmfvoqgoppoyosaj
hxjkcppaisezygpe
icvnysgixapvtoos
vbvzajjgrmjygkhu
jinptbqkyqredaos
dpmknzhkhleawfvz
ouwwkfhcedsgqqxe
owroouiyptrijzgv
bewnckpmnbrmhfyu
evdqxevdacsbfbjb
catppmrovqavxstn
dqsbjibugjkhgazg
mkcldhjochtnvvne
sblkmhtifwtfnmsx
lynnaujghehmpfpt
vrseaozoheawffoq
ytysdzbpbazorqes
sezawbudymfvziff
vrlfhledogbgxbau
bipdlplesdezbldn
ermaenjunjtbekeo
eyaedubkthdecxjq
gbzurepoojlwucuy
rsiaqiiipjlouecx
beqjhvroixhiemtw
buzlowghhqbcbdwv
ldexambveeosaimo
fpyjzachgrhxcvnx
komgvqejojpnykol
fxebehjoxdujwmfu
jnfgvheocgtvmvkx
qmcclxxgnclkuspx
rsbelzrfdblatmzu
vexzwqjqrsenlrhm
tnfbkclwetommqmh
lzoskleonvmprdri
nnahplxqscvtgfwi
ubqdsflhnmiayzrp
xtiyqxhfyqonqzrn
omdtmjeqhmlfojfr
cnimgkdbxkkcnmkb
tapyijgmxzbmqnks
byacsxavjboovukk
awugnhcrygaoppjq
yxcnwrvhojpuxehg
btjdudofhxmgqbao
nzqlfygiysfuilou
nubwfjdxavunrliq
vqxmmhsbmhlewceh
ygavmcybepzfevrp
kgflmrqsvxprkqgq
iaqyqmcaedscmakk
cvbojnbfmrawxzkh
jjjrprbnlijzatuw
lcsudrrfnnggbrmk
qzgxbiavunawfibc
gnnalgfvefdfdwwg
nokmiitzrigxavsc
etzoxwzxqkkhvais
urxxfacgjccieufi
lqrioqhuvgcotuec
dydbaeyoypsbftra
hhrotenctylggzaf
evctqvzjnozpdxzu
tbpvithmorujxlcp
pllbtcbrtkfpvxcw
fzyxdqilyvqreowv
xdleeddxwvqjfmmt
fcldzthqqpbswoin
sgomzrpjfmvgwlzi
axjyskmtdjbxpwoz
hcvaevqxsmabvswh
lfdlsfcwkwicizfk
isjbwpzdognhoxvm
oqnexibqxlyxpluh
zqfbgodsfzwgcwuf
kvmnwruwsjllbldz
kghazimdyiyhmokj
uiktgpsxpoahofxn
zkdwawxargcmidct
ftbixlyiprshrjup
nofhmbxififwroeg
mcdaqrhplffxrcdt
fbjxnwojcvlawmlb
rizoftvwfdhiwyac
eduogrtyhxfwyars
zoikunqxgjwfqqwr
zxwbbpmvctzezaqh
nghujwyeabwdqnop
vcxamijpoyyksogn
jnckdbuteoqlsdae
jurfqqawafmsiqwv
inepmztrzehfafie
tznzkyvzodbrtscf
xewbavjeppflwscl
ucndzsorexjlnplo
jpxbctscngxgusvu
mfmygcllauzuoaok
oibkuxhjmhxhhzby
zjkslwagmeoisunw
avnnxmopdgvmukuu
jmaargejcwboqhkt
yacmpeosarsrfkrv
iqhgupookcaovwgh
ebjkdnxwtikqzufc
imdhbarytcscbsvb
ifyibukeffkbqvcr
aloighmyvwybtxhx
yszqwrutbkiwkxjg
xyholyzlltjhsuhp
gykhmrwucneoxcrf
badkdgqrpjzbabet
sunaucaucykwtkjj
pumqkglgfdhneero
usgtyuestahlydxq
xmfhflphzeudjsjm
knywgmclisgpootg
mtojnyrnvxtweuzb
uuxufbwfegysabww
vobhwwocqttlbsik
yuydfezeqgqxqmnd
wbqgqkwbibiilhzc
sfdmgxsbuzsawush
ilhbxcfgordyxwvp
ahqoavuysblnqaeg
plwgtvpgotskmsey
ewjcmzkcnautrrmp
tyekgzbznlikcyqj
bqzctiuaxpriuiga
bimvbfjkiupyqiys
mpqtbcxfhwymxncw
htemlptvqhharjgb
mqbsmsruwzzxgcxc
zjyedjwhnvteuaid
pzoelkoidwglpttc
efydnsvlfimvwxhx
gfyhgoeiyjcgfyze
deqtomhwopmzvjlt
casafubtkoopuaju
yylsfarntbucfulg
mgjwsormkjsrrxan
lkkenpupgmjpnqqd
tegweszyohsoluot
lihsfdwxmxvwdxna
rrefrjjxerphejwb
guuazonjoebhymtm
ysofqzmfmyneziki
lmjgaliatcpduoal
qzthcpjwtgahbebr
wvakvephyukmpemm
simxacxxzfoaeddw
aetgqmiqzxbvbviz
jxlmhdmqggevrxes
mmuglnjmuddzgaik
svopsqhtrslgycgc
xnvcsiiqrcjkvecn
kkvumxtvashxcops
bduflsdyeectvcgl
vfrxbwmmytjvqnsj
eeqtdneiyiaiofxw
crtbgknfacjtwkfl
uuutuoxdsxolpbhd
lcrztwzreaswovtn
htorkvnvujmjdqzj
wttzuzvrzlyhfzyf
oraewznfwgdsnhuk
rctlkqqvkwbgrcgk
cfehrsrqhzyiwtmz
kbvxwcumjkhvjpui
xxlocexbmniiakfo
gtknkkzvykmlqghl
kcjuxvkuimhwqrtk
vohekwkuyuoacuww
vorctgughscysyfo
zmjevqplngzswxyq
qhswdrhrijnatkyo
joakcwpfggtitizs
juzlwjijcmtswdtq
icbyaqohpkemhkip
rpdxgpzxncedmvzh
rozkmimbqhbhcddv
wkkypomlvyglpfpf
jcaqyaqvsefwtaya
ghvmtecoxlebdwnf
lqrcyiykkkpkxvqt
eqlarfazchmzotev
vqwndafvmpguggef
dbfxzrdkkrusmdke
cmjpjjgndozcmefj
hbrdcwjuyxapyhlo
mmforetykbosdwce
zynfntqwblbnfqik
sodwujfwlasznaiz
yyvrivjiqnxzqkfp
uldbskmmjbqllpnm
fyhhrmrsukeptynl
hpfjekktvdkgdkzl
bozhkoekcxzeorob
uvpptyfrzkvmtoky
hkhfprmjdpjvfkcb
igxzwktwsqhsivqu
qceomwysgkcylipb
cglateoynluyeqgc
xcsdfkpeguxgvpfh
owjhxlcncdgkqyia
rpbmrpcesiakqpna
lueszxiourxsmezb
zelvsowimzkxliwc
vzxbttoobtvdtkca
pfxvzphzwscqkzsi
edsjorainowytbzu
ipsegdaluoiphmnz
mkhueokfpemywvuw
urxdnumhylpafdlc
ggluurzavsxkvwkl
ctclphidqgteakox
tfobosynxsktajuk
jzrmemhxqmzhllif
eemwekimdfvqslsx
yjkwpzrbanoaajgq
rlxghzanuyeimfhx
hozbgdoorhthlqpv
obkbmflhyanxilnx
xojrippyxjmpzmsz
ukykmbfheixuviue
qivlmdexwucqkres
rmyxxipqkarpjmox
fgaftctbvcvnrror
raawxozucfqvasru
dinpjbdfjfizexdh
gybxubwnnbuyvjcr
qrqitdvyoneqyxcg
jqzcfggayzyoqteo
cikqpvxizpdbmppm
stfpldgyhfmucjjv
slzbcuihmimpduri
aufajwfrsorqqsnl
iylmzraibygmgmqj
lcdyfpcqlktudfmu
pmomzzsdpvgkkliw
zpplirgtscfhbrkj
mvhyerxfiljlotjl
ofkvrorwwhusyxjx
xngzmvcgkqfltjpe
yxfxaqipmysahqqq
sdqafdzgfdjuabup
qcqajmerahcdgxfv
xqimrqtupbapawro
qfvkqwidzzrehsbl
himixxvueksiqfdf
vgtfqpuzxxmhrvvd
adiioqeiejguaost
jnzxuycjxvxehbvm
xedbpxdhphamoodk
jsrioscmwlsfuxrg
mtsynnfxunuohbnf
enamqzfzjunnnkpe
uwcvfecunobyhces
ciygixtgbsccpftq
ewjgcronizkcsfjy
wztjkoipxsikoimv
jrgalyvfelwxforw
imylyalawbqwkrwb
yflwqfnuuvgjsgcj
wkysyzusldlojoue
zopllxnidcffcuau
bscgwxuprxaerskj
zvnvprxxjkhnkkpq
nejwxbhjxxdbenid
chryiccsebdbcnkc
guoeefaeafhlgvxh
nzapxrfrrqhsingx
mkzvquzvqvwsejqs
kozmlmbchydtxeeo
keylygnoqhmfzrfp
srwzoxccndoxylxe
uqjzalppoorosxxo
potmkinyuqxsfdfw
qkkwrhpbhypxhiun
wgfvnogarjmdbxyh
gkidtvepcvxopzuf
atwhvmmdvmewhzty
pybxizvuiwwngqej
zfumwnazxwwxtiry
keboraqttctosemx
vtlzxaqdetbhclib
wjiecykptzexuayl
ejatfnyjjdawepyk
mpcrobansyssvmju
gqukndzganeueabm
ukzscvomorucdnqd
wfydhtbzehgwfazx
mtwqdzlephqvxqmx
dltmlfxbjopefibh
atcfrowdflluqtbi
vowawlophlxaqonw
vblgdjzvwnocdipw
uzerzksmkvnlvlhm
ytjwhpaylohorvxd
siprvfxvnxcdgofz
cbhjupewcyjhvtgs
apqtozaofusmfqli
tmssrtlxfouowqnr
ntutrvwnzzgmokes
zrsgpwdzokztdpis
nrobvmsxtfmrqdhv
kadkaftffaziqdze
yrovbgcyqtlsnoux
modheiwuhntdecqs
gzhjypwddizemnys
gaputpwpcsvzxjho
bgmouxwoajgaozau
oxuapfrjcpyakiwt
kntwbvhuaahdixzj
epqjdjbnkxdnaccx
dspltdvznhypykri
tdrgqmbnagrxdwtt
njfqawzjggmemtbg
chpemsgwpzjpdnkk
fpsrobmbqbmigmwk
flxptsrqaazmprnl
nzdunrxlcbfklshm
miuwljvtkgzdlbnn
xbhjakklmbhsdmdt
xwxhsbnrwnegwcov
pwosflhodjaiexwq
fhgepuluczttfvqh
tldxcacbvxyamvkt
gffxatrjglkcehim
tzotkdrpxkucsdps
wxheftdepysvmzbe
qfooyczdzoewrmku
rvlwikuqdbpjuvoo
bcbrnbtfrdgijtzt
vaxqmvuogsxonlgq
ibsolflngegravgo
txntccjmqakcoorp
vrrbmqaxfbarmlmc
dzspqmttgsuhczto
pikcscjunxlwqtiw
lwzyogwxqitqfqlv
gsgjsuaqejtzglym
feyeqguxbgmcmgpp
gmttebyebdwvprkn
mzuuwbhzdjfdryxu
fganrbnplymqbzjx
cvsrbdcvhtxxdmro
scmgkjlkqukoamyp
fkgrqbyqpqcworqc
hjsrvkdibdjarxxb
sztzziuqroeidcus
pxdfvcpvwaddrzwv
phdqqxleqdjfgfbg
cqfikbgxvjmnfncy
";
}
}
using System;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String[] L = { "1", "5", "8", "9" };
            String[] U = { "4", "6" };

            String[] inData = textBox1.Text.Split(' ');

            String K = inData[0];
            Int64 N = Convert.ToInt64(inData[1]);

            String lineData = String.Empty;
            for (int i = 0; i < N; i++)
            {
                lineData += K; ;
            }


            while (lineData.Length > 1)
            {
                lineData = sumString(lineData.ToString());
                Console.WriteLine();
            }

            textBox2.Text = (String.Format("{0} {1}", L.Contains(lineData) ? "L" : U.Contains(lineData) ? "U" : "N", lineData));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int result = 0;
            char[] inData = textBox1.Text.ToCharArray();
            for (int i = 0; i < inData.Length; i++)
            {
                if (inData[i] == 120)
                {
                    result++;
                }
            }
            textBox2.Text = result.ToString();
            Console.WriteLine();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String[] inData = textBox1.Text.Split(' ');
            int A = Convert.ToInt32(inData[0]);
            int B = Convert.ToInt32(inData[1]);
            int C = Convert.ToInt32(inData[2]);

            Random rnd = new Random();
            int rndValue = rnd.Next(A, C); ;

            while (!isValid(A, B, C, rndValue))
            {

                rndValue = rnd.Next(A, C);
            }

            Console.WriteLine();
        }




        public Boolean isValid(int A, int B, int C, int rndValue)
        {
            Boolean result = ((rndValue / 2) <= A && (rndValue / 2) < B) && (rndValue * 2) <= C;
            return result;
        }




        public String sumString(String data)
        {
            int result = 0;
            for (int i = 0; i < data.Length; i++)
            {
                result += Convert.ToInt16(data[i].ToString());
            }
            return result.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int originalLength = 0;
            String[] xx = { "A", "E", "I", "O", "U" };
            String inValue = textBox1.Text;
            originalLength = inValue.Length;

            String[] treeWordLetter =
                "aaa aad aas afa aff aga ags aha ahi ahs aid ail aim ain air ais ait ake ala alb ale all alp als alt ama ami amp ana and ane ani ant any ape apo app apt arb arc ard are arf ark arm art ash ask asp ate att auk aul ava ave avo awa awl awn axe ayr ays azo bag bak bal bam ban bap bas bat bay baz bed bee beg bel bes bet bey bib bid big bim bio bis bit biz bod bog Boi bok boo bop bos bot bov bow box boy bro brr bub bud bug bum bun bur bus but buy bye bys cab cad cam can cap car caw cay cee cep chi cig cis cob cod cog col cop cor cos cow cox coy coz cru cry cub cud cue cup cur cut cwm dab dad dag dah dak dal dan dap dat dau daw day deb dee def deg del den dev dew dex dey dib did dif dip dir dis dit div dob doc doe doh dol dom don doo dop dor dos dot dow dry dub dud due dug duh dui dun duo dup dux dye dzo ear eat eau ebb ecu edh eds eek eel eep eet eff efs eft egg eid eke eld elf elk ell elm els eme emo ems emu end eng ens ent eon era ere erk ern err ers esh esp ess eta eth ety eve ewe exy eye fab fad fag fah fan fap fas fat fax fay fed fee feh fem fen fer fes fet feu few fey fez fib fid fie fig fil fir fit fix fiz flu fly fob foe fog foh fon foo fop for fou fox foy fro fry fub fud fug fun fur gab gad gae gag gal gam gan gap gar gas gat gau gaw gay ged gee gel gem gen get gey ghi gib gid gie gig gin gip git gnu goa gob god gog gom gon goo gor gos got gox goy gry gul gum gun gut guv guy gym gyp had hae hag hah haj ham han hao hap har has hat haw hay hed heh hem hen hep hes het hew hex hey hic hid hie him hin hip hir his hit hmm hob hoe hon hop hor hot hov how hoy hub hue hug huh hum hun hup hut hyp ice ick icy ide ids iff ifs igg ile ilk ill imp ink inn ins int ion ipe ire irk ish ism isu its ivy jab jag jam Jap jar jaw jay jee jeg jer jet jeu jew jib jig jin job joe jog jot jow joy jug jun jus jut kab kae kaf kas kat kaw kay kea kef keg ken kep ket kex key khi khu kid kif kim kip kir kis kit koa kob koi kop kor kos kot kue kut kye lab lac lad lag lah lai lam Lao lap lar lat lav law lax lbs lea led lee leg lei lek les let leu lev lex ley lez lib lid lie lil lip lis lit log lol loo lop lot lug lum Luo luv lux lye mac mae mal mam man mar mat maw max may med meg meh mel mem men met meu mew mho mia mib mic mid mig mil mim min mir mis mix moa mob moc mod mog moi mol mom mon moo mop mor mos mot mow mud mug mum mus mut mux myc naa nab nae nag nah nam nan nap naw nay neb ned nee neg nen net neu new nib nid nil nip nit nix nob nod nog noh nom non noo nor nos not nth nub num nun nus nut oaf oak oar oat oba obe obi oca oda ode ods oes oft ohi ohm ohs oik oil oka oke old ole olf olm oms omy one ono ons ooh oot ope ops opt ora orb orc ord ore org ors ort ose oud our out ova owe owl own owt oxo oxy pac pal pam pan pap par pas pat pav paw pax pea pec ped pee peh pep per pes pet pew phi pht pia pic pie pig pip pis pit piu ply pod poh poi pol pom poo pop pot pov pow pro pry psi pst pub pud pug pul pun pup pur pus put pwn pya pye pyx qat qis qua rad rag rah rai raj ram ran rap ras rat raw rax ray reb rec ree ref reg rei rem ren rep res ret rev rex rez rho ria rib rid rif rig rin rip rob roc rod rog rom roo rot row rub rue rug rum run rut rya sab sac sad sae sag sai sal sam sap sat sau sav saw sax say sea sec sed see seg sei sel ser set sew sex sha she shh shy sib sic sim sip sir sis six ska ski sky sly sod soh sol som son sop sos sot sou sov sow sox soy soz spa spy sri ssh sss sty sub sue suk sum sun sup suq syn taa tab tad tae tag taj tam tan tao tap tar tas tat tau tav taw tax tea ted teg teh ten tet tew tey tho thy tia tib tic tie tik tip tis tit tod toe tog tom ton too top tor tot tow toy try tsk tub tug tui tum tun tup tut tux twa two tye udo uey ugh uke ulu ume umm ump uni uns upo ups urb urd urn urp use uta uts uzi vac van var vas vat vau vav vaw vee veg ven vet vex via vid vie vig vim vis voe vog vol vom vow vox vug vum wab wad wae wag wai wan wap war was wat waw way web wed wee wem wen wet wey wha who why wid wif wig wis wit wiz woe wok won woo wop wos wot wow wry wud wye wyn xis xor yag yah yak yam yap yar yaw yay yea yeh yem yen yep yes yet yew yex yin yip yob yod yok yom yon yow yuk yum yup yus zag zak zap zas zed zee zek zen zep zho zib zig zin zip zit zoa zoe zoo zun zuz zzz".Split(' ');

            int index = 0;
            foreach (String s in treeWordLetter)
            {
                if (inValue.IndexOf(s.ToUpper()) > 0)
                {
                    inValue = inValue.Replace(s.ToUpper(), "*" + (index+1));
                    Console.WriteLine("Match:"+s.ToUpper());
                    index++;
                }
               

             
                if (index == 2)
                {
                    break;
                }
            }



            textBox2.Text = (index==2)? inValue.Length.ToString() :originalLength+"";


        }
    }
}

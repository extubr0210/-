using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Формирователь_очтётов
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            
        }
        Subject BaseSb = new Subject();
        
        private void SubjOtch(object sender, RoutedEventArgs e)
        {
            ot = 1;
            otcht();
        }
        private void Rasp(object sender, RoutedEventArgs e)
        {
            ot = 2;
            otcht();
        }
        private void Otchspuch(object sender, RoutedEventArgs e)
        {
            ot = 3;
            otcht();
        }
        private int ot = -1;
        private void otcht()
        {
            switch (ot)
            {
                case 1:
                    Otch.Text = BaseSb.Otchot();
                    break;
                case 2:
                    Otch.Text = BaseSb.Otchotrasp();
                    break;
                case 3:
                    Otch.Text = BaseSb.Listofch();
                    break;
            }
        }
        private void PutDatp(object sender, RoutedEventArgs e)
        {
            put_lv(11);
        }
        private void Chls(object sender, RoutedEventArgs e)
        {
            put_lv(21);
        }
        private void TofLSn(object sender, RoutedEventArgs e)
        {
            put_lv(31);
        }
        private void TofLSns(object sender, RoutedEventArgs e)
        {
            put_lv(33);
            bfr = 0;
        }
        private void PChs(object sender, RoutedEventArgs e)
        {
            put_lv(41);
        }
        private void DLs(object sender, RoutedEventArgs e)
        {
            put_lv(51);
        }
        private void DChs(object sender, RoutedEventArgs e)
        {
            put_lv(52);
        }
        string[] memst=new string[4] { "", "", "", "" };
        int[] memin = new int[4] { 0, 0, 0, 0 };
        int nch = 0;
        private void inch(){ if (nch < 4) { nch++; }}
        private void dnch() { if (nch >0) { nch--; }}
        private void nl(int n) { if ((n >= 1) & (n <= 4)) { memin[n-1] = 0;memst[n-1] = ""; } }
        private void scr()
        {
            memst[0] = memst[1]; memst[1] = memst[2]; memst[2] = memst[3]; memst[3]= "";
            memin[0] = memin[1]; memin[1] = memin[2]; memin[2] = memin[3]; memin[3]= 0;
        }
        private void mem(string est,int lw)
        {
            if ((lw != 12) & (lw != 41) & (lw != 51) & (lw != 52)) { return; }
            inch();
            if ((nch == 4) & ((memst[3] != "") || (memin[3] != 0))) { scr(); }
            if ((nch >= 1) & (nch <= 3)) { for(int i = nch; i < 4; i++) { nl(i); } }
            memst[nch - 1] = est;memin[nch - 1] = lw;
        }
        private void back(object sender, RoutedEventArgs e)
        {
            if((nch>0)&(nch<=4))
            {
                put_lv(nlv(memin[nch - 1]));
                entrstr.Text = memst[nch - 1];
                Enterr();
                put_lv(memin[nch - 1]);
                dnch();
            }
        }
        private void forward(object sender, RoutedEventArgs e)
        {
            if((nch>=0)&(nch<4))
            {
                put_lv(memin[nch]);
                entrstr.Text = memst[nch];
                Enterr();
                inch();
            }
        }
        private void savetobff(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(Otch.Text);
        }
        private void savetofile(object sender, RoutedEventArgs e)
        {
            put_lv(1);
        }
        private void Enter(object sender, RoutedEventArgs e)
        {
            mem(entrstr.Text, lv);
            Enterr();
        }
        private void Enterr()
        {
            switch(lv)
            {
                case 1:
                    try
                    {
                        using(StreamWriter sw = new StreamWriter(entrstr.Text+".txt") )
                        {
                            sw.Write(Otch.Text);
                        }
                    }
                    catch(Exception e) {; }
                    break;
                case 11:
                    put_lv(12);
                    break;
                case 12:
                    if (entrstr.Text == "") { put_lv(0); }
                    else
                    {
                        BaseSb.LL.Add(new Lesson(entrstr.Text));
                    }
                    break;
                case 21:
                    int nmm = Isnumb(entrstr.Text);
                    if((nmm>=0)&(nmm<BaseSb.LL.Count))
                    { bfr = nmm;put_lv(22);break; }
                    bfr = -1;
                    for(int i=0;i<BaseSb.LL.Count;i++)
                    {
                        if((entrstr.Text==BaseSb.LL[i].Date)||(entrstr.Text==BaseSb.LL[i].LIdea))
                        { bfr = i;
                            break;
                        }
                    }
                    if (bfr == -1) { put_lv(0); }
                    else { put_lv(22); }
                    break;
                case 22:
                    if (entrstr.Text == "") { put_lv(0); }
                    else
                    {
                        BaseSb.LL[bfr].LC.Add(entrstr.Text);
                        bool b = false;
                        foreach(Children ch in BaseSb.LsC)
                        {
                            if(ch.FullName==entrstr.Text)
                            {
                                b = true;
                                break;
                            }
                        }
                        if(b==false)
                        { BaseSb.LsC.Add(new Children(entrstr.Text)); }
                    }
                    break;
                case 31:
                    int nmmm = Isnumb(entrstr.Text);
                    if ((nmmm >= 0) & (nmmm < BaseSb.LL.Count))
                    { bfr = nmmm; put_lv(32); break; }
                    bfr = -1;
                    for (int i = 0; i < BaseSb.LL.Count; i++)
                    {
                        if ((entrstr.Text == BaseSb.LL[i].Date) || (entrstr.Text == BaseSb.LL[i].LIdea))
                        {
                            bfr = i;
                            break;
                        }
                    }
                    if (bfr == -1) { put_lv(0); }
                    else { put_lv(32); }
                    break;
                case 32:
                    BaseSb.LL[bfr].LIdea = entrstr.Text;
                    put_lv(0);
                    bfr = -1;
                    break;
                case 33:
                    if ((bfr < 0) || (bfr >= BaseSb.LL.Count))
                    {
                        put_lv(0);
                        bfr = -1;
                    }
                    else
                    {
                        BaseSb.LL[bfr].LIdea = entrstr.Text;
                        bfr++;
                        if(bfr==BaseSb.LL.Count)
                        {
                            put_lv(0);
                            bfr = -1;
                        }
                    }
                    break;
                case 41:
                    if (entrstr.Text == "") { put_lv(0); }
                    else
                    {
                        BaseSb.LsC.Add(new Children(entrstr.Text));
                    }
                    break;
                case 51:
                    if (entrstr.Text == "") { put_lv(0);break; }
                    int nnm= Isnumb(entrstr.Text);
                    if ((nnm >= 0) & (nnm < BaseSb.LL.Count))
                    {
                        BaseSb.LL.RemoveAt(nnm);
                    }
                    else
                    {
                        for(int i=0;i<BaseSb.LL.Count;i++)
                        {
                            if(BaseSb.LL[i].Date==entrstr.Text)
                            {
                                BaseSb.LL.RemoveAt(i);
                                i--;
                            }
                        }
                    }
                    break;
                case 52:
                    if (entrstr.Text == "") { put_lv(0);break; }
                    int nm = Isnumb(entrstr.Text);
                    if ((nm>=0)&(nm< BaseSb.LsC.Count))
                    {
                        BaseSb.LsC.RemoveAt(nm);
                    }
                    else
                    {
                        for (int i = 0; i < BaseSb.LsC.Count; i++)
                        {
                            if (BaseSb.LsC[i].FullName == entrstr.Text)
                            {
                                BaseSb.LsC.RemoveAt(i);
                                i--;
                            }
                        }
                        foreach(Lesson lsn in BaseSb.LL)
                        {
                            for (int i = 0; i < lsn.LC.Count; i++)
                            {
                                if (lsn.LC[i] == entrstr.Text)
                                {
                                    lsn.LC.RemoveAt(i);
                                    i--;
                                }
                            }
                        }
                    }
                    break;
                default:
                    put_lv(0);
                    bfr = -1;
                    break;
            }
            entrstr.Text = "";
            otcht();
        }
        private int Isnumb(string str)
        {
            if (str[0] != '#') { return -1; }
            int op=1;
            try
            {
                op = int.Parse(str.Remove(0, 1));
            }
            catch (FormatException) {; }
            return op;
        }
        int bfr = -1;
        private int lv = 0;
        private int nlv(int lw)
        {
            if (lw == 12) { return 51; }
            if (lw == 41) { return 52; }
            if (lw == 51) { return 12; }
            if (lw == 52) { return 41; }
            return 0;
        }
        public string lvtostring()
        {
            if (lv == 11) { return "Редактирование расписания, выберете предмет написав его номер или имя"; }
            if (lv == 12) { return "Ввод дат, когда проводились уроки по этому предмету"; }
            if (lv == 21) { return "Выберете занятие для отмечения посещаемости"; }
            if (lv == 22) { return "Введите имена присутствовавших на занятии"; }
            if (lv == 31) { return "Выберите занятие для редактирования темы"; }
            if (lv == 32) { return "Введите тему занятия"; }
            if (lv == 33) { return "Введите темы занятий, в порядке этих занятий"; }
            if (lv == 41) { return "Введите имена участников для добавления"; }
            if (lv == 51) { return "Введите даты занятий для удаления"; }
            if (lv == 52) { return "Введите имена участников для удаления"; }
            if (lv == 1) { return "Введите имя файла для сохранения"; }
            return "Ожидание";
        }
        public void put_lv(int lw)
        {
            lv = lw;
            Proc.Text = lvtostring();
        }
    }
    public class Children
    {
        public string FullName;
        public Children() {; }
        public Children(string fn) { FullName = fn; }
    }
    public class Lesson
    {
        public string LIdea="none";
        public string Date="??.??.??";
        public List<string> LC = new List<string>();
        public Lesson() {; }
        public Lesson(string d) { Date = d; }
    }
    public class Subject
    {
        public string Name="some_Subgect";
        public List<Lesson> LL = new List<Lesson>();
        public List<Children> LsC = new List<Children>();
        public string Otchot()
        {
            string s = "";
            s += "Отчёт по предмету "; s += Name; s += "\n";
            foreach(Lesson Lsn in LL)
            {
                s += "Урок по теме:"; s += Lsn.LIdea; s += "\n";
                s += "Дата:"; s += Lsn.Date; s += "\n";
                s += "Присутствовавшие:"; s += "\n";
                foreach(string ch in Lsn.LC)
                {
                    s += ch; s += "\n";
                }
            }
            return s;
        }
        public string Otchotrasp()
        {
            string s = "";
            s += "Даты занятий по предмету "; s += Name; s += "\n";
            foreach(Lesson Lsn in LL)
            {
                s += Lsn.Date; s += " | "; s += Lsn.LIdea; s += "\n";
            }
            return s;
        }
        public string Listofch()
        {
            string s = "";
            s += "Список учеников:"; s += "\n";
            for(int i=0;i<LsC.Count;i++)
            {
                s += i;s += " | "; s += LsC[i].FullName; s += "\n";
            }
            return s;
        }
    }

}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var sb = new StringBuilder();
            var stTotal = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < 28; i++)
            {
                var st = System.Diagnostics.Stopwatch.StartNew();
                sb.AppendLine($"{i}={Stories.NumberOfArrangements(i)}\t{st.Elapsed.TotalSeconds:000.000}");
            }
            stTotal.Stop();
            MessageBox.Show(stTotal.Elapsed.TotalSeconds.ToString() + "\r\n" + sb.ToString());

            Console.WriteLine(Stories.NumberOfArrangements(3));
            MessageBox.Show(Stories.NumberOfArrangements(3).ToString());
            MessageBox.Show(Stories.NumberOfArrangements(4).ToString());
            MessageBox.Show(Stories.NumberOfArrangements(5).ToString());
            MessageBox.Show(Stories.NumberOfArrangements(6).ToString());

        }
    }
}
//public class Counter
//{
//    private int count = 0;
//    private int increment;

//    public Counter(int increment)
//    {
//        this.increment = increment;
//    }

//    public int GetAndIncrement()
//    {
//        this.count += this.increment;
//        return this.count;
//    }


//}

//public class DocumentNameCreator
//{
//    private string prefix;
//    private Counter Counter { get; set; }

//    public DocumentNameCreator(string prefix, Counter counter)
//    {
//        if (counter is null) throw new ArgumentNullException(nameof(counter));
//        this.prefix = prefix;
//        this.Counter = counter;
//    }

//    public string GetNewDocumentName()
//    {
//        return prefix + Counter.GetAndIncrement();
//    }
//}



public class Stories
{

    //public static int NumberOfArrangements4(int numberOfStories)
    //{
    //    //track the positions of the big ones; then cancel duplicates?
    //}

    public static int NumberOfArrangements(int numberOfStories)
    {
        var arrangements = new HashSet<int>();
        for (int i = 0; i < (1 << (numberOfStories-1)); i++)
        {
            //permute each big/small like binary; stop when we hit the top
            var arr = CountSize(numberOfStories, i);
            if (arr >= 0) arrangements.Add(arr);
        }
        return arrangements.Count;
    }

    private static int CountSize(int numberOfStories, int thisPerm)
    {
        var thisSize = 0;
        var thisArr = 0;
        //var sb = new System.Text.StringBuilder(numberOfStories);
        while (thisSize < numberOfStories)
        {
            var thisBig = thisPerm & 1;
            thisPerm = thisPerm >> 1;
            thisSize += 1 + thisBig;
            if (thisSize <= numberOfStories)
            {
                //sb.Append("sl"[thisBig]);
                thisArr = (thisArr << 1) | thisBig;
            }
            else
            {
                //sb.Append("SL"[thisBig]);
                //sb.Append($"(={thisSize})");
                return -1;// (-1, sb.ToString());
            }
        }
        //sb.Append($"(={thisSize})");
        return thisArr;//, sb.ToString());
    }


    //skip the last floor (either the 2nd last is small, and so s the last; or the 2nd last is big; and there is no more)


    public static int NumberOfArrangements2(int numberOfStories)
    {
        //3 stories: sss,sl,ls
        //4 stories: ssss, ssl, sls, lss, ll
        //5 stories: sssss, sssl, ssls, slss, lsss, sll, lsl, lls
        //6 stories: ssssss, ssssl, sssls, sslss, slsss, lssss, lssl, lsls, llss, slsl, slls, ssll, lll

        var count = 0;
        for (int nBigs = 0; nBigs <= (numberOfStories / 2); nBigs++)
        {
            var nSmalls = numberOfStories - (nBigs * 2);
            var nSlots = nBigs + nSmalls;
            //given x big and y small, there are 2^(x+y) ways to have any sizes in that many slots; but only count those which have the right number of big/small
            for (int i = 0; i < (1 << (nSlots)); i++)
            {
                var OnBits = Enumerable.Range(0, nSlots).Count(x => (i & (1 << x)) != 0);//find how many bits in the binary of i are true
                if (OnBits == nBigs) count++;
            }
        }
        return count;

    }


    //public static int NumberOfArrangements(int numberOfStories)
    //{
    //    //possible amount of large/small is up to nos/2 big, with nnos%2 small; to 0big, nos small.
    //    //for a given number of large, there will be a secific number of smalls, for a total number of slots;
    //    //ignoring floors, each "slot" is either big/small; like binary; therefore 2^slots combinations for any building of that many slots
    //    //this is defined as an int return; so only track values within that scale


    //    var minSlots = (numberOfStories / 2) + (numberOfStories % 2); //all big, plus a small if it fits
    //    var maxSlots = numberOfStories;
    //    var combinations = 0;

    //    for (var bigs = 0; (bigs * 2) < numberOfStories; bigs++)
    //    {
    //        var smalls = numberOfStories - (bigs * 2);
    //        var slots = bigs + smalls;
    //        if (bigs>0 && smalls > 0)
    //        {
    //            combinations += 1 << (slots - 1); //2^slots
    //        }

    //    }
    //    return combinations;
    //}

    public static void M(String[] args)
    {
        Console.WriteLine(NumberOfArrangements(3));
    }
}





/*
 * public abstract class DocumentCounter
{
    private int count = 0;
    private int increment;
    
    public DocumentCounter(int increment)
    {
        this.increment = increment;
    }

    protected int GetAndIncrement()
    {
        this.count += this.increment;
        return this.count;
    }
    
    public abstract string GetNewDocumentName();
}

public class DocumentNameCreator : DocumentCounter
{
    private string prefix;
    
    public DocumentNameCreator(int increment, string prefix) : base(increment)
    {
        this.prefix = prefix;
    }

    public override string GetNewDocumentName()
    {
        return prefix + GetAndIncrement();
    }
}

    */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HTML_to_text_re_mline //  **************************************** ver 2 *******
{ //
    //
    // Note c# function 'loc = content.IndexOf(' does not always work
    // content is the entire html file (large file) 
    // function seems to work with small string size 
    

    class Program
    {
        //int s, e;
        //static string con;
        public static Int32 loc9;
        public static int nofh = 0; // number of horses
        public static int sH=0;
        static void Main(string[] args)
        {
            //string rootPath = @"C:\Users\Kurt\IdeaProjects\June2019_debug\scr2";
           // string rootPath = @"C:\Users\Kurt\IdeaProjects\June2019_debug";
            string rootPath = @"C:\Users\Kurt\IdeaProjects\June2019_debug_1file"; 

            string[] dirs = Directory.GetDirectories(rootPath); // folder dirs[29] = July31
            string raceID = "  ";
            string match = "012345678901234567890123456789012345678901234";
            string lookahead = "01234567890123456789012345678901234567890";
           // string lookaheadLg =  ;

            string[] rnum = { "...", "1st R", "2nd R", "3rd R", "4th R", "5th R", "6th R", "7th R", "8th R", "9th R", "10th R", "11th R", "12th R", "13th R", "14th R" };
            string[] age = { ".....", "TWO Y", "THREE Y", "FOUR Y" };
            string ch = "  ";

            //string age234 = "";

            // surface types - surface i.e. Dirt, Synthetic, Turf

            foreach (string dir in dirs)
            {
                //    Console.WriteLine(dir);
                //}
                //var files = Directory.GetFiles(dirs[0], "*.ht*"); // limit get to html files
                var files = Directory.GetFiles(dir, "*.ht*"); // limit get to html files

                // byte i = 0;
                byte cnt = 0;
                int filecnt = files.Length;

                string fileoutname = "";

                for (byte i = 0; i <= (filecnt - 1); i++) // loops number of files in current directory
                {
                    // get file name no extention
                    string fileINpath = Path.GetFullPath(files[i]); // full path to file to convert html to txt
                    string filenameonly = Path.GetFileNameWithoutExtension(files[i]);
                    // output file name with .txt
                    //fileoutname = dirs[0] + "\\" + filenameonly + ".MLtxt";// folder dirs[29] = July31
                    fileoutname = dir + "\\" + filenameonly + ".MLtxt";// folder dirs[29] = July31

                    //get html data into a string

                    string content = File.ReadAllText(fileINpath); // 
                    string lookaheadLg = content;
                    //System.IO.File.WriteAllText(@"C:\Users\owner\Documents\TRdays\July2019\July31\contents.txt", content); // write full file to compre with original


                    StringBuilder newtxt = new StringBuilder();
                    // start txt file with date and track
                    // NOTE put in value of month that your working on - ex. July is 07/
                    // assume file not empty

                    //newtxt.Append(" November,11/"); // debug
                    newtxt.Append("June 6/");

                    Int32 loc4 = content.Length; // entire file len = number of characters
                    Int32 loc = 0;

                    //loc = content.IndexOf(" November", 0, loc4); // debug
                    loc = content.IndexOf("June", 0, loc4);

                    Int32 dcnt = 4;
                    Int32 dcnt2 = 2;
                    Int32 s, e, w1, w2, w3, nH;
                    Int32 loc2 = 0;
                    loc2 = content.IndexOf(",", loc, loc + 15);
                    Int32 loc3 = 0;
                    Int32 scratch = 0;

                    if (content.Substring(loc2 - 4, 1) == " ") { dcnt = 3; dcnt2 = 1; }
                    newtxt.Append(content.Substring(loc2 - dcnt, dcnt2) + "/2019\r\n");
                    newtxt.Append(filenameonly + "\r\n");
                    //
                    //System.IO.File.WriteAllText( @"C:\Users\kglof\Documents\July2019\July03\newtxt.txt",newtxt.ToString()  ); //this works


                    // need loop thru all races

                    for (byte r = 1; r <= 14; r++)
                    {

                        // find 1st this is where the race infortation starts
                        bool b = content.Contains(rnum[r]); // if false race# not valid break out of loop

                        if (b == false)
                        {
                            r = 15;
                            break;

                        }
                        else
                        { // valid race#

                            loc = content.IndexOf(rnum[r]); // rnum[] is a string
                            // newtxt is new output string  

                            if (loc > 0) newtxt.Append(Convert.ToString(r) + ",Race,");

                            lookaheadLg = content.Substring(loc + 40, 200);
                            if (r == 10)
                            {
                                cnt++; // debug

                            }
                            s = loc + 35;
                            // last 0 represent current culture and reconizes Case 

                            loc = lookaheadLg.IndexOf("<b>"); // get lenth of class type i.e. Claiming

                            loc2 = lookaheadLg.IndexOf("."); // get lenth of class type i.e. Claiming
                            ////for (s = loc + 35; s <= s+50; s++)
                            ////{
                            ////    match = content.Substring(s, 3);
                            ////    if (match == "<b>") { 
                            ////        s = s + 3;
                            ////        lookahead = content.Substring(s, 40);
                            ////        break; }

                            ////}
                            // loc = content.IndexOf(".", s, s + 100); // get lenth of class type i.e. Claiming
                            //loc = lookahead.IndexOf(".", 0, 39);
                            w1 = s + loc;
                            w2 = loc + 3;
                            w3 = (loc2 - w2);
                            match = lookaheadLg.Substring(w2, w3) + ",";
                            newtxt.Append(match);
                            //w3 = w3 + w2;
                            w2 = loc2 + 2;
                            loc = lookaheadLg.IndexOf("&"); // get len of distance i.e. 6 Furlongs
                            match = lookaheadLg.Substring(w2, (loc - w2)) + ","; // w2 = start pnt , loc-w2 = hom many char(s)
                            newtxt.Append(match);
                            w3 = match.Length;
                            s = w1 + w3; // start val of surface
                            //
                            lookahead = lookaheadLg.Substring(loc, 40);

                            loc = lookahead.IndexOf("&"); // get len of surface i.e. Dirt, Synthetic, Turf
                            loc2 = lookahead.IndexOf("."); // get len of surface i.e. Dirt, Synthetic, Turf
                            w2 = loc2 - (loc + 6);
                            match = lookahead.Substring(loc + 6, w2) + ",";
                            newtxt.Append(match);
                            w2 = loc2 + 2; // takes out . and 1 space
                            s = s + w2;
                            //lookahead = lookaheadLg.Substring(w2, 40);
                            loc = lookahead.IndexOf("<", w2); // get len Purse value

                            match = lookahead.Substring(w2, (loc - w2)) + ",";
                            newtxt.Append(match);
                            s = s + match.Length + 10;
                            // get new large lookahead
                            //w3 = s + 1000; 
                            loc2 = content.IndexOf("Wager", s); // no end because could must stay in range of content
                            // must be a char that is breaking .IndexOf function


                            ////for (e = s; e <= e + 1000; e++)
                            ////{
                            ////    w3 = e;
                            ////    match = content.Substring(e, 5);

                            ////    //lookahead = content.Substring(s, 40);
                            ////    if (match == "Wager") { 
                            ////        break; }

                            ////}

                            lookaheadLg = content.Substring(s, (loc2 - s)); // will contain age & CL price
                            w1 = s; // this is to go back to start of long string search
                            //nH = w1; // initialize next horse pointer
                            // at this point need to locate age and if claming claiming price.
                            loc = -1;
                            loc2 = -1;
                            w2 = -1;
                            for (e = 1; e <= 3; e++)
                            {// 2,3,or 4 plus and upwards
                                loc = lookaheadLg.IndexOf(age[e]); // get age no need to limit short search
                                if (loc > 0) // now check for upwards
                                {

                                    w2 = age[e].Length;
                                    match = age[e].Substring(0, (w2 - 2));
                                    newtxt.Append(match); // add age
                                    e = 4;
                                    loc2 = lookaheadLg.IndexOf("UPWARD"); // get age
                                    if (loc2 > 0)
                                        newtxt.Append(" & UP,");
                                    else // no upward just add ","
                                        newtxt.Append(",");

                                    w2 = 0; // flag
                                } // find age 

                            }
                            // test for found age
                            if (w2 < 0)
                                w2 = w2; // no age found ERROR

                            s = w1 + loc2; // move pointer forward start of lookaheadLg
                            // if Claiming get price else its Allowance or stakes

                            loc = -1;
                            loc2 = -1;
                            w2 = -1;

                            loc = lookaheadLg.IndexOf("Claiming P"); // 
                            if (loc > 0)
                            {
                                w1 = loc;
                                loc2 = lookaheadLg.IndexOf(".", w1); // get len CL price
                                match = lookaheadLg.Substring(w1, (loc2 - w1)) + ",";
                                newtxt.Append(match); // update

                            }
                            else // not claiming so put in 0.00
                                newtxt.Append("Claiming Price $0,"); // add age




                            ////for (e = w1; e <= e + 1000; e++)
                            ////{
                            ////    match = content.Substring(e, 5);
                            ////    lookahead = content.Substring(e, 40);
                            ////    if (match == "</td>") { e = e--; break; }

                            ////}
                            // match = content.Substring(s, e-s);
                            //lookahead = content.Substring(e, 40);
                            //loc = content.IndexOf("d><b>", loc, loc + 120);
                            //loc2 = content.IndexOf("Handi",s);
                            newtxt.Append("\r\n");

                            // get how many horses
                            //loc2 = lookaheadLg.Length;
                            loc2 = lookaheadLg.IndexOf(")</"); // get loc of (?) ? = #hourse
                            //System.IO.File.WriteAllText(@"C:\Users\owner\Documents\TRdays\July2019\July31\newtxtDEL.txt", newtxt.ToString()); //this works
                            //loc = e; // find how many horses
                            match = lookaheadLg.Substring(loc2 - 4, 5);
                            w1 = s;
                            for (s = loc2 - 4; s <= s + 5; s++)
                            {
                                match = lookaheadLg.Substring(s, 1);
                                //lookahead = content.Substring(s, 40);
                                if (match == "(")
                                {
                                    if (lookaheadLg.Substring(s + 1, 1) == "1")
                                    {
                                        //sbyte hnum = Convert.ToSByte(content.Substring(s + 1, 2)); // 10+ horses
                                        int hnum = Convert.ToInt32(lookaheadLg.Substring(s + 1, 2));
                                        loc3 = hnum;
                                        break;
                                    }
                                    else
                                    { // less than 10
                                        //sbyte hnum = Convert.ToSByte(content.Substring(s + 1, 1)); // less than 10
                                        int hnum = Convert.ToInt32(lookaheadLg.Substring(s + 1, 1));
                                        loc3 = hnum;
                                        break;
                                    }

                                }
                            }
                            nofh = loc3;// nofh is number of horses
                            s = w1;
                            loc = content.IndexOf("ns: </b", s); // handi selection

                            lookaheadLg = content.Substring(loc, 25); // handi selection
                            ////for (s = loc + 145; s <= s + 100; s++) // start of handi
                            ////{
                            ////    match = content.Substring(s, 5);
                            ////    lookahead = content.Substring(s, 40);
                            ////    if (match == "Handi") { break; }

                            ////}
                            match = lookaheadLg.Substring(8, 8);
                            loc = loc + 8;
                            loc2 = lookaheadLg.IndexOf("</td>"); // handi selection end
                            // at this point need to get end of handi
                            ////for (e = s + 25; e <= e + 100; e++)
                            ////{
                            ////    match = content.Substring(e, 5);
                            ////    lookahead = content.Substring(e, 40);
                            ////    if (match == "</td>") { e = e--; break; }

                            ////}
                            match = lookaheadLg.Substring(8, (loc2 - 8));
                            // w1 = e;

                            //loc = content.IndexOf("d><b>", loc, loc + 120);
                            //loc2 = content.IndexOf("Wager",0,loc4);
                            newtxt.Append("Handi Selections: " + match + ",Entries," + Convert.ToString(loc3) + "\r\n");
                            //System.IO.File.WriteAllText(@"C:\Users\owner\Documents\TRdays\July2019\July31\newtxtDEL.txt", newtxt.ToString()); //this works
                            loc = loc + 280; // 280 must be a fixed num of jump ahead spaces
                            //lookaheadLg = content.Substring(loc, 250);

                            // =================================== Horses ==========================================

                            for (int h = 1; h <= loc3; h++) // upto Loc3 is number of horses
                            {
                                // loc is going to val that points to 1st then all other hourses
                                // starting loc in content
                                lookaheadLg = content.Substring(loc, 250);
                                sH = loc; // sH = 1st char location of current hourse
                                loc2 = lookaheadLg.IndexOf("<b>", 0, 200);
                                if (r == 6 && h == 1)
                                {
                                    cnt++; // debug
                                }

                                s = loc2 + 3;// get poll position
                                ////for (s=loc; s <= s + 50; s++) // 
                                ////{
                                ////    match = content.Substring(s, 3);
                                ////    lookahead = content.Substring(s, 40);
                                ////    if (match == "<b>") { s += 3;  break; }

                                ////}
                                match = lookaheadLg.Substring(s, 5);// debug look

                                match = lookaheadLg.Substring(s, 1); // horse post postion

                                loc2 = s;

                                ch = lookaheadLg.Substring(loc2, 1);                                newtxt.Append(ch);
                                loc2++;
                                ch = lookaheadLg.Substring(loc2, 1);
                                if (ch != "<")
                                {
                                    newtxt.Append(ch); // for 10 or more horses ch=1,2,3,4
                                    loc2++;
                                }

                                newtxt.Append(",");
                                loc = loc2 + 13; // starts @ 1st char of horse name
                                ch = lookaheadLg.Substring(loc, 1);
                                s = loc; // 
                                // s is 1st char of horse name
                                // at this point need to end of horse name
                                for (e = s + 5; e <= e + 1000; e++)
                                {
                                    match = lookaheadLg.Substring(e, 5);
                                    //lookahead = content.Substring(s, 40);
                                    if (match == "</td>") { e = e--; break; }

                                }
                                match = lookaheadLg.Substring(s, (e - s) - 5);  // match = full name

                                loc2 = e;
                                //loc = loc2;// content.IndexOf("</", loc, (loc + 50));
                                //ch = content.Substring(loc, (loc2 - loc)-5);// 5 takes out wht space and breeding state ex. (KY)
                                newtxt.Append(match + ",");
                                // System.IO.File.WriteAllText(@"C:\Users\owner\Documents\TRdays\July2019\July31\newtxtDEL.txt", newtxt.ToString()); //this works
                                // get odds : note favorite it will be bold --needing a second look
                                // need to check if scratched - if true 

                                // Start of odds thru trainer start if hourse not scratched. scratch is rare
                                // will only test for 1 scratch - if multiples program will brake
                                // note scratch always put as last horse
                                // put in geniric odds and then loop use "break" to end for loop
                                scratch = lookaheadLg.IndexOf("SCRATCHED");
                                if (scratch > 0)
                                {// true - put in 0.0 odds
                                    newtxt.Append("99/1,SCRATCHED \r\n");
                                    loc = sH + 100;
                                    lookaheadLg = content.Substring(loc, 200);
                                    scratch = lookaheadLg.IndexOf("SCRATCHED");
                                    if (scratch > 0) // second scratch
                                    {// get PP
                                        loc2 = lookaheadLg.IndexOf("<b>", 0, 150);
                                        s = loc2 + 3;// get poll position
                                        loc2 = s;
                                        ch = lookaheadLg.Substring(loc2, 1);
                                        newtxt.Append(ch);
                                        loc2++;
                                        ch = lookaheadLg.Substring(loc2, 1);
                                        if (ch != "<")
                                        {
                                            newtxt.Append(ch); // for 10 or more horses ch=1,2,3,4
                                            loc2++;
                                        }
                                        newtxt.Append(",");
                                        loc = loc2 + 13; // starts @ 1st char of horse name
                                        ch = lookaheadLg.Substring(loc, 1);
                                        s = loc; // 
                                        // s is 1st char of horse name
                                        // at this point need to end of horse name
                                        for (e = s + 5; e <= e + 1000; e++)
                                        {
                                            match = lookaheadLg.Substring(e, 5);
                                            //lookahead = content.Substring(s, 40);
                                            if (match == "</td>") { e = e--; break; }

                                        }
                                        match = lookaheadLg.Substring(s, (e - s) - 5);  // match = full name

                                        loc2 = e;
                                        //loc = loc2;// content.IndexOf("</", loc, (loc + 50));
                                        //ch = content.Substring(loc, (loc2 - loc)-5);// 5 takes out wht space and breeding state ex. (KY)
                                        newtxt.Append(match + ",");
                                        newtxt.Append("99/1,SCRATCHED \r\n");
                                        loc = sH + 355;
                                        break; // break out of for(h loop

                                    }
                                    else
                                    { // only 1 scratch
                                        loc = sH + 225;
                                        break;
                                    }
                                    // break; may need break here to jump out of for(h loop

                                } // end if scratch > 0

                                dcnt = 9; // should be 9 chars away unless fav
                                dcnt2 = 0;
                                loc = loc2 + dcnt; // starts @ 1st char of odds
                                ch = lookaheadLg.Substring(loc, 1);
                                if (lookaheadLg.Substring(loc, 1) == "<") { loc += 3; dcnt2 = 4; } // moves +3 and 4 more after getting odds 
                                // e=get dist to next "<" after odds value
                                s = loc; // 1st char off odds                                
                                match = lookaheadLg.Substring(s, 5);
                                // at 
                                for (e = s; e <= e + 25; e++)
                                {
                                    match = lookaheadLg.Substring(e, 1);
                                    // lookahead = content.Substring(s, 40);
                                    if (match == "<") { e = e--; break; }

                                }
                                match = lookaheadLg.Substring(s, e - s);
                                loc2 = e;


                                // loc2 = content.IndexOf("<", loc, loc + 10);
                                match = lookaheadLg.Substring(loc, loc2 - loc);
                                newtxt.Append(match + ",");

                                loc = loc2 + 9 + dcnt2; // starts @ 1st char of jockey
                                ch = lookaheadLg.Substring(loc, 1);
                                //ch = "Z";
                                byte[] ch1 = Encoding.ASCII.GetBytes(ch);
                                //get dec val ch
                                if (ch1[0] < 65 || ch1[0] > 90)
                                { // no Jockey name - put in No Jockey for name
                                    s = loc;
                                    newtxt.Append("NO Jockey,");
                                    match = lookaheadLg.Substring(s, 50);
                                    loc2 = s - 4;



                                }
                                else
                                {

                                    s = loc; //1st char of Jockey
                                    for (e = s + 7; e <= e + 35; e++)
                                    {
                                        match = lookaheadLg.Substring(e, 1);
                                        //lookahead = content.Substring(s, 40);
                                        if (match == "<") { e = e--; break; }

                                    }
                                    match = lookaheadLg.Substring(s, e - s);
                                    loc2 = e;
                                    //loc2 = content.IndexOf("<", loc, loc + 10);
                                    match = lookaheadLg.Substring(loc, loc2 - loc);
                                    newtxt.Append(match + ",");

                                } // end else

                                //loc2 = content.IndexOf("<", loc, loc + 10);
                                //match = lookaheadLg.Substring(loc, loc2 - loc);
                                //newtxt.Append(match + ",");
                                loc = loc2 + 9; // starts @ 1st char of wieght
                                ch = lookaheadLg.Substring(loc, 1);
                                s = loc;
                                for (e = s; e <= e + 5; e++)
                                {
                                    match = lookaheadLg.Substring(e, 1);
                                    if (match == "<") { e = e--; break; }

                                }
                                match = lookaheadLg.Substring(s, e - s);
                                loc2 = e;
                                //loc2 = content.IndexOf("<", loc, loc + 10);
                                match = lookaheadLg.Substring(loc, loc2 - loc);
                                newtxt.Append(match + ",");
                                loc = loc2 + 9; // starts @ 1st char of age
                                ch = lookaheadLg.Substring(loc, 1);
                                s = loc;
                                loc2 = loc + 90; // moves from age passes over equipment to trainer name

                                newtxt.Append(ch + ",");
                                //System.IO.File.WriteAllText(@"C:\Users\owner\Documents\TRdays\July2019\July31\newtxtDEL.txt", newtxt.ToString()); //this works
                                loc = loc2; // starts @ 1st char of new search
                                match = lookaheadLg.Substring(loc, 20);
                                s = loc;

                                // get 1st char of trainer name
                                for (s = loc; s <= s + 20; s++)
                                {
                                    match = lookaheadLg.Substring(s, 4);
                                    if (match == "<td>") { s += 4; break; }

                                }
                                match = lookaheadLg.Substring(s, 3);
                                //s = e;
                                for (e = s + 5; e <= e + 40; e++)
                                {
                                    match = lookaheadLg.Substring(e, 1);
                                    if (match == "<") { break; }

                                }
                                match = lookaheadLg.Substring(e, 3);
                                loc = e; // s=1st char of trainer name e=last
                                //loc2 = content.IndexOf("<", e, e + 40);
                                match = lookaheadLg.Substring(s, (e - s));
                                newtxt.Append(match + "\r\n"); //add trainer then new line




                                // need loc = next hourse in content
                                // loc = content.IndexOf(match, sH);
                                loc += sH;
                                lookahead = content.Substring(loc, 10);
                                loc9 = match.Length;

                                // find next horse starting pnt
                                loc = loc + 35;


                                //  System.IO.File.WriteAllText(@"C:\Users\owner\Documents\TRdays\July2019\July31\newtxtDEL.txt", newtxt.ToString()); //this works


                            }// number of horses in current race#


                        } // end if valid race #
                        // loc2 1st char after last trainer
                        //loc = loc + loc9 +103; // starts @ 1st char of Owners
                        /// if (r != 15)
                        /// { // if r = 15 no more races
                        lookaheadLg = content.Substring(loc, 250);
                        loc9 = lookaheadLg.IndexOf("Owners:");
                        if (loc9 < 0)
                        { // all horses 
                            while (loc9 < 0)
                            {
                                loc += 250;
                                lookaheadLg = content.Substring(loc, 250);
                                loc9 = lookaheadLg.IndexOf("Owners:");

                            }
                        }


                        loc9 = loc9 + loc;
                        match = content.Substring(loc9, 20);
                        s = loc;
                        loc = nofh * 100;
                        lookaheadLg = content.Substring(loc9, loc);

                        loc2 = lookaheadLg.IndexOf("</td></tr></table");
                        // at this point need to end of Owners, Breeders:
                        ////for (s = loc; s <= s + 30; s++)
                        ////{
                        ////    lookaheadLg = content.Substring(loc9, (loc2-loc9));
                        ////    if (match == "<b>") { s += 3; break; }

                        ////}

                        lookaheadLg = lookaheadLg.Substring(0, loc2);
                        newtxt.Append(lookaheadLg + "\r\n");
                        match = content.Substring((loc9 + loc2), 20);
                        // A 70 * # of horses
                        loc = (nofh * 70) + 100;
                        lookaheadLg = content.Substring((loc9 + loc2), loc);
                        loc9 = lookaheadLg.IndexOf("Breeders:");
                        match = lookaheadLg.Substring(loc9, 20);

                        loc2 = lookaheadLg.IndexOf("</td></tr></table>", loc9);
                        lookaheadLg = lookaheadLg.Substring(loc9, (loc2 - loc9));
                        newtxt.Append(lookaheadLg + "\r\n\r\n");



                        ////for (e = s + 100; e <= e + 1000; e++)
                        ////{
                        ////    match = content.Substring(e, 5);
                        ////    if (match == "</tab") { e -= 10; break; }

                        ////}
                        //match  = content.Substring(s, s);                    
                        //newtxt.Append(match + "\r\n");
                        ////w1 =  + 121; // begin of breed
                        ////// at this point need to end of Breeders
                        ////for (e = s + 100; e <= e + 1000; e++)
                        ////{
                        ////    match = content.Substring(e, 5);
                        ////    if (match == "</tab") { e -= 10; break; }

                        ////}
                        ////match = content.Substring(s, (e - s));
                        //newtxt.Append(match + "\r\n\r\n"); // put in space between races
                        //loc = e; // furthest pnt in content

                        //System.IO.File.WriteAllText(@"C:\Users\owner\Documents\TRdays\July2019\July31\newtxtDEL.txt", newtxt.ToString()); //this works



                        //} // if r != 15 -- no more races
                        cnt++;

                    }// loops thru number of races for current day 1-14
                    // now save newtxt file
                    cnt++;
                    // System.IO.File.WriteAllText(@"C:\Users\owner\Documents\TRdays\July2019\July31\newtxtDEL.txt", newtxt.ToString()); //this works
                    System.IO.File.WriteAllText(fileoutname, newtxt.ToString()); //this works

                } // loops thru all files in current directory

            } // for each loop of all sub dir of current month

            
            
            Console.ReadLine();

        }
       
         
    }
}

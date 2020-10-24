using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Genomics_Project
{
    enum SeqType { DNA, RNA, AA }; //creating enum SeqType and declaring the constant items
    class Program
    {
        public static void Main(string[] args)
        {
            DNA dna = new DNA(); // instantiating DNA class

            dna.printWelcomeMessage();

            int x = 0; //int used as while loop condition variable            
            while (x < 1) // this loop will call the readFile method and validateSeq method until validateSeq is evaluated to true
            {

                dna.seqtype = SeqType.DNA; //assinging fixed enum element to seqtype
                dna.readSeqFile(); //reading .txt sequence file
                if (dna.validateSeq(dna.dnaSeq)) //if the dna parameter is a valid DNA string, then validateSeq will return true and if block will execute
                {
                    Console.WriteLine("\n---SEQUENCE VALIDATED---\n"); //displaying message
                    x++; //incrementing x and terminating the loop
                }
                else //printing error message and starting loop over if validateSeq returns false
                {
                    //displaying message
                    Console.WriteLine("\n!!ERROR!! The sequence submitted could not be validated\n--Unsupported characters found in the sequence - Please try submitting another file\n");
                }
            }

            dna.perA = dna.nucleotidePer('A'); //calling nucleotidePer method with char 'A' as an argument and assigning return value to perA (percentage A)
            dna.perT = dna.nucleotidePer('T'); //calling nucleotidePer method with char 'T' as an argument and assigning return value to perT (percentage T)
            dna.perG = dna.nucleotidePer('G'); //calling nucleotidePer method with char 'G' as an argument and assigning return value to perG (percentage G)
            dna.perC = dna.nucleotidePer('C'); //calling nucleotidePer method with char 'C' as an argument and assigning return value to perC (percentage C)

            dna.printNucPer(); //calling the printNucPer method, to print the nucleotide percentages to the console

            dna.compGen(dna.dnaSeq); //calling the compGen method to produce a complementary dna strand

            RecognitionSites rs = new RecognitionSites(); //instantiating RestrictionSites class

            int y = 0; //int used as while loop condition variable
            while (y < 1) //this loop will ask for the enzyme cut site the user wants to find, find the site, then ask if they want to look for another site
            {
                rs = new RecognitionSites(rs.askForEnzmye()); //using non-default constructor to call askForEnzyme method and assign the return value to the enzyme variable
                rs.RSFinder(dna.dnaSeq, rs.enzyme); //calling the RSFinder method
                int z = 0;
                while (z < 1)
                {
                    Console.WriteLine("Would you like to search for another enzyme recogition site [Yes/No]?: "); // asking if the user want to look for another enzyme's recognition site
                    string answer = (Console.ReadLine()).ToLower(); //storing their input as variable
                    if (answer == "y" | answer == "yes") //if user answered yes, the loop will start over
                    {
                        Console.WriteLine();
                        z++;
                    }
                    else if (answer == "n" | answer == "no") //if the user answers no, the loop will terminate
                    {
                        Console.WriteLine("\n-------------Exiting Program-------------\n");
                        z++;
                        y++; //incrementing y and terminating the loop
                    }
                    else
                    {
                        Console.WriteLine("\n!!ERROR!! Please enter a valid response.\n");
                    }
                }
            }
            Console.ReadLine();
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Genomics_Project
{

    class DNA //defining DNA class

    {
        public SeqType seqtype { get; set; } //creating seqtype property that will be assigned a value based on the selected item from the enum SeqType
        public string dnaSeq { get; set; } //creating dna sequence property
        private string compSeq { get; set; } //creating complementary dna sequence property
        private int dnaLength { get; set; } //creating dnaLength property to store the length of the DNA sequence
        public double perA; //creating percentage of A field
        public double perT; //creating percentage of T field
        public double perG; //creating percentage of G field
        public double perC; //creating percentage of C field
        private Dictionary<char, char> compDict; //instantiating dictionary class in the compDict object to store the key-value pairs for complementary DNA


        public DNA() //default constructor
        {
            compDict = new Dictionary<char, char>() //instantiating compDict and assigning key-value pairs
            { //each key-value pair represents a dna nucleotide and its associated complement nucleotide
            {'A','T'}, //Given 'A' as the key - 'T' will be its corresponding complement
            {'T','A'}, //Given 'T' as the key - 'A' will be its corresponding complement
            {'G','C'}, //Given 'G' as the key - 'C' will be its corresponding complement
            {'C','G'}  //Given 'C' as the key - 'G' will be its corresponding complement
            };
        }

        public void printWelcomeMessage()
        {
            Console.WriteLine("\t####################################################");
            Console.WriteLine("\t#                                                  #");
            Console.WriteLine("\t#    RESTRICTION ENZYME RECOGNITION SITE FINDER    #");
            Console.WriteLine("\t#                                                  #");
            Console.WriteLine("\t####################################################\n");



        }

        public void readSeqFile() //creating method which reads the user's file
        {
            int x = 0; //int used as while loop condition variable
            while (x < 1)
            {
                try //implementing a try block to throw any exceptions (particularly FileNotFound exception) to the catch block
                {
                    x++; //incrementing x so that the while loop will terminate if there are no exceptions
                    Console.WriteLine("Please provide the name of your DNA sequence file: "); //asking for the file name that contains the dna sequence
                    StreamReader reader = new StreamReader(Console.ReadLine()); //instantiating StreamReader with a non-default constructor containing the user's input
                    dnaSeq = "";
                    string line = ""; //creating empty string variable
                    while ((line = reader.ReadLine()) != null) // this loop reads each line of the file until the line value is null
                    {
                        dnaSeq += line; //adding each line to the dnaSeq string field
                    }
                }
                catch (FileNotFoundException) // catching any file-not-found exceptions
                {
                    Console.WriteLine("\n!!ERROR!! The file you entered could not be found. Please try again...\n"); //displaying error message
                    x--; //decrementing x if the catch block executes to restart the while loop
                }
                catch (Exception e) // catching any other unexpected exceptions
                {
                    Console.WriteLine("Whoops! Something went wrong:\n " + e.Message + "\nPlease try again"); //displaying error message
                    x--; //decrementing x if the catch block executes to restart the while loop
                }
            }
        }

        public bool validateSeq(string dna) //creating method which accepts the dna sequence as a parameter and will return a boolean value
        {
            string str = dna.ToUpper(); //converting string to all uppercase characters in order to evaluate if statements
            bool valid = true; //creating boolean value
            for (int x = 0; x < dna.Length; x++) //this for loop will look an each nucleotide in the dna sequence and determine if it's valid
            {
                if (str[x] == 'A' | str[x] == 'T' | str[x] == 'G' | str[x] == 'C') //if the nucleotide at position x is A, T, G, or C then it is a valid nucleotide
                {
                    continue; //continue the loop on to the next nucleotide
                }
                else //if the nucleatide at position x is anything other than A, T, G, or C then it is not a valid nucleotide
                {
                    valid = false; //boolean value is set to false
                    break; //loop terminates and method will return false
                }
            }
            return valid;
        }

        public double nucleotidePer(char nuc) // this method will iterate throught the dnaSeq string and count the number of times it the given nucleotide parameter occurs
        {

            int totalcount = 0; //counting every nucleotide in the sequence
            int count = 0; //couting only the nucleotide provided as a parameter
            foreach (char i in dnaSeq) //foreach loop iterates over dnaSeq string
            {
                if (i == nuc) //checking to see if char i is equal to the input parameter
                {
                    count++; //incrementing count to gather total amount of the specific nucleotide
                }
                totalcount++; //incrementing totalcount
            }
            dnaLength = totalcount;
            double percent = Math.Round(((Convert.ToDouble(count) / Convert.ToDouble(totalcount))) * 100, 1); //calculating the percentage of the given nucleotide out of all the nucleotides in the sequence
            return percent; //returning percent
        }

        public void printNucPer() //this method will print the total length of the DNA sequence and the percentage of each nucleotid present onto a separate line
        {
            //printing nucleotide percentage to the console
            Console.WriteLine($"The total length of the provided DNA sequence is: {dnaLength} bps (base pairs)");
            Console.WriteLine($"\nThe nucleotide content of your DNA sequence is as follows:\n\n\tA content: {perA}%\n\tT content: {perT}%\n\tG content: {perG}%\n\tC content: {perC}%\n");
        }

        public void compGen(string dna) //creating a method that will generate a complementary dna sequence and store it in the compSeq field
        {
            foreach (char i in dna) //iterating through dna sequence
            {
                compSeq += compDict[i]; //adding each value returned from the complement dictionary for the given key to the compSeq string
            }
        }

        //Note: I haven't actually done anything with the complement sequence generated yet, but this is a project I will continue to work on

    }

    class RecognitionSites //defining the recognition sites class (recognition sites associated with each enzyme)
    {
        public string enzyme; //creating enzyme field to store the restriction enzyme entered by the user
        protected Dictionary<string, string> enzymeSites; //enzymeSites dictionary will store an enzyme as a key and its recognition site as the value
        protected Dictionary<string, int> enzymeCut; //enzymeCut dictionary will store an enzyme as a key and its cut location defined by its distance away from the first nucleotide of the recognition site


        public string askForEnzmye() //this method will ask the user for the enzyme they want to find the recognition sites for
        {
            string enzyme1; //creating empty string variable
            Console.WriteLine("Enter the enzyme you want to find the cut sites for: "); //prompting user for the enzyme they want to use
            enzyme1 = Console.ReadLine(); //storing the users input as a string
            return enzyme1; //returning the string enzyme1
        }
        public RecognitionSites() //blank default constructor so that I can instantiate class in the main method prior to a while loop
        {

        }

        public RecognitionSites(string enz) //non-default constructor that accepts a string as an input
        {
            enzyme = enz;
            enzymeSites = new Dictionary<string, string>() //instantiating compDict and assigning string key-value pairs
            { //each key is a restriction enzyme and each value is its corresponding cut recognition site

            {"ecori","GAATTC"}, //Given the key 'ecori' - its corresponding value is the recognition sequence 'GAATTC'
            {"bamhi","GGATCC"}, //same concept as above
            {"hindiii","AAGCTT"},
            {"sau3ai","GATC"}
            };

            enzymeCut = new Dictionary<string, int>()
            {
            {"ecori", 1}, //Given the key 'ecori' - its corresponding value is the distance away from that the cut location is from the first nucleotide of the recognition site
            {"bamhi", 1},
            {"hindiii", 1},
            {"sau3ai", 0 }
            };
        }

        public void RSFinder(string dna, string enzyme1) //this method will locate each recognition site from the enzyme in the given dna sequence and print out the position of the cut and the dna fragements generated based on the amount of recognition sites found
        {
            string site = enzymeSites[(enzyme1.ToLower())]; //storing the value (the recognition sequence) of the enzyme key from enzymeSites dict into site variable
            List<int> startIndex = new List<int>(); //creating List to store the start index of each recognition site
            List<int> endIndex = new List<int>(); //creating List to store the end index of each recognition site
            int count = 0; //this count variable will be used to index the recognition site
            int totalcount = 0; //variable will keep track of the number of nucleotides we've already looked at in the dna sequence
            string rsbuild = ""; //this empty string will store a sequence of nucleotide as they match each nucleotide in the recognition seq
            int start = 0; //this variable will store the start index of the given recognition seq
            int end = 0; //this variable will store the end index of the given recognition seq

            foreach (char n in dna) //this foreach loop will iterate through the given dna sequence string
            {
                if (n == site[count]) //checking to see if the given nucleotide 'n' is equal to the nucleotide of the rec site at index count
                {
                    if (count == 0) //checking to see if the match is with the 1st nucleotide of the rec sequence
                    {
                        start = totalcount; //setting start index value to totalcount
                        rsbuild += n; //adding the nucleotide 'n' to the rsbuild string
                        count++; //incrementing count to compare 'n' to the next index of the rec seq
                    }

                    else //if this is not the first nucleotide match within the rec seq, this block will execute
                    {
                        rsbuild += n; //adding the nucleotide 'n' to the rsbuild string
                        if (rsbuild == site) //checking to see if the rsbuild is equal to the recognition seq (comparing strings)
                        {
                            end = totalcount; //setting the end index to equal to the current total count
                            startIndex.Add(start + 1); //adding the start index to the startIndex List - and adding 1 so this value reflects its actual position in the dna seq as opposed to the index
                            endIndex.Add(end + 1); //adding the end index to the endIndex List - and adding 1 so this value reflects its actual position in the dna seq as opposed to the index
                            rsbuild = ""; //resetting rsbuild to an empty string - to compare to future rec seqs in the dna
                            count = 0; //resetting count variable - so that the 1st nucleotide of the rec seq is being compared to the current nucleotide 'n'
                        }
                        else //if the rsbuild and rec seq do not match, count is incremented
                        {
                            count++; //incrementing count
                        }

                    }

                }
                else //if nucleotide 'n' is not equal to the nucleotide at index 'count' of the rec seq, then this block executes
                {
                    count = 0; //resetting count to 0 
                    rsbuild = ""; //resetting rsbuild to an empty string
                }
                totalcount++; //incrementing totalcount every iteration to keep track of the index of the nucleotide 'n' in relation to the entire dna seq
            }

            Console.WriteLine($"\n{enzyme1} recognition sites were located at the following position ranges in the given DNA sequence:\n"); //printing message that will que user the results of their search are about to be given
            for (int i = 0; i < startIndex.Count; i++) //this for loop will execute a set number of times - which will be until 'i' reaches the value the length of the startIndex List - printing out the cut site location and the length of the fragment generated
            {
                int cutLocation = enzymeCut[(enzyme1.ToLower())]; //storing the value returned from the enzymeCut dictionary to the cutLocation variable
                Console.Write($"\tCut Site #{i + 1}: {startIndex[i] + cutLocation}  --- "); //printing cut site number and its location - calculated by the start index plus the value of the cutLocation variable

                if (i == 0 && i == (startIndex.Count) - 1) //checking to see if this is the first start index being printed and if this is the ONLY start index in the startIndex List
                {
                    Console.WriteLine($"\tFragment {i + 1} Length: \t{startIndex[i] + cutLocation}\n\t\t\t\tEnd Fragment Length: \t{totalcount - (startIndex[i] + cutLocation)}\n"); //printing fragment number and its length - the 1st fragment will only be as long as the value of the 1st start index
                }

                else if (i == 0) //checking to see if this is the first start index being printed
                {
                    Console.WriteLine($"\tFragment {i + 1} Length: \t{startIndex[i] + cutLocation}"); //printing fragment number and its length - the 1st fragment will only be as long as the value of the 1st start index
                }
                else if (i != (startIndex.Count) - 1) //checking to see if this start index is NOT the final start index 
                {
                    Console.WriteLine($"\tFragment {i + 1} Length: \t{(startIndex[i] + cutLocation) - (startIndex[i - 1] + cutLocation)}"); //printing the fragment number and its length - calculated by the current start index minus the previous start index
                }
                else //if this is the final start index of the startIndex List, this block will execute
                {
                    //printing the fragment number and its length - calculated by the current start index minus the previous start index AND the trailing or end fragment - calculated by the length of the entire dna seq: 'totalcount' minus the start index of the current start index
                    Console.WriteLine($"\tFragment {i + 1} Length: \t{(startIndex[i] + cutLocation) - (startIndex[i - 1] + cutLocation)}\n\t\t\t\tEnd Fragment Length: \t{totalcount - (startIndex[i] + cutLocation)}\n");
                }
            }

        }
    }

}

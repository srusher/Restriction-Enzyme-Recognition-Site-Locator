# Where to Look?:
In order to run this application you need both the code from the Program.cs and Class1.cs file. It should be ran in Visual Studio 2019 for optimal execution. I have also included a seq.txt file to test the program on but feel free to use any other DNA sequence. ** Make sure the .txt file is contained in the correct storage folder in your project directory **

# What does this program do?:

This application prompts the user for a text file that contains the DNA sequence. It will then read the DNA sequence from the file, validate the DNA sequence, provide the A, T, G, C percentage for the sequence, and then ask the user for the Restriction Enzyme they want to locate the recognition sites for. The program will then return the locations of the cut sites for that enzyme and the lengths of the fragements produced. It will then prompt the user if they want to search for another enzyme.

# Notes:

This program is only desinged to read a .txt file with only the DNA sequence contained within. If the file contains an accession number or ID, the program will fail to validate the sequence and ask you to try another file. I plan on adding a functionality to the program that allows it to read files in the FASTA format and to read multiple sequences within the file. 

# Output Example:
![Image of Output](https://github.com/srusher/Restriction-Enzyme-Site-Locator/blob/master/Output/Output2.png)

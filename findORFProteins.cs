//Soroush First Project to learn C#.
//Rosalind Open Reading Frame assignment http://rosalind.info/problems/orf/

using System; //Needed for printing
using System.Collections.Generic; //Needed for lists

namespace ORF{ 
    public class RosalindORF{

        public static List<string> allViableSequences = new List<string>(); //this is our global list. Has to be static to be referenced in our static methods


        public static string DNAtoRNA53(string DNA)  //turn DNA TO RNA for one direction
            //I was previously only doing G to C, A to U, etc. But I was completely forgetting that that would flip the 5'3 to 3'5
        {
            var RNA = string.Empty;

            for (int i = 0; i< DNA.Length; i++)
            {
                if (DNA[i] == 'G')
                {
                    RNA += "G";
                }else if (DNA[i] == 'C')
                {
                    RNA += "C";
                } else if (DNA[i] == 'A')
                {
                    RNA += "A";
                } else if (DNA[i] == 'T')
                {
                    RNA += "U";
                }
            }
            return RNA;
        }

        public static string DNAtoRNA35(string DNA)  //turn DNA TO RNA for the other direction, this requires reversing after for reason mentioned above
        {
            var RNA = string.Empty;

            for (int i = 0; i < DNA.Length; i++)
            {
                if (DNA[i] == 'G')
                {
                    RNA += "C";
                }
                else if (DNA[i] == 'C')
                {
                    RNA += "G";
                }
                else if (DNA[i] == 'A')
                {
                    RNA += "U";
                }
                else if (DNA[i] == 'T')
                {
                    RNA += "A";
                }
            }
            return RNA;
        }

        public static string reverseDNA(string DNA) //REVERSES DNA so that we have the proper direction before transcribing, reversing RNA isn't the same
        {
            int length = DNA.Length - 1;
            var reversestring = string.Empty;
            while(length >= 0)
            {
                reversestring += DNA[length];
                length--;
            }
            return reversestring;
        }

        public static void methFinder(string aminos) //finds start codon methionine and goes till stop codon ' '
        {

            var builtUpString = "";
            int count = 0;
            int totalLength = 0;
            for (int i = 0; i < aminos.Length; i++)
            {
                totalLength++;
                if (aminos[i] == ' ')
                {
                    if (count>0)
                    {
                        allViableSequences.Add(builtUpString);  //adds the found protein to our global list
                        count = 0;
                        builtUpString = "";
                    }
                    
                }

                if (aminos[i] == 'M') //once we find a start site, we can proceed to read
                {
                    count++;
                }

                if (count > 1) //if we've found more than one start site, calls function again using remaining string, this way we get multiple start sites
                {
                    methFinder(aminos.Substring(totalLength-1));
                    count = 1;
                }

                if (count > 0) //so long as we have found a start site but no stop, add on the aminos
                {
                    builtUpString += aminos[i];
                }

            }
        }

    

        public static string mRNAToAA(string mRNA)  //convers our codon triplets to amino acids
        {
            var protein = "";
            int desiredLength = 0;

            if( (mRNA.Length % 3) == 0)  //We do this to check where to end our checking
            {
                desiredLength = mRNA.Length;
            }
            else if(((mRNA.Length-1) %3) == 0){
                desiredLength = mRNA.Length - 1;
            }
            else if (((mRNA.Length - 2) % 3) == 0)
            {
                desiredLength = mRNA.Length - 2;
            }

            var codon = "";
            for (int i = 0; i < desiredLength; i = i+3)
            {
                codon += mRNA[i]; codon += mRNA[i + 1]; codon += mRNA[i + 2];
                protein += codonsToAA[codon];
                codon = "";
            }

            return protein;
        }

        private static Dictionary<string, char> codonsToAA = new Dictionary<string, char> //typed this up by hand as I couldn't quickly find a C# version. Will save for later haha
        {
            { "UUU", 'F' },            { "UUC", 'F' },
            { "UUA", 'L' },            { "UUG", 'L' },            { "CUU", 'L' },            { "CUC", 'L' },            { "CUA", 'L' },            { "CUG", 'L' },
            { "UCU", 'S' },            { "UCC", 'S' },            { "UCA", 'S' },            { "UCG", 'S' },            { "AGU", 'S' },            { "AGC", 'S' },
            { "UAU", 'Y' },            { "UAC", 'Y' },
            { "UGU", 'C' },            { "UGC", 'C' },
            { "UGG", 'W' },
            { "CCU", 'P' },            { "CCC", 'P' },            { "CCA", 'P' },            { "CCG", 'P' },
            { "CAU", 'H' },            { "CAC", 'H' },
            { "CAA", 'Q' },            { "CAG", 'Q' },
            { "CGU", 'R' },            { "CGC", 'R' },            { "CGA", 'R' },            { "CGG", 'R' },            { "AGA", 'R' },            { "AGG", 'R' },
            { "AUU", 'I' },            { "AUC", 'I' },            { "AUA", 'I' },
            { "AUG", 'M' },
            { "ACU", 'T' },            { "ACC", 'T' },            { "ACA", 'T' },            { "ACG", 'T' },
            { "AAU", 'N' },            { "AAC", 'N' },
            { "AAA", 'K' },            { "AAG", 'K' },
            { "GUU", 'V' },            { "GUC", 'V' },            { "GUA", 'V' },            { "GUG", 'V' },
            { "GCU", 'A' },            { "GCC", 'A' },            { "GCA", 'A' },            { "GCG", 'A' },
            { "GAU", 'D' },            { "GAC", 'D' },
            { "GAA", 'E' },            { "GAG", 'E' },
            { "GGU", 'G' },            { "GGC", 'G' },            { "GGA", 'G' },            { "GGG", 'G' },
            { "UGA", ' ' },            { "UAG", ' ' },            { "UAA", ' ' },
        };
        

        public static void Main(string[] args){ //Our main method.
            var DNA = "GGCCTCTAAAGCTAACCTGTAGCTAACGCACTTGGAGACGCATGAGGTGAGCTATCATAATGTGTGATGAGCACGCGCCATGAATACGTAAGCCGACTATGCAAGAGCGTCCCGATGGTCACTTGTTCTTTACATTGCCCGCTGCAGAGGGTGAAGCCTACGCGCTCATTACTACCGTAGGTTGCCCAGTTTGTACTCTCCCCCAGAGAAGTGCAGTTTGGACCAGATCCGTGTTAAGATAGAAGGGGGAAAAATCGTAGTACATTTTCCCCGGAGCAACAGTTCCGTCCTTTAAGTACCGTCATCACCAGAAGACCTGAAACTGATATGTCCGGGTGTTGATAGACTCCGAGGCGTGATGTACTTTCCCATATGAGATTGCCTTGATATCTCACGCAACAGCAGGGTGGTAAGAGGTTTAATTCAAATCTTCGGGGCGAGCGTTCGGAAATGAAAATAGACTAGTACAATGTAGCAAAGAGTAAAAGGTGCTTAGCTAAGCACCTTTTACTCTTTGCTACATCCCGAGTACACCGGAGGCTTTCCTCGAAGGCTAACTCAGATATGTTACACAATAAACTCACGCATGTATAGTGGCCCCGCTATCCACCCCGGCGCGCTACCGTCTTTAACAGTCGGGGTGTTAATAAGTGTGAAACAGCGTTACCAATGTATTGGTTGCTTATAGCTGAAATGAGGATATAACTATCACCCCTTGAAGTAAAGGGATCAAATCGTTTCAGAACACCCGTCTGACCTTATCTATATTTCCCGGCTGGTAATCACTCTGGTCTCACAGAACGTCGTTGGGGGAAAGGGAACTCCCATAAGTTTGCCGAGGGGAATAGCGCTCTCAGTTGCCTCCAGCTTTATCTTCCATACCTCTTATTATTGAGGACATAACTAGGGTACCACCCTTGTATCGGCGCTTGGATACACAATGGTCCCCAACTGGAGGAGACGCTGGCTAGCTCAGGCTAGGTCCGCTGTGC";

            //Setup our RNA with its three forward and three reverse reading frames 
            var RNAFRAME1 = DNAtoRNA53(DNA); 
            var RNAFRAME2 = RNAFRAME1.Substring(1);
            var RNAFRAME3 = RNAFRAME2.Substring(1);

            var RNAReverse1 = DNAtoRNA35(reverseDNA(DNA)); //Calls reverse, to reverse dna, then we transcribe from the other direction as dna is reversed
            var RNAReverse2 = RNAReverse1.Substring(1);
            var RNAReverse3 = RNAReverse2.Substring(1);

            //Turn each into protein sequence

            var aaRNF1 = mRNAToAA(RNAFRAME1); //turns the mRNA to amino acids
            var aaRNF2 = mRNAToAA(RNAFRAME2);
            var aaRNF3 = mRNAToAA(RNAFRAME3);

            var aaRNR1 = mRNAToAA(RNAReverse1); //get reverse of it
            var aaRNR2 = mRNAToAA(RNAReverse2);
            var aaRNR3 = mRNAToAA(RNAReverse3);

            /*
            Console.WriteLine("RNAF1 " + RNAFRAME1);
            Console.WriteLine("RNAF2 " + RNAFRAME2);
            Console.WriteLine("RNAF3 " + RNAFRAME3);

            Console.WriteLine("");

            Console.WriteLine("RNAR1 " + RNAReverse1);
            Console.WriteLine("RNAR2 " + RNAReverse2);
            Console.WriteLine("RNAR3 " + RNAReverse3);

            Console.WriteLine("");

            Console.WriteLine(aaRNF1);
            Console.WriteLine(aaRNF2);
            Console.WriteLine(aaRNF3);
            Console.WriteLine("");

            Console.WriteLine(aaRNR1);
            Console.WriteLine(aaRNR2);
            Console.WriteLine(aaRNR3);
            Console.WriteLine("");
            Console.WriteLine("");

            */
            methFinder(aaRNF1);            methFinder(aaRNF2);            methFinder(aaRNF3); // finds and adds to the list the proteins from each of our six cases
            methFinder(aaRNR1);            methFinder(aaRNR2);            methFinder(aaRNR3);
            

            var uniqueItems = new HashSet<string>(allViableSequences);  //creating a hash was the fastest way of making our list unique so we don't have doubles
            foreach (string prot in uniqueItems)
                Console.WriteLine(prot);

            //keep console open in debug
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}

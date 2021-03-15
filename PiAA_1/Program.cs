using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiAA_1
{
    class Program
    {
        static string sumBigNum(string num1, string num2)
        {
            string numStr1 = num1;
            string numStr2 = num2;

            int cnt1 = numStr1.Length;
            int cnt2 = numStr2.Length;

            int max = cnt1 > cnt2 ? cnt1 : cnt2;
            int[] numsA1 = new int[max];
            int[] numsA2 = new int[max];

            for (int i = 0; i < max; i++)
            {
                numsA1[i] = 0;
                numsA2[i] = 0;
            }

            int j = cnt1 - 1;
            for (int i = 0; i < max; i++,j--)
            {
                if (j < 0)
                    break;
                numsA1[i] = int.Parse(numStr1[j].ToString());
            }

            j = cnt2 - 1;
            for (int i = 0; i < max; i++, j--)
            {
                if (j < 0)
                    break;
                numsA2[i] = int.Parse(numStr2[j].ToString());
            }

            

            
            int overflow = 0;
            int sum = 0;
            
            string strOut = "";
            for (int i = 0; i < max; i++)
            {
                sum = numsA1[i] + numsA2[i] + overflow;
                overflow = sum / 10;
                strOut += sum % 10;
            }

            if (overflow != 0)
            {
                strOut += sum / 10;
            }
            char[] inputarray = strOut.ToCharArray();
            Array.Reverse(inputarray);
            strOut = new string(inputarray);
            return strOut;
        }
        static string[] readNums(StreamReader reader, int cnt, FileStream inFileStream)
        {
            string line;
            string num1;
            string num2;
            int index;
            int i = 0;
            
            while ((line = reader.ReadLine()) != null)
            {
                index = line.IndexOf(" ");
                if (index == -1)
                {
                    cnt--;
                }
               

            }
            inFileStream.Position = 0;
            string[] nums = new string[cnt*2];
            while ((line = reader.ReadLine()) != null)
            {
                index = line.IndexOf(" ");

                if (index == -1)
                {
                    num1 = line.Substring(0);
                    num2 = "0";
                    continue;
                }
                else
                {
                    num1 = line.Substring(0, index);
                    num2 = line.Substring(index + 1);
                }

                nums[i] = num1;
                i++;
                nums[i] = num2;
                i++;

            }

            return nums;
        }
        static void Main(string[] args)
        {
            string inFile = args[0];
            string outFile = args[1];

            FileStream inFileStream = new FileStream("./"+inFile,FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(inFileStream);

            FileStream outFileStream = new FileStream("./"+outFile,FileMode.Open, FileAccess.Write);
            StreamWriter writer = new StreamWriter(outFileStream);
            outFileStream.SetLength(0);
            int count = System.IO.File.ReadAllLines("./" + inFile).Length;
            string[] outNums = new string[count];
            string[] nums = readNums(reader, count, inFileStream);
            
            //малые числа
            if (inFile == "1.in")
            {
                for (int i = 0; i < nums.Length - 1; i+=2)
                {
                    writer.Write(int.Parse(nums[i]) + int.Parse(nums[i+1])+"\n");
                }
            }
            //длинные числа
            if (inFile == "2.in")
            {
                
                for (int i = 0; i < nums.Length - 1; i += 2)
                {
                    string outNum = sumBigNum(nums[i], nums[i + 1]);
                    writer.Write(outNum+"\n");
                }
            }
            writer.Close();
            reader.Close();
            
        }
    }
}

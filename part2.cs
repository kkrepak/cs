using System;
using System.IO;
using System.Collections;
using System.Text;

namespace lab2
{
	class Program
	{
		static public char[] alphabet = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '+', '/'};

		static int LettersInText(string path)
		{
			int res = 0;
			using (StreamReader sr = new StreamReader(path))
			{
				while (sr.Read() != -1) res++;
			}
			return res;
		}

		static int[] SomeLettersInText(string path)
		{
			int[] result = new int[alphabet.Length];
			using (StreamReader sr = new StreamReader(path))
			{
				string text = sr.ReadToEnd();
				for (int i = 0; i < alphabet.Length; i++)
				{
					int startIndex = -1;
					int hitCount = 0;
					while (true)
					{
						startIndex = text.IndexOf(alphabet[i], startIndex + 1, text.Length - startIndex - 1);
						if (startIndex < 0)
							break;
						hitCount++;
					}
					result[i] = hitCount;
				}
			}
			return result;
		}

		static double[] Frequency(int[] countEachLetter, int count)
		{
			double[] frequency = new double[alphabet.Length];
			for (int i = 0; i < alphabet.Length; i++)
			{
				frequency[i] = (double)countEachLetter[i] / (double)count;
			}
			return frequency;
		}

		static double Entropia(double[] frequency, int count)
		{
			double result = 0, temp;
			for (int i = 0; i < alphabet.Length; i++)
			{
				temp = frequency[i] * Math.Log(frequency[i], 2);
				if (temp < 0)
				{
					result += temp;
				}
			}
			return -result;
		}

		static double Data(double entropia, int count)
		{
			return entropia * count;
		}

		static void Check(string path)
		{
			int count = LettersInText(path + ".txt");
			int[] countEachLetter = SomeLettersInText(path + ".txt");
			double[] frequency = Frequency(countEachLetter, count);
			double entropia = Entropia(frequency, count);
			double data = Data(entropia, count);
			Console.WriteLine("Кількість інформації -   " + data);
		}
		static char ConvertToBase64 (string str)
		{
			switch (str)
			{
				case "000000": return 'A';
				case "000001": return 'B';
				case "000010": return 'C';
				case "000011": return 'D';
				case "000100": return 'E';
				case "000101": return 'F';
				case "000110": return 'G';
				case "000111": return 'H';
				case "001000": return 'I';
				case "001001": return 'J';
				case "001010": return 'K';
				case "001011": return 'L';
				case "001100": return 'M';
				case "001101": return 'N';
				case "001110": return 'O';
				case "001111": return 'P';
				case "010000": return 'Q';
				case "010001": return 'R';
				case "010010": return 'S';
				case "010011": return 'T';
				case "010100": return 'U';
				case "010101": return 'V';
				case "010110": return 'W';
				case "010111": return 'X';
				case "011000": return 'Y';
				case "011001": return 'Z';
				case "011010": return 'a';
				case "011011": return 'b';
				case "011100": return 'c';
				case "011101": return 'd';
				case "011110": return 'e';
				case "011111": return 'f';
				case "100000": return 'g';
				case "100001": return 'h';
				case "100010": return 'i';
				case "100011": return 'j';
				case "100100": return 'k';
				case "100101": return 'l';
				case "100110": return 'n';
				case "100111": return 'm';
				case "101000": return 'o';
				case "101001": return 'p';
				case "101010": return 'q';
				case "101011": return 'r';
				case "101100": return 's';
				case "101101": return 't';
				case "101110": return 'u';
				case "101111": return 'v';
				case "110000": return 'w';
				case "110001": return 'x';
				case "110010": return 'y';
				case "110011": return 'z';
				case "110100": return '0';
				case "110101": return '1';
				case "110110": return '2';
				case "110111": return '3';
				case "111000": return '4';
				case "111001": return '5';
				case "111010": return '6';
				case "111011": return '7';
				case "111100": return '8';
				case "111101": return '9';
				case "111110": return '+';
				case "111111": return '/';
				default: return '=';
			}
		}

		public static BitArray Read(string filename)
		{
			byte[] data;
			using (StreamReader sr = new StreamReader(filename, Encoding.UTF8))
			{
				string temp = sr.ReadToEnd();
				data = new byte[temp.Length];
				data = System.Text.Encoding.UTF8.GetBytes(temp);
			}
			BitArray bitmap = new BitArray(data.Length * 8);
			int i = 0;
			for(int mybyte = 0; mybyte < data.Length; mybyte++)
			{
				if(data[mybyte] >= 128)
				{
					bitmap[i] = true;
					data[mybyte] -= 128;
				}
				else
				{
					bitmap[i] = false;
				}

				i++; 

				if (data[mybyte] >= 64)
				{
					bitmap[i] = true;
					data[mybyte] -= 64;
				}
				else
				{
					bitmap[i] = false;
				}

				i++;

				if (data[mybyte] >= 32)
				{
					bitmap[i] = true;
					data[mybyte] -= 32;
				}
				else
				{
					bitmap[i] = false;
				}

				i++;

				if (data[mybyte] >= 16)
				{
					bitmap[i] = true;
					data[mybyte] -= 16;
				}
				else
				{
					bitmap[i] = false;
				}

				i++;

				if (data[mybyte] >= 8)
				{
					bitmap[i] = true;
					data[mybyte] -= 8;
				}
				else
				{
					bitmap[i] = false;
				}

				i++;

				if (data[mybyte] >= 4)
				{
					bitmap[i] = true;
					data[mybyte] -= 4;
				}
				else
				{
					bitmap[i] = false;
				}

				i++;

				if (data[mybyte] >= 2)
				{
					bitmap[i] = true;
					data[mybyte] -= 2;
				}
				else
				{
					bitmap[i] = false;
				}

				i++;

				if (data[mybyte] == 1)
				{
					bitmap[i] = true;
				}
				else
				{
					bitmap[i] = false;
				}
				i++;
			}
			return bitmap;
		}

		public static string Convert(BitArray barr)
		{
			string result = "", fin = "";
			for (int i = 0; i < barr.Length; i += 6)
			{
				string temp = "";
				try
				{
					for (int j = 0; j <6; j++)
					{
						temp += (barr[i + j] ? "1" : "0");
					}
					result += ConvertToBase64(temp);
				}
				catch
				{
					if (barr.Length % 6 == 2)
					{
						temp += "0000";
						fin += "==";
					}
					else if (barr.Length % 6 == 4)
					{
						temp += "00";
						fin += "=";
					}

					result += ConvertToBase64(temp);
				}
			}
			return result + fin;
		}

		static void Main(string[] args)
		{
			BitArray barr = Read("test1.txt");
			using (StreamWriter sw = new StreamWriter("convertedtext1.txt"))
			{
				sw.Write(Convert(barr));
			}
			Check("convertedtext1");

			Console.WriteLine("file1 base64 bz2 - " + new FileInfo("convertedtext1.txt.bz2").Length);


			barr = Read("test2.txt");
			using (StreamWriter sw = new StreamWriter("convertedtext2.txt"))
			{
				sw.Write(Convert(barr));
			}
			Check("convertedtext2");

			Console.WriteLine("file2 base64 bz2 - " + new FileInfo("convertedtext2.txt.bz2").Length);


			barr = Read("test3.txt");
			using (StreamWriter sw = new StreamWriter("convertedtext3.txt"))
			{
				sw.Write(Convert(barr));
			}
			Check("convertedtext3");

			Console.WriteLine("file3 base64 bz2 - " + new FileInfo("convertedtext3.txt.bz2").Length);



			Console.ReadKey();
		}
	}
}

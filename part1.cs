using System;
using System.IO;
using System.Text;

namespace lab1
{
		class Program
		{
		static public char[] alphabet = new char[] { 'А', 'Б', 'В', 'Г', 'Ґ', 'Д', 'Е', 'Є', 'Ж', 'З', 'И', 'І', 'Ї', 'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ю', 'Я', 'а', 'б', 'в', 'г', 'ґ', 'д', 'е', 'є', 'ж', 'з', 'и', 'і', 'ї', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ю', 'ь', 'я', '.', ',', ' ', '\n', '«', '»', '!', '(', ')', '[', ']', '?', '\'' };

		static int LettersInText(string path)
		{
			int res = 0;
			using (StreamReader sr = new StreamReader(path))
			{
				while (sr.Read() != -1) res++;
			}
			//Console.WriteLine("number of letters " + res);
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
					//Console.WriteLine("for letter " + alphabet[i] + " -- " + hitCount);
				}
			}
			return result;
		}

		static double[] Frequency(int[] countEachLetter, int count)
		{
			double[] frequency = new double[alphabet.Length];
			for(int i = 0; i < alphabet.Length; i++)
			{
				frequency[i] = (double)countEachLetter[i] / (double)count;
			}
			return frequency;
		}

		static double Entropia(double[] frequency, int count)
		{
			double result = 0, temp;
			//Console.WriteLine("freq");
			for (int i = 0; i < alphabet.Length; i++)
			{
				temp = frequency[i] * Math.Log(frequency[i], 2);
				if ( temp < 0)
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
			//загальна кількість символів у тексті
			int count = LettersInText(path + ".txt");
			Console.WriteLine("Загальна кількість символів у тексті -   " + count);
			Console.WriteLine("\n\n\n");

			//кількість кожного символу у тексті
			int[] countEachLetter = SomeLettersInText(path + ".txt");

			//частота кожного символу у тексті
			double[] frequency = Frequency(countEachLetter, count);
			/*for (int i = 0; i < alphabet.Length; i++)
			{
				Console.WriteLine("Частота символу \'" + alphabet[i] + "\'  -  " + frequency[i]);
			}*/

			//загальна ентропія тексту
			double entropia = Entropia(frequency, count);
			Console.WriteLine("Загальна ентропія тексту -   " + entropia);

			//кількість інформації
			double data = Data(entropia, count);
			Console.WriteLine("Кількість інформації -   " + data);

			//порівняння розмірів файлу і кількості інформації
			FileInfo fi = new FileInfo(path + ".txt");
			Console.WriteLine("Кількість інформації: " + (data/8.0) + "\nРозмір файла: " + (fi.Length) + "\nВідношення: " + (fi.Length * 8.0 / data));
			Console.WriteLine("7z - {0} ----- {1}", new FileInfo(path + ".7z").Length, data);
			Console.WriteLine("tar - {0} ----- {1}", new FileInfo(path + ".tar").Length, data);
			Console.WriteLine("bz2 - {0} ----- {1}", new FileInfo(path + ".txt.bz2").Length, data);
			Console.WriteLine("gz - {0} ----- {1}", new FileInfo(path + ".txt.gz").Length, data);
			Console.WriteLine("xz - {0} ----- {1}\n\n", new FileInfo(path + ".txt.xz").Length, data);
		}

		static void Main(string[] args)
		{
			Check("test1");
			Check("test2");
			Check("test3");
			Console.ReadKey();
		}
	}
}

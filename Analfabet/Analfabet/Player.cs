using System;
using System.Linq;

namespace Tabupet
{
	public class Player
	{
		private string name;
		private int points;
		private int temppoints;
		private string letters;
		private string templetters;
		private ConfigClass configs;
		private Random rand = new Random(DateTime.Now.Millisecond);

		public string GetName()
		{
			return name;
		}
		public string GetLetters()
		{
			return letters;
		}

		public Player(string _name)
		{
			letters = "";
			configs = new ConfigClass ();
			name = _name;
			RestoreLetters ();
			points = 0;
		}

		public Player(string _name, int _points, string _letters)
		{
			configs = new ConfigClass ();
			letters = _letters;
			name = _name;
			points = _points;
		}

		public void RestoreLetters()
		{
			int numchars = letters.Length;
			for (int i = 0; i < 7 - numchars; ++i)
			{
				letters += configs.allbricks [rand.Next (0, configs.allbricks.Length)];
			}

		}

		public bool RemoveWord(string word, bool clear)
		{
			for (int i = 0; i < word.Length; ++i)
			{
				if (letters.IndexOf (word [i]) == -1)
				{
					return false;
				}
				int newpoints = 0;
				letters = letters.Remove (letters.IndexOf(word[i]),1);
				configs.alfabet.TryGetValue (word [i], out newpoints);
				if (!clear)
				{
					points += newpoints;
				}
			}
			return true;
		}

		public void BackupLetters()
		{
			templetters = (string)letters.Clone ();
			temppoints = points;
		}

		public void RestoreBackup()
		{
			letters = (string)templetters.Clone ();
			points = temppoints;
		}
		public int GetPoints()
		{
			return points;
		}
	}
}


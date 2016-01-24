using System;

using System.Collections.Generic;

namespace Tabupet
{
	public class ConfigClass
	{
		public Dictionary<char,int> alfabet;

		public char[] allbricks;

		public ConfigClass ()
		{
			alfabet = new Dictionary<char, int>();
			alfabet.Add ('a', 1);alfabet.Add ('b', 4);alfabet.Add ('c', 8);
			alfabet.Add ('d', 1);alfabet.Add ('e', 1);alfabet.Add ('f', 3);
			alfabet.Add ('g', 2);alfabet.Add ('h', 2);alfabet.Add ('i', 1);
			alfabet.Add ('j', 7);alfabet.Add ('k', 2);alfabet.Add ('l', 1);
			alfabet.Add ('m', 2);alfabet.Add ('n', 1);alfabet.Add ('o', 2);
			alfabet.Add ('p', 4);alfabet.Add ('q', 100);alfabet.Add ('r', 1);
			alfabet.Add ('s', 1);alfabet.Add ('t', 1);alfabet.Add ('u', 4);
			alfabet.Add ('v', 3);alfabet.Add ('w', 100);alfabet.Add ('x', 8);
			alfabet.Add ('y', 7);alfabet.Add ('z', 10);alfabet.Add ('å', 4);
			alfabet.Add ('ä', 3);alfabet.Add ('ö', 4);

			allbricks = new char[]{'a','a','a','a','a','a','a','a','b','b',
				'c','d','d','d','d','d','e','e','e','e',
				'e','e','e','f','f','g','g','g','h','h',
				'i','i','i','i','i','j','k','k','k','l',
				'l','l','l','l','m','m','m','n','n','n',
				'n','n','n','o','o','o','o','o','p','p',
				'r','r','r','r','r','r','r','r','s','s',
				's','s','s','s','s','s','t','t','t','t',
				't','t','t','t','u','u','u','v','v','x',
				'y','z','ä','ä','å','å','ö','ö'};
		}
	}
}


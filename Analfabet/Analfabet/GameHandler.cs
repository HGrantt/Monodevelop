using System;
using System.Collections.Generic;
using Tabupet;

namespace Analfabet
{
	public class GameHandler
	{
		private UIt ui;
		private Controller ctrl;
		private int plIndex = 0;
		private bool gamePlaying = true;
		private List<AbstractPlayer> playerList = new List<AbstractPlayer> ();

		public GameHandler (UIt uit, Controller ctrll)
		{
			ctrl = ctrll;
			ui = uit;
			playerList.Add (new HumanPlayer ());
			playerList.Add (new AiPlayer ());
		}

		public void Update()
		{
			if (gamePlaying)
			{
				ctrl.applyConfig ();
				int results = playerList [plIndex].Play (ui);
				if (results == 1)
				{
					if (ui.GetController().GameEnd())
					{
						gamePlaying = false;	
						System.IO.File.Delete ("Savestate.xml");
					} 
					else
					{
						plIndex = (plIndex + 1) % playerList.Count;
						State state = new State ();
						state.getState (ctrl);
						state.saveToXML ();
					}
				}
			}
		}
	}
}


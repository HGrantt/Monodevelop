using System;
using Microsoft.Xna.Framework.Input;

namespace Analfabet
{
	public class HumanPlayer : AbstractPlayer
	{
		MouseState oldState;
		bool pressing = false;


		public override int Play(UIt ui)
		{
			MouseState newState = Mouse.GetState();

			if (newState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
			{
				pressing = true;
			}

			if (newState.LeftButton == ButtonState.Released && oldState.LeftButton == ButtonState.Pressed && pressing)
			{
				pressing = false;
				int mx = newState.Position.X;
				int my = newState.Position.Y;
				return ui.ClickScreen (mx, my);
			}

			oldState = newState; // this reassigns the old state so that it is ready for next time
			return 0;
		}
	}
}


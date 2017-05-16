using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
namespace BridelleBelleMobileApplication
{
    class PinchGestureHelper : ContentView
    {
		double currentScale = 1;
		double startScale = 1;
		double xOffset = 0;
		double yOffset = 0;

		public PinchGestureHelper()
		{
			var pinchGesture = new PinchGestureRecognizer();
			pinchGesture.PinchUpdated += OnPinchUpdated;
			GestureRecognizers.Add(pinchGesture);
		}

		void OnPinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
		{
			if (e.Status == GestureStatus.Started)
			{
				startScale = Content.Scale;
				Content.AnchorX = 0;
				Content.AnchorY = 0;
			}

			if (e.Status == GestureStatus.Running)
			{
				currentScale += (e.Scale - 1) * startScale;
				currentScale = Math.Max(1, currentScale);

				var renderedX = Content.X + xOffset;
				var deltaX = renderedX / Width;
				var deltaWidth = Width / (Content.Width * startScale);
				var originX = (e.ScaleOrigin.X - deltaX) * deltaWidth;

				var renderedY = Content.Y + yOffset;
				var deltaY = renderedY / Height;
				var deltaHeight = Height / (Content.Height * startScale);
				var originY = (e.ScaleOrigin.Y - deltaY) * deltaHeight;

				var targetX = xOffset - (originX * Content.Width) * (currentScale - startScale);
				var targetY = yOffset - (originY * Content.Height) * (currentScale - startScale);

				Content.TranslationX = targetX.Clamp(-Content.Width * (currentScale - 1), 0);
				Content.TranslationY = targetY.Clamp(-Content.Height * (currentScale - 1), 0);
				Content.Scale = currentScale;
			}

			if (e.Status == GestureStatus.Completed)
			{
				xOffset = Content.TranslationX;
				yOffset = Content.TranslationY;
			}
		}
	}
}

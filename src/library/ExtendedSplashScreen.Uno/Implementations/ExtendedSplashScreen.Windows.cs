﻿#if WINDOWS_UWP
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Extensions.Logging;
using Uno.Extensions;
using Uno.Logging;
using Windows.ApplicationModel.Activation;
using Windows.Graphics.Display;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Nventive.ExtendedSplashScreen
{
	public partial class ExtendedSplashScreen
	{
		public FrameworkElement GetNativeSplashScreen(SplashScreen splashScreen)
		{
			var splashScreenImage = new Image();
			var splashScreenBackground = new SolidColorBrush();
			var scaleFactor = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;

			var splashScreenElement = new Canvas
			{
				Background = splashScreenBackground,
				Children = { splashScreenImage },
			};

			try
			{
				var doc = XDocument.Load("AppxManifest.xml", LoadOptions.None);
				var xnamespace = XNamespace.Get("http://schemas.microsoft.com/appx/manifest/uap/windows10");

				var visualElementsNode = doc.Descendants(xnamespace + "VisualElements").First();
				var splashScreenNode = visualElementsNode.Descendants(xnamespace + "SplashScreen").First();
				var splashScreenImagePath = splashScreenNode.Attribute("Image").Value;
				var splashScreenBackgroundColor = splashScreenNode.Attribute("BackgroundColor").Value;

				splashScreenImage.Source = new BitmapImage(new Uri("ms-appx:///" + splashScreenImagePath));
				splashScreenBackground.Color = (Color)XamlBindingHelper.ConvertValue(typeof(Color), splashScreenBackgroundColor);
			}
			catch (Exception e)
			{
				typeof(ExtendedSplashScreen).Log().LogError(0, e, "Error while getting native splash screen.");
			}

			void PositionImage()
			{
				splashScreenImage.SetValue(Canvas.LeftProperty, splashScreen.ImageLocation.Left);
				splashScreenImage.SetValue(Canvas.TopProperty, splashScreen.ImageLocation.Top);
				if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
				{
					splashScreenImage.Height = splashScreen.ImageLocation.Height / scaleFactor;
					splashScreenImage.Width = splashScreen.ImageLocation.Width / scaleFactor;
				}
				else
				{
					splashScreenImage.Height = splashScreen.ImageLocation.Height;
					splashScreenImage.Width = splashScreen.ImageLocation.Width;
				}
			}

			Window.Current.SizeChanged += (s, e) => PositionImage();

			PositionImage();

			return splashScreenElement;
		}
	}
}
#endif

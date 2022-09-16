#if __IOS__
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Foundation;
using Microsoft.Extensions.Logging;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Uno.Extensions;
using Uno.Logging;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Graphics.Display;


namespace Nventive.ExtendedSplashScreen.WinUI
{
	public partial class ExtendedSplashScreen
	{
		private FrameworkElement GetNativeSplashScreen(SplashScreen splashScreen)
		{
			try
			{
				var infoPlistPath = NSBundle.MainBundle.PathForResource("Info", "plist");
				var infoPlistDictionary = new NSDictionary(infoPlistPath);
				var storyboardName = infoPlistDictionary["UILaunchStoryboardName"].ToString();
				var storyboard = UIKit.UIStoryboard.FromName(storyboardName, null);
				var launchScreenViewController = storyboard.InstantiateInitialViewController();
				var launchScreenView = launchScreenViewController.View;

				// We use a Border to ensure proper layout
				var element = new Border
				{
					Child = VisualTreeHelper.AdaptNative(launchScreenView),

					// We set a background to prevent touches from going through
					Background = SolidColorBrushHelper.Transparent
				};

				return element;
			}
			catch (Exception e)
			{
				typeof(ExtendedSplashScreen).Log().LogError(0, e, "Error while getting native splash screen.");

				return null;
			}
		}
	}
}
#endif

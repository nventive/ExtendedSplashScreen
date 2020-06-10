﻿#if __ANDROID__
using Android.App;
using Microsoft.Extensions.Logging;
using System;
using Uno.Extensions;
using Uno.Logging;
using Uno.UI;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Nventive.ExtendedSplashScreen
{
	public partial class ExtendedSplashScreen
	{
		private FrameworkElement GetNativeSplashScreen(SplashScreen splashScreen)
		{
			try
			{
				var activity = ContextHelper.Current as Activity;

				// Get the theme's windowBackground (which we use as splash screen)
				var attribute = new Android.Util.TypedValue();
				activity?.Theme.ResolveAttribute(Android.Resource.Attribute.WindowBackground, attribute, true);
				var windowBackgroundResourceId = attribute.ResourceId;

				// Get the splash screen size
				var splashScreenSize = GetSplashScreenSize(activity);
				
				// Create the splash screen surface
				var splashView = new Android.Views.View(activity);
				splashView.SetBackgroundResource(attribute.ResourceId);

				// We use a Canvas to ensure it's clipped but not resized (important when device has soft-keys)
				var element = new Canvas
				{
					// We set a background to prevent touches from going through
					Background = SolidColorBrushHelper.Transparent,
					// We use a Border to ensure proper layout
					Children =
					{
						new Border()
						{
							Width = splashScreenSize.Width,
							Height = splashScreenSize.Height,
							Child = splashView,
						}
					},
				};

				return element;
			}
			catch (Exception e)
			{
				typeof(ExtendedSplashScreen).Log().LogError(0, e, "Error while getting native splash screen.");

				return null;
			}
		}

		private static Size GetSplashScreenSize(Activity activity)
		{
			var physicalDisplaySize = new Android.Graphics.Point();
			if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop)
			{
				// The windowBackground takes the size of the screen (only when using Theme.AppCompat.*)
				activity.WindowManager.DefaultDisplay.GetRealSize(physicalDisplaySize);
			}
			else
			{
				// The windowBackground takes the size of the screen minus the bottom navigation bar
				activity.WindowManager.DefaultDisplay.GetSize(physicalDisplaySize);
			}

			return new Size(
				ViewHelper.PhysicalToLogicalPixels(physicalDisplaySize.X),
				ViewHelper.PhysicalToLogicalPixels(physicalDisplaySize.Y)
			);
		}
	}
}
#endif

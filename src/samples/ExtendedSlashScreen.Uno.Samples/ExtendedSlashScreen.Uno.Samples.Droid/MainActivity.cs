﻿using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content.PM;
using Android.Views;
#if WINUI
using Uno.WINUI;
#else
using Uno.UI;
#endif

namespace ExtendedSlashScreen.Uno.Samples.Droid
{
	[Activity(
			MainLauncher = true,
			ConfigurationChanges = ActivityHelper.AllConfigChanges,
			WindowSoftInputMode = SoftInput.AdjustPan | SoftInput.StateHidden
		)]
	public class MainActivity : Windows.UI.Xaml.ApplicationActivity
	{
	}
}


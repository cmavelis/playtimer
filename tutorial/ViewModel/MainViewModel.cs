﻿using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace tutorial.ViewModel;

public partial class MainViewModel : ObservableObject
{
	public MainViewModel ()
	{
		Items = new ObservableCollection<string>();
	}
	[ObservableProperty]
	ObservableCollection<string> items;

	[ObservableProperty]
	string text;

	[RelayCommand]
	void Add()
	{
		// add item
		if (string.IsNullOrWhiteSpace(Text))
			return;
		Items.Add(Text);
		Text = string.Empty;
	}

	[RelayCommand]
	void Delete(string s)
	{
		if (Items.Contains(s))
		{
			Items.Remove(s);
		}
	}
	
}

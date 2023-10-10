using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using System.Timers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace tutorial.ViewModel;

public partial class MainViewModel : ObservableObject
{
	public MainViewModel ()
	{
		Items = new ObservableCollection<string>();
        SetTimer();
        Start();
    }

    readonly Stopwatch stopwatch = new();
    private static System.Timers.Timer aTimer;

    [ObservableProperty]
	ObservableCollection<string> items;

	[ObservableProperty]
	string text;

	[ObservableProperty]
	string elapsed;

    [ObservableProperty]
    bool isRunning;

    private void SetTimer()
    {
        aTimer = new System.Timers.Timer(1000);
        // Hook up the Elapsed event for the timer. 
        aTimer.Elapsed += OnTimedEvent;
        aTimer.AutoReset = true;
    }

    private void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        Elapsed = stopwatch.Elapsed.ToString(@"mm\:ss");
    }

    private void Start()
    {
        stopwatch.Restart();

        IsRunning = true;

        aTimer.Enabled = true;
    }

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


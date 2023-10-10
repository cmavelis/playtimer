using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Timers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace tutorial.ViewModel;

public partial class MainViewModel : ObservableObject
{
    public MainViewModel()
    {
        Items = new ObservableCollection<string>();
        Events = new ObservableCollection<Event>();
        SetTimer();
        Start();
    }

    readonly Stopwatch stopwatch = new();
    private static System.Timers.Timer aTimer;

    [ObservableProperty]
    string text;

    [ObservableProperty]
    TimeSpan elapsed;

    [ObservableProperty]
    ObservableCollection<Event> events;

    [ObservableProperty]
    bool isRunning;

    // timer feels unresponsive because this is sync?
    private void SetTimer()
    {
        aTimer = new System.Timers.Timer(100);
        // Hook up the Elapsed event for the timer. 
        aTimer.Elapsed += OnTimedEvent;
        aTimer.AutoReset = true;
    }

    private void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        Elapsed = stopwatch.Elapsed;
    }

    [RelayCommand]
    void Start()
    {
        Elapsed = TimeSpan.Zero;

        stopwatch.Restart();

        IsRunning = true;

        aTimer.Enabled = true;
    }

    [RelayCommand]
    void Record()
    {
        foreach (string item in Items)
        {
            Events.Add(new Event { Name = item, Time = Elapsed });
        }
    }

    [ObservableProperty]
    ObservableCollection<string> items;

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


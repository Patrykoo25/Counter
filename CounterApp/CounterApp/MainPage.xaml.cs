using System.Collections.Generic;
using System.Text.Json;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace CounterApp
{
    public partial class MainPage : ContentPage
    {
        private List<Counter> counters = new();

        public MainPage()
        {
            InitializeComponent();
            LoadCounters();
        }

        private void LoadCounters()
        {
            string json = Preferences.Get("Counters", "[]");
            counters = JsonSerializer.Deserialize<List<Counter>>(json) ?? new List<Counter>();
            RefreshCounterList();
        }

        private void SaveCounters()
        {
            string json = JsonSerializer.Serialize(counters);
            Preferences.Set("Counters", json);
        }

        private void RefreshCounterList()
        {
            counterListView.ItemsSource = null;
            counterListView.ItemsSource = counters;
        }

        private void OnAddCounterClicked(object sender, EventArgs e)
        {
            counters.Add(new Counter($"Licznik {counters.Count + 1}"));
            RefreshCounterList();
            SaveCounters();
        }

        private void OnIncrementCounterClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.BindingContext is Counter counter)
            {
                counter.Increment();
                RefreshCounterList();
                SaveCounters();
            }
        }

        private void OnDecrementCounterClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.BindingContext is Counter counter)
            {
                counter.Decrement();
                RefreshCounterList();
                SaveCounters();
            }
        }
    }

    public class Counter
    {
        public string Name { get; set; }
        public int Value { get; set; }

        public Counter(string name)
        {
            Name = name;
            Value = 0;
        }

        public void Increment() => Value++;
        public void Decrement() => Value--;
    }
}

using System;
using System.Collections.ObjectModel;
using uwpsamples.Models;

namespace uwpsamples.Controllers {
    public class ListViewController {

        string[] names = { "Charles", "Emma", "Tontine", "Charlene", "Four"};
        int[] ages = { 22, 32, 40, 56, 29 };
        string[] cities = { "Rennes", "Monaco", "Pensylvannie", "Quebec", "Wembley" };

        private static ObservableCollection<Person> _randomPeople { get; set; }
        public static ObservableCollection<Person> RandomPeople {
            get {
                if (_randomPeople == null) {
                    _randomPeople = new ObservableCollection<Person>();
                }
                return _randomPeople;
            }
        }

        public ListViewController() {
            LoadData();
        }

        public void LoadData() {
            if (RandomPeople.Count < 1) {
                PopulateRandomPeople();
            }
        }

        private void PopulateRandomPeople() {
            var rand = new Random();
            for (int i = 0; i < 50; i++) {
                var person = new Person() {
                    Name = names[rand.Next(names.Length)],
                    Age = ages[rand.Next(ages.Length)],
                    City = cities[rand.Next(cities.Length)]
                };
                RandomPeople.Add(person);
            }
        }
    }
}

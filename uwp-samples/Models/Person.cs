using System;

namespace uwp_samples.Models {
    public class Person {
        private string _name;
        private int _age;
        private string _city;

        public string Name {
            get {
                return _name;
            }
            set {
                if (value != _name) {
                    _name = value.ToUpper();
                }
            }
        }

        public int Age {
            get {
                return _age;
            }
            set {
                if (value > 150 || value < 0) {
                    throw new ArgumentOutOfRangeException("Age", "The for the Age property is out of range (either negative or greater than 150");
                }
                if (value != _age) {
                    _age = value;
                }
            }
        }

        public string City {
            get {
                return _city;
            }
            set {
                if (value != _city) {
                    _city = value;
                }
            }
        }
    }
}

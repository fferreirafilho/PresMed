using System;
using System.ComponentModel.DataAnnotations;

namespace PresMed.Models.ValidationModels {
    public class BirthDateValidation : ValidationAttribute {
        public override bool IsValid(object value) {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return false;

            return ValidateBirthDate(value.ToString());
        }

        public bool ValidateBirthDate(string value) {
            DateTime birthDate = DateTime.Parse(value);
            DateTime now = DateTime.Now;
            int days = (int)now.Subtract(birthDate).TotalDays;
            int year = days / 365;
            if (year < 18) {
                return false;
            }
            else {
                return true;
            }

        }

    }
}

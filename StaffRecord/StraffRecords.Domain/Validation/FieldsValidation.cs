namespace StaffRecords.Domain.Validation
{
    public static class FieldsValidation
    {
        public static class Company
        {
            public const int NameMaxLength = 120;
            public const int AdressMaxLength = 120;
        }

        public static class Department
        {
            public const int NameMaxLength = 120;
        }

        public static class Appointment
        {
            public const int NameMaxLength = 120;
        }

        public static class Employee
        {
            public const int FirstNameMaxLength = 30;
            public const int LastNameMaxLength = 30;
            public const int PatronymicMaxLength = 30;
            public const int AddressMaxLength = 120;
            public const int PhoneNumberMaxLength = 13;
            public const decimal MinSalary = 0;
            public const decimal MaxSalary = 200000;
        }

    }
}

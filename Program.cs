using System;
using System.Linq;

namespace EmployeesList
{
    class Program
    {
        static void Main()
        {
            Employee[] empArr = GenerateList();
            Console.WriteLine("Would you like to filter data before printing? Y/N?");
            if (Console.ReadLine().ToLower() == "y")
            {
                FilterMenu(empArr);
            }
            else PrintList(empArr);
        }

        static Employee[] GenerateList()
        {
            Console.WriteLine("Please enter number of employees:");
            string inputNumber = Console.ReadLine();
            while (int.TryParse(inputNumber, out int resNum) != true || int.Parse(inputNumber) <= 0)
            {
                Console.WriteLine("Enter a valid number!");
                inputNumber = Console.ReadLine();
            }

            int number = int.Parse(inputNumber);

            Employee[] employees = new Employee[number];

            for (int i = 0; i < number; i++)
            {
                Console.WriteLine($"Name of an employee \"Number {i + 1}\":");
                string nameInput = Console.ReadLine();
                string name = ReturnValidString(nameInput, "name");

                Console.WriteLine($"Surname of an employee \"Number {i + 1}\":");
                string surnameInput = Console.ReadLine();
                string surname = ReturnValidString(surnameInput, "surname");

                Console.WriteLine($"Date of birth of an employee \"Number {i + 1}\":");
                string inputDob = Console.ReadLine();
                DateTime dob = ReturnValidDob(inputDob);

                Console.WriteLine($"Salary of an employee \"Number {i + 1}\":");
                string inputSalary = Console.ReadLine();
                int salary = ReturnValidSalary(inputSalary);

                employees[i] = new Employee(name, surname, dob, salary);
            }

            return employees;
        }
        static string ReturnValidString(string input, string check)
        {
            while (input.Any(char.IsDigit))
            {
                Console.WriteLine($"Enter a valid {check}!");
                input = Console.ReadLine();
            }
            return input;
        }
        static DateTime ReturnValidDob(string inputDob)
        {
            while (DateTime.TryParse(inputDob, out DateTime resDob) != true)
            {
                Console.WriteLine("Enter a valid date (format: yyyy MM dd or yyyy-MM-dd)!");
                inputDob = Console.ReadLine();
            }
            return DateTime.Parse(inputDob);
        }
        static int ReturnValidSalary(string inputSalary)
        {
            while (int.TryParse(inputSalary, out int resSal) != true || int.Parse(inputSalary) <= 0)
            {
                Console.WriteLine("Enter a valid salary!");
                inputSalary = Console.ReadLine();
            }
            int salary = int.Parse(inputSalary);
            return salary;
        }

        static void PrintList(Employee[] empArr)
        {
            string colTitle1 = "Id";
            string colTitle2 = "Name";
            string colTitle3 = "Surname";
            string colTitle4 = "Date of birth";
            string colTitle5 = "Salary";

            int colWidth1 = colTitle1.Length;
            int colWidth2 = colTitle2.Length;
            int colWidth3 = colTitle3.Length;
            int colWidth4 = colTitle4.Length;
            int colWidth5 = colTitle5.Length;
            for (int i = 0; i < empArr.Length; i++)
            {
                if (colWidth1 < i.ToString().Length)
                {
                    colWidth1 = i.ToString().Length;
                }
                if (colWidth2 < empArr[i].Name.Length)
                {
                    colWidth2 = empArr[i].Name.Length;
                }
                if (colWidth3 < empArr[i].Surname.Length)
                {
                    colWidth3 = empArr[i].Surname.Length;
                }
                if (colWidth4 < empArr[i].Dob.ToString("yyyy-MM-dd").Length)
                {
                    colWidth4 = empArr[i].Dob.ToString("yyyy-MM-dd").Length;
                }
                if (colWidth5 < empArr[i].Salary.ToString().Length)
                {
                    colWidth5 = empArr[i].Salary.ToString().Length;
                }
            }

            int extraLines = 6;
            int totalWidth = colWidth1 + colWidth2 + colWidth3 + colWidth4 + colWidth5 + extraLines;
            Console.WriteLine("┌" + new string('-', colWidth1) + "┬" + new string('-', colWidth2) + "┬" + new string('-', colWidth3) + "┬" + new string('-', colWidth4) + "┬" + new string('-', colWidth5) + "┐");

            string paddedCol1 = colTitle1.PadRight((colTitle1.Length + colWidth1) / 2);
            string paddedCol2 = colTitle2.PadRight((colTitle2.Length + colWidth2) / 2);
            string paddedCol3 = colTitle3.PadRight((colTitle3.Length + colWidth3) / 2);
            string paddedCol4 = colTitle4.PadRight((colTitle4.Length + colWidth4) / 2);
            string paddedCol5 = colTitle5.PadRight((colTitle5.Length + colWidth5) / 2);
            Console.WriteLine(string.Format("|{0," + colWidth1 + "}|{1," + colWidth2 + "}|{2," + colWidth3 + "}|{3," + colWidth4 + "}|{4," + colWidth5 + "}|", paddedCol1, paddedCol2, paddedCol3, paddedCol4, paddedCol5));
            Console.WriteLine("├" + new string('-', colWidth1) + "┼" + new string('-', colWidth2) + "┼" + new string('-', colWidth3) + "┼" + new string('-', colWidth4) + "┼" + new string('-', colWidth5) + "┤");
            for (int j = 0; j < empArr.Length; j++)
            {
                paddedCol1 = j.ToString().PadRight((j.ToString().Length + colWidth1) / 2);
                paddedCol2 = empArr[j].Name.PadRight((empArr[j].Name.Length + colWidth2) / 2);
                paddedCol3 = empArr[j].Surname.PadRight((empArr[j].Surname.Length + colWidth3) / 2);
                paddedCol4 = empArr[j].Dob.ToString("yyyy-MM-dd").PadRight((empArr[j].Dob.ToString("yyyy-MM-dd").Length + colWidth4) / 2);
                paddedCol5 = empArr[j].Salary.ToString().PadRight((empArr[j].Salary.ToString().Length + colWidth5) / 2);
                Console.WriteLine(string.Format("|{0," + colWidth1 + "}|{1," + colWidth2 + "}|{2," + colWidth3 + "}|{3," + colWidth4 + "}|{4," + colWidth5 + "}|", paddedCol1, paddedCol2, paddedCol3, paddedCol4, paddedCol5));
            }
            Console.WriteLine("└" + new string('-', colWidth1) + "┴" + new string('-', colWidth2) + "┴" + new string('-', colWidth3) + "┴" + new string('-', colWidth4) + "┴" + new string('-', colWidth5) + "┘");
        }

        static void FilterMenu(Employee[] empArr)
        {
            Console.WriteLine("Filter by:");
            Console.WriteLine("a) Name");
            Console.WriteLine("b) Surname");
            Console.WriteLine("c) Date of birth");
            Console.WriteLine("d) Salary");
            string inputChoice = Console.ReadLine().ToLower();

            if (inputChoice == "a")
            {
                FilterByName(empArr);
            }
            else if (inputChoice == "b")
            {
                FilterBySurname(empArr);
            }
            else if (inputChoice == "c")
            {
                FilterByDob(empArr);
            }
            else if (inputChoice == "d")
            {
                FilterBySalary(empArr);
            }
            else Console.WriteLine("Enter a valid letter!");

            Console.WriteLine("Would you like to use another filter (Y) or print unfiltered (N)? Y/N?");
            if (Console.ReadLine().ToLower() == "y")
            {
                FilterMenu(empArr);
            }
            else PrintList(empArr);

        }

        static void FilterByName(Employee[] empArr)
        {
            Console.WriteLine("Enter a name to filter");
            string inputFilterName = Console.ReadLine();
            string name = ReturnValidString(inputFilterName, "name");
            int counter = 0;
            for (int i = 0; i < empArr.Length; i++)
            {
                if (empArr[i].Name == name)
                {
                    counter++;
                }
            }
            Employee[] filteredArr = new Employee[counter];
            int lastIndex = 0;
            for (int i = 0; i < empArr.Length; i++)
            {
                if (empArr[i].Name == name)
                {
                    filteredArr[lastIndex] = empArr[i];
                    lastIndex++;
                }
            }
            PrintList(filteredArr);
        }
        static void FilterBySurname(Employee[] empArr)
        {
            Console.WriteLine("Enter a surname to filter");
            string inputFilter = Console.ReadLine();
            string surname = ReturnValidString(inputFilter, "surname");
            int counter = 0;
            for (int i = 0; i < empArr.Length; i++)
            {
                if (empArr[i].Surname == surname)
                {
                    counter++;
                }
            }
            Employee[] filteredArr = new Employee[counter];
            int lastIndex = 0;
            for (int i = 0; i < empArr.Length; i++)
            {
                if (empArr[i].Surname == surname)
                {
                    filteredArr[lastIndex] = empArr[i];
                    lastIndex++;
                }
            }
            PrintList(filteredArr);
        }
        static void FilterByDob(Employee[] empArr)
        {
            Console.WriteLine("Enter a date of birth to filter");
            string inputFilter = Console.ReadLine();
            DateTime dob = ReturnValidDob(inputFilter);
            int counter = 0;
            for (int i = 0; i < empArr.Length; i++)
            {
                if (empArr[i].Dob == dob)
                {
                    counter++;
                }
            }
            Employee[] filteredArr = new Employee[counter];
            int lastIndex = 0;
            for (int i = 0; i < empArr.Length; i++)
            {
                if (empArr[i].Dob == dob)
                {
                    filteredArr[lastIndex] = empArr[i];
                    lastIndex++;
                }
            }
            PrintList(filteredArr);
        }
        static void FilterBySalary(Employee[] empArr)
        {
            Console.WriteLine("Enter a salary to filter");
            string inputFilter = Console.ReadLine();
            int salary = ReturnValidSalary(inputFilter);
            int counter = 0;
            for (int i = 0; i < empArr.Length; i++)
            {
                if (empArr[i].Salary == salary)
                {
                    counter++;
                }
            }
            Employee[] filteredArr = new Employee[counter];
            int lastIndex = 0;
            for (int i = 0; i < empArr.Length; i++)
            {
                if (empArr[i].Salary == salary)
                {
                    filteredArr[lastIndex] = empArr[i];
                    lastIndex++;
                }
            }
            PrintList(filteredArr);
        }
        //static void SortByName(Employee[] empArr)
        //{
        //    Console.WriteLine("Enter a name to sort by");
        //    string inputFilterName = Console.ReadLine();
        //    string name = ReturnValidString(inputFilterName, "name");

        //    int counter = 0;
        //    for (int i = 0; i < empArr.Length; i++)
        //    {
        //        if (empArr[i].Name == name)
        //        {
        //            counter++;
        //        }
        //    }
        //    Employee[] filteredArr = new Employee[counter];
        //    int lastIndex = 0;
        //    for (int i = 0; i < empArr.Length; i++)
        //    {
        //        if (empArr[i].Name == name)
        //        {
        //            filteredArr[lastIndex] = empArr[i];
        //            lastIndex++;
        //        }
        //    }
        //    PrintList(filteredArr);
        //}

    }
    class Employee
    {
        public string Name { get; init; }
        public string Surname { get; init; }
        public DateTime Dob { get; init; }
        public int Salary { get; init; }
        public Employee() { }
        public Employee(string name, string surname, DateTime dob) : this(name, surname, dob, 700) { }
        public Employee(string name, string surname, DateTime dob, int salary)
        {
            Name = name;
            Surname = surname;
            Dob = dob;
            Salary = salary;
        }
    }
}

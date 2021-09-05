using System;
using System.Collections;
using System.Linq;

namespace EmployeesList
{
    class Program
    {
        static void Main()
        {
            Employee[] empArr = GenerateList();
            Console.WriteLine("Would you like to filter data before printing? Y/N?");
            string inputAnswerFilter = Console.ReadLine().ToLower();
            FilterOrNot(inputAnswerFilter, empArr);
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
            DateTime min = DateTime.Now.AddYears(-120);
            DateTime max = DateTime.Now.AddYears(-14);
            DateTime dob = DateTime.Parse(inputDob);
            int tooEarly = DateTime.Compare(min, dob);
            int tooLate = DateTime.Compare(dob, max);
            if (tooEarly > 0 || tooLate > 0)
            {
                Console.WriteLine("Enter a valid date (age should be between 14-120)!");
                inputDob = Console.ReadLine();
                ReturnValidDob(inputDob);
            }
            return dob;
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

        static void FilterOrNot(string input, Employee[] empArr)
        {
            if (input == "y")
            {
                empArr = FilterMenu(empArr);
                Console.WriteLine("Would you like to sort data? Y/N?");
                string inputAnswerSort = Console.ReadLine().ToLower();
                SortOrNot(inputAnswerSort, empArr);
            }
            else if (input == "n")
            {
                Console.WriteLine("Would you like to sort data? Y/N?");
                string inputAnswerSort = Console.ReadLine().ToLower();
                SortOrNot(inputAnswerSort, empArr);
            }
            else
            {
                Console.WriteLine("Please enter Y or N:");
                FilterOrNot(Console.ReadLine().ToLower(), empArr);
            }
        }
        static void SortOrNot(string input, Employee[] empArr)
        {
            if (input == "y")
            {
                empArr = SortMenu(empArr);
                PrintList(empArr);
            }
            else if (input == "n")
            {
                PrintList(empArr);
            }
            else
            {
                Console.WriteLine("Please enter Y or N:");
                SortOrNot(Console.ReadLine().ToLower(), empArr);
            }
        }

        static Employee[] FilterMenu(Employee[] empArr)
        {
            Console.WriteLine("Filter by:");
            Console.WriteLine("a) Name");
            Console.WriteLine("b) Surname");
            Console.WriteLine("c) Date of birth");
            Console.WriteLine("d) Salary");
            string inputChoice = Console.ReadLine().ToLower();

            if (inputChoice == "a")
            {
                Console.WriteLine("Enter a name to filter");
                string inputFilterName = Console.ReadLine();
                string name = ReturnValidString(inputFilterName, "name");
                empArr = FilterByName(name, empArr);
            }
            else if (inputChoice == "b")
            {
                Console.WriteLine("Enter a surname to filter");
                string inputFilterSurame = Console.ReadLine();
                string surname = ReturnValidString(inputFilterSurame, "surname");
                empArr = FilterBySurname(surname, empArr);
            }
            else if (inputChoice == "c")
            {
                Console.WriteLine("Enter a date of birth to filter");
                string inputFilter = Console.ReadLine();
                DateTime dob = ReturnValidDob(inputFilter);
                empArr = FilterByDob(dob, empArr);
            }
            else if (inputChoice == "d")
            {
                Console.WriteLine("Enter a salary to filter");
                string inputFilter = Console.ReadLine();
                int salary = ReturnValidSalary(inputFilter);
                empArr = FilterBySalary(salary, empArr);
            }
            else
            {
                Console.WriteLine("Enter a valid letter!");
                FilterMenu(empArr);
            }

            PrintList(empArr);

            Console.WriteLine("Would you like to use another filter? Y/N?");
            if (Console.ReadLine().ToLower() == "y")
            {
                empArr = FilterMenu(empArr);
                return empArr;
            }
            else return empArr;
        }
        static Employee[] FilterByName(string name, Employee[] empArr)
        {
            int counter = 0;
            for (int i = 0; i < empArr.Length; i++)
            {
                if (empArr[i].Name == name)
                {
                    counter++;
                }
            }
            if(counter > 0)
            {
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
                return filteredArr;
            }
            else
            {
                Console.WriteLine("This name does not exist! Enter an existing name:");
                string inputFilterName = Console.ReadLine();
                name = ReturnValidString(inputFilterName, "name");
                FilterByName(name, empArr);
                return empArr;
            }
        }
        static Employee[] FilterBySurname(string surname, Employee[] empArr)
        {
            int counter = 0;
            for (int i = 0; i < empArr.Length; i++)
            {
                if (empArr[i].Surname == surname)
                {
                    counter++;
                }
            }
            if (counter > 0)
            {
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
                return filteredArr;
            }
            else
            {
                Console.WriteLine("This surname does not exist! Enter an existing surname:");
                string inputFilterName = Console.ReadLine();
                surname = ReturnValidString(inputFilterName, "name");
                FilterBySurname(surname, empArr);
                return empArr;
            }
        }
        static Employee[] FilterByDob(DateTime dob, Employee[] empArr)
        {
            int counter = 0;
            for (int i = 0; i < empArr.Length; i++)
            {
                if (empArr[i].Dob == dob)
                {
                    counter++;
                }
            }
            if (counter > 0)
            {
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
                return filteredArr;
            }
            else
            {
                Console.WriteLine("This date of birth does not exist! Enter an existing date of birth:");
                string inputFilterName = Console.ReadLine();
                dob = ReturnValidDob(inputFilterName);
                FilterByDob(dob, empArr);
                return empArr;
            }
        }
        static Employee[] FilterBySalary(int salary, Employee[] empArr)
        {
            int counter = 0;
            for (int i = 0; i < empArr.Length; i++)
            {
                if (empArr[i].Salary == salary)
                {
                    counter++;
                }
            }
            if (counter > 0)
            {
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
                return filteredArr;
            }
            else
            {
                Console.WriteLine("This salary does not exist! Enter an existing salary:");
                string inputFilterName = Console.ReadLine();
                salary = ReturnValidSalary(inputFilterName);
                FilterBySalary(salary, empArr);
                return empArr;
            }
        }

        static Employee[] SortMenu(Employee[] empArr)
        {
            Console.WriteLine("Sort by:");
            Console.WriteLine("a) Name");
            Console.WriteLine("b) Surname");
            Console.WriteLine("c) Date of birth");
            Console.WriteLine("d) Salary");
            string inputChoice = Console.ReadLine().ToLower();

            if (inputChoice == "a")
            {
                Array.Sort(empArr, new PersonComparerByName());
                return empArr;
            }
            else if (inputChoice == "b")
            {
                Array.Sort(empArr, new PersonComparerBySurname());
                return empArr;
            }
            else if (inputChoice == "c")
            {
                Array.Sort(empArr, new PersonComparerByDob());
                return empArr;
            }
            else if (inputChoice == "d")
            {
                Array.Sort(empArr, new PersonComparerBySalary());
                return empArr;
            }
            else
            {
                Console.WriteLine("Enter a valid letter!");
                SortMenu(empArr);
                return empArr;
            }
        }
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
    class PersonComparerByName : IComparer
    {
        public int Compare(object x, object y)
        {
            return (new CaseInsensitiveComparer()).Compare(((Employee)x).Name, ((Employee)y).Name);
        }
    }
    class PersonComparerBySurname : IComparer
    {
        public int Compare(object x, object y)
        {
            return (new CaseInsensitiveComparer()).Compare(((Employee)x).Surname, ((Employee)y).Surname);
        }
    }
    class PersonComparerByDob : IComparer
    {
        public int Compare(object x, object y)
        {
            return (new CaseInsensitiveComparer()).Compare(((Employee)x).Dob, ((Employee)y).Dob);
        }
    }
    class PersonComparerBySalary : IComparer
    {
        public int Compare(object x, object y)
        {
            return (new CaseInsensitiveComparer()).Compare(((Employee)x).Salary, ((Employee)y).Salary);
        }
    }
}

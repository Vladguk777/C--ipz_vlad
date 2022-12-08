using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection.Metadata;
using System.Text;

namespace BankConsole
{
    enum CurrencyExchange
    {
        GrnToDollar,
        GrnToEuro,
        DollarToGrn,
        EuroToGrn,
        EuroToDollar,
        DollarToEuro
    }
    class Program
    {
        #region Methods
        static void Main(string[] args)
        {
            ReportBankSingleton reportBankSingleton1 = ReportBankSingleton.GetInstance();
            CCustomer costumer4 = new CCustomer("Vlad", 25, 12000, 4000, 200);
            CCustomer costumer1 = new CCustomer("Vadim", 25, 12000, 4000, 200);
            CCustomer costumer2 = new CCustomer("Misha", 25, 12000, 4000, 200);
            CCustomer costumer3 = new CCustomer("Kolya", 25, 12000, 4000, 200);
            List<CCustomer> costumers = new List<CCustomer>() { costumer4, costumer1, costumer2, costumer3 };
            CBank.Hello();
            int a = Convert.ToInt32(Console.ReadLine());
            if (a == 1)
            {
                CBank bank = new CBank();
                CComputer computer = new CComputer();
                CVinnik vinik = new CVinnik();
                CClinerMan clinerMan = new CClinerMan("Olya", 18, 2,vinik,reportBankSingleton1);
                int Hp = vinik.Hp;
                CEmployee employee = new CEmployee("Vasia", 35, computer,reportBankSingleton1);
                CConsultant consultant = new CConsultant("Larisa", 20);
                foreach (var item in costumers)
                {
                    Console.WriteLine(item.Name);
                    item.Work(consultant, employee, bank);
                    Console.WriteLine($"Goodbye {item.Name}!");
                }
                
                CBank.ProcentD = clinerMan.Cline(CBank.ProcentD);
                vinik.Hp = Hp;
                reportBankSingleton1.PrintReport();
                
            }
            if (a == 2)
            {
                Console.WriteLine($"Goodbye! ");
            }
        }
        #endregion 
    }
    sealed class ReportBankSingleton
    {
        #region Fields and properties
        static ReportBankSingleton _instance;
        public List<string> Report { get; set; }
       
        #endregion
        #region Constructors
        private ReportBankSingleton()
        {
        }
        public static ReportBankSingleton GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ReportBankSingleton();
                _instance.Report = new List<string>();
            }
            return _instance;
        }
        #endregion
        #region Methods
        public void AddReport(string report)
        {
            Report.Add(report);
        }

        public void PrintReport()
        {
            foreach (var item in Report)
            {
                Console.WriteLine(item);
            }
            
        }
        #endregion 
    }
    class Facade
    {
        #region Fields and properties
        public CEmployee _employee;
        #endregion
        #region Constructors
        public Facade(CEmployee employee)
        {
            this._employee = employee;
        }
        #endregion
        #region Methods
        public void Operation1(CurrencyExchange currency,CCustomer costumer,double Bank,string GetMoney,
                               string ResultMoney)
        {
            _employee.Question2(currency);
            double customerStartMoney = float.Parse(Console.ReadLine());
            double customerResultMoney = _employee.Exchange(currency, customerStartMoney,costumer);
            if (customerResultMoney <= Bank)
            {
                Console.WriteLine("You get " + customerStartMoney + " " + GetMoney + " and received " +
                                  + customerResultMoney + " " + ResultMoney);
            }
            else
            {
                Console.WriteLine("We have not enough money");
            }
        }
        public void Operation2(double[] procent)
        {
            int punct2 = Convert.ToInt32(Console.ReadLine());
            switch (punct2)
            {
                case 0:
                    Console.WriteLine("Goodbye! ");
                    break;
                case 1:
                    Operation2Dod(0.15);
                    break;
                case 2:
                    Operation2Dod(0.17);
                    break;
                case 3:
                    Operation2Dod(0.20);
                    break;
                default:
                    Console.WriteLine("It's imposible!");//це неможливо
                    return;
            }

            void Operation2Dod(double procent)
            {
                Console.WriteLine("Enter the amount of your deposit in hryvnias:");//Вкажіть суму вашого вкладу в гривнях
                int Vklad = Convert.ToInt32(Console.ReadLine());
                if (Vklad >= 5000)
                {
                    double Prubutok = (Vklad * procent) / 2;
                    Console.WriteLine("The profit you will receive at the end of the term of the deposit excluding tax:   " +
                                    +Prubutok + "grn");//Прибуток, який ви отримаєте по закінченню терміну вкладу без вирахування податку: 
                    double PrubutokPod = Prubutok - (Prubutok * 0.19);
                    Console.WriteLine("The profit you will receive at the end of the term of the deposit with tax:   " +
                                    +PrubutokPod + "grn");//Прибуток, який ви отримаєте по закінченню терміну вкладу з податком:
                }
                else
                {
                    Console.WriteLine("You cannot make a deposit");// ви не можете зробити депозит
                }
            }
        }
        #endregion
    }
    abstract class CPerson
    {
        #region Fields and properties
        public string Name { get; set; }
        public int Age { get; set; }
        #endregion
        #region Constructors
        public CPerson(string name, int age)
        {
            Name = name;
            Age = age;
        }
        #endregion
    }
    class CCustomer : CPerson
    {
        #region Fields and properties
        public int Grivnas { get; set; }
        public int Euros { get; set; }
        public int Dollars { get; set; }
        internal CBank CBank
        {
            get => default;
            set
            {
            }
        }
        #endregion
        #region Constructors
        public CCustomer(string name, int age, int grivnas, int euros, int dollars) : base(name, age)
        {
            Grivnas = grivnas;
            Euros = euros;
            Dollars = dollars;
        }
        #endregion
        #region Methods
        public int Clientdirt()
        {
            Random random = new Random();
            int a = random.Next(1, 5);
            return a;
        }
        public void Work(CConsultant consultant, CEmployee employee, CBank bank)
        {
            
            
            bool check = true;
            while (check)
            {
                employee.Dialog(this,consultant);
                Console.WriteLine("1. Continue , 2. To finish");
                if (Int32.Parse(Console.ReadLine()) != 1)
                {
                    bank.GetD(2);
                    check = false;
                }
            }
        }
        #endregion
    }
    class CConsultant : CPerson
    {
        #region Fields and properties
        internal CCustomer CCostumer
        {
            get => default;
            set
            {
            }
        }
        #endregion
        #region Constructors
        public CConsultant(string name, int age) : base(name, age)
        {

        }
        #endregion
        #region Methods
        public void Consult()
        {
            Console.WriteLine("Good afternoon, how can I help?");//Доброго дня,чим можу допомогти?
            Console.WriteLine("0. Unfortunately, you won't be able to help me");//На жаль ви нічим мені не допоможете
            Console.WriteLine("1. Exchange money");//обміняти гроші
            Console.WriteLine("2. Invest money");//Вкласти гроші
            Console.WriteLine("3. Apply for a loan");//Оформити кредит
            Console.WriteLine("4. Pay the bill at the cash desk");//Оплатити рахунок в касі
        }
        
        #endregion
    }
    class CEmployee : CPerson
    {
        #region Fields and properties
        CComputer _computer;
        public List<string> Report { get; set; }
       
        internal CComputer CComputer
        {
            get => default;
            set
            {
            }
        }

        internal CBank CBank
        {
            get => default;
            set
            {
            }
        }

        internal ReportBankSingleton ReportBankSingleton
        {
            get => default;
            set
            {
            }
        }

        public double _grnAmount = 2147483647;//кількість гривень в касі
        public double _dollarAmount = 65535;//кількість доларів в касі
        public double _euroAmount = 1287;//кількість євро в касі
        #endregion
        #region Constructors
        public CEmployee(string name, int age, CComputer computer,ReportBankSingleton report) : base(name, age)
        {
            _computer = computer;
            Report = report.Report;
        }
        #endregion
        #region Methods
        public void Dialog(CCustomer customer,CConsultant consultant)
        {
            Facade facade = new Facade(this);
            consultant.Consult();
            int punct = Convert.ToInt32(Console.ReadLine());
            switch (punct)
            {
                case 0:
                    break;
                #region Case1
                case 1:
                    ExchangeOfMoney();
                    int punct1 = Convert.ToInt32(Console.ReadLine());
                    switch (punct1)
                    {
                        case 0:
                            Console.WriteLine("Goodbye! ");
                            break;
                        case 1:
                            facade.Operation1( CurrencyExchange.GrnToDollar, customer, 65535, "grn", "dollars");
                            break;
                        case 2:
                            facade.Operation1( CurrencyExchange.GrnToEuro, customer, 1287, "grn", "euros");
                            break;
                        case 3:
                            facade.Operation1(CurrencyExchange.DollarToGrn, customer, 2147483647, "dollars", "grn");
                            break;
                        case 4:
                            facade.Operation1( CurrencyExchange.EuroToGrn, customer, 2147483647, "euros", "grn");
                            break;
                        case 5:
                            facade.Operation1( CurrencyExchange.EuroToDollar, customer, 65535, "euros", "dollars");
                            break;
                        case 6:
                            facade.Operation1( CurrencyExchange.DollarToEuro, customer, 1287, "dollars", "euros");
                            break;
                        default:
                            Console.WriteLine("It's imposible!");//це неможливо
                            return;
                    }
                    break;
                #endregion
                #region Case2
                case 2:
                    double[] arr = new double[3] { 0.15, 0.17, 0.20 };

                    ContributionOfMoney();
                    facade.Operation2(arr);
                    break;
                #endregion
                #region Case3
                case 3:
                    Loan();
                    int punct3 = Convert.ToInt32(Console.ReadLine());
                    switch (punct3)
                    {
                        case 0:
                            Console.WriteLine("Goodbye! ");
                            break;
                        case 1:
                            CDialogs.AboutLoan();
                            break;
                        default:
                            Console.WriteLine("It's imposible!");//це неможливо
                            return;
                    }
                    break;
                #endregion
                #region Case4
                case 4:
                    Refill();
                    int punct4 = Convert.ToInt32(Console.ReadLine());
                    switch (punct4)
                    {
                        case 0:
                            Console.WriteLine("Goodbye! ");
                            break;
                        case 1:
                            CDialogs.AboutReplenishment();
                            break;
                    }
                    break;
                    #endregion
            }
        }
        public double Exchange(CurrencyExchange currencyOut, double customerStartMoney,CCustomer costumer)
        {
            double resultAmount = _computer.Exchange(currencyOut, customerStartMoney);
            switch (currencyOut)
            {
                case CurrencyExchange.GrnToDollar:
                    _dollarAmount -= resultAmount;
                    _grnAmount += customerStartMoney;
                    LocalExchange();
                    break;
                case CurrencyExchange.GrnToEuro:
                    _euroAmount -= resultAmount;
                    _grnAmount += customerStartMoney;
                    LocalExchange();
                    break;
                case CurrencyExchange.DollarToGrn:
                    _dollarAmount += customerStartMoney;
                    _grnAmount -= resultAmount;
                    LocalExchange();
                    break;
                case CurrencyExchange.EuroToGrn:
                    _euroAmount += resultAmount;
                    _grnAmount -= customerStartMoney;
                    LocalExchange();
                    break;
                case CurrencyExchange.EuroToDollar:
                    _euroAmount += resultAmount;
                    _dollarAmount -= customerStartMoney;
                    LocalExchange();
                    break;
                case CurrencyExchange.DollarToEuro:
                    _dollarAmount += customerStartMoney;
                    _euroAmount -= resultAmount;
                    LocalExchange();
                    break;
                default:
                    return 0;
            }
            void LocalExchange()
            {
                Report.Add($"Name:{costumer.Name} Start Money: {customerStartMoney} Result: {resultAmount} Date:" +
                       $" {DateTime.Now.ToString("ddd, dd MMM yyy HH':'mm':'ss 'GMT'")}\n");
            }
            return resultAmount;
        }
        public void ExchangeOfMoney()
        {
            Console.WriteLine("Choose a punct:");//виберіть пункт
            Console.WriteLine("0. End of exchange");//завершення обміну
            Console.WriteLine("1. Exchange grn to dollar");//обміняти гривні на долари
            Console.WriteLine("2. Exchange grn to euro");//обміняти гривні на євро
            Console.WriteLine("3. Exchange dollar to grn");//обміняти долари на гривні
            Console.WriteLine("4. Exchange euro to grn");//обміняти євро на гривні
            Console.WriteLine("5. Exchange euro to dollar");//обміняти євро на долари
            Console.WriteLine("6. Exchange dollar to euro");//обміняти долари на євро
        }
        public void Question2(CurrencyExchange currencyOut)
        {
            Console.WriteLine("How many " + currencyOut + " you want to exchange?");//Скільки currencyOut Ви хочете обміняти
        }
        public void ContributionOfMoney()
        {
            Console.WriteLine("Good afternoon, for which term do you want to invest money, minimum deposit 5000 hryvnias");//Доброго дня на який термін ви хочете вкласти гроші, мінімальний депозит 5000 гривень
            Console.WriteLine("0. End of invest");//завершення вкладу
            Console.WriteLine("1. For half a year at 15% per annum ");//На пів року під 15 відсотків річних
            Console.WriteLine("2. For a year at 17% per annum ");//На рік під 17 відсотків річних
            Console.WriteLine("3. For 2 years at 20% per annum ");//На 2 роки під 20 відсотків річних
        }
        public void Loan()
        {
            Console.WriteLine("Good day, please write the data from the passport and the identification code");//Доброго дня, напишіть будь ласка дані з паспорту та індифікаційний код
            Console.WriteLine("0. Completion of the loan");//завершення взяття кредиту
            Console.WriteLine("1. We continue");//Продовжуємо
        }
        public void Refill()//поповнення рахунку
        {
            Console.WriteLine("Good day, please dictate the number of the account that needs to be topped up:");//Доброго дня, продиктуйте будь-ласка номер рахунку який треба поповнити
            Console.WriteLine("0. Completion of the operation");//Завершення операції
            Console.WriteLine("1. We continue");//Продовжуємо
        }
        #endregion
    }
    class CDialogs
    {
        #region Methods
        public static void AboutLoan()
        {
            Console.WriteLine("Enter your initials(Huk Vladislav Vitaliyovuch):");//Введіть ваші ініціали
            string pib = Console.ReadLine();
            Console.WriteLine("Enter your date of birth(day):");//Введіть вашу дату народження
            double dateDay = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter your date of birth(month):");//Введіть вашу дату народження
            double dateMonth = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter your date of birth(Year):");//Введіть вашу дату народження
            double dateYear = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter your residential address(Rivne, vul.Soborna 33, kvartura 124):");//Вкажіть вашу адресу проживання
            string adress = Console.ReadLine();
            Console.WriteLine("Enter your phone number:");//Вкажіть ваш номер телефону
            int number = Convert.ToInt32(Console.ReadLine());
            if (dateDay < 13 && dateMonth == 11 && dateYear == 2004 || dateYear < 2004 || dateMonth < 11 && dateYear == 2004)
            {
                Console.WriteLine("Your details have been verified and we can provide you with a loan at 1.5% per month");//Ваші дані перевірено і ми можемо надати вам кредит під 1,5% на місяць
                Console.WriteLine("What amount do you want to take a loan for?(5000 - 100000 grn)");//На яку суму ви хочете взяти кредит?
                int kredit = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("For what term do you take a loan?( 10, 20, 36 month)");//На який термін ви берете кредит?
                int _termin = Convert.ToInt32(Console.ReadLine());
                if (kredit >= 5000 && kredit <= 100000)
                {
                    if (_termin == 10)
                    {
                        double Kredit1 = ((kredit * 0.015) + kredit) / 10;
                        Console.WriteLine("Payment for the loan per month is:   " + Kredit1 + "grn");
                    }

                    if (_termin == 20)
                    {
                        double Kredit2 = ((kredit * 0.015) + kredit) / 20;
                        Console.WriteLine("Payment for the loan per month is:   " + Kredit2 + "grn");
                    }

                    if (_termin == 36)
                    {
                        double Kredit3 = ((kredit * 0.015) + kredit) / 36;
                        Console.WriteLine("Payment for the loan per month is:   " + Kredit3 + "grn");
                    }
                }
                else
                {
                    Console.WriteLine("We cannot give you credit");//Ми не можемо надати вам кредит
                }

            }
            else
            {
                Console.WriteLine("We cannot give you credit");//Ми не можемо надати вам кредит
            }
        }
        public static void AboutReplenishment()
        {
            Console.WriteLine("Payment account: ");//Платіжний рахунок
            double Pay = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("How much do you want to top up the account?(Commission 1.5%) ");//На яку суму бажаєте поповнити рахунок?
            double Babki = Convert.ToDouble(Console.ReadLine());
            double Babki2 = ((Babki * 0.015) + Babki);
            Console.WriteLine("You need to pay:   " + Babki2 + "grn");//Потрібно оплатити:
            Console.WriteLine("Pay?(Yes - 1, NO - 2)");
            int Pay1 = Convert.ToInt32(Console.ReadLine());
            if (Pay1 == 1)
            {
                Console.WriteLine("The bill has been paid successfully");//рахунок успішно оплачено
            }
            if (Pay1 == 2)
            {
                Console.WriteLine($"Goodbye! ");
            }
        }
#endregion
    }
    class CBank
    {
        #region Fields and properties
        public static int ProcentD { get; set; }
        #endregion
        #region Constructors
        static CBank()
        {
            ProcentD = 20;
        }
        #endregion
        #region Methods
        public static void Hello()
        {
            Console.WriteLine("Good afternoon, Privatbank welcomes you, please approach a consultant for further actions.\n            (1 - We approach the consultant, 2 - We leave the bank)");
            Console.WriteLine("Your decision: ");//Твоє рішення
        }
        public void GetD(int dirty)
        {
            ProcentD += dirty;
        }
        public void PrintProcent()
        {
            Console.WriteLine($"ProcentDirty: {ProcentD} ");
        }
        #endregion
    }
    class CClinerMan : CPerson
    {
        #region Fields and properties

        public int ClinerPower { get; set; }
        public CVinnik Vinik { get; set; }
        public ReportBankSingleton _reports; 
        internal CVinnik CVinnik
        {
            get => default;
            set
            {
            }
        }

        internal CBank CBank
        {
            get => default;
            set
            {
            }
        }
        #endregion
        #region Methods

        public CClinerMan(string name, int age, int clinerPower, CVinnik vinik,ReportBankSingleton report) : base(name, age)
        {
            ClinerPower = clinerPower;
            Vinik = vinik;
            _reports = report;
        }

        public int Cline(int procentD)
        {
            string report="";
            report += $"Before Dirty: {procentD}\n";
            while (procentD - (Vinik.Power + ClinerPower) >= 0)
            {
                procentD -= (Vinik.Power + ClinerPower);
                if (Vinik.Hp <= 0)
                {
                    report += "Old vinik desroy\n";
                    report += "We give new vinik\n";
                    Vinik.Hp = 6;
                    report += $"Dirty: {procentD}\n";
                }
                Vinik.Hp -= Vinik.Power;

                report += $"The vinnik had some left over:   {Vinik.Hp} hp\n";
            }
            report += $"Cline end\n";
            report += $"After Dirty: {procentD}\n";
            _reports.Report.Add(report);
            return procentD;


        }
        #endregion
    }
    class CComputer
    {
        #region Fields and properties
        double _dollarRateSell;//ціна продажу
        double _dollarRateBuy;//ціна купівлі
        double _euroRateSell;//ціна продажу
        double _euroRateBuy;//ціна купівлі
        #endregion
        #region Methods

        public CComputer(double dollarRateSell = 41.6, double dollarRateBuy = 41.5, double euroRateSell = 40.9, 
                         double EuroRateBuy = 40.5)//конструктор з вказаними пераметрами за замовчуванням
        {
            _dollarRateSell = dollarRateSell;
            _dollarRateBuy = dollarRateBuy;
            _euroRateSell = euroRateSell;
            _euroRateBuy = euroRateSell;

        }
        
        public double Exchange(CurrencyExchange currencyOut, double customerStartMoney)
        {
            switch (currencyOut)
            {
                case CurrencyExchange.GrnToDollar:
                    return customerStartMoney / _dollarRateSell;
                case CurrencyExchange.GrnToEuro:
                    return customerStartMoney / _euroRateSell;
                case CurrencyExchange.DollarToGrn:
                    return customerStartMoney * _dollarRateBuy;
                case CurrencyExchange.EuroToGrn:
                    return customerStartMoney * _euroRateBuy;
                case CurrencyExchange.EuroToDollar:
                    return customerStartMoney / _dollarRateSell;
                case CurrencyExchange.DollarToEuro:
                    return customerStartMoney * _dollarRateBuy;
                default:
                    return 0;
            }
        }
        #endregion
    }
    class CVinnik
    {
        #region Fields and properties
        public int Hp { get; set; }
        public int Power { get; set; }
        #endregion
        #region Methods
        public CVinnik(int Hp = 6, int Power = 2)
        {
            this.Hp = Hp;
            this.Power = Power;
        }
        #endregion
    }
}

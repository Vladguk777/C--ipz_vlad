using BankConsole.Observer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;

namespace BankConsole
{
    enum CurrencyExchange
    {
        GrnToDollar,
        GrnToEuro,
        DolToGrn,
        EurToGrn,
        EurToDollar,
        DolToEuro
    }
    class Program
    {
        #region Methods
        static void Main(string[] args)
        {
            CBank bank = new CBank();
            CComputer computer = new CComputer();
            CConsultant consultant = new CConsultant("Larisa", 20);
            ReportBankSingleton reportBankSingleton1 = ReportBankSingleton.GetInstance();
            ReportBankSingleton reportBankSingleton2 = ReportBankSingleton.GetInstance1();
            CEmployee employee = new CEmployee("Vasia", 35, computer, reportBankSingleton1);
            CVinnik vinik = new CVinnik();
            CClinerMan clinerMan = new CClinerMan("Olya", 18, 2, vinik, reportBankSingleton2);
            
            CCustomer costumer4 = new CCustomer("Vlad", 25, 12000, 4000, 200,clinerMan);
            CCustomer costumer1 = new CCustomer("Vadim", 25, 12000, 4000, 200,clinerMan);
            CCustomer costumer2 = new CCustomer("Misha", 25, 12000, 4000, 200,clinerMan);
            CCustomer costumer3 = new CCustomer("Kolya", 25, 12000, 4000, 200,clinerMan);
            List<CCustomer> costumers = new List<CCustomer>() { costumer4, costumer1, costumer2, costumer3 };
            foreach (var item in costumers)
            {
                CBank.Hello();
                int a = Convert.ToInt32(Console.ReadLine());
                if (a == 1)
                {
                    {
                        Console.WriteLine(item.Name);
                        item.Work(consultant, employee, bank);
                        Console.WriteLine($"Goodbye {item.Name}!");
                    }

                    CBank.ProcentD = clinerMan.Cline(CBank.ProcentD);
                    reportBankSingleton1.PrintReport();
                    reportBankSingleton2.PrintReport();

                }
                if (a == 2)
                {
                    Console.WriteLine($"Goodbye! ");
                }
            }
        }
        #endregion 
    }
    sealed class ReportBankSingleton
    {
        #region Fields and properties
        static ReportBankSingleton _instance;
        static ReportBankSingleton _instance2;
        public List<string> Report { get; private set; }

        internal ReportBankSingleton ReportBankSingleton1
        {
            get => default;
            set
            {
            }
        }

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
        public static ReportBankSingleton GetInstance1()
        {
            if (_instance2 == null)
            {
                _instance2 = new ReportBankSingleton();
                _instance2.Report = new List<string>();
            }
            return _instance2;
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
        public void Operation1(CurrencyExchange currency,CCustomer costumer,double Bank)
        {
            _employee.Question2(currency);
            double customerStartMoney = float.Parse(Console.ReadLine());
            double customerResultMoney = _employee.Exchange(currency, customerStartMoney,costumer);
            if (customerResultMoney <= Bank)
            {
                string getmoney = currency.ToString().Substring(0,3);
                string resultmoney = currency.ToString().Substring(5);
                Console.WriteLine("You get " + customerStartMoney + " " + getmoney + " and received " +
                                  + customerResultMoney + " " + resultmoney);
            }
            else
            {
                Console.WriteLine("We have not enough money");
            }
        }
        public void Operation2(double[] procent)
        {
            double[] depoziteParsent = { 0.15, 0.17, 0.20 };
            int punct2 = Convert.ToInt32(Console.ReadLine());
            if (punct2 >= 1 && punct2 <= 3)
            {
                Operation2Dod(depoziteParsent[punct2 - 1]);
            }
            else
            {
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
        public void OperationExchangeMoney(CEmployee employee,CCustomer customer)
        {
            Random random = new Random();
            employee.ExchangeOfMoney();
            int punct1 = Convert.ToInt32(Console.ReadLine());
            if (punct1 >= 1 && punct1 <= 6)
            {
                Operation1((CurrencyExchange)(punct1 - 1), customer, random.Next(1000, 2147483647));
            }
            else
            {
                Console.WriteLine("It's imposible!");//це неможливо
                return;
            }
        }
        public void OperationConribut(CEmployee employee)
        {
            double[] arr = new double[3] { 0.15, 0.17, 0.20 };

            employee.ContributionOfMoney();
            Operation2(arr);
        }
        public void OperationLoad(CEmployee employee)
        {
            employee.Loan();
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
        }
        public void OperaionRefill(CEmployee employee)
        {
            employee.Refill();
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
    class CCustomer : CPerson,IObserver
    {
        #region Fields and properties
        public int Grivnas { get; set; }
        public int Euros { get; set; }
        public int Dollars { get; set; }
        private IObservable observable;
        internal CBank CBank
        {
            get => default;
            set
            {
            }
        }
        #endregion
        #region Constructors
        public CCustomer(string name, int age, int grivnas, int euros, int dollars, IObservable obj) : base(name, age)
        {
            Grivnas = grivnas;
            Euros = euros;
            Dollars = dollars;
            this.observable = obj;
            obj.AddObserver(this);
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

        public void Update()
        {
            Console.WriteLine("CLINE END -> (Observer)");
            observable.RemoveObserver(this);
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
            Random random= new Random();
            Facade facade = new Facade(this);
            consultant.Consult();
            int punct = Convert.ToInt32(Console.ReadLine());
            switch (punct)
            {
                case 0:
                    break;
                #region Case1
                case 1:
                    facade.OperationExchangeMoney(this,customer);
                    break;
                #endregion
                #region Case2
                case 2:
                    facade.OperationConribut(this);
                    break;
                #endregion
                #region Case3
                case 3:
                    facade.OperationLoad(this);
                    break;
                #endregion
                #region Case4
                case 4:
                    facade.OperaionRefill(this);
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
                    break;
                case CurrencyExchange.GrnToEuro:
                    _euroAmount -= resultAmount;
                    _grnAmount += customerStartMoney;
                    break;
                case CurrencyExchange.DolToGrn:
                    _dollarAmount += customerStartMoney;
                    _grnAmount -= resultAmount;
                    break;
                case CurrencyExchange.EurToGrn:
                    _euroAmount += resultAmount;
                    _grnAmount -= customerStartMoney;
                    break;
                case CurrencyExchange.EurToDollar:
                    _euroAmount += resultAmount;
                    _dollarAmount -= customerStartMoney;
                    break;
                case CurrencyExchange.DolToEuro:
                    _dollarAmount += customerStartMoney;
                    _euroAmount -= resultAmount;
                    break;
                default:
                    return 0;
            }
            LocalExchange();
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
                if (kredit >= 5000 && kredit <= 100000 && (_termin == 10 || _termin == 20 || _termin == 36))
                {
                    double Kredit1 = ((kredit * 0.015) + kredit) / _termin;
                    Console.WriteLine("Payment for the loan per month is:   " + Kredit1 + "grn");
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
    class CClinerMan : CPerson, IObservable
    {
        #region Fields and properties

        public int ClinerPower { get; set; }
        public CVinnik Vinik { get; set; }
        public ReportBankSingleton _reports;
        private List<IObserver> observers;
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
        #region Constructors
        public CClinerMan(string name, int age, int clinerPower, CVinnik vinik,ReportBankSingleton report) : base(name, age)
        {
            ClinerPower = clinerPower;
            Vinik = vinik;
            _reports = report;
            observers = new List<IObserver>();
        }
        #endregion
         #region Methods
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
            _reports.AddReport(report);
            Notify();
            return procentD;
        }

        public void AddObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var item in observers.ToList())
            {
                item.Update();
            }
        }
        #endregion

        internal ReportBankSingleton ReportBankSingleton
        {
            get => default;
            set
            {
            }
        }
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
                case CurrencyExchange.DolToGrn:
                    return customerStartMoney * _dollarRateBuy;
                case CurrencyExchange.EurToGrn:
                    return customerStartMoney * _euroRateBuy;
                case CurrencyExchange.EurToDollar:
                    return customerStartMoney / _dollarRateSell;
                case CurrencyExchange.DolToEuro:
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

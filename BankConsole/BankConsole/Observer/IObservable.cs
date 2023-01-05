using BankConsole;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankConsole.Observer
{
    public interface IObservable
    {
         void AddObserver(IObserver observer);
         void RemoveObserver(IObserver observer);
         void Notify();   
    }
}

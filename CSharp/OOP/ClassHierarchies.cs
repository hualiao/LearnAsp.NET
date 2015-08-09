using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.OOP
{
    class ClassHierarchies
    {
        public static void BaseAndDrived()
        {
            CheckingAccount oCheckingAccount = new CheckingAccount();
            oCheckingAccount.AccountNumber = 1000;
            oCheckingAccount.Withdraw(500);
        }

        public static void PolymorphismByInheritance()
        {
            List<Account> AccountList = new List<Account>();
            CheckingAccount oCheckingAccount = new CheckingAccount();
            oCheckingAccount.AccountNumber = 100;
            SavingsAccount oSavingAccount = new SavingsAccount();
            oSavingAccount.AccountNumber = 101;
            AccountList.Add(oCheckingAccount);
            AccountList.Add(oSavingAccount);
            foreach (Account a in AccountList)
            {
                Console.WriteLine(a.GetAccountInfoByInherite());
            }
        }

        public static void PolymorphismByInterface()
        {
            List<Account> AccountList = new List<Account>();
            CheckingAccount oCheckingAccount = new CheckingAccount();
            SavingsAccount oSavingAccount = new SavingsAccount();
            AccountList.Add(oCheckingAccount);
            AccountList.Add(oSavingAccount);
            foreach (IAccount a in AccountList)
            {
                Console.WriteLine(a.GetAccountInfoByInterface(1));
            }
        }
    }
}

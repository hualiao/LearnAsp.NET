using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.OOP
{
    /// <summary>
    /// can't be instantiated by client code. Access to the methods and properties of the
    /// class must be through a derived class.
    /// </summary>
    abstract class Account
    {
        long _accountNumber;

        public long AccountNumber
        {
            get { return _accountNumber; }
            set { _accountNumber = value; }
        }

        /// <summary>
        /// You may want to expose a property or method of the base class to a derived class, but not to
        /// a client of the derived class.
        /// </summary>
        /// <returns></returns>
        protected double GetBalance(int accountNumber)
        {
            _accountNumber = accountNumber;
            if (_accountNumber == 1)
            {
                return 1000;
            }
            else if (_accountNumber == 2)
            {
                return 2000;
            }
            else
            {
                throw new Exception("Account number is incorret");
            }
            // code to retrieve account balance from database
        }

        public virtual void Withdraw(double amount)
        {

        }

        public virtual void Deposit(double amount)
        {
            // Base class implementation
        }

        public abstract string GetAccountInfoByInherite();

        /// <summary>
        /// method as a template for the derived class
        /// </summary>
        /// <param name="amount"></param>
        //public abstract void Deposit(double amount);
    }

    /// <summary>
    /// a Sealed Class
    /// </summary>
    class CheckingAccount : Account, IAccount
    {
        double _minBalance;
        private double _balance = 2000;

        public double Balance
        {
            get { return _balance; }
        }

        public double MinBalance
        {
            get { return _minBalance; }
            set { _minBalance = value; }
        }

        public override void Withdraw(double amount)
        {
            double minBalance = GetMinBalance();
            if (minBalance < (Balance - amount))
            {
                base.Withdraw(amount);
                _balance -= amount;
            }
            else
            {
                throw new Exception("Minimum balance error.");
            }
            // code to withdraw from account
        }

        /// <summary>
        /// sealed prevents the overriding of the Deposit method if the CheckingAccount
        /// class is derived from
        /// </summary>
        /// <param name="amount"></param>
        public sealed override void Deposit(double amount)
        {
            // Derived class implementation
            base.Deposit(amount); // Calling a Base Class Method from a Derived Class
        }

        public virtual double GetMinBalance()
        {
            return 200;
        }

        public string GetAccountInfoByInterface(int accountNumber)
        {
            return "Printing checking account info for account number "
                + AccountNumber.ToString();
        }

        public override string GetAccountInfoByInherite()
        {
            return "Printing checking account info for account number " +
                AccountNumber.ToString();
        }
    }

    class InterestBearingCheckingAccount : CheckingAccount
    {
        public override double GetMinBalance()
        {
            // Calling a Derived Class Method from a Base Class
            //InterestBearingCheckingAccount oAccount = new InterestBearingCheckingAccount();
            //oAccount.Withdraw(500); // a minimum balance of 1,000 is used
            return 1000;
        }

        // Hiding Base Class Methods
        public new void Withdraw(double amount)
        {
        }

        // Overloading Methods of a Base Class
        public void Withdraw(double amount, double minBalance)
        {

        }
    }

    sealed class SavingsAccount : Account, IAccount
    {
        double _dblBalance;
        private double _balance = 2000;

        public double Balance
        {
            get { return _balance; }
        }

        public override void Withdraw(double amount)
        {
            if (Balance > amount)
            {
                base.Withdraw(amount);
            }
            else
            {
                throw new ApplicationException("Not enough fund");
            }
        }

        public double WithDraw(int accountNumber, double amount)
        {
            _dblBalance = GetBalance(accountNumber);
            if (_dblBalance >= amount)
            {
                _dblBalance -= amount;
                return _dblBalance;
            }
            else
            {
                throw new Exception("Not enough funds.");
            }
        }

        public override string GetAccountInfoByInherite()
        {
            return "Printing saving account info for account number " +
                AccountNumber.ToString();
        }

        public string GetAccountInfoByInterface(int accountNumber)
        {
            return "Printing saving account info for account number " +
                AccountNumber.ToString();
        }
    }
}

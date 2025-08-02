using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanSimulator.Domain.Entities
{
    public static class LoanCalculator
    {
        public static decimal CalculateMonthlyPayment(decimal amount, double interestRate, int durationMonths)
        {
            double monthlyInterestRate = interestRate / 100 / 12;
            if (monthlyInterestRate == 0)
                return amount / durationMonths;

            double payment = (double)amount * monthlyInterestRate / (1 - System.Math.Pow(1 + monthlyInterestRate, -durationMonths));
            return (decimal)payment;
        }
    }
}

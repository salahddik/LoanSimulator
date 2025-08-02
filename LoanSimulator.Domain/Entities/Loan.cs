using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanSimulator.Domain.Entities
{
    public class Loan
    {
        public int Id { get; set; }                    // Unique identifier for the loan
        public decimal Amount { get; set; }            // Loan amount requested
        public int DurationMonths { get; set; }        // Duration of the loan in months
        public double InterestRate { get; set; }       // Annual interest rate (percentage)
        public decimal MonthlyPayment { get; set; }    // Calculated monthly payment amount
        public string Email { get; set; }               // Customer email associated with the loan
    }
}

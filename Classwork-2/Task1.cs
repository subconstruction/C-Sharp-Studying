public class InvalidMonth : ArgumentException
{
    public InvalidMonth(string msg) : base(msg) { }
}

public class InvalidAmount : ArgumentException
{
    public InvalidAmount(string msg) : base(msg) { }
}

public class FinancialInstitution
{
    public string Name { get; set; }
}

public class BranchOffice
{
    public string Name { get; set; }
    public decimal TotalDeposits { get; set; }
}

public class DepositAccount
{
    public string AccountHolder { get; set; }
    public decimal Amount { get; set; }

    public DepositAccount(string accountHolder, decimal amount)
    {
        try
        {
            if (amount < 0)
                throw new InvalidAmount($"Negative deposit amount: {amount}");
            AccountHolder = accountHolder;
            Amount = amount;
        }
        catch (InvalidAmount ex)
        {
            Console.WriteLine($"Невозможно создать вклад – указана отрицательная сумма вклада: {amount}");
            throw;
        }
    }

    public virtual decimal CalculateInterest(int months) => Amount;
}

public class FixedTermDeposit : DepositAccount
{
    public FixedTermDeposit(string accountHolder, decimal amount) : base(accountHolder, amount) { }

    public override decimal CalculateInterest(int months)
    {
        try
        {
            if (months < 0)
                throw new InvalidMonth($"Negative months: {months}");
            decimal interestRate = 0.05m / 12;
            return Amount * (1 + interestRate * months);
        }
        catch (InvalidMonth ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
            return Amount;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошло исключение: {ex.Message}");
            return Amount;
        }
    }
}

public class OnDemandDeposit : DepositAccount
{
    public OnDemandDeposit(string accountHolder, decimal amount) : base(accountHolder, amount) { }

    public override decimal CalculateInterest(int months)
    {
        try
        {
            if (months < 0)
                throw new InvalidMonth($"Negative months: {months}");
            decimal interestRate = 0.02m / 12;
            return Amount * (1 + interestRate * months);
        }
        catch (InvalidMonth ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
            return Amount;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошло исключение: {ex.Message}");
            return Amount;
        }
    }
}

public class Program
{
    public static void Main()
    {
        try
        {
            var deposit = new DepositAccount("Иванов А.Д.", -1000m);
        }
        catch (InvalidAmount ex)
        {
            Console.WriteLine($"Исключение в основном методе: {ex.Message}");
        }

        var fixedDeposit = new FixedTermDeposit("Иванова И.В.", 5000m);
        var total = fixedDeposit.CalculateInterest(-12);
        Console.WriteLine($"Сумма по долгосрочному вкладу: {total}");

        var demandDeposit = new OnDemandDeposit("Совсем не Иванова И.В.", 3000m);
        total = demandDeposit.CalculateInterest(6);
        Console.WriteLine($"Сумма по вкладу до востребования: {total}");
    }
}

// Абстрактный компонент организации
public abstract class OrganizationComponent
{
    public string Name { get; protected set; }
    public virtual void Add(OrganizationComponent component) => throw new NotImplementedException();
    public virtual void Remove(OrganizationComponent component) => throw new NotImplementedException();
    public virtual void DisplayHierarchy(int depth = 0) => throw new NotImplementedException();
    public virtual decimal CalculateBudget() => throw new NotImplementedException();
    public virtual int CountEmployees() => throw new NotImplementedException();
}

// Класс сотрудника (Employee)
public class Employee : OrganizationComponent
{
    public string Position { get; private set; }
    public decimal Salary { get; private set; }

    public Employee(string name, string position, decimal salary)
    {
        Name = name;
        Position = position;
        Salary = salary;
    }

    public override void DisplayHierarchy(int depth = 0) => Console.WriteLine(new String('-', depth) + Name + " - " + Position);
    public override decimal CalculateBudget() => Salary;
    public override int CountEmployees() => 1;
}

// Класс отдела (Department)
public class Department : OrganizationComponent
{
    private List<OrganizationComponent> _components = new List<OrganizationComponent>();

    public Department(string name) => Name = name;

    public override void Add(OrganizationComponent component) => _components.Add(component);
    public override void Remove(OrganizationComponent component) => _components.Remove(component);

    public override void DisplayHierarchy(int depth = 0)
    {
        Console.WriteLine(new String('-', depth) + Name);
        foreach (var component in _components)
        {
            component.DisplayHierarchy(depth + 2);
        }
    }

    public override decimal CalculateBudget() => _components.Sum(c => c.CalculateBudget());
    public override int CountEmployees() => _components.Sum(c => c.CountEmployees());
}

public class Program
{
    public static void Main(string[] args)
    {

        var emp1 = new Employee("Ерлан Аманжолов", "Менеджер", 1000m);
        var emp2 = new Employee("Нурлан Сагынов", "Разработчик", 1500m);
        var emp3 = new Employee("Айгуль Тлеубаева", "Дизайнер", 1200m);


        var department1 = new Department("Отдел разработки");
        var department2 = new Department("Отдел дизайна");
        var mainDepartment = new Department("Главный отдел");


        department1.Add(emp2);
        department2.Add(emp3);
        mainDepartment.Add(emp1);
        mainDepartment.Add(department1);
        mainDepartment.Add(department2);

        // Отображение иерархии компании
        mainDepartment.DisplayHierarchy();

        // Расчет общего бюджета и количества сотрудников
        Console.WriteLine($"Общий бюджет компании: {mainDepartment.CalculateBudget()}");
        Console.WriteLine($"Общее количество сотрудников: {mainDepartment.CountEmployees()}");
    }
}


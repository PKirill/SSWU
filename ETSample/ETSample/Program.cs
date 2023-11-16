using System.Linq.Expressions;
using System.Reflection.Metadata;
using ETSample;

var programmers = Filler.FillInProgrammers();

bool isFiltering = true;
string filterQuery = "";
do
{
    Console.WriteLine("Enter filter query or Exit to quit");
    filterQuery = Console.ReadLine();

    if (filterQuery.Contains("exit", StringComparison.InvariantCultureIgnoreCase))
    {
        isFiltering = false;
    }
    else
    {

        var programmerParameterExpression = Expression.Parameter(typeof(Programmer));
        Expression? filterExpression = null;

        foreach (var filter in filterQuery.Split(' '))
        {
            var filterQueryParameters = filter.Split(':');
            var propExpression = Expression.Property(programmerParameterExpression, filterQueryParameters[0]);

            var filterValue = Convert.ChangeType(filterQueryParameters[1], typeof(Programmer).GetProperty(filterQueryParameters[0]).PropertyType);

            var filterValueExpression = Expression.Constant(filterValue);
            var filterPropExpression = Expression.Equal(propExpression, filterValueExpression);

            if (filterExpression is not null)
            {
                var prevExpression = filterPropExpression;
                filterExpression = Expression.And(prevExpression, filterPropExpression);
            }
            else
            {
                filterExpression = filterPropExpression;
            }
        }

        if (filterExpression is not null)
        {
            var expression = Expression.Lambda<Func<Programmer, bool>>(filterExpression, false, new List<ParameterExpression>() { programmerParameterExpression });

            var func = expression.Compile();
            programmers = programmers.Where(func);
        }

        foreach (var expert in programmers)
        {
            Console.WriteLine($"{expert.ID} - {expert.Name} {expert.Country} {expert.Age} {expert.Date}");
        }
    }
} while (isFiltering);
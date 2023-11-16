using ETSample;

var programmers = Filler.FillInProgrammers();

var linqExperts = programmers.Where(p => p.Country == "France")
    .Select(p => new LINQExpert(p));


foreach (var expert in linqExperts)
{
    Console.WriteLine($"{expert.Programmer.Name} {expert.Programmer.Country}");
}
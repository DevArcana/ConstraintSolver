using System;
using ConstraintSolver.Core;
using ConstraintSolver.Core.Interfaces;

namespace ConstraintSolver.Problems
{
  public class SimpleProblem
  {
    private readonly IProblem _problem;

    public SimpleProblem(IProblem problem)
    {
      _problem = problem;

      var a = new Variable("a", new Domain(0, 5));
      var b = new Variable("b", new Domain(0, 5));
      var c = new Variable("c", new Domain(0, 5));
      var d = new Variable("d", new Domain(0, 5));
      var e = new Variable("e", new Domain(0, 5));

      problem.AddConstrainedVariables((a, b) => a != b, a, b, c, d, e);
      
      problem.AddConstraint(new Constraint(a, e, (av, ev) => av > ev));
      problem.AddConstraint(new Constraint(b, e, (av, ev) => av > ev));
      problem.AddConstraint(new Constraint(c, e, (av, ev) => av > ev));
      problem.AddConstraint(new Constraint(d, e, (av, ev) => av > ev));
    }

    public void Solve()
    {
      if (_problem.Solve())
      {
        var assignments = _problem.Assignments;

        foreach (var key in assignments.Keys)
        {
          Console.WriteLine($"{key}: {assignments[key]}");
        }
      }
      else
      {
        Console.WriteLine("No solution found!");
      }
    }
  }
}
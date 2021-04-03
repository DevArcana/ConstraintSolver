using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ConstraintSolver.Core.Solvers
{
  public abstract class AbstractProblem
  {
    public readonly Dictionary<string, int> Assignments = new();
    protected readonly Dictionary<string, Variable> Variables = new();

    public void AddVariables(params Variable[] variables)
    {
      foreach (var variable in variables)
      {
        Variables[variable.Name] = variable;
      }
    }

    public void AddConstrainedVariables(Func<Variable, Variable, BinaryConstraint> constraint, params Variable[] variables)
    {
      AddVariables(variables);
      
      for (var i = 0; i < variables.Length - 1; i++)
      {
        for (var j = i; j < variables.Length; j++)
        {
          AddConstraint(constraint(variables[i], variables[j]));
        }
      }
    }

    public void AddConstraint(BinaryConstraint constraint)
    {
      // This ensures that constraint references only registered variables
      Variables[constraint.A.Name].AddConstraint(constraint);
      Variables[constraint.B.Name].AddConstraint(constraint);
    }

    protected abstract bool Execute();

    public bool Solve()
    {
      var stopwatch = Stopwatch.StartNew();
      var result = Execute();
      stopwatch.Stop();
      
      Console.WriteLine($"Result: {result} | Time elapsed: {stopwatch.ElapsedMilliseconds}ms");
      
      return result;
    }
  }
}
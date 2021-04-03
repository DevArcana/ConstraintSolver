using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using ConstraintSolver.Core.Interfaces;

namespace ConstraintSolver.Core.Solvers
{
  public class BacktrackingProblem : IProblem
  {
    private readonly IList<Variable> _variables;
    private readonly IList<Constraint> _constraints;
    private readonly IDictionary<string, int> _assignments;

    private int _nodes = 0;
    private int _backtracks = 0;

    public IReadOnlyDictionary<string, int> Assignments => new ReadOnlyDictionary<string, int>(_assignments);

    public BacktrackingProblem()
    {
      _variables = new List<Variable>();
      _constraints = new List<Constraint>();
      _assignments = new Dictionary<string, int>();
    }

    public void AddVariable(Variable variable)
    {
      _variables.Add(variable);
    }

    public void AddVariables(params Variable[] variables)
    {
      foreach (var variable in variables)
      {
        AddVariable(variable);
      }
    }

    public void AddConstrainedVariables(Func<int, int, bool> constraint, params Variable[] variables)
    {
      for (var i = 0; i < variables.Length - 1; i++)
      {
        for (var j = i + 1; j < variables.Length; j++)
        {
          AddConstraint(new Constraint(variables[i], variables[j], constraint));
        }
      }
      
      AddVariables(variables);
    }

    public void AddConstraint(Constraint constraint)
    {
      _constraints.Add(constraint);
    }

    private bool Solve(int i)
    {
      _nodes++; // statistics
      
      if (i == _variables.Count)
      {
        return true;
      }

      var variable = _variables[i];

      for (var valueIndex = 0; valueIndex < variable.Domain.Values.Length; valueIndex++)
      {
        if (variable.Domain.Restricted[valueIndex])
        {
          continue;
        }

        _assignments[variable.Name] = variable.Domain.Values[valueIndex];

        var invalid = false;
        
        foreach (var constraint in variable.Constraints.Where(c => _assignments.ContainsKey(c.A.Name) && _assignments.ContainsKey(c.B.Name)))
        {
          var a = _assignments[constraint.A.Name];
          var b = _assignments[constraint.B.Name];

          if (!constraint.Predicate(a, b))
          {
            invalid = true;
            break;
          }
        }
        
        if (invalid)
        {
          continue;
        }
        
        if (Solve(i + 1))
        {
          return true;
        }
      }

      _backtracks++; // statistics
      
      _assignments.Remove(variable.Name);
      return false;
    }

    public bool Solve()
    {
      _assignments.Clear();

      // _assignments["yellow"] = 0;
      // _assignments["norwegian"] = 0;
      // _assignments["water"] = 0;
      // _assignments["kools"] = 0;
      // _assignments["fox"] = 0;
      //
      // _assignments["blue"] = 1;
      // _assignments["ukrainian"] = 1;
      // _assignments["tea"] = 1;
      // _assignments["chesterfields"] = 1;
      // _assignments["horse"] = 1;
      //
      // _assignments["red"] = 2;
      // _assignments["englishman"] = 2;
      // _assignments["milk"] = 2;
      // _assignments["old gold"] = 2;
      // _assignments["snails"] = 2;
      //
      // _assignments["ivory"] = 3;
      // _assignments["spaniard"] = 3;
      // _assignments["orange juice"] = 3;
      // _assignments["lucky strike"] = 3;
      // _assignments["dog"] = 3;
      //
      // _assignments["green"] = 4;
      // _assignments["japanese"] = 4;
      // _assignments["coffee"] = 4;
      // _assignments["parliaments"] = 4;
      // _assignments["zebra"] = 4;
      //
      // foreach (var constraint in _constraints)
      // {
      //   var a = _assignments[constraint.A.Name];
      //   var b = _assignments[constraint.B.Name];
      //
      //   var result = constraint.Predicate(a, b);
      //
      //   if (!result)
      //   {
      //     break;
      //   }
      // }
      //
      // return true;
      
      var stopwatch = Stopwatch.StartNew();
      var result = Solve(0);
      stopwatch.Stop();
      
      Console.WriteLine($"Result: {result} | Time elapsed: {stopwatch.ElapsedMilliseconds}ms | Expanded {_nodes} nodes with {_backtracks} backtracks.");
      
      return result;
    }
  }
}
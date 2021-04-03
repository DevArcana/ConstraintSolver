using System;
using System.Collections.Generic;

namespace ConstraintSolver.Core.Interfaces
{
  public interface IProblem
  {
    void AddVariables(params Variable[] variables);
    void AddConstrainedVariables(Func<int, int, bool> constraint, params Variable[] variables);
    void AddConstraint(Constraint constraint);
    bool Solve();

    IReadOnlyDictionary<string, int> Assignments { get; }
  }
}
using System;

namespace ConstraintSolver.Core
{
  public class Constraint
  {
    public Variable A { get; }
    public Variable B { get; }
    
    public Func<int, int, bool> Predicate { get; }
    
    public Constraint(Variable a, Variable b, Func<int, int, bool> predicate)
    {
      A = a;
      B = b;
      Predicate = predicate;
      
      a.Constraints.Add(this);
      b.Constraints.Add(this);
    }
  }
}
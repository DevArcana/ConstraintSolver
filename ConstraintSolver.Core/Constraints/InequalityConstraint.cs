namespace ConstraintSolver.Core
{
  public class InequalityConstraint : BinaryConstraint
  {
    public InequalityConstraint(Variable a, Variable b) : base(a, b, (av, bv) => av != bv) { }
  }
}
namespace ConstraintSolver.Core
{
  public class EqualityConstraint : BinaryConstraint
  {
    public EqualityConstraint(Variable a, Variable b) : base(a, b, (av, bv) => av == bv) { }
  }
}
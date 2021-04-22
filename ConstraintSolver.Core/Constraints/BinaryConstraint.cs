using System;

namespace ConstraintSolver.Core
{
    public class BinaryConstraint
    {
        public Variable A { get; }
        public Variable B { get; }

        public Func<int, int, bool> Predicate { get; }

        public BinaryConstraint(Variable a, Variable b, Func<int, int, bool> predicate)
        {
            A = a;
            B = b;
            Predicate = predicate;
        }
    }
}
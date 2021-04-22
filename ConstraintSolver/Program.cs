using ConstraintSolver.Core.Solvers;
using ConstraintSolver.Problems;

namespace ConstraintSolver
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var problem = new ZebraProblem(new BacktrackingProblem());
            problem.Solve();
        }
    }
}
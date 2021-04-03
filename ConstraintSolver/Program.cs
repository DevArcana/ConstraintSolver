using ConstraintSolver.Core.Solvers;
using ConstraintSolver.Problems;

namespace ConstraintSolver
{
  class Program
  {
    static void Main(string[] args)
    {
      var problem = new ZebraProblem(new BacktrackingProblem());
      problem.Solve();
    }
  }
}
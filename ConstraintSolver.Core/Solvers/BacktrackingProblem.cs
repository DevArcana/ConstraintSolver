using System.Linq;

namespace ConstraintSolver.Core.Solvers
{
    public class BacktrackingProblem : AbstractProblem
    {
        private bool Solve(Variable[] variables, int i)
        {
            if (i == Variables.Count) return true;

            var variable = variables[i];

            for (var value = variable.LowerBound; value < variable.UpperBound; value++)
            {
                if (!variable.ValidateConstraints(value, Assignments)) continue;

                Assign(variable, value);

                if (Solve(variables, i + 1)) return true;

                Unassign(variable);
            }

            return false;
        }

        protected override bool Execute()
        {
            Assignments.Clear();
            var variables = Variables.Values.ToArray();
            return Solve(variables, 0);
        }
    }
}
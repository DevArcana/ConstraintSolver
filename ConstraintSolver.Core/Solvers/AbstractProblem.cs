using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ConstraintSolver.Core.Solvers
{
    public abstract class AbstractProblem
    {
        public readonly Dictionary<string, int> Assignments = new();
        protected readonly Dictionary<string, Variable> Variables = new();
        
        public int AssignmentsDone { get; private set; }
        public TimeSpan? Elapsed { get; private set; }

        protected void Assign(Variable variable, int value)
        {
            AssignmentsDone++;
            Assignments[variable.Name] = value;
        }

        protected void Unassign(Variable variable)
        {
            Assignments.Remove(variable.Name);
        }

        public void AddVariables(params Variable[] variables)
        {
            foreach (var variable in variables) Variables[variable.Name] = variable;
        }

        public void AddConstrainedVariables(Func<Variable, Variable, BinaryConstraint> constraint,
            params Variable[] variables)
        {
            AddVariables(variables);

            for (var i = 0; i < variables.Length - 1; i++)
            for (var j = i; j < variables.Length; j++)
                AddConstraint(constraint(variables[i], variables[j]));
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

            Elapsed = stopwatch.Elapsed;

            return result;
        }
    }
}
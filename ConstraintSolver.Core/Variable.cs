using System;
using System.Collections.Generic;

namespace ConstraintSolver.Core
{
    public class Variable
    {
        public string Name { get; }
        public int LowerBound { get; }
        public int UpperBound { get; }

        private readonly List<BinaryConstraint> _constraints;
        private readonly HashSet<int> _restricted;

        public Variable(string name, int lowerBound, int upperBound)
        {
            Name = name;
            LowerBound = lowerBound;
            UpperBound = upperBound;

            _constraints = new List<BinaryConstraint>();
            _restricted = new HashSet<int>();
        }

        public Variable(string name, int value) : this(name, value, value + 1)
        {
        }

        internal void AddConstraint(BinaryConstraint binaryConstraint)
        {
            _constraints.Add(binaryConstraint);
        }

        public void Restrict(int value)
        {
            if (value < LowerBound || value >= UpperBound)
                throw new ArgumentException("Can't restrict value outside of the variable's domain!", nameof(value));

            _restricted.Add(value);
        }

        public bool ValidateConstraints(int value, IDictionary<string, int> assignments)
        {
            if (value < LowerBound || value >= UpperBound) return false;

            if (_restricted.Contains(value)) return false;

            var success = true;

            foreach (var constraint in _constraints)
                if (constraint.A == this)
                {
                    if (!assignments.ContainsKey(constraint.B.Name)) continue;

                    var a = value;
                    var b = assignments[constraint.B.Name];

                    if (!constraint.Predicate(a, b))
                    {
                        success = false;
                        break;
                    }
                }
                else
                {
                    if (!assignments.ContainsKey(constraint.A.Name)) continue;

                    var a = assignments[constraint.A.Name];
                    var b = value;

                    if (!constraint.Predicate(a, b))
                    {
                        success = false;
                        break;
                    }
                }

            return success;
        }
    }
}
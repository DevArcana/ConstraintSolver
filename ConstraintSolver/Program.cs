using System;
using System.Collections.Generic;
using System.Linq;
using ConstraintSolver.Core.Solvers;
using ConstraintSolver.Problems;

namespace ConstraintSolver
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var runs = 100;

            var assignments = new List<int>(runs);
            var times = new List<TimeSpan>(runs);
            
            for (int i = 0; i < runs; i++)
            {
                var problem = new ZebraProblem(new BacktrackingProblem());
                var solution = problem.Solve();

                assignments.Add(solution.AssignmentsDone);
                // ReSharper disable once PossibleInvalidOperationException
                times.Add(solution.Elapsed.Value);
            }

            
            var worstTime = times.Select(x => x.TotalMilliseconds).Max();
            var bestTime = times.Select(x => x.TotalMilliseconds).Min();
            var medianTime = times.OrderBy(x => x.TotalMilliseconds).Select(x => x.TotalMilliseconds).ElementAt(runs / 2);
            var averageTime = times.Average(x => x.TotalMilliseconds);
            
            Console.WriteLine($"worst: {worstTime:0.001} best: {bestTime:0.001} median: {medianTime:0.001} average: {averageTime:0.001}");
            
            var worstAssignmentsNumber = assignments.Max();
            var bestAssignmentsNumber = assignments.Min();
            var medianAssignmentsNumber = assignments.OrderBy(x => x).ElementAt(runs / 2);
            var averageAssignmentsNumber = assignments.Average();
            
            Console.WriteLine($"worst: {worstAssignmentsNumber} best: {bestAssignmentsNumber} median: {medianAssignmentsNumber} average: {averageAssignmentsNumber:0.001}");
        }
    }
}
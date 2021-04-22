using System;
using System.Linq;
using ConstraintSolver.Core;
using ConstraintSolver.Core.Solvers;

namespace ConstraintSolver.Problems
{
    public class ZebraProblem
    {
        private readonly AbstractProblem _problem;

        public ZebraProblem(AbstractProblem problem)
        {
            // 1. There are five houses.
            const int min = 0;
            const int max = 5;

            // nationalities
            var englishman = new Variable("englishman", min, max);
            var spaniard = new Variable("spaniard", min, max);
            var ukrainian = new Variable("ukrainian", min, max);
            var norwegian = new Variable("norwegian", 0); // 10. The Norwegian lives in the first house.
            var japanese = new Variable("japanese", min, max);

            problem.AddConstrainedVariables((a, b) => new InequalityConstraint(a, b), englishman, spaniard, ukrainian,
                norwegian, japanese);

            // colours
            var red = new Variable("red", min, max);
            var green = new Variable("green", min, max);
            var ivory = new Variable("ivory", min, max);
            var yellow = new Variable("yellow", min, max);
            var blue = new Variable("blue", min, max);

            problem.AddConstrainedVariables((a, b) => new InequalityConstraint(a, b), red, green, ivory, yellow, blue);

            // animals
            var dog = new Variable("dog", min, max);
            var snails = new Variable("snails", min, max);
            var fox = new Variable("fox", min, max);
            var horse = new Variable("horse", min, max);

            var zebra = new Variable("zebra", min, max);

            problem.AddConstrainedVariables((a, b) => new InequalityConstraint(a, b), dog, snails, fox, horse, zebra);

            // drinks
            var coffee = new Variable("coffee", min, max);
            var tea = new Variable("tea", min, max);
            var milk = new Variable("milk", 2); // 9. Milk is drunk in the middle house.
            var orangeJuice = new Variable("orange juice", min, max);

            var water = new Variable("water", min, max);

            problem.AddConstrainedVariables((a, b) => new InequalityConstraint(a, b), coffee, tea, milk, orangeJuice,
                water);

            // cigs
            var oldGold = new Variable("old gold", min, max);
            var kools = new Variable("kools", min, max);
            var chesterfields = new Variable("chesterfields", min, max);
            var luckyStrike = new Variable("lucky strike", min, max);
            var parliaments = new Variable("parliaments", min, max);

            problem.AddConstrainedVariables((a, b) => new InequalityConstraint(a, b), oldGold, kools, chesterfields,
                luckyStrike, parliaments);

            // 2. The Englishman lives in the red house.
            problem.AddConstraint(new EqualityConstraint(englishman, red));

            // 3. The Spaniard owns the dog.
            problem.AddConstraint(new EqualityConstraint(spaniard, dog));

            // 4. Coffee is drunk in the green house.
            problem.AddConstraint(new EqualityConstraint(coffee, green));

            // 5. The Ukrainian drinks tea.
            problem.AddConstraint(new EqualityConstraint(ukrainian, tea));

            // 6. The green house is immediately to the right of the ivory house.
            problem.AddConstraint(new BinaryConstraint(green, ivory, (a, b) => a - b == 1));

            // 7. The Old Gold smoker owns snails.
            problem.AddConstraint(new EqualityConstraint(oldGold, snails));

            // 8. Kools are smoked in the yellow house.
            problem.AddConstraint(new EqualityConstraint(kools, yellow));

            // 11. The man who smokes Chesterfields lives in the house next to the man with the fox.
            problem.AddConstraint(new BinaryConstraint(chesterfields, fox, (a, b) => Math.Abs(a - b) == 1));

            // 12. Kools are smoked in the house next to the house where the horse is kept.
            problem.AddConstraint(new BinaryConstraint(kools, horse, (a, b) => Math.Abs(a - b) == 1));

            // 13. The Lucky Strike smoker drinks orange juice.
            problem.AddConstraint(new EqualityConstraint(luckyStrike, orangeJuice));

            // 14. The Japanese smokes Parliaments.
            problem.AddConstraint(new EqualityConstraint(japanese, parliaments));

            // 15. The Norwegian lives next to the blue house
            problem.AddConstraint(new BinaryConstraint(norwegian, blue, (a, b) => Math.Abs(a - b) == 1));

            _problem = problem;
        }

        public void Solve()
        {
            if (_problem.Solve())
            {
                var assignments = _problem.Assignments;
                foreach (var house in assignments.Keys.GroupBy(key => assignments[key]).OrderBy(x => x.Key))
                {
                    var attributes = string.Join("| ", house.Select(x => x.PadRight(15)).ToArray());
                    Console.WriteLine($"House {house.Key} {attributes}");
                }
            }
            else
            {
                Console.WriteLine("No solution found!");
            }
        }
    }
}
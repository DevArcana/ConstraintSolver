using System;
using System.Linq;
using ConstraintSolver.Core;
using ConstraintSolver.Core.Interfaces;

namespace ConstraintSolver.Problems
{
  public class ZebraProblem
  {
    private readonly IProblem _problem;
    
    public ZebraProblem(IProblem problem)
    {
      // 1. There are five houses.
      var min = 0;
      var max = 5;
      
      // nationalities
      var englishman = new Variable("englishman", new Domain(min, max));
      var spaniard = new Variable("spaniard", new Domain(min, max));
      var ukrainian = new Variable("ukrainian", new Domain(min, max));
      var norwegian = new Variable("norwegian", new Domain(min, max));
      var japanese = new Variable("japanese", new Domain(min, max));
      
      problem.AddConstrainedVariables((a, b) => a != b, englishman, spaniard, ukrainian, norwegian, japanese);
      
      // colours
      var red = new Variable("red", new Domain(min, max));
      var green = new Variable("green", new Domain(min, max));
      var ivory = new Variable("ivory", new Domain(min, max));
      var yellow = new Variable("yellow", new Domain(min, max));
      var blue = new Variable("blue", new Domain(min, max));

      problem.AddConstrainedVariables((a, b) => a != b, red, green, ivory, yellow, blue);
      
      // animals
      var dog = new Variable("dog", new Domain(min, max));
      var snails = new Variable("snails", new Domain(min, max));
      var fox = new Variable("fox", new Domain(min, max));
      var horse = new Variable("horse", new Domain(min, max));

      var zebra = new Variable("zebra", new Domain(min, max));
      
      problem.AddConstrainedVariables((a, b) => a != b, dog, snails, fox, horse, zebra);
      
      // drinks
      var coffee = new Variable("coffee", new Domain(min, max));
      var tea = new Variable("tea", new Domain(min, max));
      var milk = new Variable("milk", new Domain(min, max));
      var orangeJuice = new Variable("orange juice", new Domain(min, max));

      var water = new Variable("water", new Domain(min, max));
      
      problem.AddConstrainedVariables((a, b) => a != b, coffee, tea, milk, orangeJuice, water);
      
      // cigs
      var oldGold = new Variable("old gold", new Domain(min, max));
      var kools = new Variable("kools", new Domain(min, max));
      var chesterfields = new Variable("chesterfields", new Domain(min, max));
      var luckyStrike = new Variable("lucky strike", new Domain(min, max));
      var parliaments = new Variable("parliaments", new Domain(min, max));
      
      problem.AddConstrainedVariables((a, b) => a != b, oldGold, kools, chesterfields, luckyStrike, parliaments);
      
      // 2. The Englishman lives in the red house.
      problem.AddConstraint(new Constraint(englishman, red, (a, b) => a == b));
      
      // 3. The Spaniard owns the dog.
      problem.AddConstraint(new Constraint(spaniard, dog, (a, b) => a == b));
      
      // 4. Coffee is drunk in the green house.
      problem.AddConstraint(new Constraint(coffee, green, (a, b) => a == b));
      
      // 5. The Ukrainian drinks tea.
      problem.AddConstraint(new Constraint(ukrainian, tea, (a, b) => a == b));
      
      // 6. The green house is immediately to the right of the ivory house.
      problem.AddConstraint(new Constraint(green, ivory, (a, b) => a - b == 1));
      
      // 7. The Old Gold smoker owns snails.
      problem.AddConstraint(new Constraint(oldGold, snails, (a, b) => a == b));
      
      // 8. Kools are smoked in the yellow house.
      problem.AddConstraint(new Constraint(kools, yellow, (a, b) => a == b));
      
      // 9. Milk is drunk in the middle house.
      milk.Domain.Restricted[0] = true;
      milk.Domain.Restricted[1] = true;
      milk.Domain.Restricted[2] = false; // only possible value, alternatively, change the domain specification above
      milk.Domain.Restricted[3] = true;
      milk.Domain.Restricted[4] = true;
      
      // 10. The Norwegian lives in the first house.
      norwegian.Domain.Restricted[0] = false; // same as above
      norwegian.Domain.Restricted[1] = true;
      norwegian.Domain.Restricted[2] = true;
      norwegian.Domain.Restricted[3] = true;
      norwegian.Domain.Restricted[4] = true;
      
      // 11. The man who smokes Chesterfields lives in the house next to the man with the fox.
      problem.AddConstraint(new Constraint(chesterfields, fox, (a, b) => Math.Abs(a - b) == 1));
      
      // 12. Kools are smoked in the house next to the house where the horse is kept.
      problem.AddConstraint(new Constraint(kools, horse, (a, b) => Math.Abs(a - b) == 1));
      
      // 13. The Lucky Strike smoker drinks orange juice.
      problem.AddConstraint(new Constraint(luckyStrike, orangeJuice, (a, b) => a == b));
      
      // 14. The Japanese smokes Parliaments.
      problem.AddConstraint(new Constraint(japanese, parliaments, (a, b) => a == b));
      
      // 15. The Norwegian lives next to the blue house
      problem.AddConstraint(new Constraint(norwegian, blue, (a, b) => Math.Abs(a - b) == 1));

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
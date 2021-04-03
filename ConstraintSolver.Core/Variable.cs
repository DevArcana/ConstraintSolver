using System.Collections.Generic;

namespace ConstraintSolver.Core
{
  public class Variable
  {
    public string Name { get; }
    public Domain Domain { get; }
    
    public List<Constraint> Constraints { get; }

    public Variable(string name, Domain domain)
    {
      Name = name;
      Domain = domain;

      Constraints = new List<Constraint>();
    }
  }
}
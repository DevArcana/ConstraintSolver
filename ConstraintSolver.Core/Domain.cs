using System;
using System.Linq;

namespace ConstraintSolver.Core
{
  public class Domain
  {
    public int[] Values { get; }
    public bool[] Restricted { get; }
    
    public Domain(int lower, int upper)
    {
      if (lower >= upper)
      {
        throw new ArgumentException("The lower bound must be less than the upper bound!", nameof(lower));
      }
      
      Values = Enumerable.Range(lower, upper - lower).ToArray();
      Restricted = new bool[Values.Length];
    }

    // public void Restrict(int value)
    // {
    //   for (var i = 0; i < Values.Length; i++)
    //   {
    //     if (Values[i] == value)
    //     {
    //       Restricted[i] = true;
    //       return;
    //     }
    //   }
    // }
  }
}
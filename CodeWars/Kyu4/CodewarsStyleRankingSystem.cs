using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWars.Kyu4
{
    internal class CodewarsStyleRankingSystem
    {
        public class User
        {
            public int rank = -8;
            public int progress = 0;

            public int Progress
            {
                get { return progress; }

                set
                {
                    progress += value;
                    if (progress >= 100)
                    {
                        CalculatingRankTier(progress);
                        progress %= 100;
                    }
                }
            }

            public void incProgress(int actRank)
            {
                if (actRank >= 9 || actRank <= -9 || actRank == 0)
                    throw new ArgumentException();

                if (actRank == rank)
                {
                    Progress = 3;
                    return;
                }

                int difference = Math.Abs(rank - actRank);

                if (actRank < rank)
                {
                    if (difference == 1)
                    {
                        Progress = 1;
                        return;
                    }
                    return;
                }

                if ((rank < 0 && actRank > 0) || (rank > 0 && actRank < 0))
                    difference--;

                Progress = 10 * (int)Math.Pow(difference, 2);
            }

            public void CalculatingRankTier(int progressValue)
            {
                var rankTier = progressValue / 100;
                if (rankTier > 0)
                    for (int i = 0; i < rankTier; i++)
                    {
                        if (rank == 8)
                            break;
                        if (rank == -1)
                            rank++;
                        rank++;
                    }
            }
        }
    }
}

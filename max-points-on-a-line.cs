using System;

namespace Algorithms
{
    // solution for https://leetcode.com/problems/max-points-on-a-line
    public static class max_points_on_a_line
    {
        public static void RunTestCases()
        {
            var testpoints = new int[][]
            {
                new[] { 1, 1 },
                new[] { 2, 2 },
                new[] { 3, 3 }
            };

            var testpoints2 = new int[][]
            {
                new[] { -6, -1 },
                new[] { 3, 1 },
                new[] { 12, 3 }
            };

            var testpoints3 = new int[][]
            {
                new[] { 0, 0 },
                new[] { 2, 2 },
                new[] { -1, -1 }
            };

            var testpoints4 = new int[][]
            {
                new[] { 1, 1 },
                new[] { 3, 2 },
                new[] { 5, 3 },
                new[] { 4, 1 },
                new[] { 2, 3 },
                new[] { 1, 4 }
            };

            var testpoints5 = new int[][]
            {
                new[] { 2, 3 },
                new[] { 3, 3 },
                new[] { -5, 3 }
            };

            var testpoints6 = new int[][]
            {
                new[] { 9, -25 },
                new[] { -4, 1 },
                new[] { -1, 5 },
                new[] { -7, 7 }
            };

            Console.WriteLine(MaxPoints(testpoints).ToString());
            Console.WriteLine(MaxPoints(testpoints2).ToString());
            Console.WriteLine(MaxPoints(testpoints3).ToString());
            Console.WriteLine(MaxPoints(testpoints4).ToString());
            Console.WriteLine(MaxPoints(testpoints5).ToString());
            Console.WriteLine(MaxPoints(testpoints6).ToString());
        }

        static int MaxPoints(int[][] points)
        {
            var maxPointsCount = 1;

            for (var i = 0; i < points.Length; i++)
            {
                var (currentX, currentY) = GetCords(points[i]);

                for (var j = 0; j < points.Length; j++)
                {
                    var pointsCount = 0;
                    var (otherX, otherY) = GetCords(points[j]);

                    if (currentX == otherX && currentY == otherY)
                    {
                        continue;
                    }

                    pointsCount = GetPointsCountOnTheSameLine(currentX, currentY, otherX, otherY, points);

                    if (pointsCount > maxPointsCount)
                    {
                        maxPointsCount = pointsCount;
                    }
                }
            }

            return maxPointsCount;
        }

        static int GetPointsCountOnTheSameLine(int x1, int y1, int x2, int y2, int[][] points)
        {
            var pointsCount = 2;
            for (var i = 0; i < points.Length; i++)
            {
                var (x, y) = GetCords(points[i]);

                if ((x == x1 && y == y1) || (x == x2 && y == y2))
                {
                    continue;
                }

                if (OnTheSameLine(x1, y1, x2, y2, x, y))
                {
                    pointsCount++;
                }
            }

            return pointsCount;
        }
        
        static bool OnTheSameLine(decimal x1, decimal y1, decimal x2, decimal y2, decimal x3, decimal y3)
        {
            return 1m / 2m * ((x1 - x3) * (y2 - y3) - (x2 - x3) * (y1 - y3)) == 0;
        }

        static (int, int) GetCords(int[] point)
        {
            var x = point[0];
            var y = point[1];

            return (x, y);
        }
    }
}
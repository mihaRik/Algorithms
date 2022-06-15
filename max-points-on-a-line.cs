using System;

namespace Algorithms
{
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

            // Console.WriteLine(MaxPoints(testpoints).ToString());
            // Console.WriteLine(MaxPoints(testpoints2).ToString());
            // Console.WriteLine(MaxPoints(testpoints3).ToString());
            // Console.WriteLine(MaxPoints(testpoints4).ToString());
            // Console.WriteLine(MaxPoints(testpoints5).ToString());
            Console.WriteLine(MaxPoints(testpoints6).ToString());
        }

        static int MaxPoints(int[][] points)
        {
            ValidateParam(points.Length, 0, 300);
            var (maxPlaneLimit, minPlaneLimit) = FindMaxAndMinLimitsOfPlane(points);
            var maxPointsCount = 0;

            for (var i = 0; i < points.Length; i++)
            {
                var currentPoint = points[i];

                var pointsCount = FindLineForPoint(currentPoint, points, 0, 0, maxPlaneLimit, minPlaneLimit);

                if (pointsCount > maxPointsCount)
                {
                    maxPointsCount = pointsCount;
                }
            }

            return maxPointsCount;
        }

        static int FindLineForPoint(int[] point, int[][] points, int patternX, int patternY, int maxPlaneLimit,
            int minPlaneLimit)
        {
            var (currentX, currentY) = GetCords(point);
            var pointsCount = 1;

            for (var i = 0; i < points.Length; i++)
            {
                var otherPoint = points[i];
                var (otherX, otherY) = GetCords(otherPoint);

                if (currentX == otherX && currentY == otherY)
                {
                    continue;
                }

                if (patternX == 0 && patternY == 0)
                {
                    patternX = otherX - currentX;
                    patternY = otherY - currentY;

                    var currentPointsCount =
                        FindLineForPoint(otherPoint, points, patternX, patternY, maxPlaneLimit, minPlaneLimit);

                    currentPointsCount++;
                    if (currentPointsCount > pointsCount)
                    {
                        pointsCount = currentPointsCount;
                    }

                    patternX = 0;
                    patternY = 0;
                }
                else
                {
                    var tempPatternX = patternX;
                    var tempPatternY = patternY;

                    var xterm = 0;
                    var yterm = 0;

                    if (patternX < 0)
                    {
                        xterm = -1;
                    }
                    else if (patternX > 0)
                    {
                        xterm = 1;
                    }

                    if (patternY < 0)
                    {
                        yterm = -1;
                    }
                    else if (patternY > 0)
                    {
                        yterm = 1;
                    }

                    while (true)
                    {
                        var nextPossibleX = currentX + tempPatternX;
                        var nextPossibleY = currentY + tempPatternY;

                        if (nextPossibleX > maxPlaneLimit || nextPossibleX < minPlaneLimit)
                        {
                            break;
                        }

                        if (nextPossibleY > maxPlaneLimit || nextPossibleY < minPlaneLimit)
                        {
                            break;
                        }

                        if (nextPossibleX == otherX && nextPossibleY == otherY)
                        {
                            pointsCount++;
                        }

                        tempPatternX += xterm;
                        tempPatternY += yterm;
                    }
                }
            }

            return pointsCount;
        }

        static (int, int) GetCords(int[] point)
        {
            const int limitForCords = 10 * 10 * 10 * 10;

            ValidateParam(point.Length, 0, 2);

            var x = point[0];
            var y = point[1];

            ValidateParam(x, -limitForCords, limitForCords);
            ValidateParam(y, -limitForCords, limitForCords);

            return (x, y);
        }

        static void ValidateParam(int param, int min, int max)
        {
            if (param < min || param > max)
            {
                throw new ArgumentOutOfRangeException(
                    $"Length should be more than or equal {min} or less than or equal {max}");
            }
        }

        static (int, int) FindMaxAndMinLimitsOfPlane(int[][] points)
        {
            var max = -10 * 10 * 10 * 10;
            var min = 10 * 10 * 10 * 10;

            for (var i = 0; i < points.Length; i++)
            {
                var (x, y) = GetCords(points[i]);

                if (x >= y)
                {
                    if (x > max)
                    {
                        max = x;
                    }

                    if (y < min)
                    {
                        min = y;
                    }
                }
                else
                {
                    if (y > max)
                    {
                        max = y;
                    }

                    if (x < min)
                    {
                        min = x;
                    }
                }
            }

            return (max, min);
        }
    }
}
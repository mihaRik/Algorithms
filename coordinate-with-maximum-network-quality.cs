using System;

namespace Algorithms
{
    public static class coordinate_with_maximum_network_quality
    {
        public static void RunTestCases()
        {
            // var towers = new[] { new[] { 1, 2, 5 }, new[] { 2, 1, 7 }, new[] { 3, 1, 9 } };
            // var radius = 2;

            // var towers = new[] { new[] { 23, 11, 21 } };
            // var radius = 9;

            // var towers = new[] { new int[] { 1, 2, 13 }, new int[] { 2, 1, 7 }, new int[] { 0, 1, 9 } };
            // var radius = 2;

            // var towers = new[] { new int[] { 2, 1, 9 }, new int[] { 0, 1, 9 } };
            // var radius = 2;

            // var towers = new[] { new[] { 42, 0, 0 } };
            // var radius = 7;

            var towers = new[]
                { new int[] { 0, 1, 2 }, new int[] { 2, 1, 2 }, new int[] { 1, 0, 2 }, new int[] { 1, 2, 2 } };
            var radius = 1;

            var result = BestCoordinate(towers, radius);

            foreach (var a in result)
            {
                Console.Write($"[{a}]");
            }
        }

        static int[] BestCoordinate(int[][] towers, int radius)
        {
            ValidateParams(towers.Length, 1, 50);
            ValidateParams(radius, 1, 50);

            var (maxX, maxY) = GetMaxXAndY(towers);

            var maxNetworkQuality = 0;
            var cordsWithNetworkQuality = new int[(maxX + 1) * (maxY + 1)][];

            for (var i = 0; i < cordsWithNetworkQuality.Length; i++)
            {
                cordsWithNetworkQuality[i] = new int[3];
            }

            var cordsIndex = 0;

            for (var i = 0; i <= maxX; i++)
            {
                for (var j = 0; j <= maxY; j++)
                {
                    var coordinate = new[] { i, j, 0 };
                    var networkQuality = CalculateNetworkQuality(coordinate, towers, radius);

                    if (networkQuality > maxNetworkQuality)
                    {
                        maxNetworkQuality = networkQuality;
                    }

                    cordsWithNetworkQuality[cordsIndex][0] = i;
                    cordsWithNetworkQuality[cordsIndex][1] = j;
                    cordsWithNetworkQuality[cordsIndex][2] = networkQuality;

                    cordsIndex++;
                }
            }

            return LexicographicallySortCords(cordsWithNetworkQuality, maxNetworkQuality);
        }

        static (int x, int y) GetMaxXAndY(int[][] towers)
        {
            var maxX = 0;
            var maxY = 0;

            for (int i = 0; i < towers.Length; i++)
            {
                var (x, y, _) = GetParams(towers[i]);

                if (x > maxX)
                {
                    maxX = x;
                }

                if (y > maxY)
                {
                    maxY = y;
                }
            }

            return (maxX, maxY);
        }

        static int[] LexicographicallySortCords(int[][] cordsWithNetworkQualities, int maxNetworkQuality)
        {
            var bestCords = new int[2];

            for (var i = 0; i < cordsWithNetworkQualities.Length; i++)
            {
                var cords = cordsWithNetworkQualities[i];

                var (currentX, currentY, currentNetworkQuality) = (cords[0], cords[1], cords[2]);

                if (currentNetworkQuality == maxNetworkQuality && currentNetworkQuality != 0)
                {
                    if (bestCords[0] == 0 && bestCords[1] == 0)
                    {
                        bestCords[0] = currentX;
                        bestCords[1] = currentY;
                    }
                    else if (bestCords[0] > currentX)
                    {
                        bestCords[0] = currentX;
                        bestCords[1] = currentY;
                    }
                    else if (bestCords[0] == currentX && bestCords[1] > currentY)
                    {
                        bestCords[0] = currentX;
                        bestCords[1] = currentY;
                    }
                }
            }

            return bestCords;
        }

        static void ValidateParams(int param, int min, int max)
        {
            if (param < min || param > max)
            {
                throw new ArgumentOutOfRangeException(
                    $"Parameter is not valid. Value: {param}, min: {min}, max: {max}");
            }
        }

        static double FindDistanceBetweenTowers(int currentX, int currentY, int otherX, int otherY)
        {
            return Math.Sqrt(Math.Pow(currentX - otherX, 2) + Math.Pow(currentY - otherY, 2));
        }

        static (int x, int y, int q) GetParams(int[] tower)
        {
            if (tower.Length != 3)
            {
                throw new ArgumentOutOfRangeException(
                    $"Parameter is not valid. Value: {tower.Length}. Should be equal 3");
            }

            var x = tower[0];
            var y = tower[1];
            var q = tower[2];

            ValidateParams(x, 0, 50);
            ValidateParams(y, 0, 50);
            ValidateParams(q, 0, 50);

            return (x, y, q);
        }

        static int CalculateSignalQuality(int q, double d)
        {
            return (int)Math.Floor(q / (1 + d));
        }

        static int CalculateNetworkQuality(int[] tower, int[][] towers, int radius)
        {
            var (currentX, currentY, _) = GetParams(tower);

            var networkQuality = 0;

            for (var j = 0; j < towers.Length; j++)
            {
                var d = 0d;

                var otherTower = towers[j];
                var (otherX, otherY, otherQ) = GetParams(otherTower);

                if (!(currentX == otherX && currentY == otherY))
                {
                    d = FindDistanceBetweenTowers(currentX, currentY, otherX, otherY);

                    if (d > radius)
                    {
                        continue;
                    }
                }

                networkQuality += CalculateSignalQuality(otherQ, d);
            }

            return networkQuality;
        }
    }
}
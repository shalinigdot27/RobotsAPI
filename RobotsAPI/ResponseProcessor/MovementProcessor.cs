using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RobotsAPI.ResponseProcessor
{
    public class MovementProcessor
    {
        public Robot CalculateDistanceAndMaxBatteryLife(List<Robot> robots, int x, int y)
        {
            var FinalList = new List<Robot>();

            // Loop throuch each object and find out distance units lesser than 10 and max battery life
            foreach (var item in robots)
            {
                var distance = Math.Sqrt(Math.Pow((item.x - x), 2) + Math.Pow((item.y - y), 2));

                if (distance <= 10)
                {
                    FinalList.Add(item);
                }
            }

            // sort based on the battery life. 
            FinalList = FinalList.Any() ? FinalList.OrderByDescending((x) => x.batteryLevel).ToList() : new List<Robot>();
            return FinalList.FirstOrDefault();
        }

    }
}

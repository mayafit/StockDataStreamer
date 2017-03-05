using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockDataStreamer.Algorithms
{
    public class TrendCalculations
    {

        /// <summary>
        /// Calculates the trends.
        /// </summary>
        /// <param name="volumes">TArray holding stock volumes . Each cell
        /// in the array coresponds to a trading day. The values are sorted by the Trading date
        /// Ascending with no gaps.
        /// </param>
        /// <returns>Calculated array of trends</returns>
        public static double[] CalculateTrends(int[] volumes){
            if (volumes == null)
                throw new ArgumentException();
            double[] trends = new double[volumes.Length];
            for(int i=0;i<volumes.Length-1;i++)
                trends[i] = (volumes[i + 1] > volumes[i]) ? 1 : -1;
            return trends;
        }
        /// <summary>
        /// Calculates the OBV.
        /// </summary>
        /// <param name="volumes">The volumes.</param>
        /// <returns></returns>
        public static double [] CalculateOBV(int [] volumes)
        {
            if (volumes == null)
                throw new ArgumentException();
            double[] trends = CalculateTrends(volumes);
            double[] obvLine = new double[volumes.Length];
            for(int i=1;i<volumes.Length;i++)
                obvLine[i] = obvLine[i-1] + trends[i] * volumes[i];
            return obvLine;
        }

        /// <summary>
        /// Calculates the AD.
        /// </summary>
        /// <param name="closingPrices">The closing prices.</param>
        /// <param name="volumes">The volumes.</param>
        /// <returns></returns>
        public static double[] CalculateAD(int [] closingPrices , int[] volumes)
        {
            if (closingPrices == null || volumes == null || closingPrices.Length != volumes.Length || closingPrices.Length<5)
                throw new ArgumentException();
            double[] adValues = new double[ closingPrices.Length];
            double high5, low5,civ;
            for (int i = 5; i < closingPrices.Length; i++)
            {
                high5 = low5 = closingPrices[i-5];
                //find last 5 days Low & High
                for (int j = i - 5; j < i; j++)
                {
                    if (closingPrices[j] > high5)
                        high5 = closingPrices[j];
                    if (closingPrices[j] < low5)
                        low5 = closingPrices[j];
                }
                civ = 1000 * ((closingPrices[i]-low5) - (high5-closingPrices[i]))/(high5-low5);
                adValues[i] = adValues[i-1] + (civ*volumes[i])/1000 ;
            }
            return adValues;
        }

        public static double[] XDaysAvarage(int[] closingPrices, int numOfDays)
        {
            if (numOfDays == 0)
                return null;
            double[] averages = new double[closingPrices.Length];
            for (int i = numOfDays; i < closingPrices.Length; i++)
            {
                double sum = 0;
                for (int j = 0; j < numOfDays; j++)
                    sum += closingPrices[i - j];
                averages[i] = sum / numOfDays;
            }
            return averages;
        }
    }
}

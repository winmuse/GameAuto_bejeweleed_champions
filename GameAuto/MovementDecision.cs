using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace GameAuto
{
    public class MovementDecision
    {
        public const int BOARD_SIZE_W = 8;
        public const int BOARD_SIZE_H = 8;
        public const int DIRECTION = 4;

        private const int FIVE_MARKS = 15000;

        public static int[,] g_AllocCharacters = new int[BOARD_SIZE_H, BOARD_SIZE_W] {
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
        };

        public static int[,] g_TempCharacters = new int[BOARD_SIZE_H, BOARD_SIZE_W] {
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
        };
        public static int[,] g_Step2TempCharacters = new int[BOARD_SIZE_H, BOARD_SIZE_W] {
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
        };

        public static int[,,] g_ScoreByMovements = new int[BOARD_SIZE_H, BOARD_SIZE_W, DIRECTION];
        public static int[,,] g_ScoreByMovements2 = new int[BOARD_SIZE_H, BOARD_SIZE_W, DIRECTION];
        public static int[,,] g_Step2ScoreByMovements = new int[BOARD_SIZE_H, BOARD_SIZE_W, DIRECTION];

        //private static List<List<Point>>    g_LstIdenticals = new List<List<Point>>();

        private static List<List<Point>>    g_LstIdenticalsH = new List<List<Point>>();
        private static List<List<Point>>    g_LstIdenticalsV = new List<List<Point>>();

        //private static List<List<int>>      g_LstIdenticalItems = new List<List<int>>();
        private static List<List<int>>      g_LstIdenticalItemsH = new List<List<int>>();
        private static List<List<int>>      g_LstIdenticalItemsV = new List<List<int>>();
        private static List<Point>          g_LstRemainLands = new List<Point>();

        private static int                  g_scoreFive = 20000;
        private static int                  g_scoreLand = 600;

        public static bool CompareCharacter(int c, int k)
        {
            if (c == k && c < 11 && c > 0) return true;
            else if (c == 1 && k == 2 || c == 2 && k == 1) return true;
            else if (c == 3 && k == 4 || c == 4 && k == 3) return true;
            else if (c == 5 && k == 6 || c == 6 && k == 5) return true;
            else if (c == 7 && k == 8 || c == 8 && k == 7) return true;
            else if (c == 9 && k == 10 || c == 10 && k == 9) return true;

            return false;
        }

        public static void GetEqualLinks(int[,] arrCharacters)
        {
            g_LstIdenticalsH.Clear();
            g_LstIdenticalsV.Clear();
            g_LstIdenticalItemsH.Clear();
            g_LstIdenticalItemsV.Clear();

            int i = 0, j = 0;

            // Horizontal Search                    
            for (; i < BOARD_SIZE_H; i++)
            {
                j = 0;
                while( j < BOARD_SIZE_W)
                {
                    int c = arrCharacters[i, j]; int k = arrCharacters[i, j];
                    int ii = 0;

                    List<Point> LstIdenticals = new List<Point>();
                    List<int> LstIdenticalItems = new List<int>();
                    LstIdenticals.Add(new Point(i, j)); LstIdenticalItems.Add(k);

                    while (true)
                    {
                        ii++;
                        if (j + ii >= BOARD_SIZE_W)
                            break;

                        k = arrCharacters[i, j + ii];
                        if (CompareCharacter(c, k))
                        {
                            LstIdenticals.Add(new Point(i, j + ii));
                            LstIdenticalItems.Add(k);
                        }
                        else break;
                    }

                    if (LstIdenticals.Count > 2)
                    {
                        g_LstIdenticalsH.Add(LstIdenticals);
                        g_LstIdenticalItemsH.Add(LstIdenticalItems);
                    }

                    j += ii;
                }
            }

            i = 0;
            // Vertical Search
            for ( j = 0; j < BOARD_SIZE_W; j ++)
            {
                i = 0;
                while (i < BOARD_SIZE_H)
                {
                    int ii = 0;
                    int c = arrCharacters[i, j]; int k = arrCharacters[i, j];

                    List<Point> LstIdenticals = new List<Point>();
                    List<int> LstIdenticalItems = new List<int>();
                    LstIdenticals.Add(new Point(i, j)); LstIdenticalItems.Add(k);

                    while (true)
                    {
                        ii ++;
                        if (i + ii >= BOARD_SIZE_H)
                            break;

                        k = arrCharacters[i+ii, j];
                        if (CompareCharacter(c, k))
                        {
                            LstIdenticals.Add(new Point(i + ii, j));
                            LstIdenticalItems.Add(k);
                        }
                        else break;
                    }

                    if (LstIdenticals.Count > 2)
                    {
                        g_LstIdenticalsV.Add(LstIdenticals);
                        g_LstIdenticalItemsV.Add(LstIdenticalItems);
                    }

                    i += ii;
                }
            }
        }

        public static int CalcScores_new(int nStage)
        {
            int nTotalSum = 0, nSum = 0;

            if (Global.g_LandCount == 1)
                g_scoreLand = 10000;
            else if (Global.g_LandCount == 2)
                g_scoreLand = 4000;
            else
                g_scoreLand = 200;

            var temp = g_LstIdenticalsH.Concat(g_LstIdenticalsV);
            List<List<Point>> tempList = temp.ToList();

            var temp1 = g_LstIdenticalItemsH.Concat(g_LstIdenticalItemsV);
            List<List<int>> tempItemsList = temp1.ToList();

            bool isT = false;
            int tIndexH = -1;
            int tIndexV = -1;

            bool isFourLinks = false;
            List<int> lstFourLinkIndex = new List<int>();


            for (int pp = 0; pp < tempList.Count; pp++)
            {
                bool isWater = false;
                foreach (Point pH in tempList[pp])
                {
                    isWater = IsWater(pH, nStage == 1 ? g_TempCharacters : g_Step2TempCharacters);
                    if (isWater) break;
                }

                List<int> itemsH = tempItemsList[pp];

                if (tempList[pp].Count > 4 && isWater) return 15000;//five Links and water

                //check four links and water
                if (tempList[pp].Count == 4 && isWater)
                {
                    isFourLinks = true;
                    lstFourLinkIndex.Add(pp);
                }

                for (int qq = pp + 1; qq < tempList.Count; qq++)
                {
                    List<int> itemsV = tempItemsList[qq];
                    bool isWaterV = false;
                    foreach (Point pH in tempList[qq])
                    {
                        isWaterV = IsWater(pH, nStage == 1 ? g_TempCharacters : g_Step2TempCharacters);
                        if (isWaterV) break;
                    }

                    //check T,L links
                    foreach (Point p in tempList[qq])
                    {

                        if (tempList[pp].Contains(p) && (isWaterV || isWater))
                        {
                            isT = true;
                            tIndexH = pp;
                            tIndexV = qq;
                        }

                    }

                }
            }

            if (isT)
            {
                List<Point> lstH = tempList[tIndexH];
                List<Point> lstV = tempList[tIndexV];
                nSum += 5000;
                //case of horizontal
                foreach (Point p in lstH)
                {
                    if (!IsWater(p, nStage == 1 ? g_TempCharacters : g_Step2TempCharacters))
                        nSum += g_scoreLand;

                    for (int i = p.X - 1; i >= 0; i--)
                    {
                        if (!IsWater(i, p.Y, nStage == 1 ? g_TempCharacters : g_Step2TempCharacters))
                        {
                            if ((p.X - i) < 2 && (i - 1) >= 0)
                            {
                                nSum += g_scoreLand;
                            }
                            else if ((i - 2) >= 0)
                            {
                                nSum += g_scoreLand;
                            }

                            break;
                        }
                    }

                    for (int i = p.X + 1; i < BOARD_SIZE_H; i++)
                    {
                        if (!IsWater(i, p.Y, nStage == 1 ? g_TempCharacters : g_Step2TempCharacters))
                        {
                            nSum += g_scoreLand;
                            break;
                        }
                    }
                }

                //case of vertical
                foreach (Point p in lstV)
                {
                    if (!IsWater(p, nStage == 1 ? g_TempCharacters : g_Step2TempCharacters))
                        nSum += g_scoreLand;

                    for (int i = p.Y - 1; i >= 0; i--)
                    {
                        if (!IsWater(p.X, i, nStage == 1 ? g_TempCharacters : g_Step2TempCharacters))
                        {
                            nSum += g_scoreLand;
                            break;
                        }
                    }

                    for (int i = p.Y + 1; i < BOARD_SIZE_W; i++)
                    {
                        if (!IsWater(p.X, i, nStage == 1 ? g_TempCharacters : g_Step2TempCharacters))
                        {
                            nSum += g_scoreLand;
                            break;
                        }
                    }
                }
            }

            if (isFourLinks)
            {
                foreach (int index in lstFourLinkIndex)
                {
                    if (index == tIndexH || index == tIndexV)
                        continue;

                    List<Point> lst = tempList[index];
                    //check horizontal and vertical
                    bool isHoriz = false;
                    if (lst[0].X == lst[1].X)
                        isHoriz = true;
                    nSum += 3000;
                    if (isHoriz)
                    {
                        foreach (Point p in lst)
                        {
                            if (!IsWater(p, nStage == 1 ? g_TempCharacters : g_Step2TempCharacters))
                                nSum += g_scoreLand;
                            for (int i = p.X - 1; i >= 0; i--)
                            {
                                if (!IsWater(i, p.Y, nStage == 1 ? g_TempCharacters : g_Step2TempCharacters))
                                {
                                    if ((p.X - i) < 2 && (i - 1) >= 0)
                                    {
                                        nSum += g_scoreLand;
                                    }
                                    else if ((i - 2) >= 0)
                                    {
                                        nSum += g_scoreLand;
                                    }

                                    break;
                                }
                            }

                            for (int i = p.X + 1; i < BOARD_SIZE_H; i++)
                            {
                                if (!IsWater(i, p.Y, nStage == 1 ? g_TempCharacters : g_Step2TempCharacters))
                                {
                                    nSum += g_scoreLand;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (Point p in lst)
                        {
                            if (!IsWater(p, nStage == 1 ? g_TempCharacters : g_Step2TempCharacters))
                                nSum += g_scoreLand;
                            for (int i = p.Y - 1; i >= 0; i--)
                            {
                                if (!IsWater(p.X, i, nStage == 1 ? g_TempCharacters : g_Step2TempCharacters))
                                {
                                    nSum += g_scoreLand;
                                    break;
                                }
                            }

                            for (int i = p.Y + 1; i < BOARD_SIZE_W; i++)
                            {
                                if (!IsWater(p.X, i, nStage == 1 ? g_TempCharacters : g_Step2TempCharacters))
                                {
                                    nSum += g_scoreLand;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            int j = 0;
            foreach (List<Point> Links in tempList)
            {
                if (j == tIndexH || j == tIndexV || lstFourLinkIndex.Contains(j))
                    continue;
                bool isLand = false;
                bool isWater = false;
                int nLandCnt = 0;
                foreach (Point pt in Links)
                {
                    isLand = IsWater(pt, nStage == 1 ? g_TempCharacters : g_Step2TempCharacters) ? false : true;
                    if (isLand) nLandCnt++;
                    isWater = IsWater(pt, nStage == 1 ? g_TempCharacters : g_Step2TempCharacters) ? true : false;
                }
                if(isLand && isWater)
                {
                    nSum += g_scoreLand * nLandCnt;
                }
                else
                {
                    nSum += 100;
                }

                j++;
            }

            nTotalSum += nSum;

            return nTotalSum;
        }

        public static int CalcScores(int nStage)
        {
            int k = 0; int nTotalSum = 0, nSum = 0;

            int nLandCnt = 100;
            for ( int i = 0; i < BOARD_SIZE_H; i++)
            { 
                for ( int j = 0; j < BOARD_SIZE_W; j ++)
                {
                    if (g_TempCharacters[i, j] % 2 == 1 && g_TempCharacters[i, j] < 11 && nStage == 1)
                        nLandCnt++;
                }
            }

            int nAdvScore = 50;
            if (nLandCnt > 10) nAdvScore = 50;
            else if(nLandCnt > 5) nAdvScore = 250;
            else if (nLandCnt > 2) nAdvScore = 1000;
            else if ( nLandCnt == 2) nAdvScore = 1500;
            else nAdvScore = 2000;

            bool bIsHoriz = false;
            int T = 0;
            var temp = g_LstIdenticalsH.Concat(g_LstIdenticalsV);
            List<List<Point>> tempList = temp.ToList();

            var temp1 = g_LstIdenticalItemsH.Concat(g_LstIdenticalItemsV);
            List<List<int>> tempItemsList = temp1.ToList();

            foreach (List<Point> Link in tempList)
            {
                int nCntEqualH = 0, nCntEqualV = 0;
                Point pt0 = Link[0];
                for ( T = 1; T < Link.Count; T++)
                {
                    if (pt0.X == Link[T].X)
                        nCntEqualH++;

                    if (pt0.Y == Link[T].Y)
                        nCntEqualV++;
                }

                if (nCntEqualH == Link.Count - 1)
                    bIsHoriz = true;
                else if (nCntEqualV == Link.Count - 1)
                    bIsHoriz = false;

                nSum = 0;
                List<int> ListCharacter = tempItemsList[k];
                
                int nCntOdd = 0, nCntEven = 0;
                foreach(int Charac in ListCharacter)
                {
                    // if it contains sea increase by 3 points.
                    if (Charac > 0 && Charac % 2 == 0) nCntEven++;
                    if (Charac % 2 == 1) nCntOdd ++;
                }

                if (nCntEven > 1 && Link.Count > 4)
                    nSum = g_scoreFive;
                else if (nCntEven > 1 && Link.Count > 3)
                {
                    nSum = 2000;
                    int nRemoveCnt = 0;
                    if (nStage == 1)
                    {
                        if (bIsHoriz) // Check Stream to remove ground when it is above 4.
                        {
                            for (int x = 0; x < Link.Count; x++)
                            {
                                for (int y = 0; y < Link[x].X; y++)
                                {
                                    if (nStage == 1 && g_TempCharacters[y, Link[x].Y] % 2 == 1 && g_TempCharacters[y, Link[x].Y] < 11)
                                    {
                                        nRemoveCnt++;
                                        break;
                                    }

                                    if (nStage == 2 && g_Step2TempCharacters[y, Link[x].Y] % 2 == 1 && g_Step2TempCharacters[y, Link[x].Y] < 11)
                                    {
                                        nRemoveCnt++;
                                        break;
                                    }
                                }

                                for (int y = Link[x].X + 1; y < BOARD_SIZE_H; y++)
                                {
                                    if (nStage == 1 && g_TempCharacters[y, Link[x].Y] % 2 == 1 && g_TempCharacters[y, Link[x].Y] < 11)
                                    {
                                        nRemoveCnt++;
                                        break;
                                    }

                                    if (nStage == 2 && g_Step2TempCharacters[y, Link[x].Y] % 2 == 1 && g_Step2TempCharacters[y, Link[x].Y] < 11)
                                    {
                                        nRemoveCnt++;
                                        break;
                                    }
                                }
                            }
                        }
                        else // Vertical
                        {
                            for (int y = 0; y < Link.Count; y++)
                            {
                                for (int x = 0; x < Link[y].Y; x++)
                                {
                                    if (nStage == 1 && g_TempCharacters[Link[y].X, x] % 2 == 1 && g_TempCharacters[Link[y].X, x] < 11)
                                    {
                                        nRemoveCnt++;
                                        break;
                                    }

                                    if (nStage == 2 && g_Step2TempCharacters[Link[y].X, x] % 2 == 1 && g_Step2TempCharacters[Link[y].X, x] < 11)
                                    {
                                        nRemoveCnt++;
                                        break;
                                    }
                                }

                                for (int x = Link[y].Y + 1; x < BOARD_SIZE_W; x++)
                                {
                                    if (nStage == 1 && g_TempCharacters[Link[y].X, x] % 2 == 1 && g_TempCharacters[Link[y].X, x] < 11)
                                    {
                                        nRemoveCnt++;
                                        break;
                                    }

                                    if (nStage == 2 && g_Step2TempCharacters[Link[y].X, x] % 2 == 1 && g_Step2TempCharacters[Link[y].X, x] < 11)
                                    {
                                        nRemoveCnt++;
                                        break;
                                    }
                                }

                            }
                        }
                    }
                    
                    nSum += nAdvScore * nRemoveCnt;
                }
                else if (nCntEven == 0 || nCntOdd == 0)
                    nSum = ListCharacter.Count; 
                else
                    nSum = nCntOdd * nAdvScore + ListCharacter.Count;

                nTotalSum += nSum; k++;
            }

            if (nStage > 1)
                return nTotalSum;

            bool bFound = false;
            for ( int i = 0; i < tempList.Count-1; i ++)
            {
                //if (g_LstIdenticals[i].Count < 4)
                //    continue;
                for( int j = i+1; j < tempList.Count; j++)
                {
                    //if (g_LstIdenticals[j].Count < 4)
                    //    continue;
                    bool bSea = false;
                    foreach (int k1 in tempItemsList[i])
                    {
                        if (k1 % 2 == 0 && k1 > 0 && k1 < 11)
                        {
                            bSea = true;
                            break;
                        }
                    }

                    if (!bSea)
                    {
                        foreach (int k1 in tempItemsList[j])
                        {
                            if (k1 % 2 == 0 && k1 > 0 && k1 < 11)
                            {
                                bSea = true;
                                break;
                            }
                        }
                    }

                    if (!bSea) continue;

                    for ( int ii = 0; ii < tempList[i].Count; ii ++)
                    {
                        for ( int jj = 0; jj < tempList[j].Count; jj ++ )
                        {
                            if (tempList[i][ii].X == tempList[j][jj].X && tempList[i][ii].Y == tempList[j][jj].Y )
                            {
                                bFound = true;
                                nTotalSum += 3000;

                                int nRemoveCnt = CalcRemoveGround(nStage, tempList[i]);
                                nRemoveCnt += CalcRemoveGround(nStage, tempList[j]);
                                nTotalSum += nRemoveCnt * nAdvScore;

                                break;
                            }
                        }

                        if (bFound)
                            break;
                    }

                    if (bFound)
                        break;
                }

                if (bFound) break;
            }

            return nTotalSum;
        }
    
        public static int CalcRemoveGround(int nStage, List<Point> Link)
        {
            int nCntEqualH = 0, nCntEqualV = 0;
            Point pt0 = Link[0];
            bool bIsHoriz = false;

            for (int T = 1; T < Link.Count; T++)
            {
                if (pt0.X == Link[T].X)
                    nCntEqualH++;

                if (pt0.Y == Link[T].Y)
                    nCntEqualV++;
            }

            if (nCntEqualH == Link.Count - 1)
                bIsHoriz = true;
            else if (nCntEqualV == Link.Count - 1)
                bIsHoriz = false;

            int nRemoveCnt = 0;
            if (bIsHoriz) // Check Stream to remove ground when it is above 4.
            {
                for (int x = 0; x < Link.Count; x++)
                {
                    for (int y = 0; y < Link[x].X; y++)
                    {
                        if (nStage == 1 && g_TempCharacters[y, Link[x].Y] % 2 == 1 && g_TempCharacters[y, Link[x].Y] < 11)
                        {
                            nRemoveCnt++;
                            break;
                        }

                        if (nStage == 2 && g_Step2TempCharacters[y, Link[x].Y] % 2 == 1 && g_Step2TempCharacters[y, Link[x].Y] < 11)
                        {
                            nRemoveCnt++;
                            break;
                        }
                    }

                    for (int y = Link[x].X + 1; y < BOARD_SIZE_H; y++)
                    {
                        if (nStage == 1 && g_TempCharacters[y, Link[x].Y] % 2 == 1 && g_TempCharacters[y, Link[x].Y] < 11)
                        {
                            nRemoveCnt++;
                            break;
                        }

                        if (nStage == 2 && g_Step2TempCharacters[y, Link[x].Y] % 2 == 1 && g_Step2TempCharacters[y, Link[x].Y] < 11)
                        {
                            nRemoveCnt++;
                            break;
                        }
                    }
                }
            }
            else // Vertical
            {
                for (int y = 0; y < Link.Count; y++)
                {
                    for (int x = 0; x < Link[y].Y; x++)
                    {
                        if (nStage == 1 && g_TempCharacters[Link[y].X, x] % 2 == 1 && g_TempCharacters[Link[y].X, x] < 11)
                        {
                            nRemoveCnt++;
                            break;
                        }

                        if (nStage == 2 && g_Step2TempCharacters[Link[y].X, x] % 2 == 1 && g_Step2TempCharacters[Link[y].X, x] < 11)
                        {
                            nRemoveCnt++;
                            break;
                        }
                    }

                    for (int x = Link[y].Y + 1; x < BOARD_SIZE_W; x++)
                    {
                        if (nStage == 1 && g_TempCharacters[Link[y].X, x] % 2 == 1 && g_TempCharacters[Link[y].X, x] < 11)
                        {
                            nRemoveCnt++;
                            break;
                        }

                        if (nStage == 2 && g_Step2TempCharacters[Link[y].X, x] % 2 == 1 && g_Step2TempCharacters[Link[y].X, x] < 11)
                        {
                            nRemoveCnt++;
                            break;
                        }
                    }

                }
            }

            return nRemoveCnt;
        }

        public static bool ApplyMovementToArray(int[,] array)
        {

            var temp = g_LstIdenticalsH.Concat(g_LstIdenticalsV);
            List<List<Point>> tempList = temp.ToList();

            var temp1 = g_LstIdenticalItemsH.Concat(g_LstIdenticalItemsV);
            List<List<int>> tempItemsList = temp1.ToList();

            bool isT = false;
            int tIndexH = -1;
            int tIndexV = -1;

            bool isFourLinks = false;
            List<int> lstFourLinkIndex = new List<int>();

            List<Point> waterList = new List<Point>();

            for(int pp = 0; pp < tempList.Count; pp++)
            {
                bool isWater = false;
                foreach(Point pH in tempList[pp])
                {
                    isWater = IsWater(pH, array);
                    if (isWater) break;
                }

                List<int> itemsH = tempItemsList[pp];

                if (tempList[pp].Count > 4 && isWater) return true;//five Links and water

                //check four links and water
                if(tempList[pp].Count == 4 && isWater)
                {
                    isFourLinks = true;
                    lstFourLinkIndex.Add(pp);
                }

                for (int qq = pp + 1; qq < tempList.Count; qq++)
                {
                    List<int> itemsV = tempItemsList[qq];
                    bool isWaterV = false;
                    foreach (Point pH in tempList[qq])
                    {
                        isWaterV = IsWater(pH, array);
                        if (isWaterV) break;
                    }

                    //check T,L links
                    foreach (Point p in tempList[qq])
                    {
                        
                        if (tempList[pp].Contains(p) && (isWaterV || isWater))
                        {
                            isT = true;
                            tIndexH = pp;
                            tIndexV = qq;
                        }

                    }

                }
            }

            if (isT)
            {
                List<Point> lstH = tempList[tIndexH];
                List<Point> lstV = tempList[tIndexV];
                
                //case of horizontal
                foreach(Point p in lstH)
                {
                    for(int i = p.X - 1; i >= 0; i--)
                    {
                        if (!IsWater(i, p.Y, array))
                        {
                            waterList.Add(new Point(i, p.Y));
                            if ((p.X - i) < 2 && (i - 1) >= 0)
                            {
                                array[i - 1, p.Y] = 0;
                            }
                            else if ((i - 2) >= 0)
                            {
                                array[i - 2, p.Y] = 0;
                            }

                            break;
                        }
                    }

                    for(int i = p.X + 1; i < BOARD_SIZE_H; i++)
                    {
                        if (!IsWater(i, p.Y, array))
                        {
                            waterList.Add(new Point(i, p.Y));
                            array[i, p.Y] = 0;
                            break;
                        }
                    }
                }
                //case of vertical
                foreach (Point p in lstV)
                {
                    for (int i = p.Y - 1; i >= 0; i--)
                    {
                        if (!IsWater(p.X, i, array))
                        {
                            waterList.Add(new Point(p.X, i));
                            array[p.X, i] = 0;
                            break;
                        }
                    }

                    for (int i = p.Y + 1; i < BOARD_SIZE_W; i++)
                    {
                        if (!IsWater(p.X, i, array))
                        {
                            waterList.Add(new Point(p.X, i));
                            array[p.X, i] = 0;
                            break;
                        }
                    }
                }
            }

            if (isFourLinks)
            {
                foreach(int index in lstFourLinkIndex)
                {
                    if (index == tIndexH || index == tIndexV)
                        continue;

                    List<Point> lst = tempList[index];
                    //check horizontal and vertical
                    bool isHoriz = false;
                    if (lst[0].X == lst[1].X)
                        isHoriz = true;

                    if (isHoriz)
                    {
                        foreach (Point p in lst)
                        {
                            for (int i = p.X - 1; i >= 0; i--)
                            {
                                if (!IsWater(i, p.Y, array))
                                {
                                    if ((p.X - i) < 2 && (i - 1) >= 0)
                                    {
                                        array[i - 1, p.Y] = 0;
                                    }
                                    else if ((i - 2) >= 0)
                                    {
                                        array[i - 2, p.Y] = 0;
                                    }
                                    waterList.Add(new Point(i, p.Y));
                                    break;
                                }
                            }

                            for (int i = p.X + 1; i < BOARD_SIZE_H; i++)
                            {
                                if (!IsWater(i, p.Y, array))
                                {
                                    array[i, p.Y] = 0;
                                    waterList.Add(new Point(i, p.Y));
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (Point p in lst)
                        {
                            for (int i = p.Y - 1; i >= 0; i--)
                            {
                                if (!IsWater(p.X, i, array))
                                {
                                    waterList.Add(new Point(p.X, i));
                                    array[p.X, i] = 0;
                                    break;
                                }
                            }

                            for (int i = p.Y + 1; i < BOARD_SIZE_W; i++)
                            {
                                if (!IsWater(p.X, i, array))
                                {
                                    waterList.Add(new Point(p.X, i));
                                    array[p.X, i] = 0;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            foreach (List<Point> Links in tempList)
            {
                foreach (Point pt in Links)
                {
                    array[pt.X, pt.Y] = 0;
                }
            }

            int j = 0, k = 0, nDelta = 1;
            for( int i = 0; i < BOARD_SIZE_W; i ++)
            {
                j = BOARD_SIZE_H - 1;
                while (j > 0)
                {
                    if (array[j, i] == 0)
                    {
                        k = j - 1;
                        nDelta = 1;

                        while(k >= 0)
                        {
                            if (array[k, i] >= (int)Global.CHARACTER_TYPE.CHAR_WOOD || array[k, i] == 0)
                            {
                                k --;
                                nDelta ++;
                                continue;
                            }

                            array[k+nDelta, i] = array[k, i];
                            k --;
                        }

                        for (int t = 0; t < nDelta; t ++)
                        {
                            array[t, i] = 12;
                        }
                    }

                    j--;
                }
            }

            //Fill Water
            foreach(Point p in waterList)
            {
                if(array[p.X, p.Y] % 2 == 1)
                    array[p.X, p.Y] = array[p.X, p.Y] + 1;
            }
            /*Random rnd = new Random();
            for (int i = 0; i < BOARD_SIZE_H; i++)
            {
                for (j = 0; j < BOARD_SIZE_W; j++)
                {
                    // still need to fill, then randomly fill it.
                    if (array[i, j] == 0)
                    {
                        array[i, j] = 12;
                        //array[i, j] = rnd.Next(1,9);
                        //if (array[i, j] % 2 == 1) array[i, j]++;
                    }
                }
            }*/

            return false;
        }

        public static bool IsWater (Point p, int[,] array)
        {
            return IsValidItem(p, array) && array[p.X, p.Y] % 2 == 0;
        }

        public static bool IsWater(int x, int y, int[,] array)
        {
            return IsValidItem(x, y, array) && array[x, y] % 2 == 0;
        }

        public static bool IsValidItem(Point p, int[,] array)
        {
            return (array[p.X, p.Y] > 0 && array[p.X, p.Y] < 11) || array[p.X, p.Y] == 12;
        }

        public static bool IsValidItem(int x, int y, int[,] array)
        {
            return (array[x, y] > 0 && array[x, y] < 11) || array[x, y] == 12;
        }

        public static void RefreshRemainLandsList(int[,] array)
        {
            g_LstRemainLands.Clear();
            for( int i = 0; i < BOARD_SIZE_H; i ++)
            {
                for ( int j = 0; j < BOARD_SIZE_W; j ++ )
                {
                    if (array[i,j] % 2 == 1)
                    {
                        g_LstRemainLands.Add(new Point(i, j));
                    }
                }
            }
        }

        private static int ProcessStep2Movement()
        {
            for (int i = 0; i < BOARD_SIZE_H; i++)
            {
                for (int j = 0; j < BOARD_SIZE_W; j++)
                {
                    g_Step2ScoreByMovements[i, j, 0] = g_Step2ScoreByMovements[i, j, 1] = g_Step2ScoreByMovements[i, j, 2] = g_Step2ScoreByMovements[i, j, 3] = 0;
                    // not empty & not wood
                    if (g_TempCharacters[i, j] != 0 && g_TempCharacters[i, j] < 11)
                    {
                        g_Step2ScoreByMovements[i, j, 0] = TryStep2SwapWithTop(i, j);
                        g_Step2ScoreByMovements[i, j, 1] = TryStep2SwapWithLeft(i, j);
                        g_Step2ScoreByMovements[i, j, 2] = TryStep2SwapWithRight(i, j);
                        g_Step2ScoreByMovements[i, j, 3] = TryStep2SwapWithBottom(i, j);
                    }
                }
            }

            int nMaxVal = 0;

            for (int i = 0; i < BOARD_SIZE_H; i++)
            {
                for (int j = 0; j < BOARD_SIZE_W; j++)
                {
                    for (int k = 0; k < DIRECTION; k++)
                    {
                        if (nMaxVal < g_Step2ScoreByMovements[i, j, k])
                            nMaxVal = g_Step2ScoreByMovements[i, j, k];
                    }
                }
            }

            return nMaxVal;
        }

        private static void PrintArray(int[,] array)
        {
            for(int i = 0; i < BOARD_SIZE_H; i++)
            {
                Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}",array[i,0], array[i, 1], array[i, 2], array[i, 3], array[i, 4], array[i, 5], array[i, 6]);
            }
        }

        public static int TrySwapping(int i, int j, int d)
        {
            GetEqualLinks(g_TempCharacters);
            //RefreshRemainLandsList(g_TempCharacters);

            int nScore = CalcScores_new(1);
            int nTotalScore = nScore;
            
            /*-------------Debug----------------*/
            if(nScore > 0)
            {

                Console.WriteLine("Score: {0}", nScore);
                Console.WriteLine("i: {0}, j: {1}, d: {2}", i, j, d);
                Console.WriteLine("Before Apply");
                PrintArray(g_TempCharacters);

            }

            int nRepeatCnt = 0;
            bool isFiveLinks = false;
            while (nScore > 1) // predict 2 times
            {
                isFiveLinks = ApplyMovementToArray(g_TempCharacters);

                if (isFiveLinks)
                {
                    nTotalScore += g_scoreFive;
                    break;
                }
                
                GetEqualLinks(g_TempCharacters);
                nScore = CalcScores_new(1);

                /*-------------Debug----------------*/
                Console.WriteLine("Score: {0}", nScore);
                Console.WriteLine("After Apply");
                PrintArray(g_TempCharacters);

                nTotalScore += nScore;
                nRepeatCnt ++;
            }

            if (isFiveLinks)
            {
                g_ScoreByMovements2[i, j, d] = 0;
                return nTotalScore;
            }
            int nStep2Score = 0;
            if (nTotalScore > 1)
                nStep2Score = ProcessStep2Movement();

            g_ScoreByMovements2[i, j, d] = nStep2Score;
            
            nTotalScore += nStep2Score;

            return nTotalScore;
        }

        public static void CopyCharactersToStep2Temp()
        {
            for (int i = 0; i < BOARD_SIZE_H; i++)
            {
                for (int j = 0; j < BOARD_SIZE_W; j++)
                {
                    g_Step2TempCharacters[i, j] = g_TempCharacters[i, j];
                }
            }
        }

        public static int TryStep2SwapWithTop(int i, int j)
        {
            if (i < 1) return 0;
            if (g_TempCharacters[i-1, j] >= 11) // wood
                return 0;
            
            CopyCharactersToStep2Temp();

            int nTemp = g_Step2TempCharacters[i - 1, j];
            g_Step2TempCharacters[i - 1, j] = g_Step2TempCharacters[i, j];
            g_Step2TempCharacters[i, j] = nTemp;

            return TryStep2Swapping(i, j , 0);
        }

        public static int TryStep2SwapWithLeft(int i, int j)
        {
            if (j < 1) return 0;
            if (g_TempCharacters[i, j - 1] >= 11) // wood
                return 0;
            
            CopyCharactersToStep2Temp();

            int nTemp = g_Step2TempCharacters[i, j - 1];
            g_Step2TempCharacters[i, j - 1] = g_Step2TempCharacters[i, j];
            g_Step2TempCharacters[i, j] = nTemp;

            return TryStep2Swapping(i, j, 1);
        }

        public static int TryStep2SwapWithRight(int i, int j)
        {
            if (j >= BOARD_SIZE_W - 1) return 0;
            if (g_TempCharacters[i, j + 1] >= 11) // wood
                return 0;
            
            CopyCharactersToStep2Temp();
            
            int nTemp = g_Step2TempCharacters[i, j + 1];
            g_Step2TempCharacters[i, j + 1] = g_Step2TempCharacters[i, j];
            g_Step2TempCharacters[i, j] = nTemp;

            return TryStep2Swapping(i, j, 2);
        }

        public static int TryStep2SwapWithBottom(int i, int j)
        {
            if (i >= BOARD_SIZE_H - 1) return 0;
            if (g_TempCharacters[i + 1, j] >= 11) // wood
                return 0;

            CopyCharactersToStep2Temp();

            int nTemp = g_Step2TempCharacters[i + 1, j];
            g_Step2TempCharacters[i + 1, j] = g_Step2TempCharacters[i, j];
            g_Step2TempCharacters[i, j] = nTemp;

            return TryStep2Swapping(i, j, 3);
        }

        public static int TryStep2Swapping(int i, int j, int d)
        {
            GetEqualLinks(g_Step2TempCharacters);
            //RefreshRemainLandsList(g_TempCharacters);

            int nScore = CalcScores_new(2);
            int nTotalScore = nScore;
            
            /*-------------Debug----------------*/
            if(nScore > 0)
            {
                Console.WriteLine("i: {0}, j: {1}, d:{2}", i, j, d);
                Console.WriteLine("2Before Apply");
                PrintArray(g_Step2TempCharacters);
            }

            int nRepeatCnt = 0;
            while (nScore > 1) // predict 2 times
            {


                if (ApplyMovementToArray(g_Step2TempCharacters))
                {
                    nScore = 0;
                    nTotalScore += g_scoreFive;
                }
                else
                {
                    /*-------------Debug----------------*/
                    Console.WriteLine("2After Apply");
                    PrintArray(g_Step2TempCharacters);

                    GetEqualLinks(g_Step2TempCharacters);
                    nScore = CalcScores_new(2);
                    nTotalScore += nScore;
                    nRepeatCnt++;
                }
            }

            return nTotalScore;
        }

        public static int TrySwapWithTop(int i, int j)
        {
            if (i < 1) return 0;
            if (g_AllocCharacters[i-1, j] >= 11) // wood
                return 0;

            CopyCharactersToTemp();

            int nTemp = g_TempCharacters[i-1, j];
            g_TempCharacters[i-1, j] = g_TempCharacters[i, j];
            g_TempCharacters[i, j] = nTemp;

            return TrySwapping(i, j, 0);
        }

        public static int TrySwapWithLeft(int i, int j)
        {
            if (j < 1) return 0;
            if (g_AllocCharacters[i, j-1] >= 11) // wood
                return 0;

            CopyCharactersToTemp();

            int nTemp = g_TempCharacters[i, j-1];
            g_TempCharacters[i, j-1] = g_TempCharacters[i, j];
            g_TempCharacters[i, j] = nTemp;

            return TrySwapping(i, j, 1);
        }

        public static int TrySwapWithRight(int i, int j)
        {
            if (j >= BOARD_SIZE_W-1) return 0;
            if (g_AllocCharacters[i, j+1] >= 11) // wood
                return 0;

            CopyCharactersToTemp();

            int nTemp = g_TempCharacters[i, j+1];
            g_TempCharacters[i, j+1] = g_TempCharacters[i, j];
            g_TempCharacters[i, j] = nTemp;

            return TrySwapping(i, j, 2);
        }

        public static int TrySwapWithBottom(int i, int j)
        {
            if (i >= BOARD_SIZE_H - 1) return 0;
            if (g_AllocCharacters[i+1, j] >= 11) // wood
                return 0;

            CopyCharactersToTemp();

            int nTemp = g_TempCharacters[i+1, j];
            g_TempCharacters[i+1, j] = g_TempCharacters[i, j];
            g_TempCharacters[i, j]   = nTemp;

            return TrySwapping(i, j, 3);
        }

        public static void CopyCharactersToTemp()
        {
            for (int i = 0; i < BOARD_SIZE_H; i++)
            {
                for (int j = 0; j < BOARD_SIZE_W; j++)
                {
                    g_TempCharacters[i, j] = g_AllocCharacters[i, j];
                }
            }
        }
        /*public static int Process()
        {
            Global.g_moveStep1 = Global.MOVEMENT.Empty;
            Global.g_moveStep2 = Global.MOVEMENT.Empty;

            for (int i = 0; i < BOARD_SIZE_H; i++)
            {
                for (int j = 0; j < BOARD_SIZE_W; j++)
                {
                    g_ScoreByMovements[i, j, 0] = g_ScoreByMovements[i, j, 1] = g_ScoreByMovements[i, j, 2] = g_ScoreByMovements[i, j, 3] = 0;
                    g_ScoreByMovements2[i, j, 0] = g_ScoreByMovements2[i, j, 1] = g_ScoreByMovements2[i, j, 2] = g_ScoreByMovements2[i, j, 3] = 0;

                    // not empty & not wood
                    if (g_AllocCharacters[i, j] != 0 && g_AllocCharacters[i, j] < 11)
                    {
                        g_ScoreByMovements[i, j, 0] = TrySwapWithTop(i, j);
                        g_ScoreByMovements[i, j, 1] = TrySwapWithLeft(i, j);
                        g_ScoreByMovements[i, j, 2] = TrySwapWithRight(i, j);
                        g_ScoreByMovements[i, j, 3] = TrySwapWithBottom(i, j);
                    }
                }
            }

            int nMaxI = 0, nMaxJ = 0, nMaxDirection = 0;
            int nMaxI2 = 0, nMaxJ2 = 0, nMaxDirection2 = 0;
            int nMaxVal1 = 0, nMaxVal2 = 0;

            for (int i = 0; i < BOARD_SIZE_H; i++)
            {
                for (int j = 0; j < BOARD_SIZE_W; j++)
                {
                    for (int k = 0; k < DIRECTION; k++)
                    {
                        if (nMaxVal1 < (g_ScoreByMovements[i, j, k] - g_ScoreByMovements2[i, j, k]))
                        {
                            nMaxVal1 = g_ScoreByMovements[i, j, k] - g_ScoreByMovements2[i, j, k];
                            nMaxI = i; nMaxJ = j; nMaxDirection = k;
                        }

                        if (nMaxVal2 < g_ScoreByMovements2[i, j, k])
                        {
                            nMaxVal2 = g_ScoreByMovements2[i, j, k];
                            nMaxI2 = i; nMaxJ2 = j; nMaxDirection2 = k;
                        }

                        //if (nMaxVal2 < g_ScoreByMovements[i, j, k])
                        //{
                        //    nMaxVal2 = g_ScoreByMovements[i, j, k];
                        //    nMaxI2 = i; nMaxJ2 = j; nMaxDirection2 = k;
                        //}
                    }
                }
            }

            if (nMaxVal1 < 1 && nMaxVal2 < 1)
                return 0;
            //if (nMaxVal2 < 1) return 0;

            Global.g_moveStep1.nX = nMaxI; Global.g_moveStep1.nY = nMaxJ;
            Global.g_moveStep1.nD = nMaxDirection; Global.g_moveStep1.nScore = nMaxVal1;
            Global.g_moveStep2.nX = nMaxI2; Global.g_moveStep2.nY = nMaxJ2;
            Global.g_moveStep2.nD = nMaxDirection2; Global.g_moveStep2.nScore = nMaxVal2;

            if (Global.g_bAssistantMode)
                return 0;

            if (nMaxVal1 >= 20000)
                EmulateMovement(nMaxI, nMaxJ, nMaxDirection);
            else if (nMaxVal2 >= 20000)
                EmulateMovement(nMaxI2, nMaxJ2, nMaxDirection2);
            else if (nMaxVal1 >= 5000)
                EmulateMovement(nMaxI, nMaxJ, nMaxDirection);
            else if (nMaxVal2 >= 5000)
                EmulateMovement(nMaxI2, nMaxJ2, nMaxDirection2);
            else if (nMaxVal1 >= 3000)
                EmulateMovement(nMaxI, nMaxJ, nMaxDirection);
            else if (nMaxVal2 >= 3000)
                EmulateMovement(nMaxI2, nMaxJ2, nMaxDirection2);
            else if (nMaxVal1 > 1)
                EmulateMovement(nMaxI, nMaxJ, nMaxDirection);
            else
                EmulateMovement(nMaxI2, nMaxJ2, nMaxDirection2);

            return 0;
        }*/
        /*public static int Process()
        {
            int nMaxI = 0;
            int nMaxJ = 0;
            int nMaxDirection = 1;
            int step = 1;
            bool bBox = false;

            int nMaxScore = 0;
            for (int i = 0; i < BOARD_SIZE_H; i++)
            {
                Init_g_TempCharacters();
                int j = 0;
                for (int s = 0; s < BOARD_SIZE_W - 1; s++)
                {
                    if (i == 1 && s == 1)
                        j = 0;
                    if (g_AllocCharacters[i, j] != 0)
                    {
                        MoveLeft_g_TempCharacters(i, 1);
                    }
                    Global.g_Box = false;
                    int buf = Score_g_TempCharacters();
                    if ((buf > nMaxScore) || (Global.g_Box == true && buf == nMaxScore))
                    {
                        nMaxI = i;
                        nMaxJ = j;
                        nMaxDirection = 1;
                        step = s + 1;
                        bBox = Global.g_Box;
                        nMaxScore = buf;
                    }
                }
            }

            for (int j = 0; j < BOARD_SIZE_W; j++)
            {
                Init_g_TempCharacters();
                int i = 0;
                for (int s = 0; s < BOARD_SIZE_H - 1; s++)
                {
                    if (j == 1)
                        i = 0;
                    if (g_AllocCharacters[i, j] != 0)
                    {
                        MoveDown_g_TempCharacters(j, 1);
                    }
                    Global.g_Box = false;
                    int buf = Score_g_TempCharacters();
                    if ((buf > nMaxScore) || (Global.g_Box == true && buf == nMaxScore))
                    {
                        nMaxI = i;
                        nMaxJ = j;
                        nMaxDirection = 0;
                        step = s + 1;
                        bBox = Global.g_Box;
                        nMaxScore = buf;
                    }
                }
            }
            if (nMaxDirection == 0)
                nMaxI = BOARD_SIZE_H - 1;
            if (nMaxDirection == 1)
                nMaxJ = BOARD_SIZE_W - 1;
            EmulateMovement(nMaxI, nMaxJ, nMaxDirection, step);

            return 0;
        }*/
        public static int Process()
        {
            int step = 1;
            for (int i = 0; i < BOARD_SIZE_H; i++)
            {
                for (int j = 0; j < BOARD_SIZE_W-1; j++)
                {
                    Init_g_TempCharacters();
                    if (Score_g_TempCharacters() > 0)
                        return 0;
                    MoveRight_g_TempCharacters(i, j);
                    if(Score_g_TempCharactersAll(i, j) > 0)
                    {
                        /*int nMaxDirection = 2;

                        EmulateMovement(i, j, nMaxDirection, step);*/
                    }
                    Global.g_Box = false;
                    int buf = Score_g_TempCharacters5();
                    if (buf > 0)
                    {
                        int nMaxDirection = 2;

                        EmulateMovement(i, j, nMaxDirection, step);                        
                    }
                }
            }
            if (Global.license_Verify == false)
                return 0;
            for (int j = 0; j < BOARD_SIZE_W; j++)
            {
                for (int i = 0; i < BOARD_SIZE_H - 1; i++)
                {
                    Init_g_TempCharacters();
                    /*if (Score_g_TempCharacters() > 0)
                        return 0;*/
                    MoveDown_g_TempCharacters(i, j);
                    Global.g_Box = false;
                    int buf = Score_g_TempCharacters5();
                    if (buf > 0)
                    {
                        int nMaxDirection = 3;

                        EmulateMovement(i, j, nMaxDirection, step);
                    }
                }
            }

            //----------------------------------------------------------------------------------------------
            for (int i = 0; i < BOARD_SIZE_H; i++)
            {
                for (int j = 0; j < BOARD_SIZE_W - 1; j++)
                {
                    Init_g_TempCharacters();
                    if (Score_g_TempCharacters() > 0)
                        return 0;
                    MoveRight_g_TempCharacters(i, j);
                    Global.g_Box = false;
                    int buf = Score_g_TempCharacters4();
                    if (buf > 0)
                    {
                        int nMaxDirection = 2;

                        EmulateMovement(i, j, nMaxDirection, step);                        
                    }
                }
            }
            if (Global.license_Verify == false)
                return 0;
            for (int j = 0; j < BOARD_SIZE_W; j++)
            {
                for (int i = 0; i < BOARD_SIZE_H - 1; i++)
                {
                    Init_g_TempCharacters();
                    /*if (Score_g_TempCharacters() > 0)
                        return 0;*/
                    MoveDown_g_TempCharacters(i, j);
                    Global.g_Box = false;
                    int buf = Score_g_TempCharacters4();
                    if (buf > 0)
                    {
                        int nMaxDirection = 3;

                        EmulateMovement(i, j, nMaxDirection, step);
                    }
                }
            }

            //----------------------------------------------------------------------------------------------
            for (int i = 0; i < BOARD_SIZE_H; i++)
            {
                for (int j = 0; j < BOARD_SIZE_W - 1; j++)
                {
                    Init_g_TempCharacters();
                    if (Score_g_TempCharacters() > 0)
                        return 0;
                    MoveRight_g_TempCharacters(i, j);
                    Global.g_Box = false;
                    int buf = Score_g_TempCharacters3();
                    if (buf > 0)
                    {
                        int nMaxDirection = 2;

                        EmulateMovement(i, j, nMaxDirection, step);
                    }
                }
            }
            if (Global.license_Verify == false)
                return 0;
            for (int j = 0; j < BOARD_SIZE_W; j++)
            {
                for (int i = 0; i < BOARD_SIZE_H - 1; i++)
                {
                    Init_g_TempCharacters();
                    /*if (Score_g_TempCharacters() > 0)
                        return 0;*/
                    MoveDown_g_TempCharacters(i, j);
                    Global.g_Box = false;
                    int buf = Score_g_TempCharacters3();
                    if (buf > 0)
                    {
                        int nMaxDirection = 3;

                        EmulateMovement(i, j, nMaxDirection, step);
                    }
                }
            }
            return 0;
        }
        public static void Init_g_TempCharacters()
        {
            for (int j = 0; j < BOARD_SIZE_W; j++)
                for (int i = 0; i < BOARD_SIZE_H; i++)
                    g_TempCharacters[i, j] = g_AllocCharacters[i, j];
        }
        /*public static int Score_g_TempCharacters()
        {
            int total_score = 0;
            int score = 0;
            int same_buf = 0;
            bool box = false;
            int box_pos = -1;
            for (int i = 0; i < BOARD_SIZE_W; i++)
            {
                same_buf = 0;
                score = 0;
                box = false;
                box_pos = -1;
                for (int j = 0; j < BOARD_SIZE_H; j++)
                {
                    int current_buf = g_TempCharacters[i, j];
                    if (current_buf >= 10)
                        current_buf = g_TempCharacters[i, j] / 10;
                    
                    if (same_buf == current_buf)
                    {
                        score++;
                        if (g_TempCharacters[i, j] == 10 || g_TempCharacters[i, j] == 30)
                        {
                            box_pos = j;
                            box = true;
                        }
                        else if(g_TempCharacters[i, j - 1] == 10 || g_TempCharacters[i, j - 1] == 30)
                        {
                            box_pos = j - 1;
                            box = true;
                        }
                        if (j == BOARD_SIZE_H-1)
                        {
                            if (score > 1)
                            {
                                total_score += score;
                                if (box)
                                    Global.g_Box = true;
                                *//*if (box = true && Global.BALL_POS == box_pos)
                                    total_score += 100;*//*
                            }
                            if (score > 3)
                                total_score += 10;
                        }
                    }
                    else
                    {
                        if (score > 1)
                        {
                            total_score += score;
                            if (box)
                                Global.g_Box = true;
                            *//*if (box = true && Global.BALL_POS == box_pos)
                                total_score += 100;*//*
                        }
                        if (score > 3)
                            total_score += 10;
                        score = 0;
                        if(g_TempCharacters[i, j] >= 10)
                            same_buf = g_TempCharacters[i, j]/10;
                        else
                            same_buf = g_TempCharacters[i, j];
                    }
                }
            }

            same_buf = 0;

            for (int j = 0; j < BOARD_SIZE_H; j++)
            {
                same_buf = 0;
                score = 0;
                box = false;
                box_pos = -1;
                for (int i = 0; i < BOARD_SIZE_W; i++)
                {
                    int current_buf = g_TempCharacters[i, j];
                    if (current_buf >= 10)
                        current_buf = g_TempCharacters[i, j] / 10;
                    
                    if (same_buf == current_buf)
                    {
                        score++;
                        if(g_TempCharacters[i, j] == 10 || g_TempCharacters[i, j] == 30)
                        {
                            box_pos = j;
                            box = true;
                        }
                        else if(g_TempCharacters[i - 1, j] == 10 || g_TempCharacters[i - 1, j] == 30)
                        {
                            box_pos = j;
                            box = true;
                        }
                        *//*if (g_TempCharacters[i-1, j] >= 10)
                        {
                            box_pos = j;
                            box = true;
                        }*//*
                        if (j == BOARD_SIZE_W - 1)
                        {
                            if (score > 1)
                            {
                                total_score += score;
                                if (box)
                                    Global.g_Box = true;
                                *//*if(box = true && Global.BALL_POS == box_pos)
                                    total_score += 100;*//*
                            }
                            if(score > 3)
                                total_score += 10;
                        }
                    }
                    else
                    {
                        if (score > 1)
                        {
                            total_score += score;
                            if (box)
                                Global.g_Box = true;
                            *//*if (box = true && Global.BALL_POS == box_pos)
                                total_score += 100;*//*
                        }
                        if (score > 3)
                            total_score += 10;
                        score = 0;
                        if (g_TempCharacters[i, j] >= 10)
                            same_buf = g_TempCharacters[i, j] / 10;
                        else
                            same_buf = g_TempCharacters[i, j];
                    }  
                }
            }
            return total_score;
        }*/
        public static int Score_g_TempCharactersAll(int i, int j)
        {
            int total_score = 0;
            if (g_TempCharacters[i, j] == 9)
                total_score = 5;
            return total_score;
        }
        public static int Score_g_TempCharacters5()
        {
            int total_score = 0;
            int score = 0;
            int same_buf = 0;
            for (int i = 0; i < BOARD_SIZE_W; i++)
            {
                same_buf = 0;
                score = 0;

                for (int j = 0; j < BOARD_SIZE_H; j++)
                {
                    int current_buf = g_TempCharacters[i, j];
                    if (current_buf >= 10)
                        current_buf = g_TempCharacters[i, j] / 10;

                    if (same_buf == current_buf && current_buf != 8)
                    {
                        score++;

                        {
                            if (score > 3)
                            {
                                total_score += score;
                                //---->j,j-1,j-2,j-3,j-4
                                g_AllocCharacters[i, j] = 8;
                                g_AllocCharacters[i, j - 1] = 8;
                                if (i > 0)
                                    if (g_AllocCharacters[i - 1, j - 2] == same_buf)
                                        g_AllocCharacters[i - 1, j - 2] = 8;
                                if (i < BOARD_SIZE_W - 1)
                                    if (g_AllocCharacters[i + 1, j - 2] == same_buf)
                                        g_AllocCharacters[i + 1, j - 2] = 8;
                                g_AllocCharacters[i, j - 3] = 8;
                                g_AllocCharacters[i, j - 4] = 8;
                            }
                        }
                    }
                    else
                    {
                        if (score > 3)
                        {
                        }
                        score = 0;
                        if (g_TempCharacters[i, j] >= 10)
                            same_buf = g_TempCharacters[i, j] / 10;
                        else
                            same_buf = g_TempCharacters[i, j];
                    }
                }
            }

            for (int j = 0; j < BOARD_SIZE_H; j++)
            {
                same_buf = 0;
                score = 0;

                for (int i = 0; i < BOARD_SIZE_W; i++)
                {
                    int current_buf = g_TempCharacters[i, j];
                    if (current_buf >= 10)
                        current_buf = g_TempCharacters[i, j] / 10;

                    if (same_buf == current_buf && current_buf != 8)
                    {
                        score++;
                        {
                            if (score > 3)
                            {
                                total_score += score;
                                //---->i,i-1,i-2,i-3,i-4
                                g_AllocCharacters[i, j] = 8;
                                g_AllocCharacters[i - 1, j] = 8;
                                if (j > 0)
                                    if (g_AllocCharacters[i - 2, j - 1] == same_buf)
                                        g_AllocCharacters[i - 2, j - 1] = 8;
                                if (j < BOARD_SIZE_H - 1)
                                    if (g_AllocCharacters[i - 2, j + 1] == same_buf)
                                        g_AllocCharacters[i - 2, j + 1] = 8;
                                g_AllocCharacters[i - 3, j] = 8;
                                g_AllocCharacters[i - 4, j] = 8;
                            }
                        }
                    }
                    else
                    {
                        if (score > 3)
                        {
                        }
                        score = 0;
                        if (g_TempCharacters[i, j] >= 10)
                            same_buf = g_TempCharacters[i, j] / 10;
                        else
                            same_buf = g_TempCharacters[i, j];
                    }
                }
            }
            return total_score;
        }
        public static int Score_g_TempCharacters4()
        {
            int total_score = 0;
            int score = 0;
            int same_buf = 0;
            for (int i = 0; i < BOARD_SIZE_W; i++)
            {
                same_buf = 0;
                score = 0;

                for (int j = 0; j < BOARD_SIZE_H; j++)
                {
                    int current_buf = g_TempCharacters[i, j];
                    if (current_buf >= 10)
                        current_buf = g_TempCharacters[i, j] / 10;

                    if (same_buf == current_buf && current_buf != 8)
                    {
                        score++;

                        {
                            if (score > 2)
                            {
                                total_score += score;
                                //---->j,j-1,j-2,j-3
                                g_AllocCharacters[i, j] = 8;
                                if (g_AllocCharacters[i, j - 1] == same_buf)
                                    g_AllocCharacters[i, j - 1] = 8;
                                else
                                {
                                    if (i > 0)
                                        if (g_AllocCharacters[i - 1, j - 1] == same_buf)
                                            g_AllocCharacters[i - 1, j - 1] = 8;
                                    if (i < BOARD_SIZE_W - 1)
                                        if (g_AllocCharacters[i + 1, j - 1] == same_buf)
                                            g_AllocCharacters[i + 1, j - 1] = 8;
                                }
                                

                                if (g_AllocCharacters[i, j - 2] == same_buf)
                                    g_AllocCharacters[i, j - 2] = 8;
                                else
                                {
                                    if (i > 0)
                                        if (g_AllocCharacters[i - 1, j - 2] == same_buf)
                                            g_AllocCharacters[i - 1, j - 2] = 8;
                                    if (i < BOARD_SIZE_W - 1)
                                        if (g_AllocCharacters[i + 1, j - 2] == same_buf)
                                            g_AllocCharacters[i + 1, j - 2] = 8;
                                }
                                

                                if (g_AllocCharacters[i, j - 3] == same_buf)
                                    g_AllocCharacters[i, j - 3] = 8;
                                else
                                {
                                    if (i > 0)
                                        if (g_AllocCharacters[i - 1, j - 3] == same_buf)
                                            g_AllocCharacters[i - 1, j - 3] = 8;
                                    if (i < BOARD_SIZE_W - 1)
                                        if (g_AllocCharacters[i + 1, j - 3] == same_buf)
                                            g_AllocCharacters[i + 1, j - 3] = 8;
                                }
                                
                            }
                        }
                    }
                    else
                    {
                        if (score > 2)
                        {
                        }
                        score = 0;
                        if (g_TempCharacters[i, j] >= 10)
                            same_buf = g_TempCharacters[i, j] / 10;
                        else
                            same_buf = g_TempCharacters[i, j];
                    }
                }
            }

            for (int j = 0; j < BOARD_SIZE_H; j++)
            {
                same_buf = 0;
                score = 0;

                for (int i = 0; i < BOARD_SIZE_W; i++)
                {
                    int current_buf = g_TempCharacters[i, j];
                    if (current_buf >= 10)
                        current_buf = g_TempCharacters[i, j] / 10;

                    if (same_buf == current_buf && current_buf != 8)
                    {
                        score++;
                        {
                            if (score > 2)
                            {
                                total_score += score;
                                //---->i,i-1,i-2,i-3
                                g_AllocCharacters[i, j] = 8;
                                if(g_AllocCharacters[i - 1, j] == same_buf)
                                    g_AllocCharacters[i - 1, j] = 8;
                                else
                                {
                                    if (j > 0)
                                        if (g_AllocCharacters[i - 1, j - 1] == same_buf)
                                            g_AllocCharacters[i - 1, j - 1] = 8;
                                    if (j < BOARD_SIZE_H - 1)
                                        if (g_AllocCharacters[i - 1, j + 1] == same_buf)
                                            g_AllocCharacters[i - 1, j + 1] = 8;
                                }
                                

                                if (g_AllocCharacters[i - 2, j] == same_buf)
                                    g_AllocCharacters[i - 2, j] = 8;
                                else
                                {
                                    if (j > 0)
                                        if (g_AllocCharacters[i - 2, j - 1] == same_buf)
                                            g_AllocCharacters[i - 2, j - 1] = 8;
                                    if (j < BOARD_SIZE_H - 1)
                                        if (g_AllocCharacters[i - 2, j + 1] == same_buf)
                                            g_AllocCharacters[i - 2, j + 1] = 8;
                                }
                                

                                if (g_AllocCharacters[i - 3, j] == same_buf)
                                    g_AllocCharacters[i - 3, j] = 8;
                                else
                                {
                                    if (j > 0)
                                        if (g_AllocCharacters[i - 3, j - 1] == same_buf)
                                            g_AllocCharacters[i - 3, j - 1] = 8;
                                    if (j < BOARD_SIZE_H - 1)
                                        if (g_AllocCharacters[i - 3, j + 1] == same_buf)
                                            g_AllocCharacters[i - 3, j + 1] = 8;
                                }
                                
                            }
                        }
                    }
                    else
                    {
                        if (score > 2)
                        {
                        }
                        score = 0;
                        if (g_TempCharacters[i, j] >= 10)
                            same_buf = g_TempCharacters[i, j] / 10;
                        else
                            same_buf = g_TempCharacters[i, j];
                    }
                }
            }
            return total_score;
        }
        public static int Score_g_TempCharacters3()
        {
            int total_score = 0;
            int score = 0;
            int same_buf = 0;
            for (int i = 0; i < BOARD_SIZE_W; i++)
            {
                same_buf = 0;
                score = 0;

                for (int j = 0; j < BOARD_SIZE_H; j++)
                {
                    int current_buf = g_TempCharacters[i, j];
                    if (current_buf >= 10)
                        current_buf = g_TempCharacters[i, j] / 10;

                    if (same_buf == current_buf && current_buf != 8)
                    {
                        score++;

                        {
                            if (score > 1)
                            {
                                total_score += score;
                                //---->j,j-1,j-2
                                if(g_AllocCharacters[i, j] == same_buf)
                                    g_AllocCharacters[i, j] = 8;
                                else
                                {
                                    if (i > 0)
                                        if (g_AllocCharacters[i - 1, j] == same_buf)
                                            g_AllocCharacters[i - 1, j] = 8;
                                    if (i < BOARD_SIZE_W - 1)
                                        if (g_AllocCharacters[i + 1, j] == same_buf)
                                            g_AllocCharacters[i + 1, j] = 8;
                                }
                                

                                if (g_AllocCharacters[i, j - 1] == same_buf)
                                    g_AllocCharacters[i, j - 1] = 8;
                                else
                                {
                                    if (i > 0)
                                        if (g_AllocCharacters[i - 1, j - 1] == same_buf)
                                            g_AllocCharacters[i - 1, j - 1] = 8;
                                    if (i < BOARD_SIZE_W - 1)
                                        if (g_AllocCharacters[i + 1, j - 1] == same_buf)
                                            g_AllocCharacters[i + 1, j - 1] = 8;
                                }
                                

                                if (g_AllocCharacters[i, j - 2] == same_buf)
                                    g_AllocCharacters[i, j - 2] = 8;
                                else
                                {
                                    if (i > 0)
                                        if (g_AllocCharacters[i - 1, j - 2] == same_buf)
                                            g_AllocCharacters[i - 1, j - 2] = 8;
                                    if (i < BOARD_SIZE_W - 1)
                                        if (g_AllocCharacters[i + 1, j - 2] == same_buf)
                                            g_AllocCharacters[i + 1, j - 2] = 8;
                                }
                                
                            }
                        }
                    }
                    else
                    {
                        if (score > 1)
                        {
                        }
                        score = 0;
                        if (g_TempCharacters[i, j] >= 10)
                            same_buf = g_TempCharacters[i, j] / 10;
                        else
                            same_buf = g_TempCharacters[i, j];
                    }
                }
            }

            for (int j = 0; j < BOARD_SIZE_H; j++)
            {
                same_buf = 0;
                score = 0;

                for (int i = 0; i < BOARD_SIZE_W; i++)
                {
                    int current_buf = g_TempCharacters[i, j];
                    if (current_buf >= 10)
                        current_buf = g_TempCharacters[i, j] / 10;

                    if (same_buf == current_buf && current_buf != 8)
                    {
                        score++;
                        {
                            if (score > 1)
                            {
                                total_score += score;
                                //---->i,i-1,i-2
                                if (g_AllocCharacters[i, j] == same_buf)
                                    g_AllocCharacters[i, j] = 8;
                                else
                                {
                                    if (j > 0)
                                        if (g_AllocCharacters[i, j - 1] == same_buf)
                                            g_AllocCharacters[i, j - 1] = 8;
                                    if (j < BOARD_SIZE_H - 1)
                                        if (g_AllocCharacters[i, j + 1] == same_buf)
                                            g_AllocCharacters[i, j + 1] = 8;
                                }

                                if (g_AllocCharacters[i - 1, j] == same_buf)
                                    g_AllocCharacters[i - 1, j] = 8;
                                else
                                {
                                    if (j > 0)
                                        if (g_AllocCharacters[i - 1, j - 1] == same_buf)
                                            g_AllocCharacters[i - 1, j - 1] = 8;
                                    if (j < BOARD_SIZE_H - 1)
                                        if (g_AllocCharacters[i - 1, j + 1] == same_buf)
                                            g_AllocCharacters[i - 1, j + 1] = 8;
                                }

                                if (g_AllocCharacters[i - 2, j] == same_buf)
                                    g_AllocCharacters[i - 2, j] = 8;
                                else
                                {
                                    if (j > 0)
                                        if (g_AllocCharacters[i - 2, j - 1] == same_buf)
                                            g_AllocCharacters[i - 2, j - 1] = 8;
                                    if (j < BOARD_SIZE_H - 1)
                                        if (g_AllocCharacters[i - 2, j + 1] == same_buf)
                                            g_AllocCharacters[i - 2, j + 1] = 8;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (score > 1)
                        {
                        }
                        score = 0;
                        if (g_TempCharacters[i, j] >= 10)
                            same_buf = g_TempCharacters[i, j] / 10;
                        else
                            same_buf = g_TempCharacters[i, j];
                    }
                }
            }
            return total_score;
        }
        public static int Score_g_TempCharacters()
        {
            int total_score = 0;
            int score = 0;
            int same_buf = 0;
            for (int i = 0; i < BOARD_SIZE_W; i++)
            {
                same_buf = 0;
                score = 0;

                for (int j = 0; j < BOARD_SIZE_H; j++)
                {
                    int current_buf = g_TempCharacters[i, j];
                    if (current_buf >= 10)
                        current_buf = g_TempCharacters[i, j] / 10;

                    if (same_buf == current_buf && current_buf != 8)
                    {
                        score++;
                        
                        {
                            if (score > 1)
                            {
                                total_score += score;
                            }
                        }
                    }
                    else
                    {
                        if (score > 1)
                        {
                            total_score += score;
                        }
                        score = 0;
                        if (g_TempCharacters[i, j] >= 10)
                            same_buf = g_TempCharacters[i, j] / 10;
                        else
                            same_buf = g_TempCharacters[i, j];
                    }
                }
            }

            for (int j = 0; j < BOARD_SIZE_H; j++)
            {
                same_buf = 0;
                score = 0;

                for (int i = 0; i < BOARD_SIZE_W; i++)
                {
                    int current_buf = g_TempCharacters[i, j];
                    if (current_buf >= 10)
                        current_buf = g_TempCharacters[i, j] / 10;

                    if (same_buf == current_buf && current_buf!=8)
                    {
                        score++;
                        {
                            if (score > 1)
                            {
                                total_score += score;
                            }
                        }
                    }
                    else
                    {
                        if (score > 1)
                        {
                            total_score += score;
                        }
                        if (score > 3)
                            total_score += 10;
                        score = 0;
                        if (g_TempCharacters[i, j] >= 10)
                            same_buf = g_TempCharacters[i, j] / 10;
                        else
                            same_buf = g_TempCharacters[i, j];
                    }
                }
            }
            return total_score;
        }
        /*public static int Score_g_TempCharacters()
        {
            int total_score = 0;
            int score = 0;
            int same_buf = 0;
            bool box = false;
            for (int i = 0; i < BOARD_SIZE_W; i++)
            {
                same_buf = 0;
                score = 0;
                box = false;
                for (int j = 0; j < BOARD_SIZE_H; j++)
                {
                    int current_buf = g_TempCharacters[i, j];
                    if (current_buf >= 10)
                        current_buf = g_TempCharacters[i, j] / 10;
                    if (same_buf == current_buf)
                    {
                        score++;
                        if (g_TempCharacters[i, j] >= 10)
                            box = true;

                    }
                    else
                    {
                        if (score > 1)
                        {
                            total_score += score;
                            if (box)
                                Global.g_Box = true;
                        }

                        if (score > 3)
                        {
                            Global.g_Box = true;
                            total_score += 1000;
                        }
                        score = 0;
                        if (g_TempCharacters[i, j] >= 10)
                            same_buf = g_TempCharacters[i, j] / 10;
                        else
                            same_buf = g_TempCharacters[i, j];
                    }
                }
            }

            same_buf = 0;

            for (int j = 0; j < BOARD_SIZE_H; j++)
            {
                same_buf = 0;
                score = 0;
                box = false;
                for (int i = 0; i < BOARD_SIZE_W; i++)
                {
                    int current_buf = g_TempCharacters[i, j];
                    if (current_buf >= 10)
                        current_buf = g_TempCharacters[i, j] / 10;
                    if (same_buf == current_buf)
                    {
                        score++;
                        if (score > 1 && g_TempCharacters[i, j] >= 10)
                            box = true;
                    }
                    else
                    {
                        if (score > 1)
                        {
                            total_score += score;
                            if (box)
                                Global.g_Box = true;
                        }
                        if (score > 3)
                            total_score += 1000;
                        score = 0;
                        same_buf = g_TempCharacters[i, j];
                    }
                }
            }
            return total_score;
        }*/
        public static void MoveRight_g_TempCharacters(int row, int col)
        {
            int buf = 0;
            {
                buf = g_TempCharacters[row, col];

                g_TempCharacters[row, col] = g_TempCharacters[row, col+1];

                g_TempCharacters[row, col + 1] = buf;
            }
        }
        public static void MoveDown_g_TempCharacters(int row, int col)
        {
            int buf = 0;
            {
                buf = g_TempCharacters[row, col];

                g_TempCharacters[row, col] = g_TempCharacters[row+1, col];

                g_TempCharacters[row+1, col] = buf;
            }
        }
        public static void EmulateMovement(int nMaxI, int nMaxJ, int nMaxDirection, int step)
        {
            if (Global.g_MainWindow == null)
                return;

            Global.g_MainWindow.RemoveHintArrows();

            int X = Global.g_rcROI.X;
            int Y = Global.g_rcROI.Y;
            //Global.GetRatioCalcedValues(Global.g_rcROI.Width, Global.g_rcROI.Height, ref X, ref Y);

            int nStepX = Global.DEF_MAIN_BOARD_W / 8;
            int nStepY = Global.DEF_MAIN_BOARD_H / 8;

            Point pt = new Point(X + nMaxJ * nStepX+nStepX/2, Y + nMaxI * nStepY + nStepY/2);
            
            nStepX = Global.DEF_MAIN_BOARD_W / 8;
            nStepY = Global.DEF_MAIN_BOARD_H / 8;

            Point ptTarget = Point.Empty;

            if (nMaxDirection == 0)// up
            {
                ptTarget = new Point(X + nMaxJ * nStepX + nStepX / 2, Y + (nMaxI - step) * nStepY + nStepY / 2);
            }
                
            else if (nMaxDirection == 1) // left
            {
                ptTarget = new Point(X + (nMaxJ - step) * nStepX + nStepX / 2, Y + nMaxI * nStepY + nStepY / 2);
            }
                
            else if (nMaxDirection == 2) // right
            {
                ptTarget = new Point(X + (nMaxJ + step) * nStepX + nStepX / 2, Y + nMaxI * nStepY + nStepY / 2);
            }
                
            else if (nMaxDirection == 3) // bottom
            {
                ptTarget = new Point(X + nMaxJ * nStepX + nStepX / 2, Y + (nMaxI + step) * nStepY + nStepY / 2);
            }

            SendMouseEventToPoint(pt, ptTarget);
        }

        public static bool SendMouseEventToPoint(Point ptStart, Point ptTarget)
        {
            try
            {
                //if (!Global.g_bAssistantMode)
                    Global.MouseEventTo(ptStart, ptTarget);
                //Global.MouseDownTo(ptStart);
                //Global.MouseMoveToAndUp(ptTarget);
                //if (!Global.g_bAssistantMode)
                    Global.MouseMoveTo(new Point(10,100));

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

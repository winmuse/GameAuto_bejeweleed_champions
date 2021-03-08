using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameAuto
{
    public class ImageMatcher
    {
        public static bool CompareImage(Image<Bgr, Byte> imgModel, Image<Bgr, Byte> imgCandi)
        {
            bool bFound = false;

            byte[,,] pDataModel = imgModel.Data;
            byte[,,] pDataCandi = imgCandi.Data;

            int nCompX = 0, nCompY = 0, nCount = 0;
            int nWid = imgModel.Width; int nHei = imgModel.Height;

            while (nCompY < imgCandi.Height - nHei)
            {
                nCount = 0;
                for (int y = 0; y < nHei; y++)
                {
                    for (int x = 0; x < nWid; x++)
                    {
                        int B = pDataModel[y, x, 0]; int G = pDataModel[y, x, 1]; int R = pDataModel[y, x, 2];
                        int cB = pDataCandi[y + nCompY, x + nCompX, 0]; int cG = pDataCandi[y + nCompY, x + nCompX, 1]; int cR = pDataCandi[y, x, 2];

                        int diffR = Math.Abs(R - cR); int diffG = Math.Abs(G - cG); int diffB = Math.Abs(B - cB);
                        if (diffB < 5 && diffG < 5 && diffR < 5)
                            nCount++;
                    }
                }

                if (nCount >= nWid * nHei * 8 / 10)
                {
                    bFound = true;
                    break;
                }

                nCompX += 5;

                if (nCompX + nWid >= imgCandi.Width)
                {
                    nCompX = 0;
                    nCompY += 5;
                }
            }

            return bFound;
        }
        private static Rgb COL_SELECT1 = new Rgb(251, 215, 91);//red
        private static Rgb COL_SELECT2 = new Rgb(77, 219, 250);//red
        private static Rgb COL_SELECT3 = new Rgb(250, 150, 243);//red
        public static bool DetermineSelectGame(Image<Bgr, Byte> imgCandi)
        {
            byte[,,] pData = imgCandi.Data;

            bool bFound = false;
            int nWid = imgCandi.Width; int nHei = imgCandi.Height;
            int rate = 0;
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_SELECT1.Blue) < 10 && Math.Abs(G - COL_SELECT1.Green) < 10 && Math.Abs(R - COL_SELECT1.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_SELECT2.Blue) < 10 && Math.Abs(G - COL_SELECT2.Green) < 10 && Math.Abs(R - COL_SELECT2.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_SELECT3.Blue) < 10 && Math.Abs(G - COL_SELECT3.Green) < 10 && Math.Abs(R - COL_SELECT3.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            if (rate > 8)
                return true;
            return false;
        }
        private static Rgb COL_ENTER1 = new Rgb(64, 160, 61);//red
        private static Rgb COL_ENTER2 = new Rgb(12, 98, 41);//red
        public static bool DetermineEnterGame(Image<Bgr, Byte> imgCandi)
        {
            byte[,,] pData = imgCandi.Data;

            bool bFound = false;
            int nWid = imgCandi.Width; int nHei = imgCandi.Height;
            int rate = 0;
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_ENTER1.Blue) < 10 && Math.Abs(G - COL_ENTER1.Green) < 10 && Math.Abs(R - COL_ENTER1.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_ENTER2.Blue) < 10 && Math.Abs(G - COL_ENTER2.Green) < 10 && Math.Abs(R - COL_ENTER2.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            if (rate > 1000)
                return true;
            return false;
        }

        private static Rgb COL_BEGIN1 = new Rgb(228, 165, 251);//red
        private static Rgb COL_BEGIN2 = new Rgb(110, 103, 239);//red
        private static Rgb COL_BEGIN3 = new Rgb(57, 10, 119);//red
        public static bool DetermineBeginGame(Image<Bgr, Byte> imgCandi)
        {
            byte[,,] pData = imgCandi.Data;

            bool bFound = false;
            int nWid = imgCandi.Width; int nHei = imgCandi.Height;
            int rate = 0;
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_BEGIN1.Blue) < 10 && Math.Abs(G - COL_BEGIN1.Green) < 10 && Math.Abs(R - COL_BEGIN1.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_BEGIN2.Blue) < 10 && Math.Abs(G - COL_BEGIN2.Green) < 10 && Math.Abs(R - COL_BEGIN2.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_BEGIN3.Blue) < 10 && Math.Abs(G - COL_BEGIN3.Green) < 10 && Math.Abs(R - COL_BEGIN3.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            if (rate > 1000)
                return true;
            return false;
        }
        private static Rgb COL_SUBMIT1 = new Rgb(122, 229, 254);//
        private static Rgb COL_SUBMIT2 = new Rgb(63, 128, 220);//
        private static Rgb COL_SUBMIT3 = new Rgb(68, 149, 227);//
        public static bool DetermineSubmitGame(Image<Bgr, Byte> imgCandi)
        {
            byte[,,] pData = imgCandi.Data;

            bool bFound = false;
            int nWid = imgCandi.Width; int nHei = imgCandi.Height;
            int rate = 0;
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_SUBMIT1.Blue) < 10 && Math.Abs(G - COL_SUBMIT1.Green) < 10 && Math.Abs(R - COL_SUBMIT1.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_SUBMIT2.Blue) < 10 && Math.Abs(G - COL_SUBMIT2.Green) < 10 && Math.Abs(R - COL_SUBMIT2.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_SUBMIT3.Blue) < 10 && Math.Abs(G - COL_SUBMIT3.Green) < 10 && Math.Abs(R - COL_SUBMIT3.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            if (rate > 400)
                return true;
            return false;
        }
        private static Rgb COL_AGAIN1 = new Rgb(255, 250, 201);//
        private static Rgb COL_AGAIN2 = new Rgb(209, 190, 6);//
        private static Rgb COL_AGAIN3 = new Rgb(204, 77, 14);//
        public static bool DetermineAgainGame(Image<Bgr, Byte> imgCandi)
        {
            byte[,,] pData = imgCandi.Data;

            bool bFound = false;
            int nWid = imgCandi.Width; int nHei = imgCandi.Height;
            int rate = 0;
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_AGAIN1.Blue) < 10 && Math.Abs(G - COL_AGAIN1.Green) < 10 && Math.Abs(R - COL_AGAIN1.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_AGAIN2.Blue) < 10 && Math.Abs(G - COL_AGAIN2.Green) < 10 && Math.Abs(R - COL_AGAIN2.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_AGAIN3.Blue) < 10 && Math.Abs(G - COL_AGAIN3.Green) < 10 && Math.Abs(R - COL_AGAIN3.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            if (rate > 1000)
                return true;
            return false;
        }
        private static Rgb COL_LOGIN1 = new Rgb(86, 124, 222);//
        private static Rgb COL_LOGIN2 = new Rgb(103, 137, 226);//
        private static Rgb COL_LOGIN3 = new Rgb(246, 204, 84);//
        public static bool DetermineLoginGame(Image<Bgr, Byte> imgCandi)
        {
            byte[,,] pData = imgCandi.Data;

            bool bFound = false;
            int nWid = imgCandi.Width; int nHei = imgCandi.Height;
            int rate = 0;
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_LOGIN1.Blue) < 10 && Math.Abs(G - COL_LOGIN1.Green) < 10 && Math.Abs(R - COL_LOGIN1.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_LOGIN2.Blue) < 10 && Math.Abs(G - COL_LOGIN2.Green) < 10 && Math.Abs(R - COL_LOGIN2.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_LOGIN3.Blue) < 10 && Math.Abs(G - COL_LOGIN3.Green) < 10 && Math.Abs(R - COL_LOGIN3.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            if (rate > 2000)
                return true;
            return false;
        }
        private static Rgb COL_SIGNIN1 = new Rgb(255, 246, 170);//
        private static Rgb COL_SIGNIN2 = new Rgb(204, 0, 0);//
        private static Rgb COL_SIGNIN3 = new Rgb(226, 214, 74);//
        public static bool DetermineSignInGame(Image<Bgr, Byte> imgCandi)
        {
            byte[,,] pData = imgCandi.Data;

            bool bFound = false;
            int nWid = imgCandi.Width; int nHei = imgCandi.Height;
            int rate = 0;
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_SIGNIN1.Blue) < 10 && Math.Abs(G - COL_SIGNIN1.Green) < 10 && Math.Abs(R - COL_SIGNIN1.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_SIGNIN2.Blue) < 10 && Math.Abs(G - COL_SIGNIN2.Green) < 10 && Math.Abs(R - COL_SIGNIN2.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_SIGNIN3.Blue) < 10 && Math.Abs(G - COL_SIGNIN3.Green) < 10 && Math.Abs(R - COL_SIGNIN3.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            if (rate > 200)
                return true;
            return false;
        }
        private static Rgb COL_RETRY1 = new Rgb(197, 152, 242);//
        private static Rgb COL_RETRY2 = new Rgb(110, 103, 239);//
        private static Rgb COL_RETRY3 = new Rgb(159, 122, 243);//
        public static bool DetermineRetryGame(Image<Bgr, Byte> imgCandi)
        {
            byte[,,] pData = imgCandi.Data;

            bool bFound = false;
            int nWid = imgCandi.Width; int nHei = imgCandi.Height;
            int rate = 0;
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_RETRY1.Blue) < 10 && Math.Abs(G - COL_RETRY1.Green) < 10 && Math.Abs(R - COL_RETRY1.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_RETRY2.Blue) < 10 && Math.Abs(G - COL_RETRY2.Green) < 10 && Math.Abs(R - COL_RETRY2.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_RETRY3.Blue) < 10 && Math.Abs(G - COL_RETRY3.Green) < 10 && Math.Abs(R - COL_RETRY3.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            if (rate > 200)
                return true;
            return false;
        }
        //private static Rgb COL_RED = new Rgb(236, 33, 64);//red
        private static Rgb COL_RED = new Rgb(241, 13, 44);//red
        //private static Rgb COL_RED_BT = new Rgb(255, 166, 178);
        private static Rgb COL_RED_BT = new Rgb(193, 33, 52);

        //private static Rgb COL_BLUE = new Rgb(27, 137, 255);//blue
        private static Rgb COL_BLUE = new Rgb(17, 139, 252);//blue
        //private static Rgb COL_BLUE_BT = new Rgb(27, 137, 255);
        private static Rgb COL_BLUE_BT = new Rgb(6, 105, 255);

        //private static Rgb COL_YELLOW = new Rgb(252, 236, 52);//yellow
        private static Rgb COL_YELLOW = new Rgb(228, 151, 0);//yellow
        //private static Rgb COL_YELLOW_BT = new Rgb(255, 254, 209);
        private static Rgb COL_YELLOW_BT = new Rgb(239, 172, 5);

        //private static Rgb COL_PURPLE = new Rgb(229, 63, 223);//purple
        private static Rgb COL_PURPLE = new Rgb(238, 37, 232);//purple
        private static Rgb COL_PURPLE_BT = new Rgb(229, 63, 223);

        //private static Rgb COL_GREEN = new Rgb(70, 236, 88);//green
        private static Rgb COL_GREEN = new Rgb(25, 207, 46);//green
        //private static Rgb COL_GREEN_BT = new Rgb(171, 246, 173);
        private static Rgb COL_GREEN_BT = new Rgb(0, 131, 1);

        private static Rgb COL_DARK = new Rgb(253, 104, 3);//dark
        private static Rgb COL_DARK_BT = new Rgb(253, 104, 3);//

        private static Rgb COL_TEA = new Rgb(222, 215, 229);//tea
        private static Rgb COL_TEA_BT = new Rgb(222, 215, 229);

        private static Rgb COL_KING = new Rgb(74, 54, 69);
        private static Rgb COL_KING_BT = new Rgb(61, 29, 38);




        private static Rgb COL_KING2 = new Rgb(255, 255, 255);
        private static Rgb COL_KING2_BT = new Rgb(255, 245, 255);

        private static Rgb COL_KING3 = new Rgb(239, 185, 49);
        private static Rgb COL_KING3_BT = new Rgb(233, 142, 19);

        private static Rgb COL_KING4 = new Rgb(236, 163, 32);
        private static Rgb COL_KING4_BT = new Rgb(254, 251, 180);

        private static Rgb COL_KING5 = new Rgb(233, 142, 19);
        private static Rgb COL_KING5_BT = new Rgb(233, 142, 19);
/*
        private static Rgb COL_KING4 = new Rgb(236, 163, 32);
        private static Rgb COL_KING4_BT = new Rgb(254, 251, 180);

        private static Rgb COL_KING5 = new Rgb(233, 142, 19);
        private static Rgb COL_KING5_BT = new Rgb(233, 142, 19);*/




        private static Rgb COL_BALLOON = new Rgb(216, 145, 67);
        private static Rgb COL_BALLOON_BT = new Rgb(138, 100, 79);

        private static Rgb COL_WHALE = new Rgb(57, 67, 85);
        private static Rgb COL_WHALE_CT = new Rgb(254, 255, 255);

        private static Rgb COL_WHALE_WATER = new Rgb(49, 53, 63);
        private static Rgb COL_WHALE_WATER_CT = new Rgb(255, 213, 194);

        private static Rgb COL_OCTOPUS = new Rgb(202, 48, 7);
        private static Rgb COL_OCTOPUS_BT = new Rgb(157, 69, 34);

        private static Rgb COL_WATER_OCTOPUS = new Rgb(214, 75, 24);
        private static Rgb COL_WATER_OCTOPUS_BT = new Rgb(87, 82, 107);

        private static Rgb COL_WOOD = new Rgb(172, 73, 26);
        private static Rgb COL_WOOD_CT = new Rgb(73, 145, 213);
        private static Rgb COL_WOOD_BT = new Rgb(89, 32, 29);

        public static int DetermineCharacter(Image<Bgr, Byte> imgCandi)
        {
            if (DetermineRed(imgCandi))
            {
                //if (DetermineKing1(imgCandi))
                    //return (int)Global.CHARACTER_TYPE.CHAR_FROG * 10;
                return (int)Global.CHARACTER_TYPE.CHAR_RED;
            }
            else if (DetermineBlue(imgCandi))
            {
                //if (DetermineKing1(imgCandi))
                //return (int)Global.CHARACTER_TYPE.CHAR_FROG * 10;
                return (int)Global.CHARACTER_TYPE.CHAR_BLUE;
            }
            else if (DetermineYellow(imgCandi))
            {
                //if (DetermineKing1(imgCandi))
                //return (int)Global.CHARACTER_TYPE.CHAR_FROG * 10;
                return (int)Global.CHARACTER_TYPE.CHAR_YELLOW;
            }
            else if (DeterminePurple(imgCandi))
            {
                //if (DetermineKing1(imgCandi))
                //return (int)Global.CHARACTER_TYPE.CHAR_FROG * 10;
                return (int)Global.CHARACTER_TYPE.CHAR_PURPLE;
            }
            else if (DetermineGreen(imgCandi))
            {
                //if (DetermineKing1(imgCandi))
                //return (int)Global.CHARACTER_TYPE.CHAR_FROG * 10;
                return (int)Global.CHARACTER_TYPE.CHAR_GREEN;
            }
            else if (DetermineDark(imgCandi))
            {
                //if (DetermineKing1(imgCandi))
                //return (int)Global.CHARACTER_TYPE.CHAR_FROG * 10;
                return (int)Global.CHARACTER_TYPE.CHAR_DARK;
            }
            else if (DetermineTea(imgCandi))
            {
                //if (DetermineKing1(imgCandi))
                //return (int)Global.CHARACTER_TYPE.CHAR_FROG * 10;
                return (int)Global.CHARACTER_TYPE.CHAR_TEA;
            }
            else if (DetermineKing(imgCandi))
            {
                return (int)Global.CHARACTER_TYPE.CHAR_KING;
            }

            return (int)Global.CHARACTER_TYPE.CHAR_NONE;
        }

        private static bool DetermineRed(Image<Bgr, Byte> imgCandi)
        {
            byte[,,] pData = imgCandi.Data;

            bool bFound = false;
            int nWid = imgCandi.Width; int nHei = imgCandi.Height;
            int rate = 0;
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_RED.Blue) < 10 && Math.Abs(G - COL_RED.Green) < 10 && Math.Abs(R - COL_RED.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            if (rate > 20)
                return true;
            nWid = imgCandi.Width; nHei = imgCandi.Height;
            rate = 0;
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_RED_BT.Blue) < 10 && Math.Abs(G - COL_RED_BT.Green) < 10 && Math.Abs(R - COL_RED_BT.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            if (rate > 20)
                return true;
            return bFound;
        }
        private static bool DetermineBlue(Image<Bgr, Byte> imgCandi)
        {
            byte[,,] pData = imgCandi.Data;

            bool bFound = false;
            int nWid = imgCandi.Width; int nHei = imgCandi.Height;
            int rate = 0;
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_BLUE.Blue) < 10 && Math.Abs(G - COL_BLUE.Green) < 10 && Math.Abs(R - COL_BLUE.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            if (rate > 20)
                return true;
            nWid = imgCandi.Width; nHei = imgCandi.Height;
            rate = 0;
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_BLUE_BT.Blue) < 10 && Math.Abs(G - COL_BLUE_BT.Green) < 10 && Math.Abs(R - COL_BLUE_BT.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            if (rate > 15)
                return true;
            return bFound;
        }
        private static bool DetermineYellow(Image<Bgr, Byte> imgCandi)
        {
            byte[,,] pData = imgCandi.Data;

            bool bFound = false;
            int nWid = imgCandi.Width; int nHei = imgCandi.Height;
            int rate = 0;
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_YELLOW.Blue) < 10 && Math.Abs(G - COL_YELLOW.Green) < 10 && Math.Abs(R - COL_YELLOW.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            if (rate > 45)
                return true;
            nWid = imgCandi.Width; nHei = imgCandi.Height;
            rate = 0;
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_YELLOW_BT.Blue) < 10 && Math.Abs(G - COL_YELLOW_BT.Green) < 10 && Math.Abs(R - COL_YELLOW_BT.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            if (rate > 50)
                return true;
            return bFound;
        }
        private static bool DeterminePurple(Image<Bgr, Byte> imgCandi)
        {
            byte[,,] pData = imgCandi.Data;

            bool bFound = false;
            int nWid = imgCandi.Width; int nHei = imgCandi.Height;
            int rate = 0;
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_PURPLE.Blue) < 10 && Math.Abs(G - COL_PURPLE.Green) < 10 && Math.Abs(R - COL_PURPLE.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            if (rate > 20)
                return true;
            nWid = imgCandi.Width; nHei = imgCandi.Height;
            rate = 0;
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_PURPLE_BT.Blue) < 10 && Math.Abs(G - COL_PURPLE_BT.Green) < 10 && Math.Abs(R - COL_PURPLE_BT.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            if (rate > 15)
                return true;
            return bFound;
        }
        private static bool DetermineGreen(Image<Bgr, Byte> imgCandi)
        {
            byte[,,] pData = imgCandi.Data;

            bool bFound = false;
            int nWid = imgCandi.Width; int nHei = imgCandi.Height;
            int rate = 0;
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_GREEN.Blue) < 10 && Math.Abs(G - COL_GREEN.Green) < 10 && Math.Abs(R - COL_GREEN.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            if (rate > 20)
                return true;
            nWid = imgCandi.Width; nHei = imgCandi.Height;
            rate = 0;
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_GREEN_BT.Blue) < 10 && Math.Abs(G - COL_GREEN_BT.Green) < 10 && Math.Abs(R - COL_GREEN_BT.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            if (rate > 15)
                return true;
            return bFound;
        }
        private static bool DetermineDark(Image<Bgr, Byte> imgCandi)
        {
            byte[,,] pData = imgCandi.Data;

            bool bFound = false;
            int nWid = imgCandi.Width; int nHei = imgCandi.Height;
            int rate = 0;
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_DARK.Blue) < 10 && Math.Abs(G - COL_DARK.Green) < 10 && Math.Abs(R - COL_DARK.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            if (rate > 20)
                return true;
            nWid = imgCandi.Width; nHei = imgCandi.Height;
            rate = 0;
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_DARK_BT.Blue) < 10 && Math.Abs(G - COL_DARK_BT.Green) < 10 && Math.Abs(R - COL_DARK_BT.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            if (rate > 10)
                return true;
            return bFound;
        }
        private static bool DetermineTea(Image<Bgr, Byte> imgCandi)
        {
            byte[,,] pData = imgCandi.Data;

            bool bFound = false;
            int nWid = imgCandi.Width; int nHei = imgCandi.Height;
            int rate = 0;
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_TEA.Blue) < 10 && Math.Abs(G - COL_TEA.Green) < 10 && Math.Abs(R - COL_TEA.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            if (rate > 20)
                return true;
            nWid = imgCandi.Width; nHei = imgCandi.Height;
            rate = 0;
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_TEA_BT.Blue) < 10 && Math.Abs(G - COL_TEA_BT.Green) < 10 && Math.Abs(R - COL_TEA_BT.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            if (rate > 10)
                return true;
            return bFound;
        }
        private static bool DetermineKing(Image<Bgr, Byte> imgCandi)
        {
            byte[,,] pData = imgCandi.Data;

            bool bFound = false;
            int nWid = imgCandi.Width; int nHei = imgCandi.Height;
            int rate = 0;
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_KING.Blue) < 10 && Math.Abs(G - COL_KING.Green) < 10 && Math.Abs(R - COL_KING.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            if (rate > 20)
                return true;
            nWid = imgCandi.Width; nHei = imgCandi.Height;
            rate = 0;
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_KING_BT.Blue) < 10 && Math.Abs(G - COL_KING_BT.Green) < 10 && Math.Abs(R - COL_KING_BT.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            if (rate > 20)
                return true;
            return bFound;
        }

        private static bool DetermineBalloon(Image<Bgr, Byte> imgCandi)
        {
            //duck : 247, 187, 70 bottom 250, 195, 47 
            byte[,,] pData = imgCandi.Data;

            bool bFound = false;
            int nWid = imgCandi.Width; int nHei = imgCandi.Height;
            int rate = 0;
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_BALLOON.Blue) < 10 && Math.Abs(G - COL_BALLOON.Green) < 10 && Math.Abs(R - COL_BALLOON.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            if (rate > 20)//COL_DUCK_BT
                return true;
            nWid = imgCandi.Width; nHei = imgCandi.Height;
            rate = 0;
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_BALLOON_BT.Blue) < 10 && Math.Abs(G - COL_BALLOON_BT.Green) < 10 && Math.Abs(R - COL_BALLOON_BT.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            if (rate > 20)
                return true;

            return bFound;
        }
        
        private static bool DetermineKing2(Image<Bgr, Byte> imgCandi)
        {
            //duck : 247, 187, 70 bottom 250, 195, 47
            byte[,,] pData = imgCandi.Data;

            bool bFound = false;
            int nWid = imgCandi.Width; int nHei = imgCandi.Height;
            int rate = 0;
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_KING2.Blue) < 10 && Math.Abs(G - COL_KING2.Green) < 10 && Math.Abs(R - COL_KING2.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            if (rate > 15)
            {
                return true;
            }

            nWid = imgCandi.Width; nHei = imgCandi.Height;
            rate = 0;
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_KING2_BT.Blue) < 10 && Math.Abs(G - COL_KING2_BT.Green) < 10 && Math.Abs(R - COL_KING2_BT.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            if (rate > 7)
                return true;

            return bFound;
        }
        private static bool DetermineKing3(Image<Bgr, Byte> imgCandi)
        {
            //duck : 247, 187, 70 bottom 250, 195, 47
            byte[,,] pData = imgCandi.Data;

            bool bFound = false;
            int nWid = imgCandi.Width; int nHei = imgCandi.Height;
            int rate = 0;
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_KING3.Blue) < 10 && Math.Abs(G - COL_KING3.Green) < 10 && Math.Abs(R - COL_KING3.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            if (rate > 15)
            {
                return true;
            }

            nWid = imgCandi.Width; nHei = imgCandi.Height;
            rate = 0;
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_KING3_BT.Blue) < 10 && Math.Abs(G - COL_KING3_BT.Green) < 10 && Math.Abs(R - COL_KING3_BT.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            if (rate > 15)
                return true;

            return bFound;
        }
        private static bool DetermineKing4(Image<Bgr, Byte> imgCandi)
        {
            //duck : 247, 187, 70 bottom 250, 195, 47
            byte[,,] pData = imgCandi.Data;

            bool bFound = false;
            int nWid = imgCandi.Width; int nHei = imgCandi.Height;
            int rate = 0;
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_KING4.Blue) < 10 && Math.Abs(G - COL_KING4.Green) < 10 && Math.Abs(R - COL_KING4.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            if (rate > 15)
            {
                return true;
            }

            nWid = imgCandi.Width; nHei = imgCandi.Height;
            rate = 0;
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_KING4_BT.Blue) < 10 && Math.Abs(G - COL_KING4_BT.Green) < 10 && Math.Abs(R - COL_KING4_BT.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            if (rate > 15)
                return true;

            return bFound;
        }
        private static bool DetermineKing5(Image<Bgr, Byte> imgCandi)
        {
            //duck : 247, 187, 70 bottom 250, 195, 47
            byte[,,] pData = imgCandi.Data;

            bool bFound = false;
            int nWid = imgCandi.Width; int nHei = imgCandi.Height;
            int rate = 0;
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_KING5.Blue) < 10 && Math.Abs(G - COL_KING5.Green) < 10 && Math.Abs(R - COL_KING5.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            if (rate > 15)
            {
                return true;
            }

            nWid = imgCandi.Width; nHei = imgCandi.Height;
            rate = 0;
            for (int y = 0; y < nHei; y++)
            {
                for (int x = 0; x < nWid; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_KING5_BT.Blue) < 10 && Math.Abs(G - COL_KING5_BT.Green) < 10 && Math.Abs(R - COL_KING5_BT.Red) < 10)
                    {
                        rate++;
                    }
                }
            }
            if (rate > 15)
                return true;

            return bFound;
        }


        private static bool DetermineWhale(Image<Bgr, Byte> imgCandi)
        {
            //duck : 247, 187, 70 bottom 250, 195, 47
            byte[,,] pData = imgCandi.Data;

            bool bFound = false;
            int nWid = imgCandi.Width; int nHei = imgCandi.Height;
            for (int y = nHei / 2 - 5; y < nHei / 2 + 5; y++)
            {
                for (int x = nWid / 2 - 5; x < nWid / 2 + 5; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_WHALE.Blue) < 10 && Math.Abs(G - COL_WHALE.Green) < 10 && Math.Abs(R - COL_WHALE.Red) < 10)
                    {
                        bFound = true;
                        break;
                    }
                }
            }

            if (!bFound) return false;
            bFound = false;
            for (int y = nHei / 2 - 5; y < nHei / 2 + 5; y++)
            {
                for (int x = nWid / 2 - 5; x < nWid / 2 + 5; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_WHALE_CT.Blue) < 10 && Math.Abs(G - COL_WHALE_CT.Green) < 10 && Math.Abs(R - COL_WHALE_CT.Red) < 10)
                    {
                        bFound = true;
                        break;
                    }
                }
            }

            return bFound;
        }

        private static bool DetermineWhaleWater(Image<Bgr, Byte> imgCandi)
        {
            //duck : 247, 187, 70 bottom 250, 195, 47
            byte[,,] pData = imgCandi.Data;

            bool bFound = false;
            int nWid = imgCandi.Width; int nHei = imgCandi.Height;
            for (int y = nHei / 2 - 5; y < nHei / 2 + 5; y++)
            {
                for (int x = nWid / 2 - 5; x < nWid / 2 + 5; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_WHALE_WATER.Blue) < 10 && Math.Abs(G - COL_WHALE_WATER.Green) < 10 && Math.Abs(R - COL_WHALE_WATER.Red) < 10)
                    {
                        bFound = true;
                        break;
                    }
                }
            }

            if (!bFound) return false;
            bFound = false;
            for (int y = nHei / 2; y < nHei / 2 + 10; y++)
            {
                for (int x = nWid / 2; x < nWid / 2 + 10; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_WHALE_WATER_CT.Blue) < 10 && Math.Abs(G - COL_WHALE_WATER_CT.Green) < 10 && Math.Abs(R - COL_WHALE_WATER_CT.Red) < 10)
                    {
                        bFound = true;
                        break;
                    }
                }
            }

            return bFound;
        }

        private static bool DetermineOctopus(Image<Bgr, Byte> imgCandi)
        {
            //duck : 247, 187, 70 bottom 250, 195, 47
            byte[,,] pData = imgCandi.Data;

            bool bFound = false;
            int nWid = imgCandi.Width; int nHei = imgCandi.Height;
            for (int y = nHei / 2 - 5; y < nHei / 2 + 5; y++)
            {
                for (int x = nWid / 2 - 5; x < nWid / 2 + 5; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_OCTOPUS.Blue) < 10 && Math.Abs(G - COL_OCTOPUS.Green) < 10 && Math.Abs(R - COL_OCTOPUS.Red) < 10)
                    {
                        bFound = true;
                        break;
                    }
                }
            }

            if (!bFound) return false;
            bFound = false;
            for (int y = nHei / 6 * 5 - 5; y < nHei / 6 * 5 + 5; y++)
            {
                for (int x = nWid / 2 - 5; x < nWid / 2 + 5; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_OCTOPUS_BT.Blue) < 10 && Math.Abs(G - COL_OCTOPUS_BT.Green) < 10 && Math.Abs(R - COL_OCTOPUS_BT.Red) < 10)
                    {
                        bFound = true;
                        break;
                    }
                }
            }

            return bFound;
        }

        private static bool DetermineOctopusWater(Image<Bgr, Byte> imgCandi)
        {
            //duck : 247, 187, 70 bottom 250, 195, 47
            byte[,,] pData = imgCandi.Data;

            bool bFound = false;
            int nWid = imgCandi.Width; int nHei = imgCandi.Height;
            for (int y = nHei / 2 - 5; y < nHei / 2 + 5; y++)
            {
                for (int x = nWid / 2 - 5; x < nWid / 2 + 5; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_WATER_OCTOPUS.Blue) < 10 && Math.Abs(G - COL_WATER_OCTOPUS.Green) < 10 && Math.Abs(R - COL_WATER_OCTOPUS.Red) < 10)
                    {
                        bFound = true;
                        break;
                    }
                }
            }

            if (!bFound) return false;
            bFound = false;
            for (int y = nHei - 15; y < nHei - 5; y++)
            {
                for (int x = nWid / 2 - 5; x < nWid / 2 + 5; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_WATER_OCTOPUS_BT.Blue) < 10 && Math.Abs(G - COL_WATER_OCTOPUS_BT.Green) < 10 && Math.Abs(R - COL_WATER_OCTOPUS_BT.Red) < 10)
                    {
                        bFound = true;
                        break;
                    }
                }
            }

            return bFound;
        }

        private static bool DetermineWood(Image<Bgr, Byte> imgCandi)
        {
            //duck : 247, 187, 70 bottom 250, 195, 47
            byte[,,] pData = imgCandi.Data;

            bool bFound = false;
            int nWid = imgCandi.Width; int nHei = imgCandi.Height;
            for (int y = nHei / 2 - 5; y < nHei / 2 + 5; y++)
            {
                for (int x = nWid / 2 - 5; x < nWid / 2 + 5; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_WOOD.Blue) < 10 && Math.Abs(G - COL_WOOD.Green) < 10 && Math.Abs(R - COL_WOOD.Red) < 10)
                    {
                        bFound = true;
                        break;
                    }
                }
            }

            //if (!bFound) return false;
            //bFound = false;
            //for (int y = nHei / 2 - 10; y < nHei / 2; y++)
            //{
            //    for (int x = 5; x < nWid / 2 - 10; x++)
            //    {
            //        int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
            //        if (Math.Abs(B - COL_WOOD_CT.Blue) < 10 && Math.Abs(G - COL_WOOD_CT.Green) < 10 && Math.Abs(R - COL_WOOD_CT.Red) < 10)
            //        {
            //            bFound = true;
            //            break;
            //        }
            //    }
            //}

            if (!bFound) return false;
            bFound = false;
            for (int y = nHei-15; y<nHei; y++)
            {
                for (int x = 5; x<15; x++)
                {
                    int B = pData[y, x, 0]; int G = pData[y, x, 1]; int R = pData[y, x, 2];
                    if (Math.Abs(B - COL_WOOD_BT.Blue) < 10 && Math.Abs(G - COL_WOOD_BT.Green) < 10 && Math.Abs(R - COL_WOOD_BT.Red) < 10)
                    {
                        bFound = true;
                        break;
                    }
                }
            }

            return bFound;
        }
    }
}
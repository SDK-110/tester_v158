using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace can_j1939_test
{


    public static class id_pgn
    {



        private const int SA_MASK_CAN_ID = 255;
        private const int SA_SHIFT_CAN_ID = 0;
        private const int PS_MASK_CAN_ID = 65280;
        private const int PS_SHIFT_CAN_ID = 8;
        private const int GE_MASK_PGN = 255;
        private const int GE_SHIFT_PGN = 0;
        private const int PF_MASK_CAN_ID = 16711680;
        private const int PF_SHIFT_CAN_ID = 16;
        private const int PF_MASK_PGN = 65280;
        private const int PF_SHIFT_PGN = 8;
        private const int DP_MASK_CAN_ID = 16777216;
        private const int DP_SHIFT_CAN_ID = 24;
        private const int DP_MASK_PGN = 65536;
        private const int DP_SHIFT_PGN = 16;
        private const int R_MASK_CAN_ID = 33554432;
        private const int R_SHIFT_CAN_ID = 25;
        private const int R_MASK_PGN = 131072;
        private const int R_SHIFT_PGN = 17;
        private const int P_MASK_CAN_ID = 469762048;
        private const int P_SHIFT_CAN_ID = 26;
        private const int GE_THRESHOLD = 240;
        public static uint id2pgn(uint id)
        {


            uint P; // 优先级
            uint R; // 保留位
            uint DP; // 数据页
            uint PF; // PDU格式
            uint PS; // 特定PDU
            uint SA; // 源地址 
            uint GE = 0; // 群拓展
            uint PGN = 0;// 参数群编号
            uint DA; // 目标地址
            //printf("Please input CAN frame's Extended ID : 0x");
            //scanf("%08x", &id);
            //printf("Get CAN frame's ID : 0x%08x (%d)\n", id, id);
            SA = (id & SA_MASK_CAN_ID) >> SA_SHIFT_CAN_ID;
            PS = (id & PS_MASK_CAN_ID) >> PS_SHIFT_CAN_ID;
            PF = (id & PF_MASK_CAN_ID) >> PF_SHIFT_CAN_ID;
            DP = (id & DP_MASK_CAN_ID) >> DP_SHIFT_CAN_ID;
            R = (id & R_MASK_CAN_ID) >> R_SHIFT_CAN_ID;
            P = (id & P_MASK_CAN_ID) >> P_SHIFT_CAN_ID;
            //printf("|---|-|-|--------|--------|--------|\n");
            //printf("|   | | |        |        |        |\n");
            //printf("| P |R|D|   PF   |   PS   |   SA   |\n");
            //printf("|   | |P|        |        |        |\n");
            //printf("  %1d  %-1d %-1d   %-6d   %-6d   %-6d \n", P, R, DP, PF, PS, SA);
            // 群拓展根据PF数值决定
            if (PF >= GE_THRESHOLD)
            {
                GE = PS;
                //printf("PF >= %d , so GE = PS : %d\n", GE_THRESHOLD, GE);
            }
            else
            {
                GE = 0;
                DA = PS;
                //printf("PF < %d , so GE : %d, DA = PS : %d\n", GE_THRESHOLD, GE, DA);
            }
            PGN = 0;
            PGN |= (GE << GE_SHIFT_PGN) | (PF << PF_SHIFT_PGN) | (DP << DP_SHIFT_PGN) | (R << R_SHIFT_PGN);
            //printf("PGN : 0x%x(%d)\n", PGN, PGN);
            System.Windows.Forms.MessageBox.Show("Test " + PGN);

            return PGN;
        }

        public static uint pgn2id(uint pgn, uint p, uint sa, uint ps)
        {


            uint id;
            uint P = p; // 优先级
            uint R; // 保留位
            uint DP; // 数据页
            uint PF; // PDU格式
            uint PS = ps; // 特定PDU
            uint SA = sa; // 源地址 
            uint GE = 0; // 群拓展
            uint PGN = pgn; // 参数群编号
            //printf("Please input PGN Dec : ");
            //scanf("%d", &PGN);
            //printf("Get PGN Dec : %d (0x%08x)\n", PGN, PGN);
            GE = (PGN & GE_MASK_PGN) >> GE_SHIFT_PGN;
            PF = (PGN & PF_MASK_PGN) >> PF_SHIFT_PGN;
            DP = (PGN & DP_MASK_PGN) >> DP_SHIFT_PGN;
            R = (PGN & R_MASK_PGN) >> R_SHIFT_PGN;
            if (PF >= GE_THRESHOLD)
            {
                PS = GE;
                //printf("PF >= %d , so PS = GE : %d\n", GE_THRESHOLD, PS);
            }
            else
            {
                //printf("PF < %d\n", GE_THRESHOLD);
                //printf("Please input TA(Target ADDR) as PS : ");
                //scanf("%d", &PS);
            }
            //printf("Please input SA(Source ADDR) : ");
            //scanf("%d", &SA);
            //printf("Please input P(Priority) : ");
            //scanf("%d", &P);
            //printf("|---|-|-|--------|--------|--------|\n");
            //printf("|   | | |        |        |        |\n");
            //printf("| P |R|D|   PF   |   PS   |   SA   |\n");
            //printf("|   | |P|        |        |        |\n");
            //printf("  %1d  %-1d %-1d   %-6d   %-6d   %-6d \n", P, R, DP, PF, PS, SA);
            id = 0;
            id |= (SA << SA_SHIFT_CAN_ID) | (PS << PS_SHIFT_CAN_ID) | (PF << PF_SHIFT_CAN_ID) | (DP << DP_SHIFT_CAN_ID) | (R << R_SHIFT_CAN_ID) | (P << P_SHIFT_CAN_ID);
            //printf("CAN ID : 0x%x(%d)\n", id, id);
            return id;

        }













    }
}


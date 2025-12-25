using System;
using System.Collections.Generic;

namespace SDRSharp.Tetra
{
// Token: 0x02000031 RID: 49
internal class Class18
{
	// Token: 0x06000257 RID: 599 RVA: 0x0003FC44 File Offset: 0x0003DE44
	public void method_0(LogicChannel logicChannel_0, ref int int_0, Dictionary<GlobalNames, int> dictionary_0)
	{
		string text = string.Empty;
		if (this.bool_0)
		{
			return;
		}
		try
		{
			text = <Module>.smethod_4<string>(307698215);
			for (;;)
			{
				IL_12F7:
				uint num = 586984407U;
				for (;;)
				{
					uint num2;
					int num4;
					int num12;
					bool flag;
					int num18;
					int num19;
					int num27;
					int num29;
					switch ((num2 = num ^ 500339449U) % 141U)
					{
					case 0U:
						int_0 += 16;
						num = (num2 * 658292385U) ^ 405607742U;
						continue;
					case 1U:
					{
						int num3;
						dictionary_0.Add(GlobalNames.Change_of_Security_Class, num3);
						num = (num2 * 3118461409U) ^ 230191229U;
						continue;
					}
					case 2U:
						num4 = Class34.smethod_3(logicChannel_0.Ptr, int_0, 3);
						num = (num2 * 1041864955U) ^ 1121939951U;
						continue;
					case 3U:
						goto IL_0171;
					case 4U:
						Class34.smethod_3(logicChannel_0.Ptr, int_0, 1);
						num = 1594802222U;
						continue;
					case 5U:
						goto IL_0165;
					case 6U:
					{
						int num5;
						num5++;
						num = (num2 * 1422392425U) ^ 1381417879U;
						continue;
					}
					case 7U:
					{
						int num6;
						dictionary_0.Add(GlobalNames.SCK_VN, num6);
						int_0 += 16;
						num = (num2 * 1629411791U) ^ 2944132439U;
						continue;
					}
					case 8U:
						num = (num2 * 2924070323U) ^ 2069831877U;
						continue;
					case 9U:
						int_0 += 2;
						Class34.smethod_3(logicChannel_0.Ptr, int_0, 5);
						num = (num2 * 1206930029U) ^ 954254337U;
						continue;
					case 10U:
					{
						int num7 = Class34.smethod_3(logicChannel_0.Ptr, int_0, 1);
						int_0++;
						num = ((num7 == 1) ? 1175486100U : 1485817168U) ^ (num2 * 1101056454U);
						continue;
					}
					case 11U:
						goto IL_01C4;
					case 12U:
						Class34.smethod_3(logicChannel_0.Ptr, int_0, 5);
						int_0 += 5;
						num = (num2 * 3516774267U) ^ 2935936899U;
						continue;
					case 13U:
						int_0++;
						num = (num2 * 1938977661U) ^ 1290917922U;
						continue;
					case 14U:
						text = Class18.smethod_0(text, <Module>.smethod_5<string>(1611798292));
						num = 1170841094U;
						continue;
					case 15U:
						int_0 += 8;
						num = (num2 * 729566339U) ^ 1465311891U;
						continue;
					case 16U:
					{
						int num5;
						num = ((num5 != 0) ? 3733049460U : 2470622788U) ^ (num2 * 1773849007U);
						continue;
					}
					case 17U:
					{
						int num8;
						num = ((num8 == 0) ? 407985149U : 1515948076U) ^ (num2 * 3298349107U);
						continue;
					}
					case 18U:
						int_0 += 5;
						num = 2015007645U;
						continue;
					case 19U:
					{
						int num9;
						num = ((num9 != 0) ? 1992874383U : 1685486154U) ^ (num2 * 583765609U);
						continue;
					}
					case 20U:
						goto IL_0106;
					case 21U:
					{
						int num10 = Class34.smethod_3(logicChannel_0.Ptr, int_0, 16);
						num = 1639404560U;
						continue;
					}
					case 22U:
					{
						text = Class18.smethod_0(text, <Module>.smethod_5<string>(-338726931));
						int num11 = Class34.smethod_3(logicChannel_0.Ptr, int_0, 1);
						int_0++;
						num = ((num11 != 1) ? 1959296262U : 657883847U);
						continue;
					}
					case 23U:
						goto IL_01F6;
					case 24U:
						goto IL_00DB;
					case 25U:
						goto IL_01DE;
					case 26U:
						num = ((num12 == 3) ? 276417027U : 1955429483U);
						continue;
					case 27U:
						int_0 += 48;
						num = (num2 * 311314400U) ^ 1934012598U;
						continue;
					case 28U:
						goto IL_12F7;
					case 29U:
					{
						int num9 = Class34.smethod_3(logicChannel_0.Ptr, int_0, 2);
						num = 1276127741U;
						continue;
					}
					case 30U:
					{
						int num13;
						dictionary_0.Add(GlobalNames.GCK_ver_number, num13);
						num = (num2 * 1558635099U) ^ 2034682370U;
						continue;
					}
					case 31U:
						num = (num2 * 1165999329U) ^ 3003186801U;
						continue;
					case 32U:
						int_0 += 16;
						num = 1321749017U;
						continue;
					case 33U:
						goto IL_0C6B;
					case 34U:
						num = (num2 * 2805494472U) ^ 2558702904U;
						continue;
					case 35U:
						flag = false;
						num = (num2 * 162184007U) ^ 3081594320U;
						continue;
					case 36U:
					{
						int num13 = Class34.smethod_3(logicChannel_0.Ptr, int_0, 16);
						int num14;
						num = ((num14 != 0) ? 2751223188U : 2762918363U) ^ (num2 * 2079104110U);
						continue;
					}
					case 37U:
						Class34.smethod_3(logicChannel_0.Ptr, int_0, 4);
						num = (num2 * 3720270729U) ^ 2925388542U;
						continue;
					case 38U:
						goto IL_0094;
					case 39U:
						int_0 += 4;
						num = (num2 * 2587097892U) ^ 3938910952U;
						continue;
					case 40U:
					{
						int num15 = 4;
						num = (num2 * 2375846391U) ^ 2125008279U;
						continue;
					}
					case 41U:
						num = (num2 * 3380966566U) ^ 3838424796U;
						continue;
					case 42U:
					{
						int num16 = Class34.smethod_3(logicChannel_0.Ptr, int_0, 1);
						int_0++;
						num = ((num16 != 1) ? 532772390U : 2125925066U) ^ (num2 * 2953578453U);
						continue;
					}
					case 43U:
					{
						int num17 = Class34.smethod_3(logicChannel_0.Ptr, int_0, 8);
						num = (num2 * 1791878270U) ^ 471553916U;
						continue;
					}
					case 44U:
						num18 = 0;
						switch (num19)
						{
						case 0:
						case 1:
							goto IL_0617;
						case 2:
							goto IL_069D;
						case 3:
							goto IL_0C6B;
						default:
							num = (num2 * 4107389581U) ^ 4048720631U;
							continue;
						}
						break;
					case 45U:
					{
						int num6 = Class34.smethod_3(logicChannel_0.Ptr, int_0, 16);
						num = (num2 * 1611126391U) ^ 3519329713U;
						continue;
					}
					case 46U:
					{
						int num20;
						int_0 += num20;
						int num21;
						dictionary_0.Add(GlobalNames.Not_supported_sub_PDU_type, num21);
						num = (num2 * 1816832933U) ^ 2813106115U;
						continue;
					}
					case 47U:
						num = (num2 * 3257208595U) ^ 3565124806U;
						continue;
					case 48U:
						num = (num2 * 1965905111U) ^ 4062483814U;
						continue;
					case 49U:
					{
						int num22;
						num = ((num22 != 0) ? 2267914552U : 3882811549U) ^ (num2 * 2727563135U);
						continue;
					}
					case 50U:
						dictionary_0.Add(GlobalNames.UnknownData, 7);
						num = (num2 * 3255483043U) ^ 2847882630U;
						continue;
					case 51U:
					{
						int num14;
						int num23;
						num = ((num14 < num23) ? 1668930818U : 835355001U);
						continue;
					}
					case 52U:
					{
						int num22;
						num = ((num22 <= 0) ? 2043085714U : 243037868U);
						continue;
					}
					case 53U:
					{
						int num5 = 0;
						num = (num2 * 3135881357U) ^ 3095979015U;
						continue;
					}
					case 54U:
					{
						int num23 = Class34.smethod_3(logicChannel_0.Ptr, int_0, 4);
						num = (num2 * 2411473570U) ^ 2308327983U;
						continue;
					}
					case 55U:
					{
						int num24 = Class34.smethod_3(logicChannel_0.Ptr, int_0, 5);
						num = 60267445U;
						continue;
					}
					case 56U:
						num = ((!flag) ? 1747641202U : 636169749U);
						continue;
					case 57U:
					{
						int num24;
						dictionary_0.Add(GlobalNames.SCK_number, num24);
						num = (num2 * 701992375U) ^ 1346658735U;
						continue;
					}
					case 58U:
					{
						int num25 = Class34.smethod_3(logicChannel_0.Ptr, int_0, 16);
						num = 1779726520U;
						continue;
					}
					case 59U:
						Class34.smethod_3(logicChannel_0.Ptr, int_0, 48);
						num = (num2 * 1492418992U) ^ 1022324900U;
						continue;
					case 60U:
						int_0 += 16;
						num = 1326361580U;
						continue;
					case 61U:
						num = (num2 * 2382638997U) ^ 2928111786U;
						continue;
					case 62U:
						text = Class18.smethod_0(text, <Module>.smethod_4<string>(-1368165903));
						this.method_1(logicChannel_0, ref int_0, dictionary_0);
						num = 636169749U;
						continue;
					case 63U:
						dictionary_0.Add(GlobalNames.Authentication_reject_reason, num4);
						num = (num2 * 947281598U) ^ 1418308030U;
						continue;
					case 64U:
					{
						int num8;
						num = ((num8 != 1) ? 1810806311U : 2015126400U);
						continue;
					}
					case 65U:
					{
						int num10;
						dictionary_0.Add(GlobalNames.CCK_id, num10);
						num = (num2 * 1760998508U) ^ 2457105334U;
						continue;
					}
					case 66U:
						goto IL_01EA;
					case 67U:
					{
						int num8;
						dictionary_0.Add(GlobalNames.Key_change_type, num8);
						num = (num2 * 723001543U) ^ 1651464017U;
						continue;
					}
					case 68U:
					{
						int num3 = Class34.smethod_3(logicChannel_0.Ptr, int_0, 2);
						num = (num2 * 2494273419U) ^ 3530565815U;
						continue;
					}
					case 69U:
					{
						int num26 = Class34.smethod_3(logicChannel_0.Ptr, int_0, 16);
						num = (num2 * 2228649701U) ^ 236052666U;
						continue;
					}
					case 70U:
						goto IL_018E;
					case 71U:
					{
						int num15;
						MmPduType mmPduType = (MmPduType)Class34.smethod_3(logicChannel_0.Ptr, int_0, num15);
						num = (num2 * 1037730253U) ^ 2845801949U;
						continue;
					}
					case 72U:
						text = Class18.smethod_0(text, <Module>.smethod_4<string>(-1258030736));
						num = 878419936U;
						continue;
					case 73U:
					{
						int num5;
						int num22;
						num = ((num5 < num22) ? 214081653U : 2043085714U);
						continue;
					}
					case 74U:
						int_0 += 6;
						Class34.smethod_3(logicChannel_0.Ptr, int_0, 16);
						num = (num2 * 3637052022U) ^ 1280301264U;
						continue;
					case 75U:
					{
						int num5;
						num = ((num5 != 0) ? 545805095U : 316761034U) ^ (num2 * 2746671228U);
						continue;
					}
					case 76U:
					{
						int num17;
						dictionary_0.Add(GlobalNames.Length_of_the_copied_PDU, num17);
						int_0 += num17;
						num = (num2 * 1775678055U) ^ 1686639065U;
						continue;
					}
					case 77U:
						Class34.smethod_3(logicChannel_0.Ptr, int_0, 2);
						num = (num2 * 4279328922U) ^ 603931726U;
						continue;
					case 78U:
						goto IL_00BB;
					case 79U:
						goto IL_012D;
					case 80U:
						num = (num2 * 3568189715U) ^ 575559292U;
						continue;
					case 81U:
						num = (num2 * 1729151166U) ^ 4286028342U;
						continue;
					case 82U:
						int_0 += 3;
						num = (num2 * 3171941171U) ^ 2826660707U;
						continue;
					case 83U:
						goto IL_014A;
					case 84U:
						text = Class18.smethod_0(text, <Module>.smethod_2<string>(678673206));
						num = 184101094U;
						continue;
					case 85U:
					{
						int num8;
						num = ((num8 != 3) ? 2918416750U : 2631059448U) ^ (num2 * 967556996U);
						continue;
					}
					case 86U:
						num = (num2 * 1003151897U) ^ 119392815U;
						continue;
					case 87U:
					{
						int num8 = Class34.smethod_3(logicChannel_0.Ptr, int_0, 3);
						num = (num2 * 1833329320U) ^ 1629767017U;
						continue;
					}
					case 88U:
					{
						int num20;
						int num21 = Class34.smethod_3(logicChannel_0.Ptr, int_0, num20);
						num = (num2 * 2400678125U) ^ 1713513631U;
						continue;
					}
					case 89U:
						flag = true;
						num = (num2 * 2701816476U) ^ 3575005602U;
						continue;
					case 90U:
						dictionary_0.Add(GlobalNames.Status_downlink, num27);
						text = Class18.smethod_1(text, <Module>.smethod_4<string>(-356419868), num27);
						flag = true;
						num = 878692944U;
						continue;
					case 91U:
						dictionary_0.Add(GlobalNames.Authentication_sub_type, num19);
						text = Class18.smethod_1(text, <Module>.smethod_2<string>(404748281), num19);
						num = 257409629U;
						continue;
					case 92U:
						int_0 += 4;
						num = (num2 * 4167394237U) ^ 3562478122U;
						continue;
					case 93U:
						num = (num2 * 2836607870U) ^ 2589073192U;
						continue;
					case 94U:
						num = (num2 * 3916396770U) ^ 973930658U;
						continue;
					case 95U:
						int_0 += 6;
						num = (num2 * 2842726668U) ^ 3214396135U;
						continue;
					case 96U:
						goto IL_069D;
					case 97U:
						int_0 += 16;
						num = (num2 * 3128604781U) ^ 1011604627U;
						continue;
					case 98U:
					{
						int num15;
						int_0 += num15;
						MmPduType mmPduType;
						dictionary_0.Add(GlobalNames.MM_PDU_Type, (int)mmPduType);
						num = (num2 * 907391824U) ^ 3541511060U;
						continue;
					}
					case 99U:
						int_0 += 16;
						num = (num2 * 3692642235U) ^ 3867279655U;
						continue;
					case 100U:
					{
						int num9;
						num = ((num9 == 1) ? 435537396U : 1464533718U);
						continue;
					}
					case 101U:
						goto IL_0617;
					case 102U:
						num = (num2 * 4007618323U) ^ 1614444305U;
						continue;
					case 103U:
						goto IL_0112;
					case 104U:
						int_0 += 4;
						num = (num2 * 3731833060U) ^ 4055188561U;
						continue;
					case 106U:
						text = Class18.smethod_0(text, <Module>.smethod_2<string>(662924492));
						num = 1195761399U;
						continue;
					case 107U:
					{
						int num8;
						num = ((num8 != 3) ? 353519263U : 323182709U);
						continue;
					}
					case 108U:
						int_0 += 16;
						num = 785230030U;
						continue;
					case 109U:
						num = (num2 * 366766243U) ^ 232991876U;
						continue;
					case 110U:
					{
						int num28;
						dictionary_0.Add(GlobalNames.GCK_number, num28);
						num = (num2 * 1645126930U) ^ 2526349249U;
						continue;
					}
					case 111U:
						dictionary_0.Add(GlobalNames.Authentication_Result_R1_R2, num18);
						num = (num2 * 4279586539U) ^ 1395469860U;
						continue;
					case 112U:
					{
						int num9;
						dictionary_0.Add(GlobalNames.Time_type, num9);
						num = (num2 * 249656818U) ^ 3697767626U;
						continue;
					}
					case 113U:
						num = ((!dictionary_0.ContainsKey(GlobalNames.Options_bit)) ? 433305415U : 1093905333U) ^ (num2 * 274379318U);
						continue;
					case 114U:
					{
						int num23;
						dictionary_0.Add(GlobalNames.Number_of_GCKs_changed, num23);
						int_0 += 4;
						int num14 = 0;
						num = (num2 * 1596743113U) ^ 606900560U;
						continue;
					}
					case 115U:
						int_0 += 5;
						num = (num2 * 523798941U) ^ 309847273U;
						continue;
					case 116U:
						dictionary_0.Add(GlobalNames.MM_Not_supported_PDU_type, num12);
						text = Class18.smethod_0(text, <Module>.smethod_2<string>(-811120102));
						num = 565058649U;
						continue;
					case 117U:
					{
						int num28 = Class34.smethod_3(logicChannel_0.Ptr, int_0, 16);
						int num14;
						num = ((num14 == 0) ? 17341130U : 301102423U);
						continue;
					}
					case 118U:
						int_0 += 2;
						num = (num2 * 2921136651U) ^ 1331970963U;
						continue;
					case 119U:
						flag = true;
						num = 299581075U;
						continue;
					case 120U:
					{
						int num20 = 4;
						num = (num2 * 1142559195U) ^ 3613956892U;
						continue;
					}
					case 121U:
					{
						int num20 = 2;
						num = (num2 * 632796325U) ^ 3812857111U;
						continue;
					}
					case 122U:
						goto IL_01A9;
					case 123U:
						text = Class18.smethod_1(text, <Module>.smethod_6<string>(-1507730019), num29);
						flag = true;
						num = 1229628181U;
						continue;
					case 124U:
						flag = true;
						num = 1670260705U;
						continue;
					case 125U:
					{
						int num14;
						num14++;
						num = (num2 * 724364802U) ^ 3140942762U;
						continue;
					}
					case 126U:
					{
						int num20 = 6;
						num = (num2 * 441568207U) ^ 213404749U;
						continue;
					}
					case 127U:
						num = ((num12 == 0) ? 1076210607U : 361116345U) ^ (num2 * 3891093520U);
						continue;
					case 128U:
						int_0 += 2;
						num = (num2 * 867284318U) ^ 1080961796U;
						continue;
					case 129U:
						num = (num2 * 974878634U) ^ 2415997474U;
						continue;
					case 130U:
					{
						int num8;
						num = ((num8 != 4) ? 1500622899U : 405958683U) ^ (num2 * 3058167277U);
						continue;
					}
					case 131U:
						int_0 += 3;
						num = (num2 * 2544982915U) ^ 899842877U;
						continue;
					case 132U:
						num = (num2 * 2173783362U) ^ 2369803590U;
						continue;
					case 133U:
					{
						int num25;
						dictionary_0.Add(GlobalNames.GCK_VN, num25);
						num = (num2 * 1293988631U) ^ 2945213869U;
						continue;
					}
					case 134U:
						goto IL_00FA;
					case 135U:
					{
						int num26;
						dictionary_0.Add(GlobalNames.SCK_ver_number, num26);
						num = (num2 * 1051052010U) ^ 593780869U;
						continue;
					}
					case 136U:
						Class34.smethod_3(logicChannel_0.Ptr, int_0, 6);
						num = (num2 * 2298471368U) ^ 75077166U;
						continue;
					case 137U:
						flag = true;
						num = (num2 * 2606329891U) ^ 3978853784U;
						continue;
					case 138U:
					{
						int num30 = Class34.smethod_3(logicChannel_0.Ptr, int_0, 1);
						int_0++;
						int num22 = Class34.smethod_3(logicChannel_0.Ptr, int_0, 4);
						dictionary_0.Add(GlobalNames.Number_of_SCKs_changed, num22);
						int_0 += 4;
						num = ((num30 != 1) ? 3747656253U : 3292712322U) ^ (num2 * 530264172U);
						continue;
					}
					case 139U:
					{
						int num8;
						num = ((num8 != 2) ? 835355001U : 1404737450U);
						continue;
					}
					case 140U:
					{
						MmPduType mmPduType;
						text = Class18.smethod_0(text, mmPduType.ToString());
						switch (mmPduType)
						{
						case MmPduType.D_OTAR:
							goto IL_0094;
						case MmPduType.D_AUTHENTICATION:
							goto IL_00BB;
						case MmPduType.D_CK_CHANGE_DEMAND:
							goto IL_00DB;
						case MmPduType.D_DISABLE:
							goto IL_00FA;
						case MmPduType.D_ENABLE:
							goto IL_0106;
						case MmPduType.D_LOCATION_UPDATE_ACCEPT:
							goto IL_0112;
						case MmPduType.D_LOCATION_UPDATE_COMMAND:
							goto IL_012D;
						case MmPduType.D_LOCATION_UPDATE_REJECT:
							goto IL_014A;
						case MmPduType.Reserved8:
							goto IL_0165;
						case MmPduType.D_LOCATION_UPDATE_PROCEEDING:
							goto IL_0171;
						case MmPduType.D_ATTACH_DETACH_GROUP_IDENTITY:
							goto IL_018E;
						case MmPduType.D_ATTACH_DETACH_GROUP_IDENTITY_ACKNOWLEDGEMENT:
							goto IL_01A9;
						case MmPduType.D_MM_STATUS:
							goto IL_01C4;
						case MmPduType.Reserved13:
							goto IL_01DE;
						case MmPduType.Reserved14:
							goto IL_01EA;
						case MmPduType.MM_PDU_FUNCTION_NOT_SUPPORTED:
							goto IL_01F6;
						default:
							num = (num2 * 1218254275U) ^ 1958219984U;
							continue;
						}
						break;
					}
					}
					goto Block_30;
					IL_0094:
					num29 = Class34.smethod_3(logicChannel_0.Ptr, int_0, 4);
					dictionary_0.Add(GlobalNames.Otar_sub_type, num29);
					num = 138875226U;
					continue;
					IL_00BB:
					num19 = Class34.smethod_3(logicChannel_0.Ptr, int_0, 2);
					int_0 += 2;
					num = 373947247U;
					continue;
					IL_00DB:
					Class34.smethod_3(logicChannel_0.Ptr, int_0, 1);
					int_0++;
					num = 1270914544U;
					continue;
					IL_00FA:
					flag = true;
					num = 1419891712U;
					continue;
					IL_0106:
					flag = true;
					num = 1196893613U;
					continue;
					IL_0112:
					int_0 = Class21.smethod_0(logicChannel_0, int_0, this.rules_0, dictionary_0);
					num = 1747908344U;
					continue;
					IL_012D:
					int_0 = Class21.smethod_0(logicChannel_0, int_0, this.rules_1, dictionary_0);
					flag = true;
					num = 1270147716U;
					continue;
					IL_014A:
					int_0 = Class21.smethod_0(logicChannel_0, int_0, this.rules_2, dictionary_0);
					num = 222721526U;
					continue;
					IL_0165:
					flag = true;
					num = 2113697131U;
					continue;
					IL_0171:
					int_0 = Class21.smethod_0(logicChannel_0, int_0, this.rules_3, dictionary_0);
					flag = true;
					num = 1444951146U;
					continue;
					IL_018E:
					int_0 = Class21.smethod_0(logicChannel_0, int_0, this.rules_4, dictionary_0);
					num = 885030849U;
					continue;
					IL_01A9:
					int_0 = Class21.smethod_0(logicChannel_0, int_0, this.rules_5, dictionary_0);
					num = 1747908344U;
					continue;
					IL_01C4:
					num27 = Class34.smethod_3(logicChannel_0.Ptr, int_0, 6);
					num = 95295798U;
					continue;
					IL_01DE:
					flag = true;
					num = 1364992927U;
					continue;
					IL_01EA:
					flag = true;
					num = 1747908344U;
					continue;
					IL_01F6:
					num12 = Class34.smethod_3(logicChannel_0.Ptr, int_0, 4);
					num = 1364040665U;
					continue;
					IL_0617:
					flag = true;
					num = 2110045299U;
					continue;
					IL_069D:
					num18 = Class34.smethod_3(logicChannel_0.Ptr, int_0, 1);
					int_0++;
					num = 1493666365U;
					continue;
					IL_0C6B:
					num4 = 0;
					num = 1898323307U;
				}
			}
			Block_30:;
		}
		catch (Exception ex)
		{
			for (;;)
			{
				IL_13AA:
				uint num31 = 568158765U;
				for (;;)
				{
					uint num2;
					switch ((num2 = num31 ^ 500339449U) % 6U)
					{
					case 0U:
						goto IL_13AA;
					case 2U:
						this.bool_0 = false;
						num31 = 446979378U;
						continue;
					case 3U:
						Class21.smethod_5(logicChannel_0, ex, <Module>.smethod_5<string>(-1737768564), text, dictionary_0);
						num31 = (dictionary_0.ContainsKey(GlobalNames.ErrWithPDU) ? 1267062965U : 14662296U);
						continue;
					case 4U:
						this.bool_0 = true;
						num31 = (num2 * 3710826711U) ^ 2206588358U;
						continue;
					case 5U:
						dictionary_0.Add(GlobalNames.ErrWithPDU, 7);
						num31 = (num2 * 548955908U) ^ 2930667569U;
						continue;
					}
					goto Block_33;
				}
			}
			Block_33:;
		}
	}

	// Token: 0x06000258 RID: 600 RVA: 0x00041020 File Offset: 0x0003F220
	private void method_1(LogicChannel logicChannel_0, ref int int_0, Dictionary<GlobalNames, int> dictionary_0)
	{
		string text = string.Empty;
		bool flag = false;
		if (this.bool_0)
		{
			return;
		}
		text = <Module>.smethod_6<string>(-1450820006);
		try
		{
			int num = Class34.smethod_3(logicChannel_0.Ptr, int_0, 1);
			int_0++;
			int num2 = 0;
			for (;;)
			{
				IL_14F0:
				uint num3 = ((num == 1) ? 2646370890U : 2927190443U);
				for (;;)
				{
					uint num4;
					switch ((num4 = num3 ^ 4061392357U) % 140U)
					{
					case 0U:
					{
						int num5;
						num3 = ((num5 != 9) ? 4033398430U : 4276979114U);
						continue;
					}
					case 1U:
					{
						int num5;
						num3 = ((num5 != 4) ? 2704292783U : 3334654089U);
						continue;
					}
					case 2U:
					{
						int num5;
						dictionary_0.Add(GlobalNames.Group_identity_downlink, num5);
						num2 -= 7;
						num3 = 3091619280U;
						continue;
					}
					case 3U:
					{
						int num6;
						num3 = ((num6 == 3) ? 2853396124U : 2251127200U);
						continue;
					}
					case 4U:
						num3 = (dictionary_0.ContainsKey(GlobalNames.Default_group_attachment_lifetime) ? 3646364545U : 3957693260U) ^ (num4 * 1248946979U);
						continue;
					case 5U:
					{
						int num6;
						num3 = ((num6 != 4) ? 3345471239U : 3137880402U);
						continue;
					}
					case 6U:
					{
						int num7 = dictionary_0[GlobalNames.PduStartOffset];
						num3 = (num4 * 1298387528U) ^ 1943190635U;
						continue;
					}
					case 8U:
					{
						int num9;
						int num8 = num9;
						int num10 = Class34.smethod_3(logicChannel_0.Ptr, int_0, 1);
						num3 = (num4 * 1792851283U) ^ 1950938243U;
						continue;
					}
					case 9U:
					{
						int_0 += 24;
						int num11;
						num11 -= 24;
						num3 = (num4 * 3366561705U) ^ 316509132U;
						continue;
					}
					case 10U:
						int_0++;
						num3 = (num4 * 2821452613U) ^ 131233418U;
						continue;
					case 11U:
					{
						int num5;
						num3 = ((num5 != 11) ? 4078710480U : 4137363329U);
						continue;
					}
					case 12U:
						num3 = (num4 * 3090768135U) ^ 3354665622U;
						continue;
					case 13U:
						int_0++;
						num3 = (num4 * 1732205470U) ^ 3194216342U;
						continue;
					case 14U:
					{
						int num12 = Class34.smethod_3(logicChannel_0.Ptr, int_0, 1);
						int_0++;
						int num13;
						num13--;
						num2--;
						num3 = ((num12 != 1) ? 3026649793U : 3222903026U) ^ (num4 * 191913887U);
						continue;
					}
					case 15U:
					{
						int num14 = dictionary_0[GlobalNames.PduStartOffset];
						num3 = (num4 * 1445723291U) ^ 611684542U;
						continue;
					}
					case 16U:
					{
						int num11;
						num11--;
						num3 = (num4 * 2992673425U) ^ 3774295694U;
						continue;
					}
					case 17U:
					{
						int num15;
						dictionary_0.Add(GlobalNames.MM_GSSI2, num15);
						num3 = (num4 * 652455120U) ^ 2441874025U;
						continue;
					}
					case 18U:
						int_0++;
						num3 = (num4 * 1973547538U) ^ 3507727907U;
						continue;
					case 19U:
						int_0 += 4;
						num3 = (num4 * 2453656538U) ^ 3393669200U;
						continue;
					case 20U:
					{
						int num9;
						int_0 += num9;
						num3 = (num4 * 4076440689U) ^ 4129613644U;
						continue;
					}
					case 21U:
						flag = true;
						num3 = 3083455273U;
						continue;
					case 22U:
					{
						int num16 = 32;
						num3 = (num4 * 1132402677U) ^ 1732513014U;
						continue;
					}
					case 24U:
					{
						text = Class18.smethod_0(text, <Module>.smethod_2<string>(-1127859543));
						int num15 = Class34.smethod_3(logicChannel_0.Ptr, int_0, 24);
						int_0 += 24;
						num3 = 4286340218U;
						continue;
					}
					case 25U:
					{
						int num17 = Class34.smethod_3(logicChannel_0.Ptr, int_0, 1);
						dictionary_0.Add(GlobalNames.TEI_request_flag, num17);
						num3 = (num4 * 764508771U) ^ 659851328U;
						continue;
					}
					case 26U:
					{
						int num5;
						num3 = ((num5 != 5) ? 4236131843U : 3388783114U);
						continue;
					}
					case 27U:
					{
						int num14;
						int num18;
						num3 = ((int_0 + 17 <= num18 + num14) ? 3424421217U : 2381070961U) ^ (num4 * 4063039221U);
						continue;
					}
					case 28U:
					{
						int num5;
						dictionary_0.Add(GlobalNames.Group_identity_location_accept, num5);
						num3 = 3560011587U;
						continue;
					}
					case 29U:
					{
						int num5 = Class34.smethod_3(logicChannel_0.Ptr, int_0, 4);
						num3 = 2923623882U;
						continue;
					}
					case 30U:
					{
						int num7;
						int num9;
						int num19;
						num3 = ((int_0 - num7 + num9 <= num19) ? 3153280909U : 2485834896U) ^ (num4 * 3311693784U);
						continue;
					}
					case 31U:
						Class34.smethod_3(logicChannel_0.Ptr, int_0, 2);
						num3 = (num4 * 3251502021U) ^ 1536604395U;
						continue;
					case 32U:
					{
						int num20 = Class34.smethod_3(logicChannel_0.Ptr, int_0, 2);
						int_0 += 2;
						int num11;
						num11 -= 2;
						num2 -= 2;
						int num6;
						if (num20 == 0)
						{
							text = Class18.smethod_0(text, <Module>.smethod_5<string>(-1444241373));
							int num21 = Class34.smethod_3(logicChannel_0.Ptr, int_0, 24);
							int_0 += 24;
							num11 -= 24;
							num2 -= 24;
							if (num6 == 0)
							{
								dictionary_0.Add(GlobalNames.MM_GSSI, num21);
							}
							if (num6 == 1)
							{
								dictionary_0.Add(GlobalNames.MM_GSSI2, num21);
							}
							if (num6 == 2)
							{
								dictionary_0.Add(GlobalNames.MM_GSSI3, num21);
							}
							if (num6 == 3)
							{
								dictionary_0.Add(GlobalNames.MM_GSSI4, num21);
							}
							if (num6 == 4)
							{
								dictionary_0.Add(GlobalNames.MM_GSSI5, num21);
							}
						}
						if (num20 == 1)
						{
							text = Class18.smethod_0(text, <Module>.smethod_6<string>(-1032715687));
							int num22 = Class34.smethod_3(logicChannel_0.Ptr, int_0, 24);
							int_0 += 24;
							num11 -= 24;
							if (num6 == 0)
							{
								dictionary_0.Add(GlobalNames.MM_GSSI, num22);
							}
							if (num6 == 1)
							{
								dictionary_0.Add(GlobalNames.MM_GSSI2, num22);
							}
							if (num6 == 2)
							{
								dictionary_0.Add(GlobalNames.MM_GSSI3, num22);
							}
							if (num6 == 3)
							{
								dictionary_0.Add(GlobalNames.MM_GSSI4, num22);
							}
							if (num6 == 4)
							{
								dictionary_0.Add(GlobalNames.MM_GSSI5, num22);
							}
							Class34.smethod_3(logicChannel_0.Ptr, int_0, 24);
							int_0 += 24;
							num11 -= 24;
						}
						if (num20 == 2)
						{
							text = Class18.smethod_0(text, <Module>.smethod_6<string>(-1730614521));
							int num23 = Class34.smethod_3(logicChannel_0.Ptr, int_0, 24);
							int_0 += 24;
							num11 -= 24;
							if (num6 == 0)
							{
								dictionary_0.Add(GlobalNames.MM_vGSSI, num23);
							}
						}
						num3 = ((num20 == 3) ? 2836591213U : 3937622071U);
						continue;
					}
					case 33U:
					{
						int num5;
						num3 = ((num5 != 15) ? 2995449900U : 3384460098U);
						continue;
					}
					case 34U:
					{
						int num6;
						num3 = ((num6 == 1) ? 3138644044U : 4280364857U);
						continue;
					}
					case 35U:
					{
						int num9;
						int_0 += num9;
						num3 = (num4 * 1302713225U) ^ 1148792041U;
						continue;
					}
					case 36U:
					{
						int num9;
						int_0 += num9;
						num3 = (num4 * 827757639U) ^ 3511142246U;
						continue;
					}
					case 37U:
						text = Class18.smethod_0(text, <Module>.smethod_5<string>(1845601140));
						num3 = 3628531438U;
						continue;
					case 38U:
					{
						int num9;
						int_0 += num9;
						num3 = (num4 * 2715032331U) ^ 2802469473U;
						continue;
					}
					case 39U:
					{
						text = Class18.smethod_0(text, <Module>.smethod_3<string>(-605183589));
						int num9;
						int num13 = num9;
						num3 = 2229248033U;
						continue;
					}
					case 40U:
					{
						int num6;
						num3 = ((num6 == 0) ? 1362065382U : 417446791U) ^ (num4 * 958872136U);
						continue;
					}
					case 41U:
					{
						int num5;
						num3 = ((num5 != 7) ? 2934499318U : 3860477502U);
						continue;
					}
					case 43U:
					{
						int num11;
						num11 -= 2;
						num3 = (num4 * 3081798133U) ^ 1308498754U;
						continue;
					}
					case 44U:
					{
						int num24;
						dictionary_0.Add(GlobalNames.CK_provision_flag, num24);
						num3 = (num4 * 621336717U) ^ 244413075U;
						continue;
					}
					case 45U:
						num3 = 2646370890U;
						continue;
					case 47U:
					{
						int num25 = Class34.smethod_3(logicChannel_0.Ptr, int_0, 2);
						num3 = (num4 * 2041499522U) ^ 3851489507U;
						continue;
					}
					case 48U:
					{
						int num26;
						num3 = ((num26 < 8) ? 3295190627U : 3979807176U) ^ (num4 * 1520350965U);
						continue;
					}
					case 49U:
					{
						int num27 = Class34.smethod_3(logicChannel_0.Ptr, int_0, 16);
						num3 = (num4 * 3431378049U) ^ 4259092698U;
						continue;
					}
					case 50U:
					{
						int num9;
						int_0 += num9;
						num3 = (num4 * 1032963484U) ^ 1922581535U;
						continue;
					}
					case 51U:
						num3 = ((!flag) ? 1840502828U : 390827015U) ^ (num4 * 1915576948U);
						continue;
					case 52U:
					{
						int num6;
						num3 = ((num6 == 2) ? 3895284871U : 2634739446U);
						continue;
					}
					case 53U:
						int_0 += 2;
						num3 = (num4 * 3552899633U) ^ 4074523567U;
						continue;
					case 54U:
					{
						int num8;
						num8--;
						num3 = (num4 * 1488230361U) ^ 3003898618U;
						continue;
					}
					case 55U:
					{
						int num9 = Class34.smethod_3(logicChannel_0.Ptr, int_0, 11);
						int_0 += 11;
						num3 = (num4 * 3422435767U) ^ 3934449734U;
						continue;
					}
					case 56U:
						int_0++;
						num3 = (num4 * 2708271930U) ^ 653586309U;
						continue;
					case 57U:
					{
						int num5;
						num3 = ((num5 == 1) ? 3873806056U : 3263823856U);
						continue;
					}
					case 58U:
					{
						int num5;
						num3 = ((num5 != 10) ? 4185979516U : 2914931545U);
						continue;
					}
					case 59U:
					{
						int num6;
						int num28;
						num3 = ((num6 >= num28) ? 2600742918U : 3836631121U);
						continue;
					}
					case 60U:
					{
						int num29 = Class34.smethod_3(logicChannel_0.Ptr, int_0, 1);
						int_0++;
						num3 = (num4 * 546194086U) ^ 2010993873U;
						continue;
					}
					case 61U:
					{
						int num30;
						dictionary_0.Add(GlobalNames.Group_report_response, num30);
						int_0++;
						num3 = 2704292783U;
						continue;
					}
					case 62U:
						Class34.smethod_3(logicChannel_0.Ptr, int_0, 1);
						num3 = (num4 * 3084923840U) ^ 4234146769U;
						continue;
					case 63U:
					{
						int num9;
						int_0 += num9;
						num3 = (num4 * 3865665545U) ^ 3834597690U;
						continue;
					}
					case 64U:
					{
						int num5;
						num3 = ((num5 != 12) ? 3230657130U : 2621178225U);
						continue;
					}
					case 65U:
						int_0++;
						num3 = (num4 * 3282062055U) ^ 610605479U;
						continue;
					case 66U:
					{
						int num27;
						dictionary_0.Add(GlobalNames.CCK_id, num27);
						int_0 += 16;
						num3 = (num4 * 970123463U) ^ 639845178U;
						continue;
					}
					case 68U:
						Class34.smethod_3(logicChannel_0.Ptr, int_0, 1);
						int_0++;
						num3 = (num4 * 2276543748U) ^ 2293463099U;
						continue;
					case 69U:
						flag = false;
						num3 = 2233364679U;
						continue;
					case 70U:
					{
						int num10;
						dictionary_0.Add(GlobalNames.Authentication_Result_R1_R2, num10);
						num3 = (num4 * 1675173521U) ^ 499450031U;
						continue;
					}
					case 71U:
					{
						int num5;
						num3 = ((num5 == 13) ? 3759986449U : 3127808760U);
						continue;
					}
					case 72U:
					{
						int num9;
						int num11 = num9 - 6;
						num3 = 2459629185U;
						continue;
					}
					case 73U:
					{
						int num9;
						int_0 += num9;
						num3 = (num4 * 1336815774U) ^ 1020012823U;
						continue;
					}
					case 74U:
					{
						int num5;
						num3 = ((num5 != 7) ? 2233364679U : 3395061910U);
						continue;
					}
					case 75U:
					{
						int num8;
						int_0 += num8;
						num3 = 4185979516U;
						continue;
					}
					case 76U:
					{
						int num13;
						num13--;
						num2--;
						Class34.smethod_3(logicChannel_0.Ptr, int_0, 1);
						num3 = (num4 * 348746409U) ^ 1893270244U;
						continue;
					}
					case 77U:
					{
						bool flag2 = Class34.smethod_3(logicChannel_0.Ptr, int_0, 1) != 0;
						int_0++;
						int num13;
						num13--;
						num2--;
						num3 = ((!flag2) ? 325701735U : 1649645949U) ^ (num4 * 3883255189U);
						continue;
					}
					case 78U:
						Class34.smethod_3(logicChannel_0.Ptr, int_0, 24);
						int_0 += 24;
						num3 = 3027661647U;
						continue;
					case 79U:
					{
						int num5;
						num3 = ((num5 == 6) ? 2572135390U : 3561539881U);
						continue;
					}
					case 80U:
					{
						int num29;
						num3 = ((num29 == 1) ? 3281110450U : 3952205309U);
						continue;
					}
					case 81U:
					{
						int num8;
						num8 -= 16;
						num3 = (num4 * 1868256861U) ^ 2284604231U;
						continue;
					}
					case 82U:
						num3 = (dictionary_0.ContainsKey(GlobalNames.IsFragmentedPDU) ? 757473972U : 1213584786U) ^ (num4 * 1559505710U);
						continue;
					case 84U:
						num3 = (num4 * 2146421744U) ^ 1263846582U;
						continue;
					case 85U:
					{
						int num13;
						num13--;
						num2--;
						num3 = (num4 * 3224482584U) ^ 14444195U;
						continue;
					}
					case 86U:
					{
						int num5;
						num3 = ((num5 == 3) ? 3262989394U : 3413412614U);
						continue;
					}
					case 87U:
						int_0 += 2;
						num3 = (num4 * 3592800538U) ^ 2381890150U;
						continue;
					case 88U:
					{
						int num6;
						num3 = ((num6 != 0) ? 1459924407U : 691065334U) ^ (num4 * 2826293488U);
						continue;
					}
					case 89U:
						num3 = (dictionary_0.ContainsKey(GlobalNames.Group_identity_downlink) ? 197004U : 209894617U) ^ (num4 * 2027326922U);
						continue;
					case 90U:
						Class34.smethod_3(logicChannel_0.Ptr, int_0, 1);
						num2--;
						num3 = (num4 * 3916934802U) ^ 452566184U;
						continue;
					case 91U:
					{
						int num19 = dictionary_0[GlobalNames.MacPduRealSize];
						num3 = 2181406659U;
						continue;
					}
					case 92U:
					{
						int num9;
						int_0 += num9;
						num3 = (num4 * 1252184526U) ^ 3179982504U;
						continue;
					}
					case 93U:
					{
						int num31 = Class34.smethod_3(logicChannel_0.Ptr, int_0, 24);
						num3 = (num4 * 572766049U) ^ 1472224101U;
						continue;
					}
					case 94U:
					{
						int num11;
						num11 -= 24;
						num3 = (num4 * 3990017572U) ^ 3449812148U;
						continue;
					}
					case 95U:
					{
						int num15;
						dictionary_0.Add(GlobalNames.MM_GSSI5, num15);
						num3 = (num4 * 1792947558U) ^ 2937939181U;
						continue;
					}
					case 96U:
					{
						text = Class18.smethod_0(text, <Module>.smethod_3<string>(605366826));
						int num30 = Class34.smethod_3(logicChannel_0.Ptr, int_0, 1);
						num3 = (dictionary_0.ContainsKey(GlobalNames.Group_report_response) ? 3842585211U : 2624280740U);
						continue;
					}
					case 97U:
					{
						int num14;
						int num16;
						int num18;
						num3 = ((num18 + num14 != int_0 + num16) ? 2855729872U : 3248739276U);
						continue;
					}
					case 98U:
					{
						int num7;
						int num19;
						num3 = ((int_0 - num7 + 4 + 11 > num19) ? 3736393201U : 3148634308U) ^ (num4 * 3990474652U);
						continue;
					}
					case 99U:
						num2 -= 6;
						num3 = (num4 * 679060235U) ^ 2190019845U;
						continue;
					case 100U:
					{
						int num5;
						text = Class18.smethod_1(text, <Module>.smethod_4<string>(1514052669), num5);
						num3 = 3308884183U;
						continue;
					}
					case 101U:
					{
						int num14 = 0;
						num3 = (num4 * 4128692353U) ^ 3756220483U;
						continue;
					}
					case 102U:
					{
						int num24 = Class34.smethod_3(logicChannel_0.Ptr, int_0, 1);
						num3 = (num4 * 58869755U) ^ 3539708311U;
						continue;
					}
					case 103U:
					{
						int num31;
						dictionary_0.Add(GlobalNames.MM_vGSSI, num31);
						num3 = (num4 * 84820021U) ^ 3197051736U;
						continue;
					}
					case 104U:
						int_0++;
						num3 = (num4 * 1407046195U) ^ 1474642552U;
						continue;
					case 105U:
					{
						int num25;
						dictionary_0.Add(GlobalNames.Default_group_attachment_lifetime, num25);
						num3 = 3497286482U;
						continue;
					}
					case 106U:
					{
						int num15;
						dictionary_0.Add(GlobalNames.MM_GSSI3, num15);
						num3 = (num4 * 1058774578U) ^ 520773586U;
						continue;
					}
					case 107U:
						goto IL_14F0;
					case 108U:
					{
						int num5;
						num3 = ((num5 != 8) ? 2832393305U : 3924505548U);
						continue;
					}
					case 109U:
						num = Class34.smethod_3(logicChannel_0.Ptr, int_0, 1);
						int_0++;
						num3 = 3332960223U;
						continue;
					case 111U:
					{
						int num9;
						int_0 += num9;
						num3 = (num4 * 3008838003U) ^ 2863584297U;
						continue;
					}
					case 112U:
						num2 -= 2;
						num3 = (num4 * 2866796534U) ^ 3900816261U;
						continue;
					case 113U:
					{
						int num15;
						dictionary_0.Add(GlobalNames.MM_GSSI4, num15);
						num3 = (num4 * 872931520U) ^ 2683437920U;
						continue;
					}
					case 114U:
					{
						num2--;
						int num18 = 0;
						num3 = ((!dictionary_0.TryGetValue(GlobalNames.MacPduRealSize, out num18)) ? 4112676110U : 4073056910U) ^ (num4 * 3325072044U);
						continue;
					}
					case 115U:
					{
						int num11;
						num11 -= 24;
						num3 = (num4 * 238282903U) ^ 4003300468U;
						continue;
					}
					case 116U:
					{
						int num24;
						num3 = ((num24 == 1) ? 2311989963U : 2296359806U) ^ (num4 * 1631309860U);
						continue;
					}
					case 117U:
					{
						int num8;
						num8 -= 3;
						num3 = (num4 * 1160407076U) ^ 1913257581U;
						continue;
					}
					case 118U:
					{
						int num9;
						int_0 += num9;
						num3 = (num4 * 3585428508U) ^ 2687884549U;
						continue;
					}
					case 119U:
					{
						int num9;
						int_0 += num9;
						num3 = (num4 * 52010738U) ^ 2583599664U;
						continue;
					}
					case 120U:
					{
						int num9;
						num2 = num9;
						num3 = (dictionary_0.ContainsKey(GlobalNames.Group_identity_location_accept) ? 1311668647U : 933849677U) ^ (num4 * 2897867126U);
						continue;
					}
					case 121U:
					{
						int num6 = 0;
						num3 = (num4 * 2002559346U) ^ 2279848159U;
						continue;
					}
					case 122U:
					{
						int num5;
						num3 = ((num5 != 0) ? 2411659003U : 3142288757U) ^ (num4 * 1286692451U);
						continue;
					}
					case 123U:
					{
						int num16 = 0;
						int num26 = 0;
						num3 = (dictionary_0.TryGetValue(GlobalNames.LLC_Pdu_Type, out num26) ? 2430519081U : 4239962164U);
						continue;
					}
					case 124U:
					{
						int num26;
						num3 = ((num26 > 3) ? 2825855321U : 3333750788U) ^ (num4 * 3382860228U);
						continue;
					}
					case 125U:
						num3 = ((num == 0) ? 4126350268U : 4106183432U) ^ (num4 * 1131253562U);
						continue;
					case 126U:
					{
						bool flag3 = Class34.smethod_3(logicChannel_0.Ptr, int_0, 1) != 0;
						int_0++;
						int num8;
						num8--;
						num3 = (flag3 ? 439454136U : 167881275U) ^ (num4 * 1773264533U);
						continue;
					}
					case 127U:
					{
						num2--;
						int num29;
						num3 = ((num29 != 0) ? 1706194200U : 755493603U) ^ (num4 * 3628430243U);
						continue;
					}
					case 129U:
					{
						int num5;
						num3 = ((num5 != 14) ? 2842245367U : 2493885779U);
						continue;
					}
					case 130U:
					{
						int num6;
						num6++;
						num3 = 2795295318U;
						continue;
					}
					case 131U:
						text = Class18.smethod_0(text, <Module>.smethod_6<string>(378449737));
						num3 = 3725267336U;
						continue;
					case 133U:
					{
						int num28 = Class34.smethod_3(logicChannel_0.Ptr, int_0, 6);
						int_0 += 6;
						num3 = (num4 * 1016814680U) ^ 3436983090U;
						continue;
					}
					case 134U:
					{
						int num5;
						num3 = ((num5 != 2) ? 3506534635U : 2487316539U);
						continue;
					}
					case 135U:
					{
						Class34.smethod_3(logicChannel_0.Ptr, int_0, 5);
						int_0 += 5;
						int num11;
						num11 -= 5;
						num3 = (num4 * 1776031913U) ^ 3913226560U;
						continue;
					}
					case 136U:
						text = Class18.smethod_0(text, <Module>.smethod_5<string>(-498840933));
						num3 = 3754272533U;
						continue;
					case 138U:
						num2 -= 5;
						num3 = (num4 * 3196529451U) ^ 4254130551U;
						continue;
					case 139U:
					{
						int num15;
						dictionary_0.Add(GlobalNames.MM_GSSI, num15);
						num3 = (num4 * 1601993622U) ^ 3113710597U;
						continue;
					}
					}
					goto Block_64;
				}
			}
			Block_64:;
		}
		catch (Exception ex)
		{
			this.bool_0 = true;
			Class21.smethod_5(logicChannel_0, ex, <Module>.smethod_2<string>(726507735), text, dictionary_0);
			if (!dictionary_0.ContainsKey(GlobalNames.ErrWithPDU))
			{
				for (;;)
				{
					IL_156E:
					uint num32 = 3225599576U;
					for (;;)
					{
						uint num4;
						switch ((num4 = num32 ^ 4061392357U) % 3U)
						{
						case 0U:
							goto IL_156E;
						case 1U:
							dictionary_0.Add(GlobalNames.ErrWithPDU, 7);
							num32 = (num4 * 1775535193U) ^ 2501223645U;
							continue;
						}
						goto Block_68;
					}
				}
				Block_68:;
			}
			this.bool_0 = false;
		}
	}

	// Token: 0x0600025A RID: 602 RVA: 0x00011090 File Offset: 0x0000F290
	static string smethod_0(string string_0, string string_1)
	{
		return string_0 + string_1;
	}

	// Token: 0x0600025B RID: 603 RVA: 0x0002F294 File Offset: 0x0002D494
	static string smethod_1(object object_0, object object_1, object object_2)
	{
		return object_0 + object_1 + object_2;
	}

	// Token: 0x0400023B RID: 571
	private bool bool_0;

	// Token: 0x0400023C RID: 572
	private readonly Rules[] rules_0 = new Rules[]
	{
		new Rules(GlobalNames.Location_update_accept_type, 3, RulesType.Direct, 0, 0, 0),
		new Rules(GlobalNames.Options_bit, 1, RulesType.Options_bit, 0, 0, 0),
		new Rules(GlobalNames.Presence_bit, 1, RulesType.Presence_bit, 1, 0, 0),
		new Rules(GlobalNames.MM_SSI, 24, RulesType.Direct, 0, 0, 0),
		new Rules(GlobalNames.Presence_bit, 1, RulesType.Presence_bit, 1, 0, 0),
		new Rules(GlobalNames.Reserved, 24, RulesType.Reserved, 0, 0, 0),
		new Rules(GlobalNames.Presence_bit, 1, RulesType.Presence_bit, 1, 0, 0),
		new Rules(GlobalNames.Reserved, 16, RulesType.Reserved, 0, 0, 0),
		new Rules(GlobalNames.Presence_bit, 1, RulesType.Presence_bit, 1, 0, 0),
		new Rules(GlobalNames.Reserved, 14, RulesType.Reserved, 0, 0, 0),
		new Rules(GlobalNames.Presence_bit, 1, RulesType.Presence_bit, 1, 0, 0),
		new Rules(GlobalNames.Reserved, 6, RulesType.Reserved, 0, 0, 0)
	};

	// Token: 0x0400023D RID: 573
	private readonly Rules[] rules_1 = new Rules[]
	{
		new Rules(GlobalNames.Group_identity_report, 1, RulesType.Direct, 0, 0, 0),
		new Rules(GlobalNames.Cipher_control, 1, RulesType.Direct, 0, 0, 0),
		new Rules(GlobalNames.Ciphering_parameters, 10, RulesType.Switch, 389, 1, 0),
		new Rules(GlobalNames.Options_bit, 1, RulesType.Options_bit, 0, 0, 0),
		new Rules(GlobalNames.Presence_bit, 1, RulesType.Presence_bit, 1, 0, 0),
		new Rules(GlobalNames.MM_Address_extension, 24, RulesType.Direct, 0, 0, 0)
	};

	// Token: 0x0400023E RID: 574
	private readonly Rules[] rules_2 = new Rules[]
	{
		new Rules(GlobalNames.Location_update_type, 3, RulesType.Direct, 0, 0, 0),
		new Rules(GlobalNames.Reject_cause, 5, RulesType.Direct, 0, 0, 0),
		new Rules(GlobalNames.Cipher_control, 1, RulesType.Direct, 0, 0, 0),
		new Rules(GlobalNames.Ciphering_parameters, 10, RulesType.Switch, 389, 1, 0),
		new Rules(GlobalNames.Options_bit, 1, RulesType.Options_bit, 0, 0, 0),
		new Rules(GlobalNames.Presence_bit, 1, RulesType.Presence_bit, 1, 0, 0),
		new Rules(GlobalNames.MM_Address_extension, 24, RulesType.Direct, 0, 0, 0)
	};

	// Token: 0x0400023F RID: 575
	private readonly Rules[] rules_3 = new Rules[]
	{
		new Rules(GlobalNames.MM_SSI, 24, RulesType.Direct, 0, 0, 0),
		new Rules(GlobalNames.MM_Address_extension, 24, RulesType.Direct, 0, 0, 0),
		new Rules(GlobalNames.Options_bit, 1, RulesType.Options_bit, 0, 0, 0)
	};

	// Token: 0x04000240 RID: 576
	private readonly Rules[] rules_4 = new Rules[]
	{
		new Rules(GlobalNames.Group_identity_report, 1, RulesType.Direct, 0, 0, 0),
		new Rules(GlobalNames.Group_identity_acknowledgement_request, 1, RulesType.Direct, 0, 0, 0),
		new Rules(GlobalNames.Group_identity_attach_detach_mode, 1, RulesType.Direct, 0, 0, 0),
		new Rules(GlobalNames.Options_bit, 1, RulesType.Options_bit, 0, 0, 0)
	};

	// Token: 0x04000241 RID: 577
	private readonly Rules[] rules_5 = new Rules[]
	{
		new Rules(GlobalNames.Group_identity_accept_reject, 1, RulesType.Direct, 0, 0, 0),
		new Rules(GlobalNames.Reserved, 1, RulesType.Reserved, 0, 0, 0),
		new Rules(GlobalNames.Options_bit, 1, RulesType.Options_bit, 0, 0, 0)
	};
}
}

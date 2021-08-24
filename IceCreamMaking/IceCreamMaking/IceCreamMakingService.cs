using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using static System.Threading.Thread;
using static System.Console;

namespace IceCreamMaking
{
    public class IceCreamMakingService
    {
        public delegate void UpdateUI(EnumClass.UiType type,string message,EnumClass.ProcessType processType);

        public delegate void CallTaskFinish(EnumClass.SignalType signalType);

        public UpdateUI UpdateUIDelegate;

        public CallTaskFinish CallTaskFinishDelegate;
        //草莓原料份数
        static int strawberriesNumbers = 100;
        //巧克力原料份数
        static int chocolateNumbers = 100;
        //冰激凌原料份数
        static int iceCreamNumbers = 100;
        //产品份数
        static int productNumbers = 0;

        /// <summary>
        /// 窗口1添加材料的顺序为草莓 –> 巧克力 -> 冰激凌，每加一种材料的速度为5秒/种
        /// </summary>
        /// <returns></returns>
        public void Windows1ToMakeIceCream(object o)
        {

            try
            {

                UpdateUIDelegate(EnumClass.UiType.ProductWindows01,$"【窗口1开始制作冰激凌】",EnumClass.ProcessType.StartMaking);
                if (IsEnoughOfMaterials(MaterialName.Strawberries))
                {
                    UpdateUIDelegate(EnumClass.UiType.ProductWindows01, $"【窗口1正在加草莓】", EnumClass.ProcessType.AddStrawberriesStart);
                    DeductionInventoryOfMaterials(MaterialName.Strawberries, o);
                    Sleep(TimeSpan.FromSeconds(5));
                    UpdateUIDelegate(EnumClass.UiType.ProductWindows01, $"【窗口1加草莓完成】", EnumClass.ProcessType.AddStrawberriesFinish);
                }

                if (IsEnoughOfMaterials(MaterialName.Chocolate))
                {
                    UpdateUIDelegate(EnumClass.UiType.ProductWindows01, $"【窗口1正在加巧克力】", EnumClass.ProcessType.AddChocolateStart);
                    DeductionInventoryOfMaterials(MaterialName.Chocolate, o);
                    Sleep(TimeSpan.FromSeconds(5));
                    UpdateUIDelegate(EnumClass.UiType.ProductWindows01, $"【窗口1加巧克力完成】", EnumClass.ProcessType.AddChocolateFinish);
                }
                if (IsEnoughOfMaterials(MaterialName.IceCream))
                {
                    UpdateUIDelegate(EnumClass.UiType.ProductWindows01, $"【窗口1正在加冰激凌】", EnumClass.ProcessType.AddIceCreamStart);
                    DeductionInventoryOfMaterials(MaterialName.IceCream, o);
                    Sleep(TimeSpan.FromSeconds(5));
                    UpdateUIDelegate(EnumClass.UiType.ProductWindows01, $"【窗口1加冰激凌完成】",EnumClass.ProcessType.AddIceCreamFinish);
                }

                UpdateUIDelegate(EnumClass.UiType.ProductWindows01, $"【窗口1冰激凌制作完成】",EnumClass.ProcessType.FinishMaking);
                //增加产品库存
                IncreaseInventoryOfProduct();
                if(MinNumberToProduce()>0)
                {
                    CallTaskFinishDelegate(EnumClass.SignalType.StartMaking01);
                }
                

            }
            catch (Exception ex)
            {
                UpdateUIDelegate(EnumClass.UiType.ProductWindows01,$"{ ex.Message}", EnumClass.ProcessType.FinishMaking);
            }
        }


        /// <summary>
        /// 窗口2添加材料的顺序为冰激凌 –> 草莓 –> 巧克力，每加一种材料的速度为8秒/种。
        /// </summary>
        public void Windows2ToMakeIceCream(object o)
        {
            try
            {
                UpdateUIDelegate(EnumClass.UiType.ProductWindows02, $"【窗口2开始制作冰激凌】",EnumClass.ProcessType.StartMaking);

                if (IsEnoughOfMaterials(MaterialName.IceCream))
                {
                    UpdateUIDelegate(EnumClass.UiType.ProductWindows02, $"【窗口2正在加冰激凌】", EnumClass.ProcessType.AddIceCreamStart);
                    DeductionInventoryOfMaterials(MaterialName.IceCream, o);
                    Sleep(TimeSpan.FromSeconds(8));
                    UpdateUIDelegate(EnumClass.UiType.ProductWindows02, $"【窗口2加冰激凌完成】",EnumClass.ProcessType.AddIceCreamFinish);
                }
                if (IsEnoughOfMaterials(MaterialName.Strawberries))
                {
                    UpdateUIDelegate(EnumClass.UiType.ProductWindows02, $"【窗口2正在加草莓】", EnumClass.ProcessType.AddStrawberriesStart);
                    DeductionInventoryOfMaterials(MaterialName.Strawberries, o);
                    Sleep(TimeSpan.FromSeconds(8));
                    UpdateUIDelegate(EnumClass.UiType.ProductWindows02, $"【窗口2加草莓完成】",EnumClass.ProcessType.AddStrawberriesFinish);
                }

                if (IsEnoughOfMaterials(MaterialName.Chocolate))
                {
                    UpdateUIDelegate(EnumClass.UiType.ProductWindows02, $"【窗口2正在加巧克力】", EnumClass.ProcessType.AddChocolateStart);
                    DeductionInventoryOfMaterials(MaterialName.Chocolate, o);
                    Sleep(TimeSpan.FromSeconds(8));
                    UpdateUIDelegate(EnumClass.UiType.ProductWindows02, $"【窗口2加巧克力完成】", EnumClass.ProcessType.AddChocolateFinish);
                }


                UpdateUIDelegate(EnumClass.UiType.ProductWindows02, $"【窗口2冰激凌制作完成】",EnumClass.ProcessType.FinishMaking);

                //增加产品库存
                IncreaseInventoryOfProduct();
                if(MinNumberToProduce()>0)
                {
                    CallTaskFinishDelegate(EnumClass.SignalType.StartMaking02);
                }
               

            }
            catch (Exception ex)
            {
                UpdateUIDelegate(EnumClass.UiType.ProductWindows02, ex.Message, EnumClass.ProcessType.FinishMaking);
            }
        }

        /// <summary>
        /// 更换原材料
        /// </summary>
        public void ChangeMaterial(object o)
        {
            try
            {
                if(strawberriesNumbers<100)
                {
                    UpdateUIDelegate(EnumClass.UiType.ChangeMaterialWindows, $"【更换草莓中】",EnumClass.ProcessType.ChangeStrawberriesStart);
                    Sleep(TimeSpan.FromSeconds(5));
                    UpdateUIDelegate(EnumClass.UiType.ChangeMaterialWindows, $"【草莓更换完毕】",EnumClass.ProcessType.ChangeStrawberriesFinish);
                    strawberriesNumbers = 100;
                    UpdateUIDelegate(EnumClass.UiType.MaterialInventoryWindows, $"【可制作冰激凌：{MinNumberToProduce()}】【草莓库存：{strawberriesNumbers}】 【巧克力库存：{chocolateNumbers}】 【冰激凌库存：{iceCreamNumbers}】", EnumClass.ProcessType.UpdateMaterialInventory);
                    MaterialInventoryNotice();
                }
                if (chocolateNumbers < 100)
                {
                    UpdateUIDelegate(EnumClass.UiType.ChangeMaterialWindows, $"【更换巧克力中】", EnumClass.ProcessType.ChangeChocolateStart);
                    Sleep(TimeSpan.FromSeconds(5));
                    UpdateUIDelegate(EnumClass.UiType.ChangeMaterialWindows, $"【巧克力更换完毕】", EnumClass.ProcessType.ChangeChocolateFinish);
                    chocolateNumbers = 100;
                    UpdateUIDelegate(EnumClass.UiType.MaterialInventoryWindows, $"【可制作冰激凌：{MinNumberToProduce()}】【草莓库存：{strawberriesNumbers}】 【巧克力库存：{chocolateNumbers}】 【冰激凌库存：{iceCreamNumbers}】", EnumClass.ProcessType.UpdateMaterialInventory);
                    MaterialInventoryNotice();
                }
                if (iceCreamNumbers < 100)
                {
                    UpdateUIDelegate(EnumClass.UiType.ChangeMaterialWindows, $"【更换冰激凌中】", EnumClass.ProcessType.ChangeIceCreamStart);
                    Sleep(TimeSpan.FromSeconds(5));
                    UpdateUIDelegate(EnumClass.UiType.ChangeMaterialWindows, $"【冰激凌更换完毕】", EnumClass.ProcessType.ChangeIceCreamFinish);
                    iceCreamNumbers = 100;
                    UpdateUIDelegate(EnumClass.UiType.MaterialInventoryWindows, $"【可制作冰激凌：{MinNumberToProduce()}】【草莓库存：{strawberriesNumbers}】 【巧克力库存：{chocolateNumbers}】 【冰激凌库存：{iceCreamNumbers}】", EnumClass.ProcessType.UpdateMaterialInventory);
                    MaterialInventoryNotice();
                }

                UpdateUIDelegate(EnumClass.UiType.ChangeMaterialWindows, $"【所有物料更换完毕】", EnumClass.ProcessType.FinishChanging);
                CallTaskFinishDelegate(EnumClass.SignalType.ChangeMaterial);
                
            }
            catch(Exception ex)
            {
                UpdateUIDelegate(EnumClass.UiType.ChangeMaterialWindows, ex.Message, EnumClass.ProcessType.FinishChanging);
            }
        }
        /// <summary>
        /// 判断原材料是否足够生产
        /// </summary>
        /// <returns></returns>
        private bool IsEnoughOfMaterials(MaterialName materialName)
        {
            bool result = false;
            switch (materialName)
            {
                case MaterialName.Strawberries:
                    result = strawberriesNumbers > 0 ? true : false;
                    if (!result)
                    {
                        UpdateUIDelegate(EnumClass.UiType.ChangeMaterialWindows, "草莓不足，生产已停止，请更换原料！", EnumClass.ProcessType.CheckMaterialCount);
                    }
                    break;
                case MaterialName.Chocolate:
                    result = chocolateNumbers > 0 ? true : false;
                    if (!result)
                    {
                        UpdateUIDelegate(EnumClass.UiType.ChangeMaterialWindows, "巧克力不足，生产已停止，请更换原料！", EnumClass.ProcessType.CheckMaterialCount);
                    }
                    break;
                case MaterialName.IceCream:
                    result = iceCreamNumbers > 0 ? true : false;
                    if (!result)
                    {
                        UpdateUIDelegate(EnumClass.UiType.ChangeMaterialWindows, "冰激凌不足，生产已停止，请更换原料！", EnumClass.ProcessType.CheckMaterialCount);
                    }
                    break;
            }
            return result;
        }

        /// <summary>
        /// 扣减原材料库存
        /// </summary>
        /// <returns></returns>
        private void DeductionInventoryOfMaterials(MaterialName materialName, object o)
        {
            switch (materialName)
            {
                case MaterialName.Strawberries:
                    strawberriesNumbers--;
                    break;
                case MaterialName.Chocolate:
                    chocolateNumbers--;
                    break;
                case MaterialName.IceCream:
                    iceCreamNumbers--;
                    break;
            }
            UpdateUIDelegate(EnumClass.UiType.MaterialInventoryWindows, $"【可制作冰激凌：{MinNumberToProduce()}】【草莓库存：{strawberriesNumbers}】 【巧克力库存：{chocolateNumbers}】 【冰激凌库存：{iceCreamNumbers}】 ", EnumClass.ProcessType.CheckMaterialCount);
            MaterialInventoryNotice();
        }

        /// <summary>
        /// 增加产品的库存
        /// </summary>
        private void IncreaseInventoryOfProduct()
        {
            productNumbers++;
            
        }

        /// <summary>
        /// 原材料余量提示
        /// </summary>
        private void MaterialInventoryNotice()
        {
            if(MinNumberToProduce() <10)
            {
                UpdateUIDelegate(EnumClass.UiType.ChangeMaterialWindows, $"【剩余原料不足{MinNumberToProduce()}份，请及时更换】",EnumClass.ProcessType.CheckMaterialCount);
            }
        }
        /// <summary>
        /// 可生产的冰激凌量
        /// </summary>
        /// <returns></returns>
        private int MinNumberToProduce()
        {
            int Min = Math.Min(strawberriesNumbers, chocolateNumbers);
            Min = Math.Min(Min, iceCreamNumbers);
            return Min;
        }
        /// <summary>
        /// 原料种类
        /// </summary>
        private enum MaterialName
        {
            Strawberries,
            Chocolate,
            IceCream

        }
    }
}

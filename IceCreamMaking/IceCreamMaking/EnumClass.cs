using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceCreamMaking
{
    public static class EnumClass
    {
        public  enum UiType
        {
            /// <summary>
            /// 冰激凌生产窗口1
            /// </summary>
            ProductWindows01,
            /// <summary>
            /// 冰激凌生产窗口2
            /// </summary>
            ProductWindows02,
            /// <summary>
            /// 原料更换窗口
            /// </summary>
            ChangeMaterialWindows,
            /// <summary>
            /// 原料库存窗口
            /// </summary>
            MaterialInventoryWindows,
            /// <summary>
            /// 产品库存窗口
            /// </summary>
            ProductInventoryWindows

        }


        public enum SignalType
        {
            StartMaking01,
            StartMaking02,
            ChangeMaterial,
            MaterialNotEnough
        }

        public enum ProcessType
        {
            StartMaking,
            AddStrawberriesStart,
            AddStrawberriesFinish,
            AddChocolateStart,
            AddChocolateFinish,
            AddIceCreamStart,
            AddIceCreamFinish,
            FinishMaking,
            StartChanging,
            ChangeStrawberriesStart,
            ChangeStrawberriesFinish,
            ChangeChocolateStart,
            ChangeChocolateFinish,
            ChangeIceCreamStart,
            ChangeIceCreamFinish,
            FinishChanging,
            UpdateMaterialInventory,
            CheckMaterialCount

        }

    }
}

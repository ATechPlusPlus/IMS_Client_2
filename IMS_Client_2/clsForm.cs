namespace IMS_Client_2
{
    public class clsFormRights
    {
        public enum Forms
        {
            Brand_Master = 8,
            Category_Master = 9,
            Color_Master = 10,
            Customer_Master = 11,
            Country_Master = 12,
            Product_Master = 13,
            Employee_Details = 14,
            Size_Master = 15,
            Size_Type_Master = 16,
            Store_Master = 17,
            Supplier_Details = 18,
            Material_Details = 19,
            Currency_Value_Settings = 20,
            Employee_Commission_Setting = 21,
            frmOtherSetting = 22,
            Sales_Bill_Details = 23,
            Sales_Invoice = 24,
            frmSalesReport = 25,
            Delivering_Purchase_Bill = 26,
            frmDiffPurchaseReceived = 27,
            frmDiffPurchaseReceviedDetails = 28,
            Posting_Delivery = 29,
            Purchase_Bill_Details = 30,
            Purchase_Invoice = 31,
            frmBarCode = 32,
            frmBarCodeDesigner = 33,
            frmCloseCashBandMaster = 34,
            frmCloseShifWindow = 35,
            Import_ProductData = 36,
            frmBranchSaleShorting = 37,
            frmItemWiseModelNo = 38,
            frmStoreTransfer = 39,
            frmTransferCheck = 40,
            frmTransferWatch = 41,
            frmTransferWatch_Items = 42,
            frmStockDetails = 43,
            frmPettyCashExpReport = 44,
            tblMiniSalesReport = 45,
            frmPurchaseDetails=46,
            frmReceivedBranchTransfer=47,
            frmDashBoardSettings=48,
            frmDashBoard=49,
            frmQuickBarCodePrint=50,
            frmDiscountLogin=51,
            frmDatabaseMaintenance = 7///commented
        }
        public enum Operation
        {
            View = 1,
            Save = 2,
            Update = 3,
            Delete = 4,
            Other = 5
        }

        public static bool HasFormRight(Forms formName)
        {
            int fID = (int)formName;
            return CoreApp.clsUtility.HasFormRights(fID);
        }

        public static bool HasFormRight(Forms formName, Operation operation)
        {
            int fID = (int)formName;
            int Operation = (int)operation;

            return CoreApp.clsUtility.HasFormRights(fID, Operation);
        }
    }
}